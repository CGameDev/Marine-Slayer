using MarineSlayer.Audio;
using MarineSlayer.Dialogue;
using MarineSlayer.Input;
using MarineSlayer.Lore;
using MarineSlayer.Platform;
using MarineSlayer.Save;
using MarineSlayer.Settings;
using UnityEngine;

namespace MarineSlayer.Core
{
    public sealed class GameRoot : MonoBehaviour
    {
        private static GameRoot instance;
        public static GameRoot Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject root = new GameObject("MarineSlayer_GameRoot");
                    instance = root.AddComponent<GameRoot>();
                }
                return instance;
            }
        }

        public GameStateService State { get; private set; }
        public SceneFlowService Scenes { get; private set; }
        public SaveService Saves { get; private set; }
        public DifficultyService Difficulty { get; private set; }
        public ObjectiveService Objectives { get; private set; }
        public MissionProgressService Missions { get; private set; }
        public CheckpointService Checkpoints { get; private set; }
        public InputService Input { get; private set; }
        public DialogueService Dialogue { get; private set; }
        public AudioService Audio { get; private set; }
        public SettingsService Settings { get; private set; }
        public LoreService Lore { get; private set; }
        public PlatformService Platform { get; private set; }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
            State = Ensure<GameStateService>();
            Scenes = Ensure<SceneFlowService>();
            Saves = Ensure<SaveService>();
            Difficulty = Ensure<DifficultyService>();
            Objectives = Ensure<ObjectiveService>();
            Missions = Ensure<MissionProgressService>();
            Checkpoints = Ensure<CheckpointService>();
            Input = Ensure<InputService>();
            Dialogue = Ensure<DialogueService>();
            Audio = Ensure<AudioService>();
            Settings = Ensure<SettingsService>();
            Lore = Ensure<LoreService>();
            Platform = Ensure<PlatformService>();
        }

        private T Ensure<T>() where T : Component
        {
            T service = GetComponent<T>();
            return service != null ? service : gameObject.AddComponent<T>();
        }
    }
}
