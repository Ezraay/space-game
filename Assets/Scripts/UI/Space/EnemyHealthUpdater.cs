using System;
using Spaceships.Entities;
using UnityEngine;

namespace Spaceships.UI.Space
{
    public class EnemyHealthUpdater : MonoBehaviour
    {
        [SerializeField] private EnemyHealth enemyHealth;
        [SerializeField] private new Camera camera;
        [SerializeField] [Min(1)] private float distanceToShow = 100; // Pixels

        private void Update()
        {
            float closestDistance = float.MaxValue;
            Ship closestShip = null;
            foreach (Ship ship in ShipFactory.allShips)
            {
                if (ship == Player.ship)
                    continue;
                Vector2 position = camera.WorldToScreenPoint(ship.transform.position);
                float distance = Vector2.Distance(InputController.mousePosition, position);
                if (distance < closestDistance)
                {
                    closestShip = ship;
                    closestDistance = distance;
                }
            }

            if (closestShip != null && closestDistance < distanceToShow)
            {
                enemyHealth.UpdateHealth(closestShip);
                enemyHealth.Show();
            }
            else
            {
                enemyHealth.Hide();
            }
        }
    }
}
