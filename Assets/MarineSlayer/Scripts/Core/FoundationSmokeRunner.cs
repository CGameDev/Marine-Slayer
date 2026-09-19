using System;
using System.Collections;
using MarineSlayer.Combat;
using MarineSlayer.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MarineSlayer.Core
{
    public sealed class FoundationSmokeRunner : MonoBehaviour
    {
        private void Start()
        {
            string[] arguments = Environment.GetCommandLineArgs();
            for (int index = 0; index < arguments.Length; index++)
            {
                if (arguments[index] == "-marineSlayerSmoke")
                {
                    StartCoroutine(Run());
                    return;
                }
            }
            enabled = false;
        }

        private IEnumerator Run()
        {
            yield return WaitForScene("MS_MainMenu", 10f);
            yield return WaitForState(GameState.MainMenu, 10f);
            if (!Require(SceneManager.GetActiveScene().name == "MS_MainMenu", "Boot did not reach Main Menu")) yield break;
            if (!Require(GameRoot.Instance.State.CurrentState == GameState.MainMenu, "Main Menu state was not set")) yield break;

            GameRoot.Instance.Saves.BeginNewCampaign();
            GameRoot.Instance.Scenes.Load("MS_FoundationTest", GameState.Playing);
            yield return WaitForScene("MS_FoundationTest", 10f);
            yield return WaitForState(GameState.Playing, 10f);
            if (!Require(GameRoot.Instance.State.CurrentState == GameState.Playing, "Test scene did not enter Playing")) yield break;

            PlayerMotor motor = FindObjectOfType<PlayerMotor>();
            if (!Require(motor != null, "Player motor was not created")) yield break;
            Vector3 startPosition = motor.transform.position;
            GameRoot.Instance.Input.SetTestInput(Vector2.right, Vector2.right);
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            GameRoot.Instance.Input.ClearTestInput();
            if (!Require((motor.transform.position - startPosition).sqrMagnitude > 0.01f, "Player movement failed")) yield break;

            PlayerWeaponController weapon = motor.GetComponent<PlayerWeaponController>();
            Health target = FindObjectOfType<Health>();
            if (!Require(weapon != null && target != null, "Combat foundation objects were not created")) yield break;
            if (!Require(weapon.WeaponCount == 9, "Canonical nine-weapon catalog was not loaded")) yield break;
            if (!Require(weapon.CurrentWeapon.Definition.id == CanonicalWeaponId.GavelShotgun, "Gavel was not the initial weapon")) yield break;
            int initialMagazine = weapon.CurrentWeapon.Magazine;
            int initialReserve = weapon.CurrentWeapon.Reserve;
            motor.transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
            target.transform.position = motor.transform.position + Vector3.right * 4f + Vector3.up;
            yield return new WaitForFixedUpdate();
            float targetHealth = target.Current;
            Vector3 origin = motor.transform.position + Vector3.up + motor.transform.forward * 0.9f;
            Vector3 direction = (target.transform.position - origin).normalized;
            if (!Require(weapon.FireDirection(direction), "Canonical weapon did not fire")) yield break;
            if (!Require(weapon.CurrentWeapon.Magazine == initialMagazine - 1, "Weapon did not consume magazine ammunition")) yield break;
            for (int index = 0; index < 25; index++) yield return new WaitForFixedUpdate();
            if (!Require(target.Current < targetHealth, "Weapon did not apply shared damage")) yield break;

            if (!Require(weapon.BeginReload(), "Weapon reload did not begin")) yield break;
            float reloadDeadline = Time.realtimeSinceStartup + 5f;
            while (weapon.IsReloading && Time.realtimeSinceStartup < reloadDeadline) yield return null;
            if (!Require(!weapon.IsReloading, "Weapon reload timed out")) yield break;
            if (!Require(weapon.CurrentWeapon.Magazine == initialMagazine, "Reload did not refill the magazine")) yield break;
            if (!Require(weapon.CurrentWeapon.Reserve == initialReserve - 1, "Reload did not consume reserve ammunition")) yield break;
            if (!Require(weapon.SwitchNext(), "Weapon switching failed")) yield break;
            if (!Require(weapon.CurrentWeapon.Definition.id == CanonicalWeaponId.LancerRifle, "Weapon switch did not select the Lancer")) yield break;

            GameRoot.Instance.State.TogglePause();
            if (!Require(GameRoot.Instance.State.CurrentState == GameState.Paused && Time.timeScale == 0f, "Pause state failed")) yield break;
            GameRoot.Instance.State.SetState(GameState.PlayerDead);
            if (!Require(GameRoot.Instance.State.CurrentState == GameState.PlayerDead && Time.timeScale == 1f, "Death state failed")) yield break;

            GameRoot.Instance.Checkpoints.Activate("smoke-restart");
            GameRoot.Instance.Checkpoints.Restart();
            yield return null;
            while (GameRoot.Instance.Scenes.IsLoading) yield return null;
            yield return WaitForState(GameState.Playing, 10f);
            if (!Require(SceneManager.GetActiveScene().name == "MS_FoundationTest", "Checkpoint restart scene failed")) yield break;
            if (!Require(GameRoot.Instance.State.CurrentState == GameState.Playing, "Checkpoint restart state failed")) yield break;

            GameRoot.Instance.Scenes.Load("MS_MainMenu", GameState.MainMenu);
            yield return WaitForScene("MS_MainMenu", 10f);
            yield return WaitForState(GameState.MainMenu, 10f);
            if (!Require(GameRoot.Instance.State.CurrentState == GameState.MainMenu, "Return to menu failed")) yield break;

            Debug.Log("MARINE_SLAYER_FOUNDATION_FLOW_PASS");
            Application.Quit();
        }

        private IEnumerator WaitForScene(string sceneName, float timeoutSeconds)
        {
            float deadline = Time.realtimeSinceStartup + timeoutSeconds;
            while (SceneManager.GetActiveScene().name != sceneName && Time.realtimeSinceStartup < deadline)
                yield return null;
        }

        private IEnumerator WaitForState(GameState state, float timeoutSeconds)
        {
            float deadline = Time.realtimeSinceStartup + timeoutSeconds;
            while (GameRoot.Instance.State.CurrentState != state && Time.realtimeSinceStartup < deadline)
                yield return null;
        }

        private bool Require(bool condition, string message)
        {
            if (condition) return true;
            Debug.LogError("MARINE_SLAYER_FOUNDATION_FLOW_FAIL: " + message);
            Application.Quit();
            return false;
        }
    }
}
