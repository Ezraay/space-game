using System;
using UnityEngine;

namespace Spaceships.Entities.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        [SerializeField] private float distance = 100;
        private Ship creator;
        private float damage;
        private int pierce;

        public void Setup(float damage, Ship creator, float angle, int pierce)
        {
            this.damage = damage;
            this.creator = creator;
            this.pierce = 1;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            Destroy(gameObject, distance / speed);
        }
        
        

        private void FixedUpdate()
        {
            transform.Translate(-Vector2.up * speed * Time.fixedDeltaTime);
        }

        private void TryHit(GameObject target)
        {
            Damageable damageable = target.GetComponent<Damageable>();
            if (damageable == null || damageable == creator)
                return;
            damageable.TakeDamage(damage);
            pierce--;
            if (pierce == 0)
                Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            TryHit(col.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            TryHit(col.gameObject);
        }
    }
}