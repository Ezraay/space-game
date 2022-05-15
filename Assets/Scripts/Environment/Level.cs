using Spaceships.Entities;
using Spaceships.Entities.Combat;
using UnityEngine;

namespace Spaceships.Environment
{
    public class Level : MonoBehaviour
    {
        public Station mainStation;
        [SerializeField] private new string name = "New Map";
        [SerializeField] private Vector2 mapLocation = new Vector2(10, 10);
        public string Name => name;


        public void SpawnPlayer(ShipData shipData, Standing playerStanding)
        {
            // Spawns the player in the current level
            Ship spawnedShip = mainStation.SummonShip(shipData, playerStanding);
            Player.SetShip(spawnedShip);
            // Player.ship = spawnedShip;
            // Player.shipCombat = spawnedShip.GetComponent<ShipCombat>();
        }
    }
}