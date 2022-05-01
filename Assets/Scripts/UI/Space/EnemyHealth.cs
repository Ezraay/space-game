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
        
        public void UpdateHealth(Damageable damageable)
        {
            float percentage = damageable.Health / damageable.MaxHealth;
            healthSlider.value = percentage;
            
            
            damageableText.text = damageable.Name;
        }
    }
}
