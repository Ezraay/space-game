using Spaceships.Environment;
using Spaceships.Utility;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Spaceships
{
    [CustomEditor(typeof(Level))]
    public class LevelEditor : Editor
    {
        static LevelEditor()
        {
            EditorSceneManager.sceneSaving += OnSavingScene;
        }

        private static void OnSavingScene(Scene scene, string path)
        {
            try
            {
                Level instance = FindObjectOfType<Level>();
                if (instance == null || Application.isPlaying) return;
                PrefabUtility.ApplyPrefabInstance(instance.gameObject, InteractionMode.AutomatedAction);
            }
            catch
            {
                // ignored
            }
        }
    }
}