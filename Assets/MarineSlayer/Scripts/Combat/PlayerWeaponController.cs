using MarineSlayer.Core;
using UnityEngine;

namespace MarineSlayer.Combat
{
    public sealed class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] private float roundsPerSecond = 6f;
        [SerializeField] private float projectileSpeed = 20f;
        [SerializeField] private float damage = 15f;
        [SerializeField] private bool hitscan = true;
        [SerializeField] private float maximumRange = 50f;
        private ProjectilePool pool;
        private float nextShotTime;

        private void Awake() { pool = GetComponentInChildren<ProjectilePool>(); }

        private void Update()
        {
            if (GameRoot.Instance.State.CurrentState == GameState.Playing && GameRoot.Instance.Input.FireHeld)
                Fire();
        }

        public bool Fire()
        {
            return FireDirection(transform.forward);
        }

        public bool FireDirection(Vector3 direction)
        {
            if (Time.time < nextShotTime || pool == null) return false;
            PooledProjectile projectile = pool.Acquire();
            if (projectile == null) return false;
            nextShotTime = Time.time + 1f / Mathf.Max(0.1f, roundsPerSecond);
            Vector3 origin = transform.position + Vector3.up + transform.forward * 0.9f;
            RaycastHit hit;
            if (hitscan && Physics.Raycast(origin, direction.normalized, out hit, maximumRange, ~0, QueryTriggerInteraction.Collide))
            {
                IDamageable target = hit.collider.GetComponent(typeof(IDamageable)) as IDamageable;
                if (target != null)
                    target.ApplyDamage(new DamageInfo(damage, gameObject, DamageType.Ballistic, hit.point, direction.normalized));
                pool.Release(projectile);
                return true;
            }
            projectile.Launch(gameObject, origin, direction, projectileSpeed, damage);
            return true;
        }
    }
}
