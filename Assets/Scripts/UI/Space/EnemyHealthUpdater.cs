using Spaceships.Entities;
using UnityEngine;

namespace Spaceships.UI.Space
{
    public class EnemyHealthUpdater : MonoBehaviour
    {
        [SerializeField] private EnemyHealth enemyHealth;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private new Camera camera;
        [SerializeField] [Min(1)] private float distanceToShow = 100; // Pixels

        private void Update()
        {
            ShipCombat closestShip = GetClosestShip();
            if (closestShip != null)
            {
                enemyHealth.UpdateHealth(closestShip);
                enemyHealth.Show();
            }
            else
            {
                enemyHealth.Hide();
            }

            playerHealth.UpdateHealth(Player.shipCombat);
        }

        private ShipCombat GetClosestShip()
        {
            float closestDistance = float.MaxValue;
            ShipCombat closestShip = null;
            foreach (Ship ship in Ship.AllShips)
            {
                ShipCombat shipCombat = ship.GetComponent<ShipCombat>();
                if (ship == Player.ship || shipCombat == null)
                    continue;
                Vector2 position = camera.WorldToScreenPoint(ship.transform.position);
                float distance = Vector2.Distance(InputController.mousePosition, position);
                if (distance < closestDistance)
                {
                    closestShip = shipCombat;
                    closestDistance = distance;
                }
            }

            return closestDistance <= distanceToShow ? closestShip : null;
        }
    }
}