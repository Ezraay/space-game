using Spaceships.Entities;
using UnityEngine;

namespace Spaceships.Environment
{
    public class Level : MonoBehaviour
    {
        public string Name => name;
        
        public Station mainStation;
        [SerializeField] private Player player;
        [SerializeField] private new string name = "New Map"; 
        [SerializeField] private Vector2 mapLocation = new Vector2(10, 10);


        public void SpawnPlayer(string shipID)
        {
            Ship spawnedShip = mainStation.SummonShip(shipID);
            Player.ship = spawnedShip;
        }
    }
}