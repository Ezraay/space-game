using System.Collections.Generic;
using Spaceships.Entities.Combat;
using UnityEngine;

namespace Spaceships.Entities.AI.Behaviours
{
    public class FindEnemiesBehaviour : AIDataBehaviour
    {
        [SerializeField] private float viewRadius = 100;
        private readonly List<ShipCombat> enemies = new List<ShipCombat>();
        private List<Standing> enemyFactions;
        private ShipCombat shipCombat;

        public override void Tick()
        {
            enemies.Clear();
            Collider2D[] ships = Physics2D.OverlapCircleAll(ship.transform.position, viewRadius);
            foreach (Collider2D target in ships)
            {
                ShipCombat targetShip = target.GetComponent<ShipCombat>();
                if (targetShip != null && enemyFactions.Contains(targetShip.Standing))
                {
                    float currentDistance = Vector2.Distance(ship.transform.position, target.transform.position);
                    InsertEnemyByRange(targetShip, currentDistance);
                }
            }
        }

        private void InsertEnemyByRange(ShipCombat enemy, float distance)
        {
            for (int i = 0; i <= enemies.Count; i++)
            {
                if (i == enemies.Count)
                {
                    AddEnemy(enemy, enemies.Count);
                    break;
                }

                if (Vector2.Distance(enemies[i].transform.position, enemy.transform.position) > distance)
                {
                    AddEnemy(enemy, i);
                    break;
                }
            }
        }

        private void AddEnemy(ShipCombat addedShip, int index)
        {
            enemies.Insert(index, addedShip);
            addedShip.OnDie.AddListener(() => { enemies.Remove(addedShip); });
        }

        public override void Setup(Ship ship, ShipAI shipAI)
        {
            base.Setup(ship, shipAI);

            shipCombat = ship.GetComponent<ShipCombat>();
            enemyFactions = shipCombat.Standing.GetEnemies();
        }

        public bool EnemyWithinRange(float distance)
        {
            foreach (ShipCombat enemy in enemies)
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

        public (ShipCombat, float) GetClosestEnemy()
        {
            float closestDistance = Mathf.Infinity;
            ShipCombat closestEnemy = null;
            foreach (ShipCombat enemy in enemies)
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