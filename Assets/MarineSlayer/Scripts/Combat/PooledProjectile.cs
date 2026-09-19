using UnityEngine;

namespace MarineSlayer.Combat
{
    public sealed class PooledProjectile : MonoBehaviour
    {
        private ProjectilePool pool;
        private GameObject owner;
        private Rigidbody body;
        private Vector3 velocity;
        private float damage;
        private DamageType damageType;
        private float impactRadius;
        private int remainingRicochets;
        private float expiresAt;
        private readonly Collider[] overlapHits = new Collider[24];
        private readonly IDamageable[] affectedTargets = new IDamageable[24];

        public void SetPool(ProjectilePool value) { pool = value; }

        private void Awake() { body = GetComponent<Rigidbody>(); }

        public void Launch(GameObject source, Vector3 position, Vector3 direction, float speed, float damageAmount, DamageType type, float radius, int ricochets)
        {
            owner = source;
            damage = damageAmount;
            damageType = type;
            impactRadius = radius;
            remainingRicochets = ricochets;
            velocity = direction.normalized * speed;
            transform.position = position;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            expiresAt = Time.time + 2f;
            gameObject.SetActive(true);
        }

        private void FixedUpdate()
        {
            float distance = velocity.magnitude * Time.fixedDeltaTime;
            RaycastHit hit;
            if (Physics.Raycast(body.position, velocity.normalized, out hit, distance, ~0, QueryTriggerInteraction.Collide))
            {
                if (owner == null || hit.collider.transform.root.gameObject != owner.transform.root.gameObject)
                {
                    Impact(hit.collider, hit.point, hit.normal);
                    return;
                }
            }
            body.MovePosition(body.position + velocity.normalized * distance);
            if (Time.time >= expiresAt) pool.Release(this);
        }

        private void Impact(Collider other, Vector3 point, Vector3 normal)
        {
            if (impactRadius > 0f) ApplyAreaDamage(point);
            else
            {
                IDamageable target = other.GetComponent(typeof(IDamageable)) as IDamageable;
                if (target != null)
                    target.ApplyDamage(new DamageInfo(damage, owner, damageType, point, velocity.normalized));
            }

            if (remainingRicochets > 0)
            {
                remainingRicochets--;
                velocity = Vector3.Reflect(velocity, normal).normalized * velocity.magnitude;
                body.position = point + normal * 0.08f;
                transform.rotation = Quaternion.LookRotation(velocity, Vector3.up);
                return;
            }
            pool.Release(this);
        }

        private void ApplyAreaDamage(Vector3 point)
        {
            int count = Physics.OverlapSphereNonAlloc(point, impactRadius, overlapHits, ~0, QueryTriggerInteraction.Collide);
            int affectedCount = 0;
            for (int index = 0; index < count; index++)
            {
                Collider collider = overlapHits[index];
                if (collider == null || (owner != null && collider.transform.root.gameObject == owner.transform.root.gameObject)) continue;
                IDamageable target = collider.GetComponent(typeof(IDamageable)) as IDamageable;
                if (target == null || target.IsDead || WasAffected(target, affectedCount)) continue;
                affectedTargets[affectedCount++] = target;
                float distance = Vector3.Distance(point, collider.ClosestPointOnBounds(point));
                float falloff = 1f - Mathf.Clamp01(distance / Mathf.Max(0.1f, impactRadius));
                target.ApplyDamage(new DamageInfo(damage * Mathf.Max(0.35f, falloff), owner, damageType, point, velocity.normalized));
            }
            for (int index = 0; index < affectedCount; index++) affectedTargets[index] = null;
        }

        private bool WasAffected(IDamageable target, int count)
        {
            for (int index = 0; index < count; index++)
                if (affectedTargets[index] == target) return true;
            return false;
        }
    }
}
