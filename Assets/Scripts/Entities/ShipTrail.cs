using UnityEngine;

namespace Spaceships.Entities
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ShipTrail : MonoBehaviour
    {
        private ParticleSystem particle;
        private float initialScale;
        
        private void Start()
        {
            particle = GetComponent<ParticleSystem>();
            initialScale = transform.localScale.z;
        }

        public void SetLength(float length)
        {
            length = Mathf.Max(length, 0);
            transform.localScale = new Vector3(initialScale, initialScale, initialScale * length);
        }
    }
}
