using MarineSlayer.Core;
using UnityEngine;

namespace MarineSlayer.Combat
{
    [RequireComponent(typeof(Health))]
    public sealed class EnemyDifficultyScaler : MonoBehaviour
    {
        private bool applied;

        private void Start()
        {
            if (applied) return;
            applied = true;
            Health health = GetComponent<Health>();
            health.Configure(health.Maximum * GameRoot.Instance.Difficulty.EnemyHealthMultiplier);
        }
    }
}
