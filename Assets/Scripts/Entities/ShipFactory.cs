using System.Collections.Generic;
using System.Linq;
using Spaceships.Entities.AI;
using Spaceships.Entities.Combat;
using UnityEngine;

namespace Spaceships.Entities
{
    public class ShipFactory : MonoBehaviour
    {
        private static Dictionary<string, ShipData> shipDatasByID;
        private static Dictionary<ShipData, Ship> shipData;

        [SerializeField] private Ship[] ships;

        private void Awake()
        {
            shipData = new Dictionary<ShipData, Ship>();
            shipDatasByID = new Dictionary<string, ShipData>();
            foreach (Ship ship in ships)
            {
                shipData.Add(ship.ShipData, ship);
                shipDatasByID.Add(ship.ShipData.ID, ship.ShipData);
            }
        }

        public static ShipData GetShipData(string id)
        {
            if (!shipDatasByID.ContainsKey(id))
                Debug.LogError("No such ShipData: " + id);
            return shipDatasByID[id];
        }

        public static ShipData[] GetAllShipData() => shipDatasByID.Values.ToArray();

        public static Ship SpawnShip(ShipData shipToSpawn, Standing standing, Vector3 position, Quaternion rotation)
        {
            if (!shipData.ContainsKey(shipToSpawn))
                Debug.LogError("No such ship ID: " + shipToSpawn.ID);

            Ship newShip = Instantiate(shipData[shipToSpawn], position, rotation);
            ShipCombat shipCombat = newShip.GetComponent<ShipCombat>();
            if (shipCombat != null)
                shipCombat.Setup(standing);
            
            return newShip;
        }

        public static Ship SpawnAIShip(ShipData shipToSpawn, Standing standing, AIPersonality personality, Vector3 position,
            Quaternion rotation, LootTable lootTable)
        {
            Ship ship = SpawnShip(shipToSpawn, standing, position, rotation);
            ShipAI shipAI = ship.gameObject.AddComponent<ShipAI>();
            shipAI.Setup(personality, lootTable);
            return ship;
        }

        public static GameObject SpawnShipModel(ShipData shipToSpawn, Vector3 position, Quaternion rotation)
        {
            Ship newShip = SpawnShip(shipToSpawn, null, position, rotation);
            GameObject model = newShip.gameObject;
            Destroy(newShip);
            return model;
        }
    }
}