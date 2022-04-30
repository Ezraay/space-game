using Spaceships.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Spaceships.UI.Space
{
    public class PlayerHealth : Window
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private int segments;

        protected override void Start()
        {
            UpdateHealth();
            Player.ship.OnHealthChange.AddListener(UpdateHealth);
        }

        private void UpdateHealth()
        {
            float percentage = Player.ship.Health / Player.ship.MaxHealth;
            healthSlider.value = percentage;
        }
    }
}
