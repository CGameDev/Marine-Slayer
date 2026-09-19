using MarineSlayer.Core;
using UnityEngine;

namespace MarineSlayer.Campaign
{
    [RequireComponent(typeof(Collider))]
    public sealed class CampaignExitTrigger : MonoBehaviour
    {
        [SerializeField] private string prerequisiteObjectiveId;
        [SerializeField] private CampaignLevelController level;
        private bool consumed;

        public void Configure(string prerequisiteId, CampaignLevelController campaignLevel)
        {
            prerequisiteObjectiveId = prerequisiteId;
            level = campaignLevel;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other != null) TryExit(other.transform.root.gameObject);
        }

        public bool TryExit(GameObject actor)
        {
            if (consumed || actor == null || !actor.CompareTag("Player") || level == null) return false;
            if (GameRoot.Instance.State.CurrentState != GameState.Playing) return false;
            if (!string.IsNullOrEmpty(prerequisiteObjectiveId) && !GameRoot.Instance.Objectives.IsComplete(prerequisiteObjectiveId)) return false;
            if (!level.CompleteLevel()) return false;
            consumed = true;
            return true;
        }
    }
}
