using System.Collections.Generic;
using UnityEngine;

namespace MarineSlayer.Combat
{
    public sealed class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private int capacity = 24;
        private readonly Queue<PooledProjectile> available = new Queue<PooledProjectile>();

        private void Awake()
        {
            capacity = Mathf.Clamp(capacity, 4, 64);
            for (int index = 0; index < capacity; index++) available.Enqueue(CreateProjectile(index));
        }

        public PooledProjectile Acquire()
        {
            if (available.Count == 0) return null;
            return available.Dequeue();
        }

        public void Release(PooledProjectile projectile)
        {
            projectile.gameObject.SetActive(false);
            projectile.transform.SetParent(transform, false);
            available.Enqueue(projectile);
        }

        private PooledProjectile CreateProjectile(int index)
        {
            GameObject instance = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            instance.name = "PooledProjectile_" + index;
            instance.transform.SetParent(transform, false);
            instance.transform.localScale = Vector3.one * 0.12f;
            SphereCollider collider = instance.GetComponent<SphereCollider>();
            collider.isTrigger = true;
            Rigidbody body = instance.AddComponent<Rigidbody>();
            body.isKinematic = true;
            body.useGravity = false;
            PooledProjectile projectile = instance.AddComponent<PooledProjectile>();
            projectile.SetPool(this);
            instance.SetActive(false);
            return projectile;
        }
    }
}
