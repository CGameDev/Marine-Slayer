using MarineSlayer.Core;
using UnityEngine;

namespace MarineSlayer.UI
{
    public sealed class FoundationFlowController : MonoBehaviour
    {
        private void Start()
        {
            GameRoot.Instance.State.SetState(GameState.Playing);
            GameRoot.Instance.Checkpoints.Activate("foundation-start");
        }

        private void Update()
        {
            if (GameRoot.Instance.Input.PausePressed) GameRoot.Instance.State.TogglePause();
            if (GameRoot.Instance.Input.DebugDeathPressed) GameRoot.Instance.State.SetState(GameState.PlayerDead);
            if (GameRoot.Instance.Input.RestartPressed && GameRoot.Instance.State.CurrentState == GameState.PlayerDead)
                GameRoot.Instance.Checkpoints.Restart();
            if (GameRoot.Instance.Input.MenuPressed)
                GameRoot.Instance.Scenes.Load("MS_MainMenu", GameState.MainMenu);
        }

        private void OnGUI()
        {
            GameState state = GameRoot.Instance.State.CurrentState;
            GUI.Box(new Rect(20f, 20f, 460f, 150f), string.Empty);
            GUI.Label(new Rect(40f, 40f, 420f, 30f), "Marine Slayer runtime foundation");
            GUI.Label(new Rect(40f, 72f, 420f, 30f), "State: " + state);
            GUI.Label(new Rect(40f, 104f, 420f, 45f), "Start/Esc: pause  |  K: death  |  R: restart  |  M: menu");
        }
    }
}
