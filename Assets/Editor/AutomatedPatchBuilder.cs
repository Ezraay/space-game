using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MHLab.Patch.Admin.Editor.Localization;
using MHLab.Patch.Core;
using MHLab.Patch.Core.Admin;
using MHLab.Patch.Core.Admin.Progresses;
using MHLab.Patch.Core.IO;
using MHLab.Patch.Core.Logging;
using MHLab.Patch.Core.Versioning;
using MHLab.Patch.Utilities.Serializing;
using UnityEditor;
using UnityEngine;

namespace Spaceships
{
    public static class AutomatedPatchBuilder
    {
        private const bool IsDebug = false;
        private static readonly string[] LauncherScenes = {"Assets/Scenes/Launcher.unity",};

        [MenuItem("Tools/PATCH/Create Major Version")]
        public static void NewMajorVersion()
        {
            FullBuild(UpdateType.Major);
        }

        [MenuItem("Tools/PATCH/Create Minor Version")]
        public static void NewMinorVersion()
        {
            FullBuild(UpdateType.Minor);
        }

        [MenuItem("Tools/PATCH/Create Patch Version")]
        public static void NewPatchVersion()
        {
            FullBuild(UpdateType.Patch);
        }

        private static void FullBuild(UpdateType type)
        {
            // Build the launcher and game into /App and /Updater, following Unity system
            BuildCompletely();

            // Settings for all steps of builder
            AdminSettings settings = new AdminSettings();
            settings.RootPath = Path.Combine(Path.GetDirectoryName(Application.dataPath), "PATCHWorkspace");
            settings.AppDataPath = PathsManager.GetSpecialPath(System.Environment.SpecialFolder.ApplicationData);

            Progress<BuilderProgress> progress = new Progress<BuilderProgress>();

            // Create build
            BuildApp(settings, progress, type);

            // Remove all but last x versions
            BuildsIndex versions = RemoveOlderVersions(settings, progress, 5);

            // Delete everything in /Patch
            Directory.Delete(Path.Combine(settings.RootPath, "Patches"), true);

            // Create a new patch from every version to latest, following the wiki's suggestion
            BuildPatches(settings, progress, versions);

            // Create a launcher
            BuildLauncher(settings, progress);
        }

        private static void BuildApp(AdminSettings settings, Progress<BuilderProgress> progress, UpdateType type)
        {
            AdminBuildContext context = new AdminBuildContext(settings, progress);
            context.Serializer = new JsonSerializer();
            context.LocalizedMessages = new EnglishAdminLocalizedMessages();

            IVersion currentVersion = context.VersionFactory.Create(context.GetLastVersion());
            if (currentVersion == null) // No latest version
                currentVersion = context.VersionFactory.Create();
            else
            {
                switch (type)
                {
                    case UpdateType.Major:
                        currentVersion.UpdateMajor();
                        break;
                    case UpdateType.Minor:
                        currentVersion.UpdateMinor();
                        break;
                    case UpdateType.Patch:
                        currentVersion.UpdatePatch();
                        break;
                }
            }

            context.BuildVersion = currentVersion;

            context.Initialize();
            BuildBuilder builder = new BuildBuilder(context);
            builder.Build();
        }

        private static void BuildPatches(AdminSettings settings, Progress<BuilderProgress> progress,
            BuildsIndex versions)
        {
            AdminPatchContext patchContext = new AdminPatchContext(settings, progress);
            patchContext.LocalizedMessages = new EnglishAdminLocalizedMessages();
            patchContext.Serializer = new JsonSerializer();
            patchContext.Logger = new SimpleLogger(patchContext.FileSystem, settings.GetLogsFilePath(), IsDebug);
            patchContext.VersionTo = versions.GetLast();
            PatchBuilder patcher = new PatchBuilder(patchContext);
            for (int i = 0; i < versions.AvailableBuilds.Count - 1; i++)
            {
                patchContext.VersionFrom = versions.AvailableBuilds[i];
                patchContext.Initialize();
                patcher.Build();
            }
        }

        private static void BuildLauncher(AdminSettings settings, Progress<BuilderProgress> progress)
        {
            AdminPatcherUpdateContext updaterContext = new AdminPatcherUpdateContext(settings, progress);
            updaterContext.LauncherArchiveName = "Ceres";
            updaterContext.Logger = new SimpleLogger(updaterContext.FileSystem, settings.GetLogsFilePath(), IsDebug);
            updaterContext.Serializer = new JsonSerializer();
            updaterContext.LocalizedMessages = new EnglishAdminLocalizedMessages();
            updaterContext.Initialize();

            UpdaterBuilder updaterBuilder = new UpdaterBuilder(updaterContext);
            updaterBuilder.Build();
        }

        private static BuildsIndex RemoveOlderVersions(AdminSettings settings, Progress<BuilderProgress> progress,
            int versionToLeave)
        {
            AdminPatchContext context = new AdminPatchContext(settings, progress);
            context.Serializer = new JsonSerializer();
            context.LocalizedMessages = new EnglishAdminLocalizedMessages();

            FilePath buildsIndexPath = settings.GetBuildsIndexPath();

            BuildsIndex versions;
            if (context.FileSystem.FileExists(buildsIndexPath))
            {
                string readData = context.FileSystem.ReadAllTextFromFile(buildsIndexPath);
                BuildsIndex index = context.Serializer.Deserialize<BuildsIndex>(readData);
                versions = index;
            }
            else
                return null;

            while (versions.AvailableBuilds.Count > versionToLeave)
            {
                string version = versions.GetFirst().ToString();
                // Remove version from file
                versions.AvailableBuilds.RemoveAt(0);

                // Remove build folder
                Directory.Delete(Path.Combine(settings.RootPath, "Builds", version), true);

                // Remove build index file
                File.Delete(Path.Combine(settings.RootPath, "Builds", $"build_{version}.json"));
            }

            // Save builds_index file
            context.FileSystem.WriteAllTextToFile(settings.GetBuildsIndexPath(),
                context.Serializer.Serialize(versions));

            return versions;
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

        private enum UpdateType
        {
            Patch,
            Minor,
            Major,
        }
    }
}