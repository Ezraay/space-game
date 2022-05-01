using Spaceships.Environment;
using UnityEngine;

namespace Spaceships.Entities
{
    public class Player : MonoBehaviour
    {
        public static Interactable availableInteractable;
        public static Ship ship;
        [SerializeField] private float interactRadius = 10;
        [SerializeField] private LayerMask interactMask;

        public Vector3 Position => ship == null ? Vector3.zero : ship.transform.position;

        protected void Update()
        {
            if (ship == null)
                return;

            ship.thrustInput = InputController.thrustInput;
            ship.strafeInput = InputController.strafeInput;
            ship.rotationInput = InputController.rotationInput;
            if (InputController.shootInput)
                ship.Shoot(InputController.mouseWorldPosition);

            Collider2D[] interactables =
                Physics2D.OverlapCircleAll(ship.transform.position, interactRadius, interactMask);
            availableInteractable = null;
            foreach (Collider2D item in interactables)
            {
                Interactable interactable = item.GetComponent<Interactable>();
                if (interactable != null)
                {
                    if (InputController.interactInput)
                        Interact(interactable);
                    availableInteractable = interactable;
                    break;
                }
            }
        }

        private void Interact(Interactable interactable)
        {
            interactable.Interact();
        }
    }
}