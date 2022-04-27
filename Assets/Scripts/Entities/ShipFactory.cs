using System.Collections.Generic;
using UnityEngine;

namespace Spaceships.Entities
{
    public class ShipFactory : MonoBehaviour
    {
        public static Dictionary<string, Ship> shipData;

        [SerializeField] private Ship[] ships;

        private void Awake()
        {
            shipData = new Dictionary<string, Ship>();
            foreach (Ship ship in ships)
            {
                shipData.Add(ship.ShipData.ID, ship);
            }
        }

        public static Ship SpawnShip(string shipID, Vector3 position, Quaternion rotation)
        {
            shipData.TryGetValue(shipID, out Ship ship);
            if (ship == null)
                Debug.LogError("No such ship ID: " + shipID);

            Ship newShip = Instantiate(ship, position, rotation);
            return newShip;
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