using MarineSlayer.Combat;
using MarineSlayer.Core;
using MarineSlayer.Player;
using UnityEngine;

namespace MarineSlayer.UI
{
    public sealed class RuntimeHudController : MonoBehaviour
    {
        private Health playerHealth;
        private PlayerWeaponController weapon;
        private TextMesh status;
        private TextMesh objective;
        private TextMesh banner;

        private void Start()
        {
            PlayerMotor player = FindObjectOfType<PlayerMotor>();
            if (player != null)
            {
                playerHealth = player.GetComponent<Health>();
                weapon = player.GetComponent<PlayerWeaponController>();
            }

            Camera camera = Camera.main;
            if (camera == null) return;
            status = CreateText(camera.transform, "CombatStatus", new Vector3(-4.7f, 2.45f, 5f), 0.055f, 30, TextAnchor.UpperLeft, new Color(0.65f, 0.95f, 1f, 1f));
            objective = CreateText(camera.transform, "Objective", new Vector3(-4.7f, -2.1f, 5f), 0.045f, 26, TextAnchor.LowerLeft, new Color(0.75f, 0.82f, 0.86f, 1f));
            banner = CreateText(camera.transform, "StateBanner", new Vector3(0f, 0.2f, 5f), 0.09f, 48, TextAnchor.MiddleCenter, Color.white);
            Refresh();
        }

        private void Update()
        {
            Refresh();
        }

        private void Refresh()
        {
            if (status == null) return;

            string healthValue = playerHealth == null ? "--" : Mathf.CeilToInt(playerHealth.Current).ToString();
            string weaponName = "NO WEAPON";
            string ammunition = "--";
            if (weapon != null && weapon.CurrentWeapon != null)
            {
                WeaponRuntimeState current = weapon.CurrentWeapon;
                weaponName = current.Definition.displayName;
                if (current.Definition.UsesHeat)
                    ammunition = "HEAT " + Mathf.CeilToInt(current.Heat) + "%" + (current.Overheated ? "  OVERHEATED" : string.Empty);
                else
                    ammunition = current.Definition.UsesAmmunition
                        ? current.Magazine + " / " + current.Reserve
                        : "UNLIMITED";
                if (weapon.IsReloading) ammunition += "  RELOADING";
            }

            status.text = "VOSS // HEALTH " + healthValue + "\n" + weaponName + "\nAMMO " + ammunition;
            string objectiveText = GameRoot.Instance.Objectives.CurrentText;
            if (string.IsNullOrEmpty(objectiveText)) objectiveText = "AWAITING MISSION OBJECTIVE";
            objective.text = objectiveText + "\nMOVE LS  AIM RS  FIRE RT  RELOAD Y  SWITCH RB";

            GameState state = GameRoot.Instance.State.CurrentState;
            if (state == GameState.Paused) banner.text = "PAUSED";
            else if (state == GameState.PlayerDead) banner.text = "VOSS DOWN\nPRESS A TO RESTART";
            else banner.text = string.Empty;
        }

        private static TextMesh CreateText(Transform camera, string objectName, Vector3 localPosition, float characterSize, int fontSize, TextAnchor anchor, Color color)
        {
            GameObject textObject = new GameObject(objectName);
            textObject.transform.SetParent(camera, false);
            textObject.transform.localPosition = localPosition;
            textObject.transform.localRotation = Quaternion.identity;
            TextMesh text = textObject.AddComponent<TextMesh>();
            text.anchor = anchor;
            text.alignment = TextAlignment.Left;
            text.characterSize = characterSize;
            text.fontSize = fontSize;
            text.color = color;
            return text;
        }
    }
}
