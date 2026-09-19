using System;
using System.IO;
using MarineSlayer.Core;
using MarineSlayer.Save;
using UnityEngine;

namespace MarineSlayer.Settings
{
    [Serializable]
    public sealed class SettingsData
    {
        public int version = 1;
        public float masterVolume = 1f;
        public bool subtitlesEnabled = true;
    }

    public sealed class SettingsService : MonoBehaviour
    {
        private ISaveBackend backend;

        public SettingsData Current { get; private set; }
        public bool HasSettings { get { return backend != null && backend.Exists; } }

        private void Awake()
        {
            backend = new DevelopmentFileBackend(Path.Combine(Application.persistentDataPath, "marine-slayer-settings.json"));
            Current = new SettingsData();
            Read();
            Apply();
        }

        public void SetMasterVolume(float value)
        {
            Current.masterVolume = Mathf.Clamp01(value);
            Write();
            Apply();
        }

        public void CycleMasterVolume()
        {
            float value = Current.masterVolume;
            if (value > 0.875f) SetMasterVolume(0.75f);
            else if (value > 0.625f) SetMasterVolume(0.5f);
            else if (value > 0.375f) SetMasterVolume(0.25f);
            else if (value > 0.125f) SetMasterVolume(0f);
            else SetMasterVolume(1f);
        }

        public void SetSubtitles(bool value)
        {
            Current.subtitlesEnabled = value;
            Write();
        }

        public void ToggleSubtitles()
        {
            SetSubtitles(!Current.subtitlesEnabled);
        }

        public void Read()
        {
            if (!backend.Exists) return;
            try
            {
                SettingsData loaded = JsonUtility.FromJson<SettingsData>(backend.Read());
                if (loaded != null && loaded.version == 1) Current = loaded;
            }
            catch (Exception exception)
            {
                Debug.LogWarning("Settings load failed safely: " + exception.Message);
                Current = new SettingsData();
            }
            Apply();
        }

        public void Write()
        {
            try { backend.Write(JsonUtility.ToJson(Current, true)); }
            catch (Exception exception) { Debug.LogError("Settings write failed: " + exception.Message); }
        }

        private void Apply()
        {
            AudioListener.volume = Mathf.Clamp01(Current.masterVolume);
            if (GameRoot.Instance.Audio != null) GameRoot.Instance.Audio.masterVolume = AudioListener.volume;
        }
    }
}
