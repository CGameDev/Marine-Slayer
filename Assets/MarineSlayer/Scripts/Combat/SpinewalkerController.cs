using System.Collections;
using MarineSlayer.Core;
using MarineSlayer.Player;
using UnityEngine;

namespace MarineSlayer.Combat
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Health))]
    public sealed class SpinewalkerController : MonoBehaviour
    {
        private enum SpinewalkerState { Dormant, Dropping, Hunting, Dying }

        [SerializeField] private float ambushRange = 7f;
        [SerializeField] private float moveSpeed = 3.2f;
        [SerializeField] private float attackRange = 1.25f;
        [SerializeField] private float attackDamage = 12f;
        private SpinewalkerState state;
        private Rigidbody body;
        private Health health;
        private Collider bodyCollider;
        private Transform player;
        private Health playerHealth;
        private float nextAttackTime;

        public bool HasDeployed { get; private set; }

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            health = GetComponent<Health>();
            bodyCollider = GetComponent<Collider>();
            body.isKinematic = true;
            body.useGravity = false;
            body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            if (bodyCollider != null) bodyCollider.enabled = false;
            health.Died += OnDied;
        }

        private void Start()
        {
            PlayerMotor motor = FindObjectOfType<PlayerMotor>();
            if (motor == null) return;
            player = motor.transform;
            playerHealth = motor.GetComponent<Health>();
        }

        private void OnDestroy()
        {
            if (health != null) health.Died -= OnDied;
        }

        private void FixedUpdate()
        {
            if (GameRoot.Instance.State.CurrentState != GameState.Playing || health.IsDead || player == null) return;
            Vector3 delta = player.position - transform.position;
            delta.y = 0f;

            if (state == SpinewalkerState.Dormant)
            {
                if (delta.sqrMagnitude <= ambushRange * ambushRange) StartCoroutine(DeployRoutine());
                return;
            }

            if (state != SpinewalkerState.Hunting || playerHealth == null || playerHealth.IsDead) return;
            float distance = delta.magnitude;
            if (distance < 0.01f) return;
            Vector3 direction = delta / distance;
            body.MoveRotation(Quaternion.RotateTowards(body.rotation, Quaternion.LookRotation(direction, Vector3.up), 640f * Time.fixedDeltaTime));
            if (distance > attackRange)
            {
                Vector3 destination = body.position + direction * moveSpeed * Time.fixedDeltaTime;
                destination.x = Mathf.Clamp(destination.x, -8.25f, 8.25f);
                destination.z = Mathf.Clamp(destination.z, -5.25f, 5.25f);
                body.MovePosition(destination);
                return;
            }

            if (Time.time < nextAttackTime) return;
            nextAttackTime = Time.time + 1.1f;
            playerHealth.ApplyDamage(new DamageInfo(attackDamage, gameObject, DamageType.Melee, player.position, direction));
        }

        private IEnumerator DeployRoutine()
        {
            state = SpinewalkerState.Dropping;
            Vector3 start = transform.position;
            Vector3 destination = new Vector3(start.x, 1f, start.z);
            float elapsed = 0f;
            const float duration = 0.45f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsed / duration);
                transform.position = Vector3.Lerp(start, destination, progress * progress);
                yield return null;
            }

            transform.position = destination;
            if (bodyCollider != null) bodyCollider.enabled = true;
            body.isKinematic = false;
            body.useGravity = true;
            HasDeployed = true;
            state = SpinewalkerState.Hunting;
        }

        private void OnDied(Health value)
        {
            StopAllCoroutines();
            state = SpinewalkerState.Dying;
            if (bodyCollider != null) bodyCollider.enabled = false;
            body.isKinematic = true;
            StartCoroutine(DeathRoutine());
        }

        private IEnumerator DeathRoutine()
        {
            Vector3 start = transform.localScale;
            float elapsed = 0f;
            const float duration = 0.35f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                transform.localScale = Vector3.Lerp(start, Vector3.zero, Mathf.Clamp01(elapsed / duration));
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}
