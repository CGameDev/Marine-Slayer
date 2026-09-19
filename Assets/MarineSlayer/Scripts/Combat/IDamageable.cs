namespace MarineSlayer.Combat
{
    public interface IDamageable
    {
        bool IsDead { get; }
        void ApplyDamage(DamageInfo damage);
    }
}
