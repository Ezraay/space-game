using UnityEngine;

namespace Spaceships.Entities.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        [SerializeField] private float distance = 100;
        [HideInInspector] public Ship creator;

        private void Start()
        {
            Destroy(gameObject, distance / speed);
        }

        private void FixedUpdate()
        {
            transform.Translate(-Vector2.up * speed * Time.fixedDeltaTime);
        }
    }
}