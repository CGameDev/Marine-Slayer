using System.IO;
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
        private const string TestPath = "Assets/MarineSlayer/Scenes/Test/MS_FoundationTest.unity";

        [MenuItem("Marine Slayer/Build Foundation Scenes")]
        public static void Build()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(BootPath));
            Directory.CreateDirectory(Path.GetDirectoryName(MenuPath));
            Directory.CreateDirectory(Path.GetDirectoryName(TestPath));
            BuildBoot();
            BuildMenu();
            BuildTest();
            EditorBuildSettings.scenes = new[]
            {
                new EditorBuildSettingsScene(BootPath, true),
                new EditorBuildSettingsScene(MenuPath, true),
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
            return new[] { BootPath, MenuPath, TestPath };
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
            GameObject thrall = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            thrall.name = objectName;
            thrall.transform.position = position;
            thrall.transform.localScale = new Vector3(0.8f, 1.15f, 0.8f);
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
