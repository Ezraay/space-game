using UnityEngine;
using UnityEngine.Events;

namespace Spaceships.Entities.AI.Behaviours
{
    [CreateAssetMenu(menuName = "AI/Behaviour/Move Warp Point", fileName = "MoveAndWarpBehaviour", order = 0)]
    public class MoveAndWarpBehaviour : MovePointBehaviour
    {
        private const float WarpDistance = 300;

        public override void Setup(Ship ship, ShipAI shipAI)
        {
            base.Setup(ship, shipAI);

            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            Vector2 point = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * WarpDistance;
            shipAI.data.Add("movePoint", point + (Vector2) ship.transform.position);
            ((UnityEvent) shipAI.data["onMovePointReached"]).AddListener(ship.Warp);
        }
    }
}