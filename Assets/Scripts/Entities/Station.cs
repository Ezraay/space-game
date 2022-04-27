using Spaceships.Entities.AI;
using Spaceships.Environment;
using UnityEngine;

namespace Spaceships.Entities
{
    public class Station : Interactable
    {
        public override string InteractText => "Board Station";
        [SerializeField] private float rotateSpeed = 5;
        [SerializeField] private Transform spawnParent;
        [SerializeField] private ShipData[] shipsToSpawn;
        [SerializeField] private float minSpawnCooldown = 3;
        [SerializeField] private float maxSpawnCooldown = 20;
        [SerializeField] private AIPersonality randomShipPersonality;
        [SerializeField] private new string name = "New Station";

        private Transform[] spawns;
        private float spawnCooldown;

        private void Awake()
        {
            spawns = spawnParent.GetComponentsInChildren<Transform>();
        }

        private void Update()
        {
            transform.Rotate(Vector3.back, rotateSpeed * Time.deltaTime);

            spawnCooldown -= Time.deltaTime;
            if (spawnCooldown < 0) 
                SpawnRandomShip();
        }

        private void SpawnRandomShip()
        {
            spawnCooldown = Random.Range(minSpawnCooldown, maxSpawnCooldown);
            ShipData shipData = shipsToSpawn[Random.Range(0, shipsToSpawn.Length)];
            Ship newShip = SummonShip(shipData.ID);
            ShipAI shipAI = newShip.gameObject.AddComponent<ShipAI>();
            shipAI.personality = randomShipPersonality;
            shipAI.Setup();
        }

        public Ship SummonShip(string shipID)
        {
            Transform spawn = spawns[Random.Range(0, spawns.Length)];
            // Ship ship = Instantiate(shipPrefab, spawn.position, spawn.rotation);
            Ship ship = ShipFactory.SpawnShip(shipID, spawn.position, spawn.rotation);
            ship.forwardVelocity = ship.ShipData.ForwardSpeed;
            return ship;
        }

        public override void Interact()
        {
            SceneTransitions.HangarData.stationName = name;
            SceneTransitions.Loader.LoadHangar();
        }
    }
}
