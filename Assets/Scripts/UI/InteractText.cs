using Spaceships.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Spaceships.UI
{
    public class InteractText : MonoBehaviour
    {
        // [SerializeField] private Vector2 offset;
        [SerializeField] private GameObject parent;
        [SerializeField] private Text text;

        private void Update()
        {
            if (Player.availableInteractable != null)
            {
                text.text = Player.availableInteractable.InteractText;
                parent.SetActive(true);
            }
            else
            {
                parent.SetActive(false);
            }
        }
    }
}