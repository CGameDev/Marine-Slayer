using System;

namespace MarineSlayer.Core
{
    public static class GameEvents
    {
        public static event Action<GameState, GameState> StateChanged;
        public static event Action<string, string> CheckpointActivated;

        internal static void RaiseStateChanged(GameState previous, GameState current)
        {
            Action<GameState, GameState> handler = StateChanged;
            if (handler != null) handler(previous, current);
        }

        internal static void RaiseCheckpointActivated(string sceneName, string checkpointId)
        {
            Action<string, string> handler = CheckpointActivated;
            if (handler != null) handler(sceneName, checkpointId);
        }
    }
}
