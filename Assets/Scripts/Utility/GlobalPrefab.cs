using UnityEngine;

namespace Spaceships.Utility
{
    public class GlobalPrefab : MonoBehaviour
    {
        private static bool active;

        private void Awake()
        {
            if (active)
                DestroyImmediate(gameObject);
            else
            {
                active = true;
                DontDestroyOnLoad(gameObject);

                Application.targetFrameRate = Application.isEditor ? 1000 : 60;
            }
        }
    }
}