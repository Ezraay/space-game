using UnityEngine;

namespace Spaceships.Entities.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        [SerializeField] private float distance = 100;
        private ShipCombat creator;
        private float damage;
        private int pierce;


        private void FixedUpdate()
        {
            transform.Translate(-Vector2.up * speed * Time.fixedDeltaTime);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            TryHit(col.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            TryHit(col.gameObject);
        }

        public void Setup(float damage, ShipCombat creator, float angle, int pierce)
        {
            this.damage = damage;
            this.creator = creator;
            this.pierce = pierce;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            Destroy(gameObject, distance / speed);
        }

        private void TryHit(GameObject target)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable == null || (ShipCombat) damageable == creator)
                return;
            damageable.TakeDamage(damage);
            pierce--;
            if (pierce == 0)
                Destroy(gameObject);
        }
    }
}