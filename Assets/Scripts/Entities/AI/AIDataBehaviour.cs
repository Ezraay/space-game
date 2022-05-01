using UnityEngine;

namespace Spaceships.Entities.AI
{
    public abstract class AIDataBehaviour : MonoBehaviour
    {
        protected Ship ship;
        protected ShipAI shipAI;
        
        public virtual void Setup(Ship ship, ShipAI shipAI)
        {
            this.ship = ship;
            this.shipAI = shipAI;
        }

        public virtual void Tick()
        {
            
        }
    }
}