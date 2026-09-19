using MarineSlayer.Save;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MarineSlayer.Core
{
    public sealed class CheckpointService : MonoBehaviour
    {
        public void Activate(string checkpointId)
        {
            CampaignSaveData data = GameRoot.Instance.Saves.Current;
            data.sceneName = SceneManager.GetActiveScene().name;
            data.checkpointId = checkpointId;
            GameRoot.Instance.Saves.Write();
            GameEvents.RaiseCheckpointActivated(data.sceneName, checkpointId);
        }

        public void Restart()
        {
            CampaignSaveData data = GameRoot.Instance.Saves.Current;
            if (string.IsNullOrEmpty(data.sceneName)) return;
            GameRoot.Instance.State.SetState(GameState.CheckpointRestart);
            GameRoot.Instance.Scenes.Load(data.sceneName, GameState.Playing);
        }
    }
}
