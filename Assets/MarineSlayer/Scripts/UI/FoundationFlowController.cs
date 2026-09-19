using MarineSlayer.Core;
using MarineSlayer.Combat;
using MarineSlayer.Player;
using UnityEngine;

namespace MarineSlayer.UI
{
    public sealed class FoundationFlowController : MonoBehaviour
    {
        private Health playerHealth;
        private float levelCompleteAt = -1f;

        private void Start()
        {
            GameRoot.Instance.State.SetState(GameState.Playing);
            if (GameRoot.Instance.Saves.Current.checkpointId == "start")
                GameRoot.Instance.Checkpoints.Activate("foundation-start");
            PlayerMotor player = FindObjectOfType<PlayerMotor>();
            if (player != null)
            {
                playerHealth = player.GetComponent<Health>();
                if (playerHealth != null) playerHealth.Died += OnPlayerDied;
            }
        }

        private void OnDestroy()
        {
            if (playerHealth != null) playerHealth.Died -= OnPlayerDied;
        }

        private void Update()
        {
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
            if ((GameRoot.Instance.Input.RestartPressed || GameRoot.Instance.Input.SubmitPressed) && GameRoot.Instance.State.CurrentState == GameState.PlayerDead)
                GameRoot.Instance.Checkpoints.Restart();
            if (GameRoot.Instance.Input.MenuPressed)
                GameRoot.Instance.Scenes.Load("MS_MainMenu", GameState.MainMenu);
        }

        private void OnPlayerDied(Health value)
        {
            GameRoot.Instance.State.SetState(GameState.PlayerDead);
        }
    }
}
