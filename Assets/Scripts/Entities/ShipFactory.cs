using System.Collections.Generic;
using Spaceships.Entities.AI;
using Spaceships.Entities.Combat;
using UnityEngine;

namespace Spaceships.Entities
{
    public class ShipFactory : MonoBehaviour
    {
        public static Dictionary<string, Ship> shipData;
        public static List<Ship> allShips = new List<Ship>();

        [SerializeField] private Ship[] ships;

        private void Awake()
        {
            shipData = new Dictionary<string, Ship>();
            foreach (Ship ship in ships)
            {
                shipData.Add(ship.ShipData.ID, ship);
            }
        }

        public static Ship SpawnShip(string shipID, Standing standing, Vector3 position, Quaternion rotation)
        {
            shipData.TryGetValue(shipID, out Ship ship);
            if (ship == null)
                Debug.LogError("No such ship ID: " + shipID);

            Ship newShip = Instantiate(ship, position, rotation);
            newShip.Setup(standing);
            
            allShips.Add(newShip);
            newShip.OnDie.AddListener(() => allShips.Remove(newShip));
            return newShip;
        }

        public static Ship SpawnAIShip(string shipID, Standing standing,AIPersonality personality,  Vector3 position, Quaternion rotation)
        {
            Ship ship = SpawnShip(shipID, standing, position, rotation);
            ShipAI shipAI = ship.gameObject.AddComponent<ShipAI>();
            shipAI.Setup(personality);
            return ship;
        }
        
        public static GameObject SpawnShipModel(string shipID, Vector3 position, Quaternion rotation)
        {
            shipData.TryGetValue(shipID, out Ship ship);
            if (ship == null)
                Debug.LogError("No such ship ID: " + shipID);

            Ship newShip = Instantiate(ship, position, rotation);
            GameObject model = newShip.gameObject;
            Destroy(newShip);
            return model;
        }
    }
}