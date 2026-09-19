using UnityEngine;

namespace MarineSlayer.Core
{
    public sealed class GameStateService : MonoBehaviour
    {
        [SerializeField] private GameState currentState = GameState.Boot;

        public GameState CurrentState { get { return currentState; } }

        public void SetState(GameState nextState)
        {
            if (currentState == nextState) return;
            GameState previous = currentState;
            currentState = nextState;
            Time.timeScale = nextState == GameState.Paused || nextState == GameState.Lore ? 0f : 1f;
            GameEvents.RaiseStateChanged(previous, currentState);
        }

        public void TogglePause()
        {
            if (currentState == GameState.Playing) SetState(GameState.Paused);
            else if (currentState == GameState.Paused) SetState(GameState.Playing);
        }
    }
}
