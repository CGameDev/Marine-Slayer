using System.Collections;
using MarineSlayer.Core;
using MarineSlayer.Player;
using UnityEngine;

namespace MarineSlayer.Combat
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Health))]
    public sealed class ConvergenceThrallController : MonoBehaviour
    {
        [SerializeField] private float detectionRange = 8f;
        [SerializeField] private float attackRange = 1.4f;
        [SerializeField] private float moveSpeed = 2.6f;
        [SerializeField] private float attackDamage = 8f;
        [SerializeField] private float attacksPerSecond = 0.8f;
        private Rigidbody body;
        private Health health;
        private Transform player;
        private Health playerHealth;
        private float nextAttackTime;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            health = GetComponent<Health>();
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
        }

        private void OnDestroy()
        {
            if (health != null) health.Died -= OnDied;
        }

        private void FixedUpdate()
        {
            if (GameRoot.Instance.State.CurrentState != GameState.Playing || health.IsDead || player == null || playerHealth == null || playerHealth.IsDead)
                return;

            Vector3 delta = player.position - body.position;
            delta.y = 0f;
            float distance = delta.magnitude;
            if (distance > detectionRange || distance < 0.01f) return;

            Vector3 direction = delta / distance;
            Quaternion facing = Quaternion.LookRotation(direction, Vector3.up);
            body.MoveRotation(Quaternion.RotateTowards(body.rotation, facing, 540f * Time.fixedDeltaTime));

            if (distance > attackRange)
            {
                Vector3 destination = body.position + direction * moveSpeed * Time.fixedDeltaTime;
                destination.x = Mathf.Clamp(destination.x, -8.25f, 8.25f);
                destination.z = Mathf.Clamp(destination.z, -5.25f, 5.25f);
                body.MovePosition(destination);
                return;
            }

            if (Time.time < nextAttackTime) return;
            nextAttackTime = Time.time + 1f / Mathf.Max(0.1f, attacksPerSecond);
            playerHealth.ApplyDamage(new DamageInfo(attackDamage, gameObject, DamageType.Melee, player.position, direction));
        }

        private void OnDied(Health value)
        {
            Collider[] colliders = GetComponents<Collider>();
            for (int index = 0; index < colliders.Length; index++) colliders[index].enabled = false;
            body.isKinematic = true;
            StartCoroutine(DeathRoutine());
        }

        private IEnumerator DeathRoutine()
        {
            Vector3 startingScale = transform.localScale;
            float elapsed = 0f;
            const float duration = 0.4f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsed / duration);
                transform.localScale = Vector3.Lerp(startingScale, startingScale * 0.15f, progress);
                transform.Rotate(Vector3.up, 540f * Time.deltaTime, Space.World);
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}
