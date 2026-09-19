using System;
using System.Collections.Generic;

namespace MarineSlayer.Save
{
    public enum CampaignDifficulty
    {
        Recruit,
        Marine,
        Slayer
    }

    [Serializable]
    public sealed class CampaignSaveData
    {
        public int version = 2;
        public int highestUnlockedLevel = 1;
        public CampaignDifficulty difficulty = CampaignDifficulty.Marine;
        public string sceneName = "MS_L01_ColdRebirth";
        public string checkpointId = "start";
        public List<string> collectedLoreIds = new List<string>();
        public List<string> readLoreIds = new List<string>();
        public List<string> completedObjectiveIds = new List<string>();
        public List<string> completedMissionIds = new List<string>();
    }
}
