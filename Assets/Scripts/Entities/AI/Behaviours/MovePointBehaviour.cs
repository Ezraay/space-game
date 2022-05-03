using UnityEngine;
using UnityEngine.Events;

namespace Spaceships.Entities.AI.Behaviours
{
    public class MovePointBehaviour : AIBehaviour
    {
        private const float StoppingDistance = 0.1f;
        [HideInInspector] public Vector2 target;
        public bool Reached { get; private set; } = true;
        public UnityEvent OnReached { get; } = new UnityEvent();


        protected override float GetUtility()
        {
            return Reached ? 0 : 1;
        }

        public override void Tick()
        {
            if (Reached) return;

            Vector3 inputs = GetInputs();
            ship.rotationInput = inputs.x;
            ship.thrustInput = inputs.y;
            ship.strafeInput = inputs.z;
        }

        public void SetTarget(Vector2 position)
        {
            Reached = false;
            target = position;
        }

        protected virtual Vector3 GetInputs()
        {
            // x is rotate, y is forward thrust, z is strafe
            Vector3 result = new Vector3();
            Vector2 distance = target - (Vector2) ship.transform.position;
            float distanceToTarget = distance.magnitude;

            if (distanceToTarget < StoppingDistance)
            {
                // Reached destination
                ship.thrustInput = 0;
                Reached = true;
                OnReached.Invoke();
                return Vector3.zero;
            }


            // Turn
            float decelerateTurnDistance =
                ship.rotationVelocity * ship.rotationVelocity / (2 * ship.ShipData.RotateAcceleration);
            float directionToTarget = -Mathf.Atan2(distance.x, distance.y) * Mathf.Rad2Deg;
            float angleChange = (ship.transform.eulerAngles.z - directionToTarget + 180) % 360 - 180;
            result.x = Mathf.Abs(angleChange) > decelerateTurnDistance ? Mathf.Sign(angleChange) : 0;

            // Thrust
            float decelerateDistance = ship.forwardVelocity * ship.forwardVelocity / (2 * ship.ShipData.Acceleration);
            result.y = Mathf.Sign(distanceToTarget - decelerateDistance);

            return result;
        }
    }
}