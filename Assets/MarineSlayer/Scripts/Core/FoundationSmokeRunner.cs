using System;
using System.Collections;
using System.Collections.Generic;
using MarineSlayer.Combat;
using MarineSlayer.Encounters;
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
            Health playerHealth = motor.GetComponent<Health>();
            if (!Require(playerHealth != null, "Player health was not created")) yield break;
            if (!Require(FindObjectOfType<MarineSlayer.UI.RuntimeHudController>() != null, "Runtime HUD was not created")) yield break;
            ArenaEncounterController encounter = FindObjectOfType<ArenaEncounterController>();
            if (!Require(encounter != null, "Foundation arena encounter was not created")) yield break;
            yield return null;
            if (!Require(encounter.WaveCount == 3 && encounter.IsActive, "Three-wave encounter did not activate")) yield break;
            if (!Require(encounter.ActivationCount == 1 && !encounter.Activate(), "Encounter activation was not idempotent")) yield break;
            if (!Require(encounter.AreGatesLocked, "Encounter gates did not lock")) yield break;
            if (!Require(encounter.CurrentWaveIndex == 0 && encounter.ActiveActorCount == 3, "Pressure wave did not activate correctly")) yield break;

            GameObject[] roster = encounter.GetAllActors();
            List<ConvergenceThrallController> thralls = new List<ConvergenceThrallController>();
            List<CanonicalEnemyController> canonicalEnemies = new List<CanonicalEnemyController>();
            SpinewalkerController spinewalker = null;
            for (int rosterIndex = 0; rosterIndex < roster.Length; rosterIndex++)
            {
                ConvergenceThrallController thrall = roster[rosterIndex].GetComponent<ConvergenceThrallController>();
                if (thrall != null) thralls.Add(thrall);
                CanonicalEnemyController canonical = roster[rosterIndex].GetComponent<CanonicalEnemyController>();
                if (canonical != null) canonicalEnemies.Add(canonical);
                SpinewalkerController candidate = roster[rosterIndex].GetComponent<SpinewalkerController>();
                if (candidate != null) spinewalker = candidate;
            }
            if (!Require(roster.Length == 8 && thralls.Count == 3, "Canonical Thrall encounter roster was not created")) yield break;
            if (!Require(spinewalker != null, "Canonical Spinewalker encounter actor was not created")) yield break;
            if (!Require(canonicalEnemies.Count == 4, "Remaining canonical enemy encounter roster was not created")) yield break;
            bool apex = false;
            bool brute = false;
            bool siren = false;
            bool riftbound = false;
            CanonicalEnemyController apexHunter = null;
            for (int index = 0; index < canonicalEnemies.Count; index++)
            {
                CanonicalEnemyArchetype archetype = canonicalEnemies[index].Archetype;
                if (archetype == CanonicalEnemyArchetype.ApexHunter)
                {
                    apex = true;
                    apexHunter = canonicalEnemies[index];
                }
                else if (archetype == CanonicalEnemyArchetype.ConvergenceBrute) brute = true;
                else if (archetype == CanonicalEnemyArchetype.MeshSiren) siren = true;
                else if (archetype == CanonicalEnemyArchetype.RiftboundAbomination) riftbound = true;
            }
            if (!Require(apex && brute && siren && riftbound, "Canonical enemy archetype mapping is incomplete")) yield break;
            Vector3 startPosition = motor.transform.position;
            GameRoot.Instance.Input.SetTestInput(Vector2.right, Vector2.right);
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            GameRoot.Instance.Input.ClearTestInput();
            if (!Require((motor.transform.position - startPosition).sqrMagnitude > 0.01f, "Player movement failed")) yield break;

            float playerHealthBeforeAttack = playerHealth.Current;
            thralls[0].transform.position = motor.transform.position + Vector3.forward * 1.2f;
            for (int index = 0; index < 4; index++) yield return new WaitForFixedUpdate();
            if (!Require(playerHealth.Current < playerHealthBeforeAttack, "Thrall melee attack did not damage the player")) yield break;
            thralls[0].gameObject.SetActive(false);

            Health doomedThrall = thralls[1].GetComponent<Health>();
            doomedThrall.ApplyDamage(new DamageInfo(1000f, motor.gameObject, DamageType.Ballistic, doomedThrall.transform.position, Vector3.forward));
            yield return new WaitForSeconds(0.6f);
            if (!Require(!thralls[1].gameObject.activeSelf, "Thrall death cleanup did not complete")) yield break;
            thralls[2].gameObject.SetActive(false);

            float waveDeadline = Time.realtimeSinceStartup + 3f;
            while ((encounter.CurrentWaveIndex != 1 || encounter.ActiveActorCount != 2) && Time.realtimeSinceStartup < waveDeadline) yield return null;
            if (!Require(encounter.CurrentWaveIndex == 1 && encounter.ActiveActorCount == 2, "Ambush wave did not sequence correctly")) yield break;
            float spinewalkerDeadline = Time.realtimeSinceStartup + 2f;
            while (!spinewalker.HasDeployed && Time.realtimeSinceStartup < spinewalkerDeadline) yield return null;
            if (!Require(spinewalker.HasDeployed, "Spinewalker ambush deployment did not complete")) yield break;
            spinewalker.gameObject.SetActive(false);
            if (apexHunter != null) apexHunter.gameObject.SetActive(false);

            waveDeadline = Time.realtimeSinceStartup + 3f;
            while ((encounter.CurrentWaveIndex != 2 || encounter.ActiveActorCount != 3) && Time.realtimeSinceStartup < waveDeadline) yield return null;
            if (!Require(encounter.CurrentWaveIndex == 2 && encounter.ActiveActorCount == 3, "Anomaly wave did not sequence correctly")) yield break;
            for (int index = 0; index < canonicalEnemies.Count; index++)
                if (canonicalEnemies[index].Archetype != CanonicalEnemyArchetype.ApexHunter)
                    canonicalEnemies[index].gameObject.SetActive(false);

            float completionDeadline = Time.realtimeSinceStartup + 3f;
            while (!encounter.IsComplete && Time.realtimeSinceStartup < completionDeadline) yield return null;
            if (!Require(encounter.IsComplete && encounter.CompletionCount == 1, "Encounter did not complete exactly once")) yield break;
            if (!Require(!encounter.AreGatesLocked, "Encounter gates did not unlock")) yield break;
            if (!Require(GameRoot.Instance.Objectives.IsComplete("foundation-secure-arena"), "Encounter objective was not persisted")) yield break;
            if (!Require(GameRoot.Instance.Objectives.ChangeCount == 2 && GameRoot.Instance.Objectives.CompletionCount == 1, "Objective updates were not deduplicated")) yield break;
            if (!Require(GameRoot.Instance.Saves.Current.checkpointId == "foundation-encounter-cleared", "Encounter checkpoint was not recorded")) yield break;

            PlayerWeaponController weapon = motor.GetComponent<PlayerWeaponController>();
            GameObject targetObject = GameObject.Find("CombatFoundationTarget");
            Health target = targetObject == null ? null : targetObject.GetComponent<Health>();
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
            if (!Require(weapon.SelectWeapon(CanonicalWeaponId.PlasmaCutter) && weapon.CurrentWeapon.Definition.penetrationTargets == 2, "Plasma penetration profile is invalid")) yield break;
            if (!Require(weapon.SelectWeapon(CanonicalWeaponId.ArcThrower) && weapon.CurrentWeapon.Definition.chainTargets == 2, "Arc chaining profile is invalid")) yield break;
            if (!Require(weapon.SelectWeapon(CanonicalWeaponId.RiftGrenade) && weapon.CurrentWeapon.Definition.impactRadius >= 4f, "RIFT blast profile is invalid")) yield break;
            if (!Require(weapon.SelectWeapon(CanonicalWeaponId.SawbladeLauncher) && weapon.CurrentWeapon.Definition.ricochetCount == 3, "Sawblade ricochet profile is invalid")) yield break;
            if (!Require(weapon.SelectWeapon(CanonicalWeaponId.UnityBeamRifle) && weapon.CurrentWeapon.Definition.UsesHeat, "Unity beam heat profile is invalid")) yield break;
            if (!Require(weapon.FireDirection(Vector3.right) && weapon.CurrentWeapon.Heat > 0f, "Unity beam heat did not increase")) yield break;

            GameRoot.Instance.State.TogglePause();
            if (!Require(GameRoot.Instance.State.CurrentState == GameState.Paused && Time.timeScale == 0f, "Pause state failed")) yield break;
            GameRoot.Instance.State.SetState(GameState.PlayerDead);
            if (!Require(GameRoot.Instance.State.CurrentState == GameState.PlayerDead && Time.timeScale == 1f, "Death state failed")) yield break;

            GameRoot.Instance.Checkpoints.Restart();
            yield return null;
            while (GameRoot.Instance.Scenes.IsLoading) yield return null;
            yield return WaitForState(GameState.Playing, 10f);
            if (!Require(SceneManager.GetActiveScene().name == "MS_FoundationTest", "Checkpoint restart scene failed")) yield break;
            if (!Require(GameRoot.Instance.State.CurrentState == GameState.Playing, "Checkpoint restart state failed")) yield break;
            ArenaEncounterController restoredEncounter = FindObjectOfType<ArenaEncounterController>();
            yield return null;
            if (!Require(restoredEncounter != null && restoredEncounter.IsComplete, "Checkpoint did not restore the cleared encounter state")) yield break;
            if (!Require(restoredEncounter.ActivationCount == 0 && !restoredEncounter.AreGatesLocked && restoredEncounter.ActiveActorCount == 0, "Restored encounter was not checkpoint-safe")) yield break;
            if (!Require(GameRoot.Instance.Saves.Current.checkpointId == "foundation-encounter-cleared", "Checkpoint identity was overwritten on restart")) yield break;
            if (!Require(GameRoot.Instance.Objectives.ChangeCount == 2 && GameRoot.Instance.Objectives.CompletionCount == 1, "Objective state duplicated during restart")) yield break;

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
