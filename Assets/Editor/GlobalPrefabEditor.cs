using Spaceships.Utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

namespace Spaceships
{
    [InitializeOnLoadAttribute]
    [CustomEditor(typeof(GlobalPrefab))]
    public class GlobalPrefabEditor : Editor
    {
        static GlobalPrefabEditor()
        {
            EditorSceneManager.sceneSaving += OnSavingScene;
        }

        private static void OnSavingScene(Scene scene, string path)
        {
            try
            {
                GlobalPrefab instance = FindObjectOfType<GlobalPrefab>();
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