using UnityEngine;
using UnityEngine.SceneManagement;

namespace Spaceships.SceneTransitions
{
    public class Loader : MonoBehaviour
    {
        public static void LoadHangar()
        {
            SceneManager.LoadScene("Hangar");
        }

        public static void LoadSpace()
        {
            SceneManager.LoadScene("Space");
        }
    }
}