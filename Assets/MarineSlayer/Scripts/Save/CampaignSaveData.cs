using System;
using System.Collections.Generic;

namespace MarineSlayer.Save
{
    [Serializable]
    public sealed class CampaignSaveData
    {
        public int version = 1;
        public int highestUnlockedLevel = 1;
        public string sceneName = "MS_FoundationTest";
        public string checkpointId = "start";
        public List<string> collectedLoreIds = new List<string>();
        public List<string> readLoreIds = new List<string>();
        public List<string> completedObjectiveIds = new List<string>();
        public List<string> completedMissionIds = new List<string>();
    }
}
