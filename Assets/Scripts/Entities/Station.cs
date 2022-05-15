using System.Collections.Generic;
using Spaceships.Entities.AI;
using Spaceships.Entities.Combat;
using Spaceships.Environment;
using Spaceships.SceneTransitions;
using UnityEngine;

namespace Spaceships.Entities
{
    public class Station : Interactable
    {
        [SerializeField] private float rotateSpeed = 5;
        [SerializeField] private Transform spawnParent;
        [SerializeField] private ShipData[] shipsToSpawn;
        [SerializeField] private float minSpawnCooldown = 3;
        [SerializeField] private float maxSpawnCooldown = 20;
        [SerializeField] private AIPersonality randomShipPersonality;
        [SerializeField] private Standing randomShipStanding;
        [SerializeField] private new string name = "New Station";
        [SerializeField] private float interactRadius = 10;
        private readonly List<Transform> spawns = new List<Transform>();

        private float spawnCooldown;
        public override string InteractText => "Board Station";
        public override float InteractRadius => interactRadius;

        public override void Interact()
        {
            HangarData.stationName = name;
            Loader.LoadHangar();
        }

        private void Awake()
        {
            foreach (Transform spawn in spawnParent)
            {
                spawns.Add(spawn);
            }
        }

        private void Update()
        {
            transform.Rotate(Vector3.back, rotateSpeed * Time.deltaTime);

            spawnCooldown -= Time.deltaTime;
            if (spawnCooldown < 0)
                SpawnRandomShip();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, interactRadius);
        }

        private void SpawnRandomShip()
        {
            // Creates a random AI ship
            spawnCooldown = Random.Range(minSpawnCooldown, maxSpawnCooldown);
            ShipData shipData = shipsToSpawn[Random.Range(0, shipsToSpawn.Length)];
            Ship newShip = SummonShip(shipData, randomShipStanding);
            ShipAI shipAI = newShip.gameObject.AddComponent<ShipAI>();
            shipAI.Setup(randomShipPersonality);
        }

        public Ship SummonShip(ShipData shipToSpawn, Standing standing)
        {
            // Summons a ship and returns it
            Transform spawn = spawns[Random.Range(0, spawns.Count)];
            Ship ship = ShipFactory.SpawnShip(shipToSpawn, standing, spawn.position, spawn.rotation);
            ship.forwardVelocity = ship.ShipData.ForwardSpeed;
            return ship;
        }
    }
}