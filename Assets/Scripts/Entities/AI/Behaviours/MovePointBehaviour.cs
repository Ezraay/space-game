using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace Spaceships.Entities.AI.Behaviours
{
    [CreateAssetMenu(menuName = "AI/Behaviour/Move Point", fileName = "MovePointBehaviour", order = 0)]
    public class MovePointBehaviour : AIBehaviour
    {
        private const float StoppingDistance = 0.1f; 
        
        public override float GetWeight(Ship ship, ShipAI shipAI)
        {
            return shipAI.data.ContainsKey("movePoint") ? 1 : 0;
        }

        public override void Setup(Ship ship, ShipAI shipAI)
        {
            shipAI.data.Add("onMovePointReached", new UnityEvent());
        }

        public override void Tick(Ship ship, ShipAI shipAI)
        {
            Vector2 point = GetTarget(ship, shipAI);
            Vector3 inputs = GetInputs(ship, shipAI, point);
            ship.rotationInput = inputs.x;
            ship.thrustInput = inputs.y;
            ship.strafeInput = inputs.z;
        }

        protected virtual Vector2 GetTarget(Ship ship, ShipAI shipAI)
        {
            Vector2 point = (Vector2) shipAI.data["movePoint"];
            return point;
        }

        protected virtual Vector3 GetInputs(Ship ship, ShipAI shipAI, Vector2 target)
        {
            // x is rotate, y is forward thrust, z is strafe
            Vector3 result = new Vector3();
            Vector2 distance = target - (Vector2) ship.transform.position;
            float distanceToTarget = distance.magnitude;

            if (distanceToTarget < StoppingDistance)
            {
                // Reached destination
                shipAI.data.Remove("movePoint");
                ship.thrustInput = 0;
                ((UnityEvent)shipAI.data["onMovePointReached"]).Invoke();
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