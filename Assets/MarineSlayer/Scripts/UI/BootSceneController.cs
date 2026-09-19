using MarineSlayer.Core;
using UnityEngine;

namespace MarineSlayer.UI
{
    public sealed class BootSceneController : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("MARINE_SLAYER_BOOT_READY");
            GameRoot.Instance.State.SetState(GameState.Boot);
            GameRoot.Instance.Scenes.Load("MS_MainMenu", GameState.MainMenu);
        }
    }
}
