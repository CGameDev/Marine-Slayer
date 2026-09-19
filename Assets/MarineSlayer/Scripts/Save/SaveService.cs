using System;
using System.IO;
using MarineSlayer.Core;
using MarineSlayer.Lore;
using UnityEngine;

namespace MarineSlayer.Save
{
    public sealed class SaveService : MonoBehaviour
    {
        private ISaveBackend backend;
        public CampaignSaveData Current { get; private set; }
        public bool HasSave { get { return backend != null && backend.Exists; } }

        private void Awake()
        {
            backend = new DevelopmentFileBackend(Path.Combine(Application.persistentDataPath, "marine-slayer-save.json"));
            Current = new CampaignSaveData();
            Read();
        }

        public void BeginNewCampaign()
        {
            BeginNewCampaign(CampaignDifficulty.Marine);
        }

        public void BeginNewCampaign(CampaignDifficulty difficulty)
        {
            Current = new CampaignSaveData();
            Current.difficulty = difficulty;
            ObjectiveService objectives = GetComponent<ObjectiveService>();
            if (objectives != null) objectives.ResetSession();
            MissionProgressService missions = GetComponent<MissionProgressService>();
            if (missions != null) missions.ResetSession();
            LoreService lore = GetComponent<LoreService>();
            if (lore != null) lore.ResetSession();
            Write();
        }

        public void Read()
        {
            if (!backend.Exists) return;
            try
            {
                CampaignSaveData loaded = JsonUtility.FromJson<CampaignSaveData>(backend.Read());
                if (loaded == null) return;
                if (loaded.version == 1)
                {
                    loaded.version = 2;
                    loaded.difficulty = CampaignDifficulty.Marine;
                    Current = loaded;
                    Write();
                }
                else if (loaded.version == 2)
                {
                    Current = loaded;
                }
            }
            catch (Exception exception)
            {
                Debug.LogWarning("Save load failed safely: " + exception.Message);
                Current = new CampaignSaveData();
            }
        }

        public void Write()
        {
            try { backend.Write(JsonUtility.ToJson(Current, true)); }
            catch (Exception exception) { Debug.LogError("Save write failed: " + exception.Message); }
        }
    }
}
