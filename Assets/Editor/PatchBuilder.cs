using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Spaceships
{
    public static class PatchBuilder
    {
        private static readonly string[] LauncherScenes = {"Assets/Scenes/Launcher.unity",};

        public static void NewMajorVersion()
        {
            
        }

        public static void NewMinorVersion()
        {
            
        }

        public static void NewPatchVersion()
        {
            // Build the launcher and game into /App and /Updater, following Unity system
            BuildCompletely();
            
            // Create a new build... somehow? 
            // Gotta probably remove all except latest 5 versions
            // version.UpdatePatch(); ?
            // Then build
            
            // Delete everything in /Patch
            
            // Create a new patch from every version to latest, following the wiki's suggestion
            
            BuildLauncher();
        }

        private static void BuildLauncher()
        {
            // Build new launcher, compresses into zip
            
        }
        
        private static void BuildCompletely()
        {
            BuildGameAndLauncher();
        }
        
        [MenuItem("Tools/Build Game and Launcher")]
        public static void BuildGameAndLauncher()
        {
            EditorBuildSettingsScene[] original = EditorBuildSettings.scenes;
            List<string> launcherScenes = new List<string>();
            List<string> gameScenes = new List<string>();

            foreach (EditorBuildSettingsScene scene in original)
            {
                string scenePath = scene.path;
                if (LauncherScenes.Contains(scenePath))
                    launcherScenes.Add(scenePath);
                else
                    gameScenes.Add(scenePath);
                Debug.Log(scenePath);
            }
            
            if (launcherScenes.Count == 0)
                Debug.LogError("Launcher tried to build 0 scenes");
            if (gameScenes.Count == 0)
                Debug.LogError("Game tried to build 0 scenes");
            
            BuildPlayer("Ceres Launcher.exe", "PATCHWorkspace/Updater", launcherScenes.ToArray());
            BuildPlayer("Ceres.exe", "PATCHWorkspace/App", gameScenes.ToArray());
        }

        private static void BuildPlayer(string name, string path, string[] scenes)
        {
            BuildPipeline.BuildPlayer(scenes, Path.Combine(path, name), BuildTarget.StandaloneWindows,
                BuildOptions.None);
        }
    }
}