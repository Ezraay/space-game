using UnityEngine;
using UnityEngine.Events;

namespace Spaceships.Entities.Combat
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Damageable : MonoBehaviour
    {
        public UnityEvent OnTakeDamage { get; } = new UnityEvent();
        public UnityEvent OnDie { get; } = new UnityEvent();

        public UnityEvent OnHealthChange { get; } = new UnityEvent();
        public float Health { get; private set; }

        public abstract float MaxHealth { get; }
        public abstract string Name { get; }

        protected virtual void Start()
        {
            Health = MaxHealth;
            OnHealthChange.Invoke();
        }

        public virtual void TakeDamage(float amount)
        {
            Health -= amount;
            Health = Mathf.Max(0, Health);
            OnTakeDamage.Invoke();
            OnHealthChange.Invoke();
            if (Health == 0)
                Die();
        }

        public virtual void Die()
        {
            OnDie.Invoke();
            Destroy(gameObject);
        }
    }
}