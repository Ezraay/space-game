namespace Spaceships.Entities.AI
{
    public class AICombatBehaviour : AIBehaviour
    {
        protected ShipCombat shipCombat;

        public override void Setup(Ship ship, ShipAI shipAI)
        {
            base.Setup(ship, shipAI);

            shipCombat = ship.GetComponent<ShipCombat>();
        }
    }
}