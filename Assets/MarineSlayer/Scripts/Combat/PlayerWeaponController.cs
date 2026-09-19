using System;
using MarineSlayer.Core;
using UnityEngine;

namespace MarineSlayer.Combat
{
    public sealed class PlayerWeaponController : MonoBehaviour
    {
        private ProjectilePool pool;
        private WeaponRuntimeState[] weapons;
        private int currentIndex;
        private float nextShotTime;
        private float reloadCompleteTime;
        private bool reloading;

        public event Action<WeaponRuntimeState> WeaponChanged;
        public event Action<WeaponRuntimeState> AmmoChanged;

        public WeaponRuntimeState CurrentWeapon
        {
            get { return weapons != null && weapons.Length > 0 ? weapons[currentIndex] : null; }
        }

        public int WeaponCount { get { return weapons == null ? 0 : weapons.Length; } }
        public bool IsReloading { get { return reloading; } }

        private void Awake()
        {
            pool = GetComponentInChildren<ProjectilePool>();
            WeaponDefinition[] definitions = CanonicalWeaponCatalog.CreateAll();
            weapons = new WeaponRuntimeState[definitions.Length];
            for (int index = 0; index < definitions.Length; index++)
                weapons[index] = new WeaponRuntimeState(definitions[index]);
        }

        private void Update()
        {
            CompleteReloadWhenReady();
            if (GameRoot.Instance.State.CurrentState != GameState.Playing) return;

            if (GameRoot.Instance.Input.WeaponNextPressed) SwitchNext();
            if (GameRoot.Instance.Input.ReloadPressed) BeginReload();

            WeaponRuntimeState current = CurrentWeapon;
            if (current == null) return;
            bool wantsFire = current.Definition.automatic
                ? GameRoot.Instance.Input.FireHeld
                : GameRoot.Instance.Input.FirePressed;
            if (wantsFire) Fire();
        }

        public bool Fire()
        {
            return FireDirection(transform.forward);
        }

        public bool FireDirection(Vector3 direction)
        {
            CompleteReloadWhenReady();
            WeaponRuntimeState current = CurrentWeapon;
            if (current == null || reloading || Time.time < nextShotTime || direction.sqrMagnitude < 0.001f)
                return false;

            WeaponDefinition definition = current.Definition;
            if (definition.UsesAmmunition && current.Magazine <= 0)
            {
                BeginReload();
                return false;
            }

            if (definition.delivery == WeaponDeliveryMode.Projectile && pool == null) return false;

            bool fired;
            Vector3 normalizedDirection = direction.normalized;
            Vector3 origin = transform.position + Vector3.up + normalizedDirection * 0.9f;
            if (definition.delivery == WeaponDeliveryMode.Projectile)
                fired = FireProjectile(definition, origin, normalizedDirection);
            else if (definition.delivery == WeaponDeliveryMode.Melee)
                fired = FireMelee(definition, origin, normalizedDirection);
            else
                fired = FireHitscan(definition, origin, normalizedDirection);

            if (!fired) return false;
            nextShotTime = Time.time + 1f / Mathf.Max(0.1f, definition.roundsPerSecond);
            if (definition.UsesAmmunition)
            {
                current.Magazine--;
                RaiseAmmoChanged();
                if (current.Magazine == 0) BeginReload();
            }
            return true;
        }

        public bool BeginReload()
        {
            WeaponRuntimeState current = CurrentWeapon;
            if (current == null || reloading || !current.Definition.UsesAmmunition) return false;
            if (current.Magazine >= current.Definition.magazineSize || current.Reserve <= 0) return false;
            reloading = true;
            reloadCompleteTime = Time.time + Mathf.Max(0.05f, current.Definition.reloadSeconds);
            return true;
        }

        public bool SwitchNext()
        {
            if (weapons == null || weapons.Length < 2) return false;
            reloading = false;
            currentIndex = (currentIndex + 1) % weapons.Length;
            nextShotTime = Time.time;
            Action<WeaponRuntimeState> handler = WeaponChanged;
            if (handler != null) handler(CurrentWeapon);
            return true;
        }

        private bool FireHitscan(WeaponDefinition definition, Vector3 origin, Vector3 direction)
        {
            int pelletCount = Mathf.Max(1, definition.pellets);
            bool fired = false;
            for (int index = 0; index < pelletCount; index++)
            {
                float offset = pelletCount == 1
                    ? 0f
                    : ((float)index / (pelletCount - 1) - 0.5f) * definition.spreadDegrees;
                Vector3 shotDirection = Quaternion.AngleAxis(offset, Vector3.up) * direction;
                RaycastHit hit;
                if (Physics.Raycast(origin, shotDirection, out hit, definition.maximumRange, ~0, QueryTriggerInteraction.Collide))
                {
                    IDamageable target = hit.collider.GetComponent(typeof(IDamageable)) as IDamageable;
                    if (target != null)
                        target.ApplyDamage(new DamageInfo(definition.damage, gameObject, definition.damageType, hit.point, shotDirection));
                }
                fired = true;
            }
            return fired;
        }

        private bool FireProjectile(WeaponDefinition definition, Vector3 origin, Vector3 direction)
        {
            PooledProjectile projectile = pool.Acquire();
            if (projectile == null) return false;
            projectile.Launch(gameObject, origin, direction, definition.projectileSpeed, definition.damage, definition.damageType);
            return true;
        }

        private bool FireMelee(WeaponDefinition definition, Vector3 origin, Vector3 direction)
        {
            RaycastHit hit;
            if (Physics.SphereCast(origin, 0.75f, direction, out hit, definition.maximumRange, ~0, QueryTriggerInteraction.Collide))
            {
                IDamageable target = hit.collider.GetComponent(typeof(IDamageable)) as IDamageable;
                if (target != null)
                    target.ApplyDamage(new DamageInfo(definition.damage, gameObject, definition.damageType, hit.point, direction));
            }
            return true;
        }

        private void CompleteReloadWhenReady()
        {
            if (!reloading || Time.time < reloadCompleteTime) return;
            WeaponRuntimeState current = CurrentWeapon;
            reloading = false;
            if (current == null) return;
            int needed = current.Definition.magazineSize - current.Magazine;
            int transferred = Mathf.Min(needed, current.Reserve);
            current.Magazine += transferred;
            current.Reserve -= transferred;
            RaiseAmmoChanged();
        }

        private void RaiseAmmoChanged()
        {
            Action<WeaponRuntimeState> handler = AmmoChanged;
            if (handler != null) handler(CurrentWeapon);
        }
    }
}
