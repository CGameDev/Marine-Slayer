using System.Collections;
using MarineSlayer.Combat;
using MarineSlayer.Core;
using MarineSlayer.Encounters;
using MarineSlayer.Player;
using UnityEngine;

namespace MarineSlayer.Campaign
{
    public sealed class CampaignLevelController : MonoBehaviour
    {
        [SerializeField] private int levelNumber = 1;
        [SerializeField] private string locationName = "CRYO-BAY 09";
        [SerializeField] private string levelTitle = "COLD REBIRTH";
        [SerializeField] private string missionId = "l01-cold-rebirth";
        [SerializeField] private int nextUnlockedLevel = 2;
        [SerializeField] private string startingObjectiveId = "l01-release-cryo";
        [SerializeField] private string startingObjectiveText = "ACCESS THE CRYO-BAY RELEASE CONSOLE";
        [SerializeField] private string traversalObjectiveId = "l01-reach-decon";
        [SerializeField] private string traversalObjectiveText = "MOVE THROUGH CRYO-BAY 09 // REACH DECONTAMINATION";
        [SerializeField] private string encounterObjectiveId = "l01-clear-thralls";
        [SerializeField] private string exitObjectiveId = "l01-reach-crew-ring";
        [SerializeField] private string exitObjectiveText = "ESCAPE CRYO-BAY 09 // REACH THE CREW RING";
        [SerializeField] private string startCheckpointId = "l01-awakening";
        [SerializeField] private string encounterCheckpointId = "l01-decon";
        [SerializeField] private string clearedCheckpointId = "l01-thralls-cleared";
        [SerializeField] private string completionCheckpointId = "l01-complete";
        [SerializeField] private Vector3 startSpawn = new Vector3(0f, 1.1f, -17f);
        [SerializeField] private Vector3 encounterSpawn = new Vector3(0f, 1.1f, -2f);
        [SerializeField] private Vector3 clearedSpawn = new Vector3(0f, 1.1f, 11.8f);
        [SerializeField] private ArenaEncounterController encounter;

        private PlayerMotor player;
        private Health playerHealth;
        private bool initialized;
        private float introUntil;
        private float levelCompleteAt = -1f;

        public int LevelNumber { get { return levelNumber; } }
        public string LocationName { get { return locationName; } }
        public string LevelTitle { get { return levelTitle; } }
        public string MissionId { get { return missionId; } }
        public string IntroBanner { get { return "LEVEL " + levelNumber.ToString("00") + " // " + locationName + "\n" + levelTitle; } }
        public string CompletionBanner { get { return "LEVEL " + levelNumber.ToString("00") + " COMPLETE\n" + levelTitle; } }
        public bool IntroVisible { get { return initialized && GameRoot.Instance.State.CurrentState == GameState.Playing && Time.realtimeSinceStartup < introUntil; } }

        public void Configure(
            int number,
            string location,
            string title,
            string completedMissionId,
            int unlockLevel,
            string firstObjectiveId,
            string firstObjectiveText,
            string movementObjectiveId,
            string movementObjectiveText,
            string combatObjectiveId,
            string finalObjectiveId,
            string finalObjectiveText,
            string initialCheckpointId,
            string combatCheckpointId,
            string combatClearedCheckpointId,
            string completedCheckpointId,
            Vector3 initialSpawn,
            Vector3 combatSpawn,
            Vector3 combatClearedSpawn,
            ArenaEncounterController levelEncounter)
        {
            levelNumber = number;
            locationName = location;
            levelTitle = title;
            missionId = completedMissionId;
            nextUnlockedLevel = unlockLevel;
            startingObjectiveId = firstObjectiveId;
            startingObjectiveText = firstObjectiveText;
            traversalObjectiveId = movementObjectiveId;
            traversalObjectiveText = movementObjectiveText;
            encounterObjectiveId = combatObjectiveId;
            exitObjectiveId = finalObjectiveId;
            exitObjectiveText = finalObjectiveText;
            startCheckpointId = initialCheckpointId;
            encounterCheckpointId = combatCheckpointId;
            clearedCheckpointId = combatClearedCheckpointId;
            completionCheckpointId = completedCheckpointId;
            startSpawn = initialSpawn;
            encounterSpawn = combatSpawn;
            clearedSpawn = combatClearedSpawn;
            encounter = levelEncounter;
        }

        private void OnEnable()
        {
            GameEvents.ObjectiveCompleted += OnObjectiveCompleted;
        }

        private void Start()
        {
            StartCoroutine(Initialize());
        }

        private IEnumerator Initialize()
        {
            yield return null;
            player = FindObjectOfType<PlayerMotor>();
            if (player != null)
            {
                playerHealth = player.GetComponent<Health>();
                if (playerHealth != null) playerHealth.Died += OnPlayerDied;
                PlayerWeaponController weapon = player.GetComponent<PlayerWeaponController>();
                if (weapon != null) weapon.SelectWeapon(CanonicalWeaponId.FuryGauntlet);
            }

            initialized = true;
            introUntil = Time.realtimeSinceStartup + 3.5f;
            if (GameRoot.Instance.Missions.IsComplete(missionId))
            {
                MovePlayer(clearedSpawn);
                GameRoot.Instance.State.SetState(GameState.LevelComplete);
                levelCompleteAt = Time.realtimeSinceStartup;
                yield break;
            }

            GameRoot.Instance.State.SetState(GameState.Playing);
            string checkpoint = GameRoot.Instance.Saves.Current.checkpointId;
            if (checkpoint == "start" || string.IsNullOrEmpty(checkpoint))
            {
                GameRoot.Instance.Checkpoints.Activate(startCheckpointId);
                checkpoint = startCheckpointId;
            }

            if (GameRoot.Instance.Objectives.IsComplete(encounterObjectiveId) || checkpoint == clearedCheckpointId)
            {
                MovePlayer(clearedSpawn);
                GameRoot.Instance.Objectives.SetCurrent(exitObjectiveId, exitObjectiveText);
            }
            else if (checkpoint == encounterCheckpointId || GameRoot.Instance.Objectives.IsComplete(traversalObjectiveId))
            {
                MovePlayer(encounterSpawn);
                if (encounter != null) encounter.Activate();
            }
            else if (GameRoot.Instance.Objectives.IsComplete(startingObjectiveId))
            {
                MovePlayer(startSpawn);
                GameRoot.Instance.Objectives.SetCurrent(traversalObjectiveId, traversalObjectiveText);
            }
            else
            {
                MovePlayer(startSpawn);
                GameRoot.Instance.Objectives.SetCurrent(startingObjectiveId, startingObjectiveText);
            }
        }

        private void Update()
        {
            if (!initialized) return;
            GameState state = GameRoot.Instance.State.CurrentState;
            if (state == GameState.Lore) return;
            if (state == GameState.LevelComplete)
            {
                if (levelCompleteAt < 0f)
                {
                    levelCompleteAt = Time.realtimeSinceStartup;
                    return;
                }
                if (GameRoot.Instance.Input.SubmitPressed && Time.realtimeSinceStartup - levelCompleteAt > 0.25f)
                    GameRoot.Instance.Scenes.Load("MS_MainMenu", GameState.MainMenu);
                return;
            }
            if (GameRoot.Instance.Input.PausePressed) GameRoot.Instance.State.TogglePause();
            if (GameRoot.Instance.Input.DebugDeathPressed) GameRoot.Instance.State.SetState(GameState.PlayerDead);
            if ((GameRoot.Instance.Input.RestartPressed || GameRoot.Instance.Input.SubmitPressed) && state == GameState.PlayerDead)
                GameRoot.Instance.Checkpoints.Restart();
            if (GameRoot.Instance.Input.MenuPressed)
                GameRoot.Instance.Scenes.Load("MS_MainMenu", GameState.MainMenu);
        }

        public bool CompleteLevel()
        {
            if (!initialized || GameRoot.Instance.Missions.IsComplete(missionId)) return false;
            if (!GameRoot.Instance.Objectives.IsComplete(encounterObjectiveId)) return false;
            GameRoot.Instance.Objectives.Complete(exitObjectiveId, "CREW RING ACCESS REACHED // LEVEL COMPLETE");
            GameRoot.Instance.Checkpoints.Activate(completionCheckpointId);
            return GameRoot.Instance.Missions.CompleteMission(missionId, nextUnlockedLevel);
        }

        private void OnObjectiveCompleted(string objectiveId)
        {
            if (objectiveId == startingObjectiveId)
                GameRoot.Instance.Objectives.SetCurrent(traversalObjectiveId, traversalObjectiveText);
            else if (objectiveId == encounterObjectiveId)
                GameRoot.Instance.Objectives.SetCurrent(exitObjectiveId, exitObjectiveText);
        }

        private void OnPlayerDied(Health value)
        {
            GameRoot.Instance.State.SetState(GameState.PlayerDead);
        }

        private void MovePlayer(Vector3 position)
        {
            if (player == null) return;
            Rigidbody body = player.GetComponent<Rigidbody>();
            if (body != null)
            {
                body.position = position;
                body.velocity = Vector3.zero;
                body.angularVelocity = Vector3.zero;
            }
            player.transform.position = position;
        }

        private void OnDisable()
        {
            GameEvents.ObjectiveCompleted -= OnObjectiveCompleted;
            if (playerHealth != null) playerHealth.Died -= OnPlayerDied;
        }
    }
}
