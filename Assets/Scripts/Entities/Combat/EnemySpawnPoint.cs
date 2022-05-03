using Spaceships.Entities.AI;
using UnityEngine;

namespace Spaceships.Entities.Combat
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
                currentShip = ShipFactory.SpawnAIShip(ship, standing, personality, transform.position, Quaternion.identity);
            }
        }
    }
}
