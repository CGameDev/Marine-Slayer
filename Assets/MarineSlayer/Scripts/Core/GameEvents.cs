using System;

namespace MarineSlayer.Core
{
    public static class GameEvents
    {
        public static event Action<GameState, GameState> StateChanged;
        public static event Action<string, string> CheckpointActivated;
        public static event Action<string, string> ObjectiveChanged;
        public static event Action<string> ObjectiveCompleted;
        public static event Action<string> LoreOpened;
        public static event Action LoreClosed;
        public static event Action<string> MissionCompleted;

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

        internal static void RaiseObjectiveChanged(string objectiveId, string displayText)
        {
            Action<string, string> handler = ObjectiveChanged;
            if (handler != null) handler(objectiveId, displayText);
        }

        internal static void RaiseObjectiveCompleted(string objectiveId)
        {
            Action<string> handler = ObjectiveCompleted;
            if (handler != null) handler(objectiveId);
        }

        internal static void RaiseLoreOpened(string loreId)
        {
            Action<string> handler = LoreOpened;
            if (handler != null) handler(loreId);
        }

        internal static void RaiseLoreClosed()
        {
            Action handler = LoreClosed;
            if (handler != null) handler();
        }

        internal static void RaiseMissionCompleted(string missionId)
        {
            Action<string> handler = MissionCompleted;
            if (handler != null) handler(missionId);
        }
    }
}
