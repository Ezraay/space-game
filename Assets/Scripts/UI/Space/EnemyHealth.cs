using Spaceships.Entities;
using Spaceships.Entities.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace Spaceships.UI.Space
{
    public class EnemyHealth : Window
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Text damageableText;
        [SerializeField] private Image fillImage;
        [SerializeField] private Image borderImage;
        [SerializeField] private Color noStandingColour = Color.gray;

        public void UpdateHealth(IDamageable damageable)
        {
            float percentage = damageable.Health / damageable.MaxHealth;
            healthSlider.value = percentage;

            ShipCombat ship = (ShipCombat) damageable;
            SetColour(ship ? ship.Standing.Colour : noStandingColour);

            damageableText.text = damageable.Name;
        }

        private void SetColour(Color color)
        {
            fillImage.color = color;
            borderImage.color = color;
        }
    }
}