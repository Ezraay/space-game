using System.Collections.Generic;
using Spaceships.Entities.Combat;
using UnityEngine;

namespace Spaceships.Entities.AI.Behaviours
{
    public class FindEnemiesBehaviour : AIDataBehaviour
    {
        [SerializeField] private float viewRadius = 100;
        private readonly List<Ship> enemies = new List<Ship>();
        private List<Standing> enemyFactions;

        public override void Tick()
        {
            enemies.Clear();
            Collider2D[] ships = Physics2D.OverlapCircleAll(ship.transform.position, viewRadius);
            foreach (Collider2D target in ships)
            {
                Ship targetShip = target.GetComponent<Ship>();
                if (targetShip != null && enemyFactions.Contains(targetShip.Standing))
                {
                    float currentDistance = Vector2.Distance(ship.transform.position, target.transform.position);
                    for (int i = 0; i <= enemies.Count; i++)
                    {
                        if (i == enemies.Count)
                        {
                            AddEnemy(targetShip, enemies.Count);
                            break;
                        }

                        if (Vector2.Distance(enemies[i].transform.position, ship.transform.position) > currentDistance)
                        {
                            AddEnemy(targetShip, i);
                            break;
                        }
                    }
                }
            }
        }

        private void AddEnemy(Ship addedShip, int index)
        {
            enemies.Insert(index, addedShip);
            addedShip.OnDie.AddListener(() =>
            {
                enemies.Remove(addedShip);
            });
        }

        public override void Setup(Ship ship, ShipAI shipAI)
        {
            base.Setup(ship, shipAI);

            enemyFactions = ship.Standing.GetEnemies();
        }

        public bool EnemyWithinRange(float distance)
        {
            foreach (Ship enemy in enemies)
            {
                if (Vector2.Distance(ship.transform.position, enemy.transform.position) <= distance)
                {
                    return true;
                }
            }

            return false;
        }

        public int NearbyEnemyCount()
        {
            return enemies.Count;
        }

        public (Ship, float) GetClosestEnemy()
        {
            float closestDistance = Mathf.Infinity;
            Ship closestEnemy = null;
            foreach (Ship enemy in enemies)
            {
                float distance = Vector2.Distance(enemy.transform.position, ship.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return (closestEnemy, closestDistance);
        }
    }
}