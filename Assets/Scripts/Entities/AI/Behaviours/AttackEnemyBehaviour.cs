using UnityEngine;

namespace Spaceships.Entities.AI.Behaviours
{
    [RequireComponent(typeof(FindEnemiesBehaviour))]
    public class AttackEnemyBehaviour : AIBehaviour
    {
        [SerializeField] private float shootDistance = 25;
        private FindEnemiesBehaviour enemyFinder;
        
        public override void Setup(Ship ship, ShipAI shipAI)
        {
            base.Setup(ship, shipAI);
            
            enemyFinder = GetComponent<FindEnemiesBehaviour>();
        }

        protected override float GetUtility()
        {
            return enemyFinder.EnemyWithinRange(shootDistance) ? 1 : 0;
        }

        public override void Tick()
        {
            (Ship closestEnemy, float distance) = enemyFinder.GetClosestEnemy();
            if (distance <= shootDistance)
                ship.Shoot(closestEnemy.transform.position);
        }
    }
}