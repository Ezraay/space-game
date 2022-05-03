using UnityEngine.Events;

namespace Spaceships.Entities.Combat
{
    public interface IDamageable
    {
        public UnityEvent<float> OnTakeDamage { get; }
        public UnityEvent OnDie { get; }
        public UnityEvent OnHealthChange { get; }
        public float Health { get; }

        public abstract float MaxHealth { get; }
        public abstract string Name { get; }

        public void TakeDamage(float amount);

        public void Die();
    }
}