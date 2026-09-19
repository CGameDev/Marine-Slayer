using System.Collections.Generic;
using MarineSlayer.Save;
using UnityEngine;

namespace MarineSlayer.Core
{
    public sealed class MissionProgressService : MonoBehaviour
    {
        private int completionCount;

        public int CompletionCount { get { return completionCount; } }

        public bool CompleteMission(string missionId, int nextUnlockedLevel)
        {
            if (string.IsNullOrEmpty(missionId) || IsComplete(missionId)) return false;
            CampaignSaveData data = GameRoot.Instance.Saves.Current;
            CompletedIds().Add(missionId);
            data.highestUnlockedLevel = Mathf.Max(data.highestUnlockedLevel, nextUnlockedLevel);
            GameRoot.Instance.Saves.Write();
            completionCount++;
            GameEvents.RaiseMissionCompleted(missionId);
            GameRoot.Instance.State.SetState(GameState.LevelComplete);
            return true;
        }

        public bool IsComplete(string missionId)
        {
            return !string.IsNullOrEmpty(missionId) && CompletedIds().Contains(missionId);
        }

        public void ResetSession()
        {
            completionCount = 0;
        }

        private static List<string> CompletedIds()
        {
            CampaignSaveData data = GameRoot.Instance.Saves.Current;
            if (data.completedMissionIds == null) data.completedMissionIds = new List<string>();
            return data.completedMissionIds;
        }
    }
}
