using MarineSlayer.Core;
using UnityEngine;

namespace MarineSlayer.UI
{
    public sealed class BootSceneController : MonoBehaviour
    {
        private void Start()
        {
            GameRoot.Instance.State.SetState(GameState.Boot);
            GameRoot.Instance.Scenes.Load("MS_MainMenu", GameState.MainMenu);
        }
    }
}
