using System;
using System.Collections;
using System.Collections.Generic;
using MarineSlayer.Campaign;
using MarineSlayer.Combat;
using MarineSlayer.Encounters;
using MarineSlayer.Lore;
using MarineSlayer.Player;
using MarineSlayer.Save;
using MarineSlayer.UI;
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
            MarineSlayer.UI.FoundationMenuController menu = FindObjectOfType<MarineSlayer.UI.FoundationMenuController>();
            if (!Require(menu != null && menu.RenderedMenu.Contains("NEW GAME") && menu.RenderedMenu.Contains("OPTIONS") && menu.RenderedMenu.Contains("CREDITS"), "Controller main menu was not created")) yield break;
            float originalMasterVolume = GameRoot.Instance.Settings.Current.masterVolume;
            bool originalSubtitles = GameRoot.Instance.Settings.Current.subtitlesEnabled;
            GameRoot.Instance.Settings.SetMasterVolume(1f);
            GameRoot.Instance.Settings.SetSubtitles(true);
            menu.SetSelection(2);
            if (!Require(menu.ActivateSelected() && menu.CurrentScreen == MarineSlayer.UI.FoundationMenuScreen.Options, "Options menu did not open")) yield break;
            menu.SetSelection(0);
            if (!Require(menu.ActivateSelected() && Mathf.Approximately(GameRoot.Instance.Settings.Current.masterVolume, 0.75f), "Master volume option did not change")) yield break;
            menu.SetSelection(1);
            if (!Require(menu.ActivateSelected() && !GameRoot.Instance.Settings.Current.subtitlesEnabled && GameRoot.Instance.Settings.HasSettings, "Subtitle option did not persist")) yield break;
            menu.Back();
            menu.SetSelection(3);
            if (!Require(menu.ActivateSelected() && menu.CurrentScreen == MarineSlayer.UI.FoundationMenuScreen.Credits && menu.RenderedMenu.Contains("BACK"), "Credits screen did not open")) yield break;
            menu.Back();

            GameRoot.Instance.Saves.BeginNewCampaign(CampaignDifficulty.Marine);
            GameRoot.Instance.Saves.Current.checkpointId = "confirmation-probe";
            GameRoot.Instance.Saves.Write();
            menu.SetSelection(1);
            if (!Require(menu.ActivateSelected() && menu.CurrentScreen == FoundationMenuScreen.ConfirmNewGame && menu.SelectedIndex == 1, "Existing campaign did not open a safe New Game confirmation")) yield break;
            if (!Require(GameRoot.Instance.Saves.Current.checkpointId == "confirmation-probe", "Opening New Game confirmation changed campaign progress")) yield break;
            if (!Require(menu.ActivateSelected() && menu.CurrentScreen == FoundationMenuScreen.Main, "Default Cancel choice did not leave New Game confirmation")) yield break;
            if (!Require(GameRoot.Instance.Saves.Current.checkpointId == "confirmation-probe", "Cancelling New Game changed campaign progress")) yield break;
            menu.SetSelection(1);
            if (!Require(menu.ActivateSelected() && menu.CurrentScreen == FoundationMenuScreen.ConfirmNewGame, "New Game confirmation did not reopen")) yield break;
            menu.SetSelection(0);
            if (!Require(menu.ActivateSelected() && menu.CurrentScreen == FoundationMenuScreen.Difficulty && menu.RenderedMenu.Contains("RECRUIT") && menu.RenderedMenu.Contains("MARINE") && menu.RenderedMenu.Contains("SLAYER"), "Difficulty selection did not open")) yield break;
            menu.SetSelection((int)CampaignDifficulty.Recruit);
            if (!Require(menu.ActivateSelected() && menu.IsStarting, "Recruit campaign did not start from difficulty selection")) yield break;
            if (!Require(GameRoot.Instance.Saves.HasSave && GameRoot.Instance.Saves.Current.difficulty == CampaignDifficulty.Recruit && GameRoot.Instance.Saves.Current.checkpointId == "start", "Fresh Recruit campaign save was not created")) yield break;
            GameRoot.Instance.Settings.Read();
            if (!Require(Mathf.Approximately(GameRoot.Instance.Settings.Current.masterVolume, 0.75f) && !GameRoot.Instance.Settings.Current.subtitlesEnabled, "Campaign reset changed independent settings")) yield break;
            GameRoot.Instance.Settings.SetMasterVolume(originalMasterVolume);
            GameRoot.Instance.Settings.SetSubtitles(originalSubtitles);

            yield return WaitForScene("MS_L01_ColdRebirth", 10f);
            yield return WaitForState(GameState.Playing, 10f);
            yield return null;
            yield return null;
            CampaignLevelController level01 = FindObjectOfType<CampaignLevelController>();
            PlayerMotor level01Player = FindObjectOfType<PlayerMotor>();
            if (!Require(level01 != null && level01.LevelNumber == 1 && level01.LocationName == "CRYO-BAY 09" && level01.LevelTitle == "COLD REBIRTH", "Level 01 campaign identity was not created")) yield break;
            if (!Require(level01Player != null && GameRoot.Instance.Saves.Current.sceneName == "MS_L01_ColdRebirth" && GameRoot.Instance.Saves.Current.checkpointId == "l01-awakening", "Level 01 did not establish its opening checkpoint")) yield break;
            if (!Require(GameObject.Find("MalfunctioningCryoPod_1") != null && GameObject.Find("CryoTechnician_Casualty") != null && FindObjectsOfType<EmergencyLightFlicker>().Length == 4 && FindObjectsOfType<ParticleSystem>().Length >= 2, "Level 01 Cryo-Bay environmental identity is incomplete")) yield break;
            PlayerWeaponController level01Weapon = level01Player.GetComponent<PlayerWeaponController>();
            if (!Require(level01Weapon != null && level01Weapon.CurrentWeapon.Definition.id == CanonicalWeaponId.FuryGauntlet, "Level 01 did not begin with the melee onboarding weapon")) yield break;
            LoreTerminal level01Terminal = FindObjectOfType<LoreTerminal>();
            if (!Require(level01Terminal != null && level01Terminal.Entry != null && level01Terminal.Entry.id == "cryo09-wake-failure", "Level 01 release console lore was not created")) yield break;
            level01Player.transform.position = level01Terminal.transform.position + Vector3.back;
            if (!Require(level01Terminal.TryInteract(level01Player.transform.position), "Level 01 release console interaction failed")) yield break;
            if (!Require(GameRoot.Instance.Objectives.IsComplete("l01-release-cryo") && GameRoot.Instance.Objectives.CurrentId == "l01-reach-decon", "Release console did not advance the traversal objective")) yield break;
            if (!Require(level01Terminal.Close() && GameRoot.Instance.State.CurrentState == GameState.Playing, "Level 01 release console did not close safely")) yield break;

            CampaignObjectiveTrigger deconTrigger = FindObjectOfType<CampaignObjectiveTrigger>();
            ArenaEncounterController level01Encounter = FindObjectOfType<ArenaEncounterController>();
            if (!Require(deconTrigger != null && level01Encounter != null && deconTrigger.TryActivate(level01Player.gameObject), "Decontamination trigger did not activate")) yield break;
            yield return null;
            if (!Require(level01Encounter.IsActive && level01Encounter.WaveCount == 2 && level01Encounter.CurrentWaveIndex == 0 && level01Encounter.ActiveActorCount == 2, "Level 01 first Thrall wave did not activate")) yield break;
            GameRoot.Instance.State.SetState(GameState.PlayerDead);
            GameRoot.Instance.Checkpoints.Restart();
            yield return null;
            while (GameRoot.Instance.Scenes.IsLoading) yield return null;
            yield return WaitForState(GameState.Playing, 10f);
            yield return null;
            yield return null;
            level01 = FindObjectOfType<CampaignLevelController>();
            level01Player = FindObjectOfType<PlayerMotor>();
            level01Encounter = FindObjectOfType<ArenaEncounterController>();
            CampaignObjectiveTrigger restoredDeconTrigger = FindObjectOfType<CampaignObjectiveTrigger>();
            if (!Require(level01 != null && level01Player != null && level01Encounter != null && level01Encounter.IsActive && level01Encounter.CurrentWaveIndex == 0 && level01Encounter.ActiveActorCount == 2, "Level 01 decontamination checkpoint did not restart the encounter safely")) yield break;
            if (!Require(GameRoot.Instance.Saves.Current.checkpointId == "l01-decon" && GameRoot.Instance.Objectives.IsComplete("l01-reach-decon") && restoredDeconTrigger != null && restoredDeconTrigger.IsConsumed, "Level 01 restart duplicated or lost traversal progression")) yield break;
            GameObject[] level01Actors = level01Encounter.GetAllActors();
            for (int index = 0; index < level01Actors.Length; index++)
                if (level01Actors[index].activeSelf) level01Actors[index].SetActive(false);
            float level01WaveDeadline = Time.realtimeSinceStartup + 3f;
            while ((level01Encounter.CurrentWaveIndex != 1 || level01Encounter.ActiveActorCount != 2) && Time.realtimeSinceStartup < level01WaveDeadline) yield return null;
            if (!Require(level01Encounter.CurrentWaveIndex == 1 && level01Encounter.ActiveActorCount == 2, "Level 01 second Thrall wave did not sequence")) yield break;
            for (int index = 0; index < level01Actors.Length; index++)
                if (level01Actors[index].activeSelf) level01Actors[index].SetActive(false);
            level01WaveDeadline = Time.realtimeSinceStartup + 3f;
            while (!level01Encounter.IsComplete && Time.realtimeSinceStartup < level01WaveDeadline) yield return null;
            if (!Require(level01Encounter.IsComplete && GameRoot.Instance.Objectives.IsComplete("l01-clear-thralls") && GameRoot.Instance.Saves.Current.checkpointId == "l01-thralls-cleared", "Level 01 encounter did not create its safe checkpoint")) yield break;
            CampaignExitTrigger level01Exit = FindObjectOfType<CampaignExitTrigger>();
            if (!Require(level01Exit != null && level01Exit.TryExit(level01Player.gameObject), "Level 01 Crew Ring exit did not complete the mission")) yield break;
            if (!Require(GameRoot.Instance.State.CurrentState == GameState.LevelComplete && GameRoot.Instance.Missions.IsComplete("l01-cold-rebirth") && GameRoot.Instance.Saves.Current.highestUnlockedLevel == 2, "Level 01 completion state or unlock is invalid")) yield break;
            if (!Require(GameRoot.Instance.Saves.Current.checkpointId == "l01-complete" && GameRoot.Instance.Objectives.IsComplete("l01-reach-crew-ring"), "Level 01 completion checkpoint was not persisted")) yield break;
            GameRoot.Instance.Saves.Read();
            if (!Require(GameRoot.Instance.Missions.IsComplete("l01-cold-rebirth") && GameRoot.Instance.Saves.Current.checkpointId == "l01-complete" && GameRoot.Instance.Saves.Current.difficulty == CampaignDifficulty.Recruit, "Level 01 completion did not survive save reload")) yield break;

            GameRoot.Instance.Saves.BeginNewCampaign(CampaignDifficulty.Recruit);
            GameRoot.Instance.Saves.Current.sceneName = "MS_FoundationTest";
            GameRoot.Instance.Saves.Write();
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
            Health scaledThrallHealth = thralls[0].GetComponent<Health>();
            if (!Require(scaledThrallHealth != null && Mathf.Approximately(scaledThrallHealth.Maximum, 45f * DifficultyService.EnemyHealthFor(CampaignDifficulty.Recruit)), "Recruit enemy health tuning was not applied")) yield break;
            if (!Require(DifficultyService.EnemyDamageFor(CampaignDifficulty.Recruit) < DifficultyService.EnemyDamageFor(CampaignDifficulty.Marine) && DifficultyService.EnemyDamageFor(CampaignDifficulty.Marine) < DifficultyService.EnemyDamageFor(CampaignDifficulty.Slayer), "Difficulty enemy damage profiles are not ordered")) yield break;
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
            if (!Require(Mathf.Approximately(playerHealthBeforeAttack - playerHealth.Current, 8f * DifficultyService.EnemyDamageFor(CampaignDifficulty.Recruit)), "Recruit enemy damage tuning was not applied")) yield break;
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
            if (!Require(GameRoot.Instance.Objectives.ChangeCount == 3 && GameRoot.Instance.Objectives.CompletionCount == 1, "Objective updates were not deduplicated")) yield break;
            if (!Require(GameRoot.Instance.Objectives.CurrentId == "foundation-access-terminal", "Terminal objective did not follow encounter completion")) yield break;
            if (!Require(GameRoot.Instance.Saves.Current.checkpointId == "foundation-encounter-cleared", "Encounter checkpoint was not recorded")) yield break;

            PlayerWeaponController weapon = motor.GetComponent<PlayerWeaponController>();
            GameObject targetObject = GameObject.Find("CombatFoundationTarget");
            Health target = targetObject == null ? null : targetObject.GetComponent<Health>();
            if (!Require(weapon != null && target != null, "Combat foundation objects were not created")) yield break;
            if (!Require(weapon.WeaponCount == 9, "Canonical nine-weapon catalog was not loaded")) yield break;
            if (!Require(weapon.CurrentWeapon.Definition.id == CanonicalWeaponId.GavelShotgun, "Gavel was not the initial weapon")) yield break;
            int initialMagazine = weapon.CurrentWeapon.Magazine;
            int initialReserve = weapon.CurrentWeapon.Reserve;
            if (!Require(initialReserve == Mathf.RoundToInt(40f * DifficultyService.PlayerResourcesFor(CampaignDifficulty.Recruit)), "Recruit ammunition tuning was not applied")) yield break;
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
            if (!Require(GameRoot.Instance.Objectives.ChangeCount == 3 && GameRoot.Instance.Objectives.CompletionCount == 1, "Objective state duplicated during restart")) yield break;

            LoreTerminal terminal = FindObjectOfType<LoreTerminal>();
            PlayerMotor restoredMotor = FindObjectOfType<PlayerMotor>();
            if (!Require(terminal != null && restoredMotor != null && terminal.IsAvailable, "Foundation lore terminal was not available after encounter clear")) yield break;
            restoredMotor.transform.position = terminal.transform.position + Vector3.back;
            yield return new WaitForFixedUpdate();
            if (!Require(!string.IsNullOrEmpty(terminal.InteractionPrompt), "Lore terminal interaction prompt was not shown")) yield break;
            if (!Require(terminal.TryInteract(restoredMotor.transform.position), "Lore terminal interaction failed")) yield break;
            yield return null;
            if (!Require(GameRoot.Instance.State.CurrentState == GameState.Lore && Time.timeScale == 0f, "Lore reading state did not pause gameplay")) yield break;
            if (!Require(GameRoot.Instance.Lore.IsOpen && GameRoot.Instance.Lore.CurrentEntry != null && GameRoot.Instance.Lore.CurrentEntry.id == "cryo09-wake-failure", "Lore entry did not open")) yield break;
            if (!Require(GameRoot.Instance.Saves.Current.collectedLoreIds.Contains("cryo09-wake-failure") && GameRoot.Instance.Saves.Current.readLoreIds.Contains("cryo09-wake-failure"), "Lore collected/read state was not persisted")) yield break;
            if (!Require(GameRoot.Instance.Lore.CollectionCount == 1 && GameRoot.Instance.Lore.ReadCount == 1, "Lore collection/read events duplicated")) yield break;
            if (!Require(GameRoot.Instance.Objectives.IsComplete("foundation-access-terminal") && terminal.CompletionCount == 1, "Terminal objective did not complete exactly once")) yield break;
            if (!Require(GameRoot.Instance.Objectives.ChangeCount == 4 && GameRoot.Instance.Objectives.CompletionCount == 2, "Terminal objective update count is invalid")) yield break;
            if (!Require(!terminal.TryInteract(restoredMotor.transform.position), "Terminal accepted a duplicate interaction while open")) yield break;

            if (!Require(terminal.Close(), "Lore terminal did not close")) yield break;
            if (!Require(GameRoot.Instance.State.CurrentState == GameState.LevelComplete && Time.timeScale == 1f, "Mission completion state was not entered")) yield break;
            if (!Require(GameRoot.Instance.Missions.IsComplete("foundation-combat-certification") && GameRoot.Instance.Missions.CompletionCount == 1, "Mission did not complete exactly once")) yield break;
            if (!Require(GameRoot.Instance.Saves.Current.highestUnlockedLevel == 2, "Mission completion did not unlock the next level")) yield break;
            if (!Require(!GameRoot.Instance.Missions.CompleteMission("foundation-combat-certification", 2), "Mission completion accepted a duplicate")) yield break;
            if (!Require(!terminal.TryInteract(restoredMotor.transform.position) && !GameRoot.Instance.Lore.Close(), "Completed terminal could be replayed into duplicate progression")) yield break;
            GameRoot.Instance.Saves.Read();
            if (!Require(GameRoot.Instance.Missions.IsComplete("foundation-combat-certification") && GameRoot.Instance.Saves.Current.highestUnlockedLevel == 2, "Completed mission did not survive save reload")) yield break;
            if (!Require(GameRoot.Instance.Saves.Current.completedObjectiveIds.Contains("foundation-access-terminal") && GameRoot.Instance.Saves.Current.collectedLoreIds.Contains("cryo09-wake-failure") && GameRoot.Instance.Saves.Current.readLoreIds.Contains("cryo09-wake-failure"), "Objective or lore state did not survive save reload")) yield break;
            if (!Require(GameRoot.Instance.Saves.Current.difficulty == CampaignDifficulty.Recruit, "Difficulty did not survive save reload")) yield break;

            GameRoot.Instance.Scenes.Load("MS_MainMenu", GameState.MainMenu);
            yield return WaitForScene("MS_MainMenu", 10f);
            yield return WaitForState(GameState.MainMenu, 10f);
            if (!Require(GameRoot.Instance.State.CurrentState == GameState.MainMenu, "Return to menu failed")) yield break;

            FoundationMenuController continueMenu = FindObjectOfType<FoundationMenuController>();
            if (!Require(continueMenu != null && GameRoot.Instance.Saves.HasSave && continueMenu.RenderedMenu.Contains("CONTINUE"), "Continue option was not available for the saved campaign")) yield break;
            if (!Require(continueMenu.ContinueGame(), "Continue did not start the saved campaign")) yield break;
            yield return WaitForScene("MS_FoundationTest", 10f);
            yield return WaitForState(GameState.Playing, 10f);
            if (!Require(GameRoot.Instance.Saves.Current.checkpointId == "foundation-encounter-cleared", "Continue loaded the wrong checkpoint")) yield break;
            if (!Require(GameRoot.Instance.Saves.Current.difficulty == CampaignDifficulty.Recruit, "Continue did not restore the saved difficulty")) yield break;
            ArenaEncounterController continuedEncounter = FindObjectOfType<ArenaEncounterController>();
            LoreTerminal continuedTerminal = FindObjectOfType<LoreTerminal>();
            yield return null;
            if (!Require(continuedEncounter != null && continuedEncounter.IsComplete && !continuedEncounter.AreGatesLocked, "Continue did not restore the safe encounter state")) yield break;
            if (!Require(continuedTerminal != null && !continuedTerminal.IsAvailable && GameRoot.Instance.Missions.IsComplete("foundation-combat-certification"), "Continue did not restore mission completion")) yield break;

            GameRoot.Instance.Scenes.Load("MS_MainMenu", GameState.MainMenu);
            yield return WaitForScene("MS_MainMenu", 10f);
            yield return WaitForState(GameState.MainMenu, 10f);

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
