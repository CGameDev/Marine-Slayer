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
        private readonly RaycastHit[] raycastHits = new RaycastHit[16];
        private readonly Collider[] chainHits = new Collider[24];

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
            CoolWeapons();
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
            if (current == null || reloading || current.Overheated || Time.time < nextShotTime || direction.sqrMagnitude < 0.001f)
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
            if (definition.UsesHeat)
            {
                current.Heat = Mathf.Min(100f, current.Heat + definition.heatPerShot);
                if (current.Heat >= 100f) current.Overheated = true;
                RaiseAmmoChanged();
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

        public bool SelectWeapon(CanonicalWeaponId id)
        {
            if (weapons == null) return false;
            for (int index = 0; index < weapons.Length; index++)
            {
                if (weapons[index].Definition.id != id) continue;
                reloading = false;
                currentIndex = index;
                nextShotTime = Time.time;
                Action<WeaponRuntimeState> handler = WeaponChanged;
                if (handler != null) handler(CurrentWeapon);
                return true;
            }
            return false;
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
                FireHitscanRay(definition, origin, shotDirection);
                fired = true;
            }
            return fired;
        }

        private bool FireProjectile(WeaponDefinition definition, Vector3 origin, Vector3 direction)
        {
            PooledProjectile projectile = pool.Acquire();
            if (projectile == null) return false;
            projectile.Launch(gameObject, origin, direction, definition.projectileSpeed, definition.damage, definition.damageType, definition.impactRadius, definition.ricochetCount);
            return true;
        }

        private void FireHitscanRay(WeaponDefinition definition, Vector3 origin, Vector3 direction)
        {
            int maximumTargets = Mathf.Max(1, definition.penetrationTargets);
            int count = Physics.RaycastNonAlloc(origin, direction, raycastHits, definition.maximumRange, ~0, QueryTriggerInteraction.Collide);
            SortHitsByDistance(count);
            int damagedTargets = 0;
            for (int index = 0; index < count && damagedTargets < maximumTargets; index++)
            {
                RaycastHit hit = raycastHits[index];
                if (hit.collider == null || hit.collider.transform.root.gameObject == transform.root.gameObject) continue;
                IDamageable target = hit.collider.GetComponent(typeof(IDamageable)) as IDamageable;
                if (target == null) break;
                target.ApplyDamage(new DamageInfo(definition.damage, gameObject, definition.damageType, hit.point, direction));
                damagedTargets++;
                if (definition.chainTargets > 0) ApplyChainDamage(definition, target, hit.point, direction);
            }
        }

        private void ApplyChainDamage(WeaponDefinition definition, IDamageable primary, Vector3 point, Vector3 direction)
        {
            int count = Physics.OverlapSphereNonAlloc(point, definition.chainRadius, chainHits, ~0, QueryTriggerInteraction.Collide);
            int chained = 0;
            for (int index = 0; index < count && chained < definition.chainTargets; index++)
            {
                Collider collider = chainHits[index];
                if (collider == null || collider.transform.root.gameObject == transform.root.gameObject) continue;
                IDamageable candidate = collider.GetComponent(typeof(IDamageable)) as IDamageable;
                if (candidate == null || candidate == primary || candidate.IsDead) continue;
                candidate.ApplyDamage(new DamageInfo(definition.damage * definition.secondaryDamageMultiplier, gameObject, definition.damageType, collider.transform.position, direction));
                chained++;
            }
        }

        private void SortHitsByDistance(int count)
        {
            for (int left = 0; left < count - 1; left++)
            {
                int nearest = left;
                for (int right = left + 1; right < count; right++)
                    if (raycastHits[right].distance < raycastHits[nearest].distance) nearest = right;
                if (nearest == left) continue;
                RaycastHit swap = raycastHits[left];
                raycastHits[left] = raycastHits[nearest];
                raycastHits[nearest] = swap;
            }
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

        private void CoolWeapons()
        {
            if (weapons == null || Time.deltaTime <= 0f) return;
            for (int index = 0; index < weapons.Length; index++)
            {
                WeaponRuntimeState state = weapons[index];
                if (!state.Definition.UsesHeat || state.Heat <= 0f) continue;
                state.Heat = Mathf.Max(0f, state.Heat - state.Definition.heatDissipationPerSecond * Time.deltaTime);
                if (state.Overheated && state.Heat <= 35f) state.Overheated = false;
            }
        }

        private void RaiseAmmoChanged()
        {
            Action<WeaponRuntimeState> handler = AmmoChanged;
            if (handler != null) handler(CurrentWeapon);
        }
    }
}
