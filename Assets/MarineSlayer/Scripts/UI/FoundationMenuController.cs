using MarineSlayer.Core;
using UnityEngine;

namespace MarineSlayer.UI
{
    public sealed class FoundationMenuController : MonoBehaviour
    {
        private bool starting;

        private void Start()
        {
            GameRoot.Instance.State.SetState(GameState.MainMenu);
            BuildWorldSpaceDisplay();
            Debug.Log("MARINE_SLAYER_MENU_READY");
        }

        private void Update()
        {
            if (!starting && GameRoot.Instance.Input.SubmitPressed) StartNewGame();
        }

        private void StartNewGame()
        {
            if (starting) return;
            starting = true;
            GameRoot.Instance.State.SetState(GameState.NewGameSetup);
            GameRoot.Instance.Saves.BeginNewCampaign();
            GameRoot.Instance.Scenes.Load("MS_FoundationTest", GameState.Playing);
        }

        private void BuildWorldSpaceDisplay()
        {
            CreateText("Title", "MARINE SLAYER", new Vector3(0f, 3.2f, 0f), 0.18f, 72, new Color(0.3f, 0.9f, 1f, 1f));
            CreateText("Subtitle", "FOUNDATION COMBAT BUILD", new Vector3(0f, 1.8f, 0f), 0.09f, 42, Color.white);
            CreateText("Prompt", "PRESS  A  OR  ENTER  TO  DEPLOY", new Vector3(0f, -0.3f, 0f), 0.08f, 38, new Color(0.75f, 0.95f, 1f, 1f));
            CreateText("Status", "XBOX 360 / WINDOWS RUNTIME ONLINE", new Vector3(0f, -2.2f, 0f), 0.055f, 30, new Color(0.45f, 0.65f, 0.75f, 1f));
        }

        private void CreateText(string objectName, string value, Vector3 position, float characterSize, int fontSize, Color color)
        {
            GameObject textObject = new GameObject(objectName);
            textObject.transform.SetParent(transform, false);
            textObject.transform.position = position;
            TextMesh text = textObject.AddComponent<TextMesh>();
            text.text = value;
            text.anchor = TextAnchor.MiddleCenter;
            text.alignment = TextAlignment.Center;
            text.characterSize = characterSize;
            text.fontSize = fontSize;
            text.color = color;
        }
    }
}
