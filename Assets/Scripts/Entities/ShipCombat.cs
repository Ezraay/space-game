using System.Collections.Generic;
using Spaceships.Entities.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace Spaceships.Entities
{
    public class ShipCombat : MonoBehaviour, IDamageable
    {
        [SerializeField] private Transform gunParent;
        [SerializeField] private Standing standing;

        private readonly List<Transform> gunLocations = new List<Transform>();
        private Ship ship;
        private float shotCooldown;
        private int shotNumber;

        public Standing Standing => standing;
        private ShipData ShipData => ship.ShipData;
        public UnityEvent<float> OnTakeDamage { get; } = new UnityEvent<float>();
        public UnityEvent OnDie { get; } = new UnityEvent();
        public UnityEvent OnHealthChange { get; } = new UnityEvent();
        public float Health { get; private set; }
        public float MaxHealth => ShipData.MaxHealth;
        public string Name => ShipData.Name;

        public void TakeDamage(float amount)
        {
            Health -= amount;
            Health = Mathf.Max(0, Health);
            if (Health == 0)
                Die();
        }

        public void Die()
        {
            OnDie.Invoke();
            Destroy(gameObject);
        }

        protected void Start()
        {
            ship = GetComponent<Ship>();

            Health = MaxHealth;

            foreach (Transform gunChild in gunParent)
            {
                gunLocations.Add(gunChild);
            }
        }

        private void Update()
        {
            shotCooldown = Mathf.Max(0, shotCooldown - Time.deltaTime);
        }

        private void OnDestroy()
        {
            OnDie.Invoke();
        }


        public void Setup(Standing standing)
        {
            this.standing = standing;
        }

        public void Shoot(Vector2 target)
        {
            if (shotCooldown > 0)
                return;
            shotCooldown = ShipData.ShotCooldown * ShipData.ShotsPerClick;
            for (int i = 0; i < ShipData.ShotsPerClick; i++)
            {
                Vector2 difference = target - (Vector2) gunLocations[shotNumber].position;
                float angle = Mathf.Atan2(difference.x, -difference.y) * Mathf.Rad2Deg;
                Projectile newProjectile =
                    Instantiate(ShipData.Projectile, gunLocations[shotNumber].position, Quaternion.identity);
                newProjectile.Setup(ShipData.DamagePerShot, this, angle, ShipData.Pierce);

                shotNumber = ++shotNumber % gunLocations.Count;
            }
        }
    }
}