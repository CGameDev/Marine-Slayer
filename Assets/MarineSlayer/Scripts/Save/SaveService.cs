using System;
using System.IO;
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
            Current = new CampaignSaveData();
            Write();
        }

        public void Read()
        {
            if (!backend.Exists) return;
            try
            {
                CampaignSaveData loaded = JsonUtility.FromJson<CampaignSaveData>(backend.Read());
                if (loaded != null && loaded.version == 1) Current = loaded;
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
