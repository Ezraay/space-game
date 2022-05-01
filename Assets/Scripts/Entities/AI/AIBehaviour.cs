using UnityEngine;

namespace Spaceships.Entities.AI
{
    public abstract class AIBehaviour : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] private float weight;
        // [SerializeField] private bool concurrent = true; // Whether or not to block other behaviours from happening
        protected Ship ship;
        protected ShipAI shipAI;

        protected virtual float GetUtility() => 0;

        public float Weight => GetUtility() * weight;

        public virtual void Setup(Ship ship, ShipAI shipAI)
        {
            this.ship = ship;
            this.shipAI = shipAI;
        }

        public virtual void Tick() {}
    }
}