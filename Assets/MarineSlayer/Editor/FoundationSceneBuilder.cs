using System.IO;
using MarineSlayer.Campaign;
using MarineSlayer.CameraSystem;
using MarineSlayer.Combat;
using MarineSlayer.Core;
using MarineSlayer.Encounters;
using MarineSlayer.Lore;
using MarineSlayer.Player;
using MarineSlayer.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MarineSlayer.EditorTools
{
    public static class FoundationSceneBuilder
    {
        private const string BootPath = "Assets/MarineSlayer/Scenes/Boot/MS_Boot.unity";
        private const string MenuPath = "Assets/MarineSlayer/Scenes/Menus/MS_MainMenu.unity";
        private const string Level01Path = "Assets/MarineSlayer/Scenes/Campaign/MS_L01_ColdRebirth.unity";
        private const string TestPath = "Assets/MarineSlayer/Scenes/Test/MS_FoundationTest.unity";

        [MenuItem("Marine Slayer/Build Foundation Scenes")]
        public static void Build()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(BootPath));
            Directory.CreateDirectory(Path.GetDirectoryName(MenuPath));
            Directory.CreateDirectory(Path.GetDirectoryName(Level01Path));
            Directory.CreateDirectory(Path.GetDirectoryName(TestPath));
            BuildBoot();
            BuildMenu();
            BuildLevel01();
            BuildTest();
            EditorBuildSettings.scenes = new[]
            {
                new EditorBuildSettingsScene(BootPath, true),
                new EditorBuildSettingsScene(MenuPath, true),
                new EditorBuildSettingsScene(Level01Path, true),
                new EditorBuildSettingsScene(TestPath, true)
            };
            AssetDatabase.SaveAssets();
            Debug.Log("MARINE_SLAYER_FOUNDATION_SCENES_PASS");
        }

        public static void BuildWindows()
        {
            Build();
            Directory.CreateDirectory("Builds/FoundationWindows");
            string error = BuildPipeline.BuildPlayer(ScenePaths(),
                "Builds/FoundationWindows/MarineSlayerFoundation.exe",
                BuildTarget.StandaloneWindows, BuildOptions.Development);
            if (!string.IsNullOrEmpty(error)) throw new IOException(error);
            Debug.Log("MARINE_SLAYER_FOUNDATION_WINDOWS_BUILD_PASS");
        }

        public static void BuildXbox360()
        {
            Build();
            Directory.CreateDirectory("Builds/FoundationXbox360");
            string error = BuildPipeline.BuildPlayer(ScenePaths(),
                "Builds/FoundationXbox360", BuildTarget.XBOX360, BuildOptions.Development);
            if (!string.IsNullOrEmpty(error)) throw new IOException(error);
            Debug.Log("MARINE_SLAYER_FOUNDATION_XBOX360_BUILD_PASS");
        }

        private static string[] ScenePaths()
        {
            return new[] { BootPath, MenuPath, Level01Path, TestPath };
        }

        private static void BuildBoot()
        {
            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            GameObject root = new GameObject("MarineSlayer_GameRoot");
            root.AddComponent<GameRoot>();
            root.AddComponent<BootSceneController>();
            root.AddComponent<FoundationSmokeRunner>();
            Save(scene, BootPath);
        }

        private static void BuildMenu()
        {
            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            new GameObject("FoundationMenu").AddComponent<FoundationMenuController>();
            AddCamera(new Vector3(0f, 2f, -10f), Quaternion.identity);
            Save(scene, MenuPath);
        }

        private static void BuildLevel01()
        {
            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            RenderSettings.ambientLight = new Color(0.055f, 0.075f, 0.1f, 1f);
            BuildLevel01Environment();

            GameObject player = BuildPlayer();
            player.transform.position = new Vector3(0f, 1.1f, -17f);

            GameObject[] firstWave =
            {
                BuildThrall("L01_UnstableThrall_A", new Vector3(-2.1f, 1f, 2.2f)),
                BuildThrall("L01_UnstableThrall_B", new Vector3(2.1f, 1f, 3.2f))
            };
            GameObject[] secondWave =
            {
                BuildThrall("L01_UnstableThrall_C", new Vector3(-1.8f, 1f, 7.2f)),
                BuildThrall("L01_UnstableThrall_D", new Vector3(1.8f, 1f, 8.2f))
            };
            EncounterGate southGate = BuildEncounterGate("L01_DeconContainmentGate", new Vector3(0f, 1.25f, -2.8f), new Vector3(9.6f, 2.5f, 0.3f));
            EncounterGate northGate = BuildEncounterGate("L01_CrewRingContainmentGate", new Vector3(0f, 1.25f, 10.8f), new Vector3(9.6f, 2.5f, 0.3f));
            GameObject encounterObject = new GameObject("L01_CryoEscapeEncounter");
            ArenaEncounterController encounter = encounterObject.AddComponent<ArenaEncounterController>();
            encounter.Configure(
                "l01-clear-thralls",
                "SURVIVE THE CRYO-BAY BREACH // ELIMINATE UNSTABLE THRALLS",
                "THRALLS NEUTRALIZED // CREW RING ROUTE OPEN",
                "l01-thralls-cleared",
                new[] { southGate, northGate },
                new[]
                {
                    new EncounterWave("wake-remnants", 0f, firstWave),
                    new EncounterWave("decon-breach", 0.35f, secondWave)
                });
            encounter.SetActivateOnStart(false);

            GameObject levelObject = new GameObject("L01_ColdRebirth_Controller");
            CampaignLevelController level = levelObject.AddComponent<CampaignLevelController>();
            level.Configure(
                1,
                "CRYO-BAY 09",
                "COLD REBIRTH",
                "l01-cold-rebirth",
                2,
                "l01-release-cryo",
                "ACCESS THE CRYO-BAY RELEASE CONSOLE",
                "l01-reach-decon",
                "MOVE THROUGH CRYO-BAY 09 // REACH DECONTAMINATION",
                "l01-clear-thralls",
                "l01-reach-crew-ring",
                "ESCAPE CRYO-BAY 09 // REACH THE CREW RING",
                "l01-awakening",
                "l01-decon",
                "l01-thralls-cleared",
                "l01-complete",
                new Vector3(0f, 1.1f, -17f),
                new Vector3(0f, 1.1f, -1.6f),
                new Vector3(0f, 1.1f, 11.8f),
                encounter);

            BuildLevel01Terminal();
            BuildLevel01TraversalTrigger(encounter);
            BuildLevel01Exit(level);

            Camera camera = AddCamera(player.transform.position + new Vector3(0f, 12f, -10f), Quaternion.Euler(45f, 0f, 0f));
            TopDownCameraRig rig = camera.gameObject.AddComponent<TopDownCameraRig>();
            rig.SetTarget(player.transform);
            camera.farClipPlane = 80f;
            new GameObject("RuntimeHUD").AddComponent<RuntimeHudController>();
            Save(scene, Level01Path);
        }

        private static void BuildLevel01Environment()
        {
            const string floorPath = "Assets/SciFi_TopDown_SpaceStation/Prefabs/SpaceStation_Room_A_Floor.prefab";
            for (int index = 0; index < 4; index++)
            {
                GameObject floor = InstantiateStationPrefab(floorPath, "CryoBayFloor_" + index, new Vector3(-5f, 0f, -20f + index * 10f), Quaternion.identity);
                if (floor == null)
                {
                    floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    floor.name = "CryoBayFloor_" + index;
                    floor.transform.position = new Vector3(0f, -0.25f, -15f + index * 10f);
                    floor.transform.localScale = new Vector3(10f, 0.5f, 10f);
                }
            }

            BuildStaticBlock("CryoBayWestBulkhead", new Vector3(-5.25f, 1.5f, 0f), new Vector3(0.5f, 3f, 40f));
            BuildStaticBlock("CryoBayEastBulkhead", new Vector3(5.25f, 1.5f, 0f), new Vector3(0.5f, 3f, 40f));
            BuildStaticBlock("CryoBayAftBulkhead", new Vector3(0f, 1.5f, -20.25f), new Vector3(11f, 3f, 0.5f));
            BuildStaticBlock("CryoObservationDividerWest", new Vector3(-3.8f, 1.5f, -9.8f), new Vector3(2.8f, 3f, 0.35f));
            BuildStaticBlock("CryoObservationDividerEast", new Vector3(3.8f, 1.5f, -9.8f), new Vector3(2.8f, 3f, 0.35f));

            for (int index = 0; index < 4; index++)
            {
                float z = -18f + index * 2.35f;
                GameObject pod = GameObject.CreatePrimitive(PrimitiveType.Cube);
                pod.name = "MalfunctioningCryoPod_" + (index + 1);
                pod.transform.position = new Vector3(index % 2 == 0 ? -4.15f : 4.15f, 1.05f, z);
                pod.transform.localScale = new Vector3(1.45f, 2.1f, 0.95f);
            }

            BuildCorpse("CryoTechnician_Casualty", new Vector3(-2.1f, 0.55f, -12.8f), 18f);
            BuildCorpse("UEMFGuard_Casualty", new Vector3(2.2f, 0.55f, -7.2f), -24f);

            InstantiateStationPrefab("Assets/SciFi_TopDown_SpaceStation/Prefabs/Props/SpaceStation_Panel_A.prefab", "CryoReleasePanelVisual", new Vector3(4.65f, 0.2f, -15.2f), Quaternion.Euler(0f, -90f, 0f));
            InstantiateStationPrefab("Assets/SciFi_TopDown_SpaceStation/Prefabs/Props/SpaceStation_FloorVent_A.prefab", "CryoSteamVent_A", new Vector3(-3.1f, 0.05f, -11.2f), Quaternion.identity);
            InstantiateStationPrefab("Assets/SciFi_TopDown_SpaceStation/Prefabs/Props/SpaceStation_FloorVent_A.prefab", "CryoSteamVent_B", new Vector3(3.1f, 0.05f, -0.2f), Quaternion.identity);
            InstantiateStationPrefab("Assets/SciFi_TopDown_SpaceStation/Prefabs/Props/SpaceStation_Crate_A.prefab", "AbandonedMedicalCrate", new Vector3(-3.8f, 0f, 13.8f), Quaternion.Euler(0f, 18f, 0f));
            InstantiateStationPrefab("Assets/SciFi_TopDown_SpaceStation/Prefabs/Props/SpaceStation_Barrel_A.prefab", "EmergencyCoolantBarrel", new Vector3(3.7f, 0f, 5.4f), Quaternion.identity);

            BuildSteamVent("CryoSteam_A", new Vector3(-3.1f, 0.35f, -11.2f));
            BuildSteamVent("CryoSteam_B", new Vector3(3.1f, 0.35f, -0.2f));
            BuildEmergencyLight("CryoEmergencyLight_A", new Vector3(0f, 3.2f, -16f), new Color(0.95f, 0.12f, 0.06f, 1f));
            BuildEmergencyLight("CryoEmergencyLight_B", new Vector3(0f, 3.2f, -5f), new Color(0.15f, 0.4f, 0.95f, 1f));
            BuildEmergencyLight("CryoEmergencyLight_C", new Vector3(0f, 3.2f, 6f), new Color(0.95f, 0.12f, 0.06f, 1f));
            BuildEmergencyLight("CryoEmergencyLight_D", new Vector3(0f, 3.2f, 16f), new Color(0.15f, 0.4f, 0.95f, 1f));
        }

        private static void BuildLevel01Terminal()
        {
            GameObject terminalObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            terminalObject.name = "CryoBay09_ReleaseConsole";
            terminalObject.transform.position = new Vector3(3.9f, 0.85f, -15.2f);
            terminalObject.transform.localScale = new Vector3(0.8f, 1.7f, 0.6f);
            LoreTerminal terminal = terminalObject.AddComponent<LoreTerminal>();
            LoreEntry entry = new LoreEntry(
                "cryo09-wake-failure",
                "UEMF EMERGENCY REPORT",
                "CRYO-BAY 09 // WAKE FAILURE",
                "Emergency release protocol engaged without command authority.\n" +
                "Station mesh traffic has synchronized around an unknown signal.\n" +
                "Medical staff are non-responsive. Cryogenic seals are failing.\n\n" +
                "LT. RHYKER VOSS: CONSCIOUS // UNLINKED // UNACCOUNTED FOR",
                string.Empty);
            terminal.Configure(
                entry,
                string.Empty,
                "l01-release-cryo",
                "ACCESS THE CRYO-BAY RELEASE CONSOLE",
                "CRYO LOCK RELEASED // FIND DECONTAMINATION",
                string.Empty,
                2);
        }

        private static void BuildLevel01TraversalTrigger(ArenaEncounterController encounter)
        {
            GameObject triggerObject = new GameObject("L01_DecontaminationTrigger");
            triggerObject.transform.position = new Vector3(0f, 1.2f, -3.8f);
            BoxCollider collider = triggerObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            collider.size = new Vector3(9f, 2.4f, 1.2f);
            CampaignObjectiveTrigger trigger = triggerObject.AddComponent<CampaignObjectiveTrigger>();
            trigger.Configure("l01-release-cryo", "l01-reach-decon", "DECONTAMINATION REACHED // CONTAINMENT FAILURE", "l01-decon", encounter);
        }

        private static void BuildLevel01Exit(CampaignLevelController level)
        {
            GameObject exitObject = new GameObject("L01_CrewRingExit");
            exitObject.transform.position = new Vector3(0f, 1.2f, 18.2f);
            BoxCollider collider = exitObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            collider.size = new Vector3(9f, 2.4f, 1.5f);
            CampaignExitTrigger exit = exitObject.AddComponent<CampaignExitTrigger>();
            exit.Configure("l01-clear-thralls", level);
        }

        private static GameObject InstantiateStationPrefab(string path, string objectName, Vector3 position, Quaternion rotation)
        {
            GameObject source = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (source == null) return null;
            GameObject instance = PrefabUtility.InstantiatePrefab(source) as GameObject;
            if (instance == null) return null;
            PrefabUtility.DisconnectPrefabInstance(instance);
            instance.name = objectName;
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.transform.localScale = Vector3.one;
            return instance;
        }

        private static void BuildStaticBlock(string objectName, Vector3 position, Vector3 scale)
        {
            GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
            block.name = objectName;
            block.transform.position = position;
            block.transform.localScale = scale;
        }

        private static void BuildCorpse(string objectName, Vector3 position, float yaw)
        {
            const string prefabPath = "Assets/SciFi_Space_Soldier_Complete/Prefabs/Player/Soldier_LOD0.prefab";
            GameObject source = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            GameObject corpse = source == null ? GameObject.CreatePrimitive(PrimitiveType.Capsule) : PrefabUtility.InstantiatePrefab(source) as GameObject;
            if (source != null) PrefabUtility.DisconnectPrefabInstance(corpse);
            corpse.name = objectName;
            corpse.transform.position = position;
            corpse.transform.rotation = Quaternion.Euler(90f, yaw, 0f);
            corpse.transform.localScale = Vector3.one * 0.92f;
            MonoBehaviour[] behaviours = corpse.GetComponentsInChildren<MonoBehaviour>(true);
            for (int index = 0; index < behaviours.Length; index++) Object.DestroyImmediate(behaviours[index]);
            Collider[] colliders = corpse.GetComponentsInChildren<Collider>(true);
            for (int index = 0; index < colliders.Length; index++) Object.DestroyImmediate(colliders[index]);
            Rigidbody[] bodies = corpse.GetComponentsInChildren<Rigidbody>(true);
            for (int index = 0; index < bodies.Length; index++) Object.DestroyImmediate(bodies[index]);
            Animator[] animators = corpse.GetComponentsInChildren<Animator>(true);
            for (int index = 0; index < animators.Length; index++) animators[index].enabled = false;
        }

        private static void BuildSteamVent(string objectName, Vector3 position)
        {
            GameObject steamObject = new GameObject(objectName);
            steamObject.transform.position = position;
            steamObject.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            ParticleSystem steam = steamObject.AddComponent<ParticleSystem>();
#pragma warning disable 0618
            steam.startLifetime = 1.15f;
            steam.startSpeed = 1.8f;
            steam.startSize = 0.32f;
            steam.startColor = new Color(0.72f, 0.82f, 0.88f, 0.58f);
            steam.emissionRate = 7f;
#pragma warning restore 0618
        }

        private static void BuildEmergencyLight(string objectName, Vector3 position, Color color)
        {
            GameObject lightObject = new GameObject(objectName);
            lightObject.transform.position = position;
            Light light = lightObject.AddComponent<Light>();
            light.type = LightType.Point;
            light.range = 8f;
            light.intensity = 2.4f;
            light.color = color;
            lightObject.AddComponent<EmergencyLightFlicker>();
        }

        private static void BuildTest()
        {
            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            new GameObject("FoundationFlow").AddComponent<FoundationFlowController>();

            GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.name = "FoundationFloor";
            floor.transform.position = Vector3.zero;
            floor.transform.localScale = new Vector3(18f, 0.5f, 12f);

            GameObject playerMarker = BuildPlayer();
            BuildDamageTarget();
            GameObject[] pressureWave =
            {
                BuildThrall("ConvergenceThrall_A", new Vector3(7f, 1f, 4f)),
                BuildThrall("ConvergenceThrall_B", new Vector3(-7f, 1f, 4f)),
                BuildThrall("ConvergenceThrall_C", new Vector3(0f, 1f, 5f))
            };
            GameObject[] ambushWave =
            {
                BuildSpinewalker(),
                BuildCanonicalEnemy("ApexHunter_Stalker", CanonicalEnemyArchetype.ApexHunter, PrimitiveType.Capsule, new Vector3(8f, 1f, -4f), new Vector3(0.65f, 0.85f, 0.65f), 65f, 1.1f)
            };
            GameObject[] anomalyWave =
            {
                BuildCanonicalEnemy("ConvergenceBrute_Heavy", CanonicalEnemyArchetype.ConvergenceBrute, PrimitiveType.Cube, new Vector3(-8f, 1.4f, -4f), new Vector3(1.5f, 2.5f, 1.5f), 140f, 3.2f),
                BuildCanonicalEnemy("MeshSiren_Psychic", CanonicalEnemyArchetype.MeshSiren, PrimitiveType.Sphere, new Vector3(8f, 1.3f, 4.8f), new Vector3(0.72f, 1.25f, 0.72f), 55f, 0.9f),
                BuildCanonicalEnemy("Riftbound_Anomaly", CanonicalEnemyArchetype.RiftboundAbomination, PrimitiveType.Sphere, new Vector3(-8f, 1.2f, 4.8f), new Vector3(1.05f, 1.7f, 1.05f), 90f, 1.8f)
            };
            BuildEncounter(pressureWave, ambushWave, anomalyWave);
            BuildLoreTerminal();

            Camera camera = AddCamera(new Vector3(0f, 12f, -10f), Quaternion.Euler(45f, 0f, 0f));
            TopDownCameraRig rig = camera.gameObject.AddComponent<TopDownCameraRig>();
            rig.SetTarget(playerMarker.transform);
            new GameObject("RuntimeHUD").AddComponent<RuntimeHudController>();
            GameObject light = new GameObject("FoundationKeyLight");
            Light component = light.AddComponent<Light>();
            component.type = LightType.Directional;
            component.intensity = 0.8f;
            light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
            Save(scene, TestPath);
        }

        private static GameObject BuildPlayer()
        {
            const string prefabPath = "Assets/SciFi_Space_Soldier_Complete/Prefabs/Player/Soldier_LOD0.prefab";
            GameObject source = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            GameObject player;
            if (source != null)
            {
                player = PrefabUtility.InstantiatePrefab(source) as GameObject;
                PrefabUtility.DisconnectPrefabInstance(player);
                MonoBehaviour[] behaviours = player.GetComponentsInChildren<MonoBehaviour>(true);
                for (int index = 0; index < behaviours.Length; index++) Object.DestroyImmediate(behaviours[index]);
                Camera[] cameras = player.GetComponentsInChildren<Camera>(true);
                for (int index = 0; index < cameras.Length; index++) Object.DestroyImmediate(cameras[index]);
                AudioListener[] listeners = player.GetComponentsInChildren<AudioListener>(true);
                for (int index = 0; index < listeners.Length; index++) Object.DestroyImmediate(listeners[index]);
                Light[] lights = player.GetComponentsInChildren<Light>(true);
                for (int index = 0; index < lights.Length; index++) Object.DestroyImmediate(lights[index]);
                Collider[] colliders = player.GetComponentsInChildren<Collider>(true);
                for (int index = 0; index < colliders.Length; index++) Object.DestroyImmediate(colliders[index]);
                Rigidbody[] bodies = player.GetComponentsInChildren<Rigidbody>(true);
                for (int index = 0; index < bodies.Length; index++) Object.DestroyImmediate(bodies[index]);
            }
            else
            {
                player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            }

            player.name = "RhykerVoss_Player";
            player.tag = "Player";
            player.transform.position = new Vector3(0f, 1.1f, 0f);
            CapsuleCollider capsule = player.GetComponent<CapsuleCollider>();
            if (capsule == null) capsule = player.AddComponent<CapsuleCollider>();
            capsule.center = new Vector3(0f, 0.9f, 0f);
            capsule.height = 1.8f;
            capsule.radius = 0.45f;
            player.AddComponent<Rigidbody>();
            player.AddComponent<PlayerMotor>();
            Health playerHealth = player.AddComponent<Health>();
            playerHealth.Configure(100f);
            player.AddComponent<DamageFlashFeedback>();
            GameObject pool = new GameObject("ProjectilePool");
            pool.transform.SetParent(player.transform, false);
            pool.AddComponent<ProjectilePool>();
            player.AddComponent<PlayerWeaponController>();
            return player;
        }

        private static GameObject BuildThrall(string objectName, Vector3 position)
        {
            const string prefabPath = "Assets/SciFi_Space_Soldier_Complete/Prefabs/Enemies/EnemySoldier_Melee.prefab";
            GameObject source = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            GameObject thrall = source == null ? GameObject.CreatePrimitive(PrimitiveType.Capsule) : PrefabUtility.InstantiatePrefab(source) as GameObject;
            if (source != null) PrefabUtility.DisconnectPrefabInstance(thrall);
            MonoBehaviour[] behaviours = thrall.GetComponentsInChildren<MonoBehaviour>(true);
            for (int index = 0; index < behaviours.Length; index++) Object.DestroyImmediate(behaviours[index]);
            Camera[] cameras = thrall.GetComponentsInChildren<Camera>(true);
            for (int index = 0; index < cameras.Length; index++) Object.DestroyImmediate(cameras[index]);
            AudioListener[] listeners = thrall.GetComponentsInChildren<AudioListener>(true);
            for (int index = 0; index < listeners.Length; index++) Object.DestroyImmediate(listeners[index]);
            Light[] lights = thrall.GetComponentsInChildren<Light>(true);
            for (int index = 0; index < lights.Length; index++) Object.DestroyImmediate(lights[index]);
            Collider[] colliders = thrall.GetComponentsInChildren<Collider>(true);
            for (int index = 0; index < colliders.Length; index++) Object.DestroyImmediate(colliders[index]);
            Rigidbody[] bodies = thrall.GetComponentsInChildren<Rigidbody>(true);
            for (int index = 0; index < bodies.Length; index++) Object.DestroyImmediate(bodies[index]);
            thrall.name = objectName;
            thrall.transform.position = position;
            thrall.transform.rotation = Quaternion.identity;
            thrall.transform.localScale = new Vector3(0.9f, 0.95f, 0.9f);
            CapsuleCollider capsule = thrall.AddComponent<CapsuleCollider>();
            capsule.center = new Vector3(0f, 0.95f, 0f);
            capsule.height = 1.9f;
            capsule.radius = 0.48f;
            Rigidbody body = thrall.AddComponent<Rigidbody>();
            body.mass = 1.4f;
            Health health = thrall.AddComponent<Health>();
            health.Configure(45f);
            thrall.AddComponent<DamageFlashFeedback>();
            thrall.AddComponent<EnemyDifficultyScaler>();
            thrall.AddComponent<ConvergenceThrallController>();
            return thrall;
        }

        private static void BuildDamageTarget()
        {
            GameObject target = GameObject.CreatePrimitive(PrimitiveType.Cube);
            target.name = "CombatFoundationTarget";
            target.transform.position = new Vector3(5f, 1.25f, 0f);
            target.transform.localScale = new Vector3(1f, 2.5f, 1f);
            Health health = target.AddComponent<Health>();
            health.Configure(30f);
            target.AddComponent<DamageFlashFeedback>();
        }

        private static GameObject BuildSpinewalker()
        {
            GameObject spinewalker = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            spinewalker.name = "Spinewalker_Ambusher";
            spinewalker.transform.position = new Vector3(-4f, 4f, -3f);
            spinewalker.transform.localScale = new Vector3(0.7f, 0.9f, 0.7f);
            Rigidbody body = spinewalker.AddComponent<Rigidbody>();
            body.mass = 1.1f;
            Health health = spinewalker.AddComponent<Health>();
            health.Configure(32f);
            spinewalker.AddComponent<DamageFlashFeedback>();
            spinewalker.AddComponent<EnemyDifficultyScaler>();
            spinewalker.AddComponent<SpinewalkerController>();
            return spinewalker;
        }

        private static GameObject BuildCanonicalEnemy(string objectName, CanonicalEnemyArchetype archetype, PrimitiveType primitive, Vector3 position, Vector3 scale, float healthValue, float mass)
        {
            GameObject enemy = GameObject.CreatePrimitive(primitive);
            enemy.name = objectName;
            enemy.transform.position = position;
            enemy.transform.localScale = scale;
            Rigidbody body = enemy.AddComponent<Rigidbody>();
            body.mass = mass;
            Health health = enemy.AddComponent<Health>();
            health.Configure(healthValue);
            enemy.AddComponent<DamageFlashFeedback>();
            enemy.AddComponent<EnemyDifficultyScaler>();
            CanonicalEnemyController controller = enemy.AddComponent<CanonicalEnemyController>();
            controller.Configure(archetype);
            return enemy;
        }

        private static void BuildEncounter(GameObject[] pressureWave, GameObject[] ambushWave, GameObject[] anomalyWave)
        {
            EncounterGate southGate = BuildEncounterGate("SouthContainmentGate", new Vector3(0f, 1.25f, -5.6f), new Vector3(5f, 2.5f, 0.35f));
            EncounterGate northGate = BuildEncounterGate("NorthContainmentGate", new Vector3(0f, 1.25f, 5.6f), new Vector3(5f, 2.5f, 0.35f));
            GameObject encounterObject = new GameObject("FoundationArenaEncounter");
            ArenaEncounterController encounter = encounterObject.AddComponent<ArenaEncounterController>();
            encounter.Configure(
                "foundation-secure-arena",
                "CONTAIN THE CONVERGENCE // CLEAR ALL WAVES",
                "AREA SECURED // CHECKPOINT ACTIVE",
                "foundation-encounter-cleared",
                new[] { southGate, northGate },
                new[]
                {
                    new EncounterWave("pressure", 0f, pressureWave),
                    new EncounterWave("ambush", 0.2f, ambushWave),
                    new EncounterWave("anomaly", 0.2f, anomalyWave)
                });
        }

        private static EncounterGate BuildEncounterGate(string objectName, Vector3 position, Vector3 scale)
        {
            GameObject gate = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gate.name = objectName;
            gate.transform.position = position;
            gate.transform.localScale = scale;
            return gate.AddComponent<EncounterGate>();
        }

        private static void BuildLoreTerminal()
        {
            GameObject terminalObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            terminalObject.name = "CryoBay09_DataTerminal";
            terminalObject.transform.position = new Vector3(0f, 0.85f, 3.6f);
            terminalObject.transform.localScale = new Vector3(0.9f, 1.7f, 0.65f);
            LoreTerminal terminal = terminalObject.AddComponent<LoreTerminal>();
            LoreEntry entry = new LoreEntry(
                "cryo09-wake-failure",
                "UEMF EMERGENCY REPORT",
                "CRYO-BAY 09 // WAKE FAILURE",
                "Emergency systems report neural-mesh synchronization\n" +
                "across Eidolon Station. Cryo-Bay 09 remains isolated\n" +
                "from the ASCENDANT command network.\n\n" +
                "LT. RHYKER VOSS: CONSCIOUS // UNLINKED // UNACCOUNTED FOR",
                string.Empty);
            terminal.Configure(
                entry,
                "foundation-secure-arena",
                "foundation-access-terminal",
                "ACCESS THE CRYO-BAY DATA TERMINAL",
                "DATA RECOVERED // FOUNDATION COMPLETE",
                "foundation-combat-certification",
                2);
        }

        private static Camera AddCamera(Vector3 position, Quaternion rotation)
        {
            GameObject cameraObject = new GameObject("Main Camera");
            Camera camera = cameraObject.AddComponent<Camera>();
            cameraObject.tag = "MainCamera";
            cameraObject.transform.position = position;
            cameraObject.transform.rotation = rotation;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.015f, 0.025f, 0.04f, 1f);
            return camera;
        }

        private static void Save(Scene scene, string path)
        {
            if (!EditorSceneManager.SaveScene(scene, path))
                throw new IOException("Could not save scene: " + path);
        }
    }
}
