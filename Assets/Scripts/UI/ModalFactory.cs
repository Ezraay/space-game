using UnityEngine;
using UnityEngine.UI;

namespace Spaceships.UI
{
    public class ModalFactory : MonoBehaviour
    {
        [SerializeField] private Button buttonPrefab;
        [SerializeField] private Modal modalPrefab;

        public static Modal CreateModal(string title, string description)
        {
            return null;
        }
    }
}