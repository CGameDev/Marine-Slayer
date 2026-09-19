using System.IO;
using MarineSlayer.CameraSystem;
using MarineSlayer.Combat;
using MarineSlayer.Core;
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
            BuildThrall("ConvergenceThrall_A", new Vector3(7f, 1f, 4f));
            BuildThrall("ConvergenceThrall_B", new Vector3(-7f, 1f, 4f));
            BuildThrall("ConvergenceThrall_C", new Vector3(0f, 1f, 5f));

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

        private static void BuildThrall(string objectName, Vector3 position)
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
            thrall.AddComponent<ConvergenceThrallController>();
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
