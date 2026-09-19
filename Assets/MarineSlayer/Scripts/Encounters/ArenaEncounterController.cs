using System;
using System.Collections;
using System.Collections.Generic;
using MarineSlayer.Combat;
using MarineSlayer.Core;
using UnityEngine;

namespace MarineSlayer.Encounters
{
    [Serializable]
    public sealed class EncounterWave
    {
        public string waveId;
        public float delaySeconds;
        public GameObject[] actors;

        public EncounterWave(string id, float delay, GameObject[] members)
        {
            waveId = id;
            delaySeconds = delay;
            actors = members;
        }
    }

    public sealed class ArenaEncounterController : MonoBehaviour
    {
        private enum EncounterState { Dormant, Active, Completed }

        [SerializeField] private string objectiveId = "foundation-secure-arena";
        [SerializeField] private string objectiveText = "CONTAIN THE CONVERGENCE // CLEAR ALL WAVES";
        [SerializeField] private string completionText = "AREA SECURED // CHECKPOINT ACTIVE";
        [SerializeField] private string checkpointId = "foundation-encounter-cleared";
        [SerializeField] private bool activateOnStart = true;
        [SerializeField] private EncounterGate[] gates = new EncounterGate[0];
        [SerializeField] private EncounterWave[] waves = new EncounterWave[0];
        private EncounterState state;
        private int currentWaveIndex = -1;
        private int activationCount;
        private int completionCount;

        public bool IsActive { get { return state == EncounterState.Active; } }
        public bool IsComplete { get { return state == EncounterState.Completed; } }
        public int CurrentWaveIndex { get { return currentWaveIndex; } }
        public int ActivationCount { get { return activationCount; } }
        public int CompletionCount { get { return completionCount; } }
        public int WaveCount { get { return waves == null ? 0 : waves.Length; } }

        public bool AreGatesLocked
        {
            get
            {
                if (gates == null || gates.Length == 0) return false;
                for (int index = 0; index < gates.Length; index++)
                    if (gates[index] == null || !gates[index].IsLocked) return false;
                return true;
            }
        }

        public int ActiveActorCount
        {
            get { return CountLiveActors(currentWaveIndex); }
        }

        public void Configure(string id, string activeText, string clearedText, string clearedCheckpointId, EncounterGate[] encounterGates, EncounterWave[] encounterWaves)
        {
            objectiveId = id;
            objectiveText = activeText;
            completionText = clearedText;
            checkpointId = clearedCheckpointId;
            gates = encounterGates;
            waves = encounterWaves;
        }

        private void Awake()
        {
            SetGatesLocked(false);
            SetAllActorsActive(false);
        }

        private void Start()
        {
            if (GameRoot.Instance.Objectives.IsComplete(objectiveId))
            {
                RestoreCompletedState();
                return;
            }
            if (activateOnStart) Activate();
        }

        public bool Activate()
        {
            if (state != EncounterState.Dormant) return false;
            state = EncounterState.Active;
            activationCount++;
            SetGatesLocked(true);
            GameRoot.Instance.Objectives.SetCurrent(objectiveId, objectiveText);
            StartCoroutine(RunWaves());
            return true;
        }

        public GameObject[] GetAllActors()
        {
            List<GameObject> result = new List<GameObject>();
            if (waves == null) return result.ToArray();
            for (int waveIndex = 0; waveIndex < waves.Length; waveIndex++)
            {
                EncounterWave wave = waves[waveIndex];
                if (wave == null || wave.actors == null) continue;
                for (int actorIndex = 0; actorIndex < wave.actors.Length; actorIndex++)
                    if (wave.actors[actorIndex] != null) result.Add(wave.actors[actorIndex]);
            }
            return result.ToArray();
        }

        private IEnumerator RunWaves()
        {
            if (waves == null || waves.Length == 0)
            {
                CompleteEncounter();
                yield break;
            }

            for (int index = 0; index < waves.Length; index++)
            {
                currentWaveIndex = index;
                EncounterWave wave = waves[index];
                if (wave != null && wave.delaySeconds > 0f) yield return new WaitForSeconds(wave.delaySeconds);
                SetWaveActive(index, true);
                yield return null;
                while (state == EncounterState.Active && CountLiveActors(index) > 0) yield return null;
                if (state != EncounterState.Active) yield break;
            }

            CompleteEncounter();
        }

        private void CompleteEncounter()
        {
            if (state == EncounterState.Completed) return;
            state = EncounterState.Completed;
            completionCount++;
            SetAllActorsActive(false);
            SetGatesLocked(false);
            GameRoot.Instance.Objectives.Complete(objectiveId, completionText);
            GameRoot.Instance.Checkpoints.Activate(checkpointId);
        }

        private void RestoreCompletedState()
        {
            state = EncounterState.Completed;
            currentWaveIndex = waves == null ? -1 : waves.Length;
            SetAllActorsActive(false);
            SetGatesLocked(false);
            GameRoot.Instance.Objectives.SetCurrent(objectiveId, completionText);
        }

        private void SetGatesLocked(bool value)
        {
            if (gates == null) return;
            for (int index = 0; index < gates.Length; index++)
                if (gates[index] != null) gates[index].SetLocked(value);
        }

        private void SetAllActorsActive(bool value)
        {
            if (waves == null) return;
            for (int index = 0; index < waves.Length; index++) SetWaveActive(index, value);
        }

        private void SetWaveActive(int waveIndex, bool value)
        {
            if (waves == null || waveIndex < 0 || waveIndex >= waves.Length) return;
            EncounterWave wave = waves[waveIndex];
            if (wave == null || wave.actors == null) return;
            for (int index = 0; index < wave.actors.Length; index++)
                if (wave.actors[index] != null) wave.actors[index].SetActive(value);
        }

        private int CountLiveActors(int waveIndex)
        {
            if (waves == null || waveIndex < 0 || waveIndex >= waves.Length) return 0;
            EncounterWave wave = waves[waveIndex];
            if (wave == null || wave.actors == null) return 0;
            int count = 0;
            for (int index = 0; index < wave.actors.Length; index++)
            {
                GameObject actor = wave.actors[index];
                if (actor == null || !actor.activeSelf) continue;
                Health health = actor.GetComponent<Health>();
                if (health == null || !health.IsDead) count++;
            }
            return count;
        }
    }
}
