using System.Collections.Generic;
using MarineSlayer.Save;
using UnityEngine;

namespace MarineSlayer.Core
{
    public sealed class ObjectiveService : MonoBehaviour
    {
        private string currentId = string.Empty;
        private string currentText = string.Empty;
        private int changeCount;
        private int completionCount;

        public string CurrentId { get { return currentId; } }
        public string CurrentText { get { return currentText; } }
        public int ChangeCount { get { return changeCount; } }
        public int CompletionCount { get { return completionCount; } }

        public bool SetCurrent(string objectiveId, string displayText)
        {
            if (string.IsNullOrEmpty(objectiveId)) return false;
            string normalizedText = displayText ?? string.Empty;
            if (currentId == objectiveId && currentText == normalizedText) return false;
            currentId = objectiveId;
            currentText = normalizedText;
            changeCount++;
            GameEvents.RaiseObjectiveChanged(currentId, currentText);
            return true;
        }

        public bool Complete(string objectiveId, string completionText)
        {
            if (string.IsNullOrEmpty(objectiveId) || IsComplete(objectiveId)) return false;
            CompletedIds().Add(objectiveId);
            GameRoot.Instance.Saves.Write();
            completionCount++;
            SetCurrent(objectiveId, completionText);
            GameEvents.RaiseObjectiveCompleted(objectiveId);
            return true;
        }

        public bool IsComplete(string objectiveId)
        {
            return !string.IsNullOrEmpty(objectiveId) && CompletedIds().Contains(objectiveId);
        }

        public void ResetSession()
        {
            currentId = string.Empty;
            currentText = string.Empty;
            changeCount = 0;
            completionCount = 0;
        }

        private static List<string> CompletedIds()
        {
            CampaignSaveData data = GameRoot.Instance.Saves.Current;
            if (data.completedObjectiveIds == null) data.completedObjectiveIds = new List<string>();
            return data.completedObjectiveIds;
        }
    }
}
