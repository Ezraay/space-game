using UnityEngine;

namespace Spaceships.Entities
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ShipTrail : MonoBehaviour
    {
        private float initialScale;

        private void Start()
        {
            initialScale = transform.localScale.z;
        }

        public void SetLength(float length)
        {
            // Sets the length of the trail, called by the ship
            length = Mathf.Max(length, 0);
            transform.localScale = new Vector3(initialScale, initialScale, initialScale * length);
        }
    }
}