using MarineSlayer.Core;
using MarineSlayer.Save;
using UnityEngine;

namespace MarineSlayer.UI
{
    public enum FoundationMenuScreen
    {
        Main,
        ConfirmNewGame,
        Difficulty,
        Options,
        Credits
    }

    public sealed class FoundationMenuController : MonoBehaviour
    {
        private bool starting;
        private int selectedIndex;
        private TextMesh menuText;
        private TextMesh detailText;
        private TextMesh statusText;

        public FoundationMenuScreen CurrentScreen { get; private set; }
        public int SelectedIndex { get { return selectedIndex; } }
        public string RenderedMenu { get { return menuText == null ? string.Empty : menuText.text; } }
        public bool IsStarting { get { return starting; } }

        private void Start()
        {
            GameRoot.Instance.State.SetState(GameState.MainMenu);
            BuildWorldSpaceDisplay();
            CurrentScreen = FoundationMenuScreen.Main;
            selectedIndex = GameRoot.Instance.Saves.HasSave ? 0 : 1;
            Refresh();
            Debug.Log("MARINE_SLAYER_MENU_READY");
        }

        private void Update()
        {
            if (starting) return;
            int verticalStep = GameRoot.Instance.Input.ConsumeMenuVerticalStep();
            if (verticalStep != 0) MoveSelection(-verticalStep);
            if (GameRoot.Instance.Input.CancelPressed && CurrentScreen != FoundationMenuScreen.Main) Back();
            else if (GameRoot.Instance.Input.SubmitPressed) ActivateSelected();
        }

        public bool StartNewGame()
        {
            return StartNewGame(CampaignDifficulty.Marine);
        }

        public bool StartNewGame(CampaignDifficulty difficulty)
        {
            if (starting) return false;
            starting = true;
            GameRoot.Instance.State.SetState(GameState.NewGameSetup);
            GameRoot.Instance.Saves.BeginNewCampaign(difficulty);
            GameRoot.Instance.Scenes.Load("MS_FoundationTest", GameState.Playing);
            return true;
        }

        public void RequestNewGame()
        {
            if (starting) return;
            if (GameRoot.Instance.Saves.HasSave)
            {
                CurrentScreen = FoundationMenuScreen.ConfirmNewGame;
                selectedIndex = 1;
            }
            else
            {
                CurrentScreen = FoundationMenuScreen.Difficulty;
                selectedIndex = (int)CampaignDifficulty.Marine;
            }
            Refresh();
        }

        public void ShowDifficulty()
        {
            if (starting) return;
            CurrentScreen = FoundationMenuScreen.Difficulty;
            selectedIndex = (int)CampaignDifficulty.Marine;
            Refresh();
        }

        public bool ContinueGame()
        {
            if (starting || !GameRoot.Instance.Saves.HasSave) return false;
            GameRoot.Instance.Saves.Read();
            string sceneName = GameRoot.Instance.Saves.Current.sceneName;
            if (string.IsNullOrEmpty(sceneName)) return false;
            starting = true;
            GameRoot.Instance.Scenes.Load(sceneName, GameState.Playing);
            return true;
        }

        public void ShowOptions()
        {
            if (starting) return;
            CurrentScreen = FoundationMenuScreen.Options;
            selectedIndex = 0;
            Refresh();
        }

        public void ShowCredits()
        {
            if (starting) return;
            CurrentScreen = FoundationMenuScreen.Credits;
            selectedIndex = 0;
            Refresh();
        }

        public void Back()
        {
            if (starting || CurrentScreen == FoundationMenuScreen.Main) return;
            CurrentScreen = FoundationMenuScreen.Main;
            selectedIndex = GameRoot.Instance.Saves.HasSave ? 0 : 1;
            Refresh();
        }

        public void SetSelection(int value)
        {
            int count = ItemCount();
            if (count == 0) return;
            selectedIndex = Mathf.Clamp(value, 0, count - 1);
            Refresh();
        }

        public bool ActivateSelected()
        {
            if (starting) return false;
            if (CurrentScreen == FoundationMenuScreen.Main)
            {
                if (selectedIndex == 0) return ContinueGame();
                if (selectedIndex == 1) RequestNewGame();
                if (selectedIndex == 2) ShowOptions();
                else if (selectedIndex == 3) ShowCredits();
                return true;
            }

            if (CurrentScreen == FoundationMenuScreen.ConfirmNewGame)
            {
                if (selectedIndex == 0) ShowDifficulty();
                else Back();
                return true;
            }

            if (CurrentScreen == FoundationMenuScreen.Difficulty)
            {
                if (selectedIndex < 3) return StartNewGame((CampaignDifficulty)selectedIndex);
                Back();
                return true;
            }

            if (CurrentScreen == FoundationMenuScreen.Options)
            {
                if (selectedIndex == 0) GameRoot.Instance.Settings.CycleMasterVolume();
                else if (selectedIndex == 1) GameRoot.Instance.Settings.ToggleSubtitles();
                else Back();
                Refresh();
                return true;
            }

            Back();
            return true;
        }

        private void MoveSelection(int direction)
        {
            int count = ItemCount();
            if (count == 0) return;
            selectedIndex = (selectedIndex + direction + count) % count;
            Refresh();
        }

        private int ItemCount()
        {
            if (CurrentScreen == FoundationMenuScreen.Main) return 4;
            if (CurrentScreen == FoundationMenuScreen.ConfirmNewGame) return 2;
            if (CurrentScreen == FoundationMenuScreen.Difficulty) return 4;
            if (CurrentScreen == FoundationMenuScreen.Options) return 3;
            return 1;
        }

        private void BuildWorldSpaceDisplay()
        {
            CreateText("Title", "MARINE SLAYER", new Vector3(0f, 3.2f, 0f), 0.18f, 72, new Color(0.3f, 0.9f, 1f, 1f));
            CreateText("Subtitle", "FOUNDATION COMBAT BUILD", new Vector3(0f, 1.8f, 0f), 0.09f, 42, Color.white);
            menuText = CreateText("Menu", string.Empty, new Vector3(0f, 0.1f, 0f), 0.08f, 38, new Color(0.75f, 0.95f, 1f, 1f));
            detailText = CreateText("Details", string.Empty, new Vector3(0f, -1.65f, 0f), 0.05f, 28, new Color(0.65f, 0.76f, 0.82f, 1f));
            statusText = CreateText("Status", string.Empty, new Vector3(0f, -2.8f, 0f), 0.045f, 26, new Color(0.45f, 0.65f, 0.75f, 1f));
        }

        private void Refresh()
        {
            if (menuText == null) return;
            if (CurrentScreen == FoundationMenuScreen.Main)
            {
                string continueLabel = GameRoot.Instance.Saves.HasSave ? "CONTINUE" : "CONTINUE [NO SAVE]";
                menuText.text = Line(0, continueLabel) + "\n" + Line(1, "NEW GAME") + "\n" + Line(2, "OPTIONS") + "\n" + Line(3, "CREDITS");
                string campaign = GameRoot.Instance.Saves.HasSave ? "\nSAVED DIFFICULTY: " + GameRoot.Instance.Saves.Current.difficulty.ToString().ToUpper() : string.Empty;
                detailText.text = "LEFT STICK / ARROWS: NAVIGATE   A: SELECT" + campaign;
            }
            else if (CurrentScreen == FoundationMenuScreen.ConfirmNewGame)
            {
                menuText.text = Line(0, "REPLACE CAMPAIGN") + "\n" + Line(1, "CANCEL");
                detailText.text = "WARNING: NEW GAME REPLACES CURRENT CAMPAIGN PROGRESS\nOPTIONS WILL BE PRESERVED";
            }
            else if (CurrentScreen == FoundationMenuScreen.Difficulty)
            {
                menuText.text = Line(0, "RECRUIT") + "\n" + Line(1, "MARINE") + "\n" + Line(2, "SLAYER") + "\n" + Line(3, "BACK");
                if (selectedIndex == 0) detailText.text = "RECRUIT: LOWER ENEMY DAMAGE AND HEALTH, MORE AMMUNITION";
                else if (selectedIndex == 1) detailText.text = "MARINE: STANDARD COMBAT AND RESOURCE TUNING";
                else if (selectedIndex == 2) detailText.text = "SLAYER: STRONGER ENEMIES, SCARCER AMMUNITION";
                else detailText.text = "RETURN WITHOUT REPLACING CAMPAIGN PROGRESS";
            }
            else if (CurrentScreen == FoundationMenuScreen.Options)
            {
                int volumePercent = Mathf.RoundToInt(GameRoot.Instance.Settings.Current.masterVolume * 100f);
                string subtitles = GameRoot.Instance.Settings.Current.subtitlesEnabled ? "ON" : "OFF";
                menuText.text = Line(0, "MASTER VOLUME  " + volumePercent + "%") + "\n" + Line(1, "SUBTITLES  " + subtitles) + "\n" + Line(2, "BACK");
                detailText.text = "A: CHANGE   B: BACK\nSETTINGS SAVE INDEPENDENTLY OF CAMPAIGN DATA";
            }
            else
            {
                menuText.text = Line(0, "BACK");
                detailText.text = "A CGAMEDEV PRODUCTION\nBUILT FROM THE MARINE SLAYER OWNER CANON\nUNITY 5.4.1f1 // XBOX 360";
            }
            statusText.text = "XBOX 360 / WINDOWS RUNTIME ONLINE";
        }

        private string Line(int index, string value)
        {
            return selectedIndex == index ? ">  " + value + "  <" : value;
        }

        private TextMesh CreateText(string objectName, string value, Vector3 position, float characterSize, int fontSize, Color color)
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
            return text;
        }
    }
}
