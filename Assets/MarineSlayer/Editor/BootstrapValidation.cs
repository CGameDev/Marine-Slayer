using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace MarineSlayer.EditorTools
{
    // Setup-only scene: intentionally excluded from campaign build settings.
    public static class BootstrapValidation
    {
        private const string ScenePath = "Assets/MarineSlayer/Scenes/Test/MS_ToolchainBaseline.unity";

        [MenuItem("Marine Slayer/Validate Bootstrap")]
        public static void Validate()
        {
            if (Application.unityVersion != "5.4.1f1")
                throw new InvalidOperationException("Marine Slayer requires Unity 5.4.1f1.");
            EditorSettings.serializationMode = SerializationMode.ForceText;
            EditorSettings.externalVersionControl = "Visible Meta Files";
            PlayerSettings.productName = "Marine Slayer";
            PlayerSettings.defaultScreenWidth = 1280;
            PlayerSettings.defaultScreenHeight = 720;
            if (!File.Exists(ScenePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(ScenePath));
                var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                if (!EditorSceneManager.SaveScene(scene, ScenePath))
                    throw new InvalidOperationException("Could not save toolchain baseline scene.");
            }
            AssetDatabase.SaveAssets();
            Debug.Log("MARINE_SLAYER_BOOTSTRAP_PASS Unity=" + Application.unityVersion);
        }

        public static void BuildWindowsBaseline()
        {
            Validate();
            Directory.CreateDirectory("Builds/WindowsBaseline");
            string error = BuildPipeline.BuildPlayer(new[] { ScenePath },
                "Builds/WindowsBaseline/MarineSlayerBaseline.exe",
                BuildTarget.StandaloneWindows, BuildOptions.Development);
            if (!string.IsNullOrEmpty(error)) throw new InvalidOperationException(error);
            Debug.Log("MARINE_SLAYER_WINDOWS_BASELINE_BUILD_PASS");
        }

        public static void BuildXboxBaseline()
        {
            Validate();
            Directory.CreateDirectory("Builds/XboxBaseline");
            string error = BuildPipeline.BuildPlayer(new[] { ScenePath },
                "Builds/XboxBaseline", BuildTarget.XBOX360, BuildOptions.Development);
            if (!string.IsNullOrEmpty(error)) throw new InvalidOperationException(error);
            Debug.Log("MARINE_SLAYER_XBOX_BASELINE_BUILD_PASS");
        }
    }
}
