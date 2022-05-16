using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Spaceships.Environment;
using Spaceships.ItemSystem.Items;
using Spaceships.SceneTransitions;
using UnityEngine;

namespace Spaceships.Entities
{
    public class Player : MonoBehaviour
    {
        public static Interactable availableInteractable;
        public static Ship ship;
        public static ShipCombat shipCombat;
        [SerializeField] private float interactRadius = 10;

        public Vector3 Position => ship == null ? Vector3.zero : ship.transform.position;
        public static ShipInventory Inventory { get; private set; }

        public static void SetShip(Ship ship)
        {
            Player.ship = ship;
            shipCombat = ship.GetComponent<ShipCombat>();
            shipCombat.OnDie.AddListener(() =>
            {
                SpaceData.playerShipID = null;
                HangarData.stationName = LevelManager.StationName;
                Loader.LoadHangar();
            });
            Inventory = new ShipInventory(ship.ShipData);
        }

        public static float DistanceTo(Vector3 position)
        {
            if (ship == null) return 0;
            return Vector2.Distance(ship.transform.position, position);
        }
        
        protected void Update()
        {
            if (ship == null)
                return;

            ship.thrustInput = InputController.thrustInput;
            ship.strafeInput = InputController.strafeInput;
            ship.rotationInput = InputController.rotationInput;
            if (InputController.leftMouseDown)
                shipCombat.Shoot(InputController.mouseWorldPosition);


            availableInteractable = null;
            List<Interactable> interactables = GetNearbyInteractables();
            foreach (Interactable item in interactables)
            {
                if (InputController.interactInput)
                    Interact(item);
                availableInteractable = item;
                break;
            }
        }

        private List<Interactable> GetNearbyInteractables()
        {
            List<Interactable> result = new List<Interactable>();
            foreach (Interactable interactable in Interactable.AllInteractables)
            {
                float distance = Vector2.Distance(interactable.transform.position, ship.transform.position);
                if (distance <= interactable.InteractRadius + interactRadius)
                    result.Add(interactable);
            }
            return result;
        }

        private void Interact(Interactable interactable)
        {
            interactable.Interact();
        }
    }
}