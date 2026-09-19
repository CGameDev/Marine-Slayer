using MarineSlayer.Core;
using MarineSlayer.Player;
using UnityEngine;

namespace MarineSlayer.Lore
{
    public sealed class LoreTerminal : MonoBehaviour
    {
        [SerializeField] private LoreEntry entry;
        [SerializeField] private string prerequisiteObjectiveId;
        [SerializeField] private string interactionObjectiveId;
        [SerializeField] private string interactionObjectiveText;
        [SerializeField] private string interactionCompletionText;
        [SerializeField] private string missionId;
        [SerializeField] private int nextUnlockedLevel = 2;
        [SerializeField] private float interactionRange = 2.25f;
        private Transform player;
        private bool pendingMissionCompletion;
        private int interactionCount;
        private int completionCount;

        public int InteractionCount { get { return interactionCount; } }
        public int CompletionCount { get { return completionCount; } }
        public LoreEntry Entry { get { return entry; } }

        public bool IsAvailable
        {
            get
            {
                bool prerequisiteMet = string.IsNullOrEmpty(prerequisiteObjectiveId) || GameRoot.Instance.Objectives.IsComplete(prerequisiteObjectiveId);
                bool missionOpen = string.IsNullOrEmpty(missionId) || !GameRoot.Instance.Missions.IsComplete(missionId);
                return prerequisiteMet && missionOpen;
            }
        }

        public string InteractionPrompt
        {
            get
            {
                if (GameRoot.Instance.State.CurrentState != GameState.Playing || !IsAvailable || player == null) return string.Empty;
                if ((player.position - transform.position).sqrMagnitude > interactionRange * interactionRange) return string.Empty;
                return "PRESS A // ACCESS " + (entry == null ? "TERMINAL" : entry.title);
            }
        }

        public void Configure(LoreEntry loreEntry, string prerequisiteId, string objectiveId, string objectiveText, string completionText, string completedMissionId, int unlockLevel)
        {
            entry = loreEntry;
            prerequisiteObjectiveId = prerequisiteId;
            interactionObjectiveId = objectiveId;
            interactionObjectiveText = objectiveText;
            interactionCompletionText = completionText;
            missionId = completedMissionId;
            nextUnlockedLevel = unlockLevel;
        }

        private void Start()
        {
            PlayerMotor motor = FindObjectOfType<PlayerMotor>();
            if (motor != null) player = motor.transform;
            GameEvents.ObjectiveCompleted += OnObjectiveCompleted;
            PresentObjectiveIfReady();
        }

        private void OnDestroy()
        {
            GameEvents.ObjectiveCompleted -= OnObjectiveCompleted;
        }

        private void Update()
        {
            if (GameRoot.Instance.State.CurrentState == GameState.Lore && GameRoot.Instance.Lore.IsOpen && GameRoot.Instance.Lore.CurrentEntry == entry)
            {
                if (GameRoot.Instance.Input.InteractPressed) Close();
                return;
            }

            if (GameRoot.Instance.State.CurrentState != GameState.Playing || player == null) return;
            if (GameRoot.Instance.Input.InteractPressed) TryInteract(player.position);
        }

        public bool TryInteract(Vector3 userPosition)
        {
            if (GameRoot.Instance.State.CurrentState != GameState.Playing || !IsAvailable || entry == null) return false;
            if ((userPosition - transform.position).sqrMagnitude > interactionRange * interactionRange) return false;

            interactionCount++;
            if (GameRoot.Instance.Objectives.Complete(interactionObjectiveId, interactionCompletionText)) completionCount++;
            pendingMissionCompletion = !string.IsNullOrEmpty(missionId) && !GameRoot.Instance.Missions.IsComplete(missionId);
            GameRoot.Instance.Lore.Open(entry);
            return true;
        }

        public bool Close()
        {
            if (!GameRoot.Instance.Lore.Close()) return false;
            bool shouldCompleteMission = pendingMissionCompletion;
            pendingMissionCompletion = false;
            if (shouldCompleteMission)
                GameRoot.Instance.Missions.CompleteMission(missionId, nextUnlockedLevel);
            return true;
        }

        private void OnObjectiveCompleted(string objectiveId)
        {
            if (objectiveId == prerequisiteObjectiveId) PresentObjectiveIfReady();
        }

        private void PresentObjectiveIfReady()
        {
            if (!IsAvailable || GameRoot.Instance.Objectives.IsComplete(interactionObjectiveId)) return;
            GameRoot.Instance.Objectives.SetCurrent(interactionObjectiveId, interactionObjectiveText);
        }
    }
}
