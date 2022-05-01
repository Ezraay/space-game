using UnityEngine;

namespace Spaceships.Entities.AI.Behaviours
{
    [RequireComponent(typeof(FindEnemiesBehaviour))]
    [RequireComponent(typeof(MovePointBehaviour))]
    public class FollowEnemyBehaviour : AIBehaviour
    {
        private FindEnemiesBehaviour enemyFinder;
        private MovePointBehaviour movePoint;

        public override void Setup(Ship ship, ShipAI shipAI)
        {
            base.Setup(ship, shipAI);

            enemyFinder = GetComponent<FindEnemiesBehaviour>();
            movePoint = GetComponent<MovePointBehaviour>();
        }

        protected override float GetUtility()
        {
            return enemyFinder.NearbyEnemyCount() > 0 ? 1 : 0;
        }

        public override void Tick()
        {
            movePoint.SetTarget(enemyFinder.GetClosestEnemy().Item1.transform.position);
        }
    }
}