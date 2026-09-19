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
        private float expiresAt;

        public void SetPool(ProjectilePool value) { pool = value; }

        private void Awake() { body = GetComponent<Rigidbody>(); }

        public void Launch(GameObject source, Vector3 position, Vector3 direction, float speed, float damageAmount)
        {
            owner = source;
            damage = damageAmount;
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
                    Hit(hit.collider, hit.point);
                    return;
                }
            }
            body.MovePosition(body.position + velocity.normalized * distance);
            if (Time.time >= expiresAt) pool.Release(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (owner != null && other.transform.root.gameObject == owner.transform.root.gameObject) return;
            Hit(other, transform.position);
        }

        private void Hit(Collider other, Vector3 point)
        {
            IDamageable target = other.GetComponent(typeof(IDamageable)) as IDamageable;
            if (target != null)
                target.ApplyDamage(new DamageInfo(damage, owner, DamageType.Ballistic, point, velocity.normalized));
            pool.Release(this);
        }
    }
}
