using UnityEngine;

namespace MarineSlayer.Combat
{
    public enum DamageType { Ballistic, Energy, Explosive, Melee, Environmental }

    public struct DamageInfo
    {
        public float amount;
        public GameObject source;
        public DamageType type;
        public Vector3 hitPoint;
        public Vector3 direction;

        public DamageInfo(float amount, GameObject source, DamageType type, Vector3 hitPoint, Vector3 direction)
        {
            this.amount = amount;
            this.source = source;
            this.type = type;
            this.hitPoint = hitPoint;
            this.direction = direction;
        }
    }
}
