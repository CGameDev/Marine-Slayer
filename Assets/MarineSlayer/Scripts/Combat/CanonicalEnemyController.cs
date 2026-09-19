using System.Collections;
using MarineSlayer.Core;
using MarineSlayer.Player;
using UnityEngine;

namespace MarineSlayer.Combat
{
    public enum CanonicalEnemyArchetype
    {
        ApexHunter,
        ConvergenceBrute,
        MeshSiren,
        RiftboundAbomination
    }

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Health))]
    public sealed class CanonicalEnemyController : MonoBehaviour
    {
        [SerializeField] private CanonicalEnemyArchetype archetype;
        private Rigidbody body;
        private Health health;
        private Collider bodyCollider;
        private Transform player;
        private Health playerHealth;
        private float nextAttackTime;
        private float nextAbilityTime;
        private bool dying;

        public CanonicalEnemyArchetype Archetype { get { return archetype; } }

        public void Configure(CanonicalEnemyArchetype value)
        {
            archetype = value;
        }

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            health = GetComponent<Health>();
            bodyCollider = GetComponent<Collider>();
            body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            body.interpolation = RigidbodyInterpolation.Interpolate;
            health.Died += OnDied;
        }

        private void Start()
        {
            PlayerMotor motor = FindObjectOfType<PlayerMotor>();
            if (motor == null) return;
            player = motor.transform;
            playerHealth = motor.GetComponent<Health>();
            nextAbilityTime = Time.time + 1.5f;
        }

        private void OnDestroy()
        {
            if (health != null) health.Died -= OnDied;
        }

        private void FixedUpdate()
        {
            if (dying || health.IsDead || player == null || playerHealth == null || playerHealth.IsDead) return;
            if (GameRoot.Instance.State.CurrentState != GameState.Playing) return;

            Vector3 delta = player.position - body.position;
            delta.y = 0f;
            float distance = delta.magnitude;
            if (distance < 0.01f) return;
            Vector3 direction = delta / distance;

            switch (archetype)
            {
                case CanonicalEnemyArchetype.ApexHunter:
                    RunApexHunter(direction, distance);
                    break;
                case CanonicalEnemyArchetype.ConvergenceBrute:
                    RunBrute(direction, distance);
                    break;
                case CanonicalEnemyArchetype.MeshSiren:
                    RunSiren(direction, distance);
                    break;
                case CanonicalEnemyArchetype.RiftboundAbomination:
                    RunRiftbound(direction, distance);
                    break;
            }
        }

        private void RunApexHunter(Vector3 direction, float distance)
        {
            if (distance > 10f) return;
            Face(direction, 760f);
            if (distance > 1.5f)
            {
                Vector3 tangent = new Vector3(-direction.z, 0f, direction.x);
                Vector3 pursuit = (direction + tangent * 0.28f).normalized;
                MoveBounded(pursuit, 4.4f);
            }
            else Attack(15f, DamageType.Melee, direction, 0.75f);
        }

        private void RunBrute(Vector3 direction, float distance)
        {
            if (distance > 8f) return;
            Face(direction, 260f);
            if (distance > 1.8f) MoveBounded(direction, 1.55f);
            else Attack(24f, DamageType.Melee, direction, 1.7f);
        }

        private void RunSiren(Vector3 direction, float distance)
        {
            if (distance > 11f) return;
            Face(direction, 360f);
            if (distance < 4f) MoveBounded(-direction, 2.2f);
            else if (distance > 7f) MoveBounded(direction, 1.8f);
            if (distance <= 7.5f) Attack(7f, DamageType.Energy, direction, 1.35f);
            float pulse = 1f + Mathf.Sin(Time.time * 8f) * 0.035f;
            transform.localScale = new Vector3(0.72f * pulse, 1.25f, 0.72f * pulse);
        }

        private void RunRiftbound(Vector3 direction, float distance)
        {
            if (distance > 10f) return;
            Face(direction, 480f);
            if (Time.time >= nextAbilityTime && distance > 2.2f)
            {
                nextAbilityTime = Time.time + 2.6f;
                Vector3 side = Vector3.Cross(Vector3.up, direction) * (Mathf.Sin(Time.time * 3.1f) >= 0f ? 1f : -1f);
                Vector3 destination = player.position - direction * 1.8f + side * 1.1f;
                destination.x = Mathf.Clamp(destination.x, -8f, 8f);
                destination.y = 1f;
                destination.z = Mathf.Clamp(destination.z, -5f, 5f);
                body.position = destination;
                return;
            }
            if (distance > 1.6f) MoveBounded(direction, 2.1f);
            else Attack(18f, DamageType.Energy, direction, 1.15f);
        }

        private void Face(Vector3 direction, float degreesPerSecond)
        {
            Quaternion facing = Quaternion.LookRotation(direction, Vector3.up);
            body.MoveRotation(Quaternion.RotateTowards(body.rotation, facing, degreesPerSecond * Time.fixedDeltaTime));
        }

        private void MoveBounded(Vector3 direction, float speed)
        {
            Vector3 destination = body.position + direction * speed * Time.fixedDeltaTime;
            destination.x = Mathf.Clamp(destination.x, -8.25f, 8.25f);
            destination.z = Mathf.Clamp(destination.z, -5.25f, 5.25f);
            body.MovePosition(destination);
        }

        private void Attack(float damage, DamageType type, Vector3 direction, float cooldown)
        {
            if (Time.time < nextAttackTime) return;
            nextAttackTime = Time.time + cooldown;
            float scaledDamage = damage * GameRoot.Instance.Difficulty.EnemyDamageMultiplier;
            playerHealth.ApplyDamage(new DamageInfo(scaledDamage, gameObject, type, player.position, direction));
        }

        private void OnDied(Health value)
        {
            dying = true;
            if (bodyCollider != null) bodyCollider.enabled = false;
            body.isKinematic = true;
            StartCoroutine(DeathRoutine());
        }

        private IEnumerator DeathRoutine()
        {
            Vector3 start = transform.localScale;
            float elapsed = 0f;
            const float duration = 0.45f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsed / duration);
                transform.localScale = Vector3.Lerp(start, Vector3.zero, progress);
                transform.Rotate(Vector3.up, 420f * Time.deltaTime, Space.World);
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}
