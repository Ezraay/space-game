using Spaceships.Entities;
using TMPro;
using UnityEngine;

namespace Spaceships
{
    public class InteractText : MonoBehaviour
    {
        [SerializeField] private Vector2 offset;
        private TMP_Text text;

        private void Start()
        {
            text = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            if (Player.availableInteractable != null)
            {
                text.text = "[E] Interact - " + Player.availableInteractable.InteractText;
                Vector2 position = Camera.main.WorldToScreenPoint(Player.ship.transform.position);
                text.transform.position = position + offset;
            }
            else
            {
                text.text = "";
            }
        }
    }
}