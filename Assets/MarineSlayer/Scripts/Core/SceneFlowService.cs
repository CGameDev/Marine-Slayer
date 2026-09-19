using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MarineSlayer.Core
{
    public sealed class SceneFlowService : MonoBehaviour
    {
        private bool loading;
        public bool IsLoading { get { return loading; } }

        public void Load(string sceneName, GameState destinationState)
        {
            if (!loading)
            {
                Debug.Log("MARINE_SLAYER_SCENE_LOAD_BEGIN: " + sceneName);
                StartCoroutine(LoadRoutine(sceneName, destinationState));
            }
        }

        private IEnumerator LoadRoutine(string sceneName, GameState destinationState)
        {
            loading = true;
            GameRoot.Instance.State.SetState(GameState.Loading);
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            while (!operation.isDone) yield return null;
            loading = false;
            GameRoot.Instance.State.SetState(destinationState);
            Debug.Log("MARINE_SLAYER_SCENE_LOAD_COMPLETE: " + sceneName);
        }
    }
}
