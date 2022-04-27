using UnityEngine;

namespace Spaceships
{
    public class InputController : MonoBehaviour
    {
        public static float StrafeInput;
        public static float ThrustInput;
        public static float RotationInput;
        public static float ZoomInput;
        public static bool ShootInput;
        public static bool InteractInput;

        private void Update()
        {
            ThrustInput = Input.GetAxisRaw("Vertical");
            RotationInput = Input.GetAxisRaw("Horizontal");
            StrafeInput = Input.GetKey(KeyCode.E) ? 1 : 0 - (Input.GetKey(KeyCode.Q) ? 1 : 0);
            ZoomInput = -Input.mouseScrollDelta.y;
            ShootInput = Input.GetMouseButton(0);
            InteractInput = Input.GetKeyDown(KeyCode.E);
        }
    }
}