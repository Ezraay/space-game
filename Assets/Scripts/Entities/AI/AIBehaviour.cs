using UnityEngine;

namespace Spaceships.Entities.AI
{
    public class AIBehaviour : ScriptableObject
    {
        public virtual float GetWeight(Ship ship, ShipAI shipAI) => 1;

        public virtual void Setup(Ship ship, ShipAI shipAI)
        {
        }

        public virtual void Tick(Ship ship, ShipAI shipAI) {}
    }
}