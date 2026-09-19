using System;
using UnityEngine;

namespace MarineSlayer.Combat
{
    public sealed class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maximum = 100f;
        [SerializeField] private float current = 100f;
        public event Action<Health, DamageInfo> Damaged;
        public event Action<Health> Died;

        public float Current { get { return current; } }
        public float Maximum { get { return maximum; } }
        public bool IsDead { get { return current <= 0f; } }

        public void Configure(float value)
        {
            maximum = Mathf.Max(1f, value);
            current = maximum;
        }

        public void ApplyDamage(DamageInfo damage)
        {
            if (IsDead || damage.amount <= 0f) return;
            current = Mathf.Max(0f, current - damage.amount);
            Action<Health, DamageInfo> damaged = Damaged;
            if (damaged != null) damaged(this, damage);
            if (IsDead)
            {
                Action<Health> died = Died;
                if (died != null) died(this);
            }
        }
    }
}
