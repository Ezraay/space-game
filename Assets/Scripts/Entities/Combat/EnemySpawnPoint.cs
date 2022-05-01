using System;
using System.Collections;
using System.Collections.Generic;
using Spaceships.Entities;
using Spaceships.Entities.AI;
using Spaceships.Entities.Combat;
using UnityEngine;

namespace Spaceships
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private AIPersonality personality;
        [SerializeField] private ShipData ship;
        [SerializeField] private Standing standing;
        private Ship currentShip;

        private void Update()
        {
            if (currentShip == null)
            {
                currentShip = ShipFactory.SpawnAIShip(ship.ID, standing, personality, transform.position, Quaternion.identity);
            }
        }
    }
}
