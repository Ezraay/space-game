using Spaceships.Environment;
using UnityEngine;

namespace Spaceships.Entities
{
    public class Player : MonoBehaviour
    {
        public static Interactable availableInteractable;
        public static Ship ship;
        public static ShipCombat shipCombat;
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
                shipCombat.Shoot(InputController.mouseWorldPosition);


            availableInteractable = null;
            Collider2D[] interactables = GetNearbyInteractables();
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

        private Collider2D[] GetNearbyInteractables()
        {
            Collider2D[] interactables =
                Physics2D.OverlapCircleAll(ship.transform.position, interactRadius, interactMask);
            return interactables;
        }

        private void Interact(Interactable interactable)
        {
            interactable.Interact();
        }
    }
}