using MarineSlayer.Core;
using MarineSlayer.Encounters;
using UnityEngine;

namespace MarineSlayer.Campaign
{
    [RequireComponent(typeof(Collider))]
    public sealed class CampaignObjectiveTrigger : MonoBehaviour
    {
        [SerializeField] private string prerequisiteObjectiveId;
        [SerializeField] private string completedObjectiveId;
        [SerializeField] private string completionText;
        [SerializeField] private string checkpointId;
        [SerializeField] private ArenaEncounterController encounter;
        private bool consumed;

        public bool IsConsumed { get { return consumed; } }

        public void Configure(string prerequisiteId, string objectiveId, string completedText, string activatedCheckpointId, ArenaEncounterController activatedEncounter)
        {
            prerequisiteObjectiveId = prerequisiteId;
            completedObjectiveId = objectiveId;
            completionText = completedText;
            checkpointId = activatedCheckpointId;
            encounter = activatedEncounter;
        }

        private void Start()
        {
            consumed = GameRoot.Instance.Objectives.IsComplete(completedObjectiveId);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other != null) TryActivate(other.transform.root.gameObject);
        }

        public bool TryActivate(GameObject actor)
        {
            if (consumed || actor == null || !actor.CompareTag("Player")) return false;
            if (GameRoot.Instance.State.CurrentState != GameState.Playing) return false;
            if (!string.IsNullOrEmpty(prerequisiteObjectiveId) && !GameRoot.Instance.Objectives.IsComplete(prerequisiteObjectiveId)) return false;
            consumed = true;
            GameRoot.Instance.Objectives.Complete(completedObjectiveId, completionText);
            if (!string.IsNullOrEmpty(checkpointId)) GameRoot.Instance.Checkpoints.Activate(checkpointId);
            if (encounter != null) encounter.Activate();
            return true;
        }
    }
}
