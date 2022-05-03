using UnityEngine;

namespace Spaceships.Entities.AI.Behaviours
{
    [RequireComponent(typeof(FindEnemiesBehaviour))]
    public class AttackEnemyBehaviour : AICombatBehaviour
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
            (ShipCombat closestEnemy, float distance) = enemyFinder.GetClosestEnemy();
            if (distance <= shootDistance)
                shipCombat.Shoot(closestEnemy.transform.position);
        }
    }
}