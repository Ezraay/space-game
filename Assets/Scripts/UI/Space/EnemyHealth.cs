using Spaceships.Entities;
using Spaceships.Entities.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace Spaceships.UI.Space
{
    public class EnemyHealth : Window
    {
        // private Damageable damageable;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Text damageableText;
        [SerializeField] private Image fillImage;
        [SerializeField] private Image borderImage;
        [SerializeField] private Color noStandingColour = Color.gray;
        
        public void UpdateHealth(Damageable damageable)
        {
            float percentage = damageable.Health / damageable.MaxHealth;
            healthSlider.value = percentage;

            Ship ship = (Ship) damageable;
            if (ship != null)
            {
                SetColour(ship.Standing.Colour);
            }
            else
            {
                SetColour(noStandingColour);
            }
            
            damageableText.text = damageable.Name;
        }

        private void SetColour(Color color)
        {
            fillImage.color = color;
            borderImage.color = color;
        }
    }
}
