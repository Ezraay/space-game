using Spaceships.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Spaceships.UI.Space
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;

        public void UpdateHealth(ShipCombat ship)
        {
            float percentage = ship.Health / ship.MaxHealth;
            healthSlider.value = percentage;
        }
    }
}