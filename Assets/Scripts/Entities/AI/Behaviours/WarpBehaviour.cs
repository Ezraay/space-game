using UnityEngine;

namespace Spaceships.Entities.AI.Behaviours
{
    [RequireComponent(typeof(MovePointBehaviour))]
    public class WarpBehaviour : AIBehaviour
    {
        [SerializeField] private float warpDistance = 300;
        private MovePointBehaviour movePointBehaviour;

        public override void Setup(Ship ship, ShipAI shipAI)
        {
            base.Setup(ship, shipAI);

            movePointBehaviour = GetComponent<MovePointBehaviour>();
            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            Vector2 point = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * warpDistance;
            movePointBehaviour.SetTarget(point + (Vector2) ship.transform.position);
            movePointBehaviour.OnReached.AddListener(ship.Warp);
        }
    }
}