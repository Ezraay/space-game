using System;
using Spaceships.Entities.AI;
using Spaceships.Entities.AI.Loot;
using UnityEngine;

namespace Spaceships.Entities.Combat
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private AIPersonality personality;
        [SerializeField] private ShipData ship;
        [SerializeField] private Standing standing;
        [SerializeField] private LootTable lootTable;
        [SerializeField] private float spawnBlockSize = 10f; // Square diameter in which a player can't be in order to spawn
        private Ship currentShip;

        private void Update()
        {
            if (CanSpawn())
            {
                SpawnShip();
            }
        }

        private bool CanSpawn()
        {
            return currentShip == null && Vector2.Distance(Player.ship.transform.position, transform.position) > spawnBlockSize;
        }

        private void SpawnShip()
        {
            currentShip = ShipFactory.SpawnAIShip(ship, standing, personality, transform.position, Quaternion.identity, lootTable);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, spawnBlockSize);
        }
    }
}
