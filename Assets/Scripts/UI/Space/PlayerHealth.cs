using Spaceships.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Spaceships.UI.Space
{
    public class PlayerHealth : Window
    {
        [SerializeField] private Slider healthSlider;

        public void UpdateHealth(ShipCombat ship)
        {
            Show();
            float percentage = ship.Health / ship.MaxHealth;
            healthSlider.value = percentage;
        }
    }
}