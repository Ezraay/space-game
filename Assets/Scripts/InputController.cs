using UnityEngine;

namespace Spaceships
{
    public class InputController : MonoBehaviour
    {
        public static float strafeInput;
        public static float thrustInput;
        public static float rotationInput;
        public static float zoomInput;
        public static bool leftMouseDown;
        public static bool interactInput;
        public static Vector2 mouseWorldPosition;
        public static Vector2 mousePosition;
        public static bool inventoryInput;

        private void Update()
        {
            thrustInput = Input.GetAxisRaw("Vertical");
            rotationInput = Input.GetAxisRaw("Horizontal");
            strafeInput = (Input.GetKey(KeyCode.E) ? 1 : 0) - (Input.GetKey(KeyCode.Q) ? 1 : 0);
            zoomInput = -Input.mouseScrollDelta.y;
            leftMouseDown = Input.GetMouseButton(0);
            interactInput = Input.GetKeyDown(KeyCode.F);
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition = Input.mousePosition;
            inventoryInput = Input.GetKeyDown(KeyCode.Tab);
        }
    }
}