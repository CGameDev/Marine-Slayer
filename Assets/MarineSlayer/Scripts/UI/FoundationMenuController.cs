using MarineSlayer.Core;
using UnityEngine;

namespace MarineSlayer.UI
{
    public sealed class FoundationMenuController : MonoBehaviour
    {
        private void Start()
        {
            GameRoot.Instance.State.SetState(GameState.MainMenu);
        }

        private void Update()
        {
            if (GameRoot.Instance.Input.SubmitPressed) StartNewGame();
        }

        private void StartNewGame()
        {
            GameRoot.Instance.State.SetState(GameState.NewGameSetup);
            GameRoot.Instance.Saves.BeginNewCampaign();
            GameRoot.Instance.Scenes.Load("MS_FoundationTest", GameState.Playing);
        }

        private void OnGUI()
        {
            const float width = 520f;
            const float height = 260f;
            Rect panel = new Rect((Screen.width - width) * 0.5f, (Screen.height - height) * 0.5f, width, height);
            GUI.Box(panel, string.Empty);
            GUI.Label(new Rect(panel.x + 30f, panel.y + 25f, 460f, 40f), "MARINE SLAYER");
            GUI.Label(new Rect(panel.x + 30f, panel.y + 70f, 460f, 60f), "Foundation build — controller-first runtime validation");
            if (GUI.Button(new Rect(panel.x + 110f, panel.y + 145f, 300f, 45f), "New Game / Foundation Test")) StartNewGame();
            GUI.Label(new Rect(panel.x + 30f, panel.y + 210f, 460f, 30f), "Press Enter or controller A");
        }
    }
}
