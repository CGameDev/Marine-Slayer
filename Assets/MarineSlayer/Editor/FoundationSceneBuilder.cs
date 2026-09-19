using System.IO;
using MarineSlayer.Core;
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

            GameObject playerMarker = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            playerMarker.name = "RhykerVoss_FoundationMarker";
            playerMarker.transform.position = new Vector3(0f, 1.25f, 0f);

            AddCamera(new Vector3(0f, 12f, -10f), Quaternion.Euler(45f, 0f, 0f));
            GameObject light = new GameObject("FoundationKeyLight");
            Light component = light.AddComponent<Light>();
            component.type = LightType.Directional;
            component.intensity = 0.8f;
            light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
            Save(scene, TestPath);
        }

        private static void AddCamera(Vector3 position, Quaternion rotation)
        {
            GameObject cameraObject = new GameObject("Main Camera");
            Camera camera = cameraObject.AddComponent<Camera>();
            cameraObject.tag = "MainCamera";
            cameraObject.transform.position = position;
            cameraObject.transform.rotation = rotation;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.015f, 0.025f, 0.04f, 1f);
        }

        private static void Save(Scene scene, string path)
        {
            if (!EditorSceneManager.SaveScene(scene, path))
                throw new IOException("Could not save scene: " + path);
        }
    }
}
