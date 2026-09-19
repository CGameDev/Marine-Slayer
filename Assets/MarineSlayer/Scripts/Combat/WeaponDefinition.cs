using System;

namespace MarineSlayer.Combat
{
    public enum CanonicalWeaponId
    {
        GavelShotgun,
        LancerRifle,
        PlasmaCutter,
        ArcThrower,
        FuryGauntlet,
        RiftGrenade,
        TriShotRailPistol,
        SawbladeLauncher,
        UnityBeamRifle
    }

    public enum WeaponDeliveryMode
    {
        Hitscan,
        Projectile,
        Melee,
        SustainedBeam
    }

    [Serializable]
    public sealed class WeaponDefinition
    {
        public CanonicalWeaponId id;
        public string displayName;
        public WeaponDeliveryMode delivery;
        public DamageType damageType;
        public float damage;
        public float roundsPerSecond;
        public int magazineSize;
        public int startingReserve;
        public float reloadSeconds;
        public float maximumRange;
        public float projectileSpeed;
        public bool automatic;
        public int pellets;
        public float spreadDegrees;
        public int penetrationTargets;
        public int chainTargets;
        public float chainRadius;
        public float secondaryDamageMultiplier = 0.65f;
        public float impactRadius;
        public int ricochetCount;
        public float heatPerShot;
        public float heatDissipationPerSecond;

        public bool UsesAmmunition { get { return magazineSize > 0; } }
        public bool UsesHeat { get { return heatPerShot > 0f; } }

        public WeaponDefinition(
            CanonicalWeaponId id,
            string displayName,
            WeaponDeliveryMode delivery,
            DamageType damageType,
            float damage,
            float roundsPerSecond,
            int magazineSize,
            int startingReserve,
            float reloadSeconds,
            float maximumRange,
            float projectileSpeed,
            bool automatic,
            int pellets,
            float spreadDegrees)
        {
            this.id = id;
            this.displayName = displayName;
            this.delivery = delivery;
            this.damageType = damageType;
            this.damage = damage;
            this.roundsPerSecond = roundsPerSecond;
            this.magazineSize = magazineSize;
            this.startingReserve = startingReserve;
            this.reloadSeconds = reloadSeconds;
            this.maximumRange = maximumRange;
            this.projectileSpeed = projectileSpeed;
            this.automatic = automatic;
            this.pellets = pellets;
            this.spreadDegrees = spreadDegrees;
        }
    }

    public sealed class WeaponRuntimeState
    {
        public WeaponDefinition Definition { get; private set; }
        public int Magazine { get; set; }
        public int Reserve { get; set; }
        public float Heat { get; set; }
        public bool Overheated { get; set; }

        public WeaponRuntimeState(WeaponDefinition definition)
        {
            Definition = definition;
            Magazine = definition.magazineSize;
            Reserve = definition.startingReserve;
        }
    }

    public static class CanonicalWeaponCatalog
    {
        public static WeaponDefinition[] CreateAll()
        {
            return new[]
            {
                new WeaponDefinition(CanonicalWeaponId.GavelShotgun, "M-77 Gavel Combat Shotgun", WeaponDeliveryMode.Hitscan, DamageType.Ballistic, 12f, 1.2f, 8, 40, 2.2f, 20f, 0f, false, 6, 11f),
                new WeaponDefinition(CanonicalWeaponId.LancerRifle, "VX-90 Lancer Assault Rifle", WeaponDeliveryMode.Hitscan, DamageType.Ballistic, 14f, 8f, 36, 180, 1.8f, 55f, 0f, true, 1, 0f),
                new WeaponDefinition(CanonicalWeaponId.PlasmaCutter, "EID-3 Plasma Cutter", WeaponDeliveryMode.Hitscan, DamageType.Energy, 34f, 2.5f, 12, 60, 2f, 36f, 0f, false, 1, 0f) { penetrationTargets = 2 },
                new WeaponDefinition(CanonicalWeaponId.ArcThrower, "TX-40 Arc Thrower", WeaponDeliveryMode.Hitscan, DamageType.Energy, 22f, 3f, 20, 80, 2.4f, 22f, 0f, true, 1, 0f) { chainTargets = 2, chainRadius = 5f },
                new WeaponDefinition(CanonicalWeaponId.FuryGauntlet, "Fury Gauntlet MKII", WeaponDeliveryMode.Melee, DamageType.Melee, 48f, 1.4f, 0, 0, 0f, 2.4f, 0f, false, 1, 0f),
                new WeaponDefinition(CanonicalWeaponId.RiftGrenade, "Horizon RIFT Grenade", WeaponDeliveryMode.Projectile, DamageType.Explosive, 85f, 0.5f, 1, 5, 1.2f, 25f, 12f, false, 1, 0f) { impactRadius = 4f },
                new WeaponDefinition(CanonicalWeaponId.TriShotRailPistol, "UEMF Tri-Shot Rail Pistol", WeaponDeliveryMode.Hitscan, DamageType.Ballistic, 18f, 2f, 18, 90, 1.6f, 48f, 0f, false, 3, 2.5f) { penetrationTargets = 3 },
                new WeaponDefinition(CanonicalWeaponId.SawbladeLauncher, "Helion Industrial Sawblade Launcher", WeaponDeliveryMode.Projectile, DamageType.Ballistic, 65f, 1f, 4, 20, 2.5f, 35f, 18f, false, 1, 0f) { ricochetCount = 3 },
                new WeaponDefinition(CanonicalWeaponId.UnityBeamRifle, "ASCENDANT Unity Beam Rifle", WeaponDeliveryMode.SustainedBeam, DamageType.Energy, 10f, 12f, 0, 0, 0f, 60f, 0f, true, 1, 0f) { heatPerShot = 9f, heatDissipationPerSecond = 28f }
            };
        }
    }
}
