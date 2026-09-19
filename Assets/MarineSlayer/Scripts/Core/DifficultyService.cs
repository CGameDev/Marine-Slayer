using MarineSlayer.Save;
using UnityEngine;

namespace MarineSlayer.Core
{
    public sealed class DifficultyService : MonoBehaviour
    {
        public CampaignDifficulty Current { get { return GameRoot.Instance.Saves.Current.difficulty; } }

        public float EnemyDamageMultiplier { get { return EnemyDamageFor(Current); } }
        public float EnemyHealthMultiplier { get { return EnemyHealthFor(Current); } }
        public float PlayerResourceMultiplier { get { return PlayerResourcesFor(Current); } }

        public static float EnemyDamageFor(CampaignDifficulty difficulty)
        {
            if (difficulty == CampaignDifficulty.Recruit) return 0.75f;
            if (difficulty == CampaignDifficulty.Slayer) return 1.35f;
            return 1f;
        }

        public static float EnemyHealthFor(CampaignDifficulty difficulty)
        {
            if (difficulty == CampaignDifficulty.Recruit) return 0.85f;
            if (difficulty == CampaignDifficulty.Slayer) return 1.2f;
            return 1f;
        }

        public static float PlayerResourcesFor(CampaignDifficulty difficulty)
        {
            if (difficulty == CampaignDifficulty.Recruit) return 1.25f;
            if (difficulty == CampaignDifficulty.Slayer) return 0.8f;
            return 1f;
        }
    }
}
