using System.Collections.Generic;
using Spaceships.Entities;
using Spaceships.Entities.Combat;
using Spaceships.SceneTransitions;
using UnityEngine;

namespace Spaceships.Environment
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Level[] levels;
        [SerializeField] private string levelToLoad = "New Level";
        [SerializeField] private ShipData testingShip;
        [SerializeField] private Standing playerStanding;
        private readonly Dictionary<string, Level> levelData = new Dictionary<string, Level>();

        private Level currentLevel;

        private void Start()
        {
            foreach (Level level in FindObjectsOfType<Level>())
            {
                Destroy(level.gameObject);
            }

            foreach (Level level in levels)
            {
                levelData.Add(level.Name, level);
            }

            LoadLevel(levelToLoad);
            SpaceData.playerShipID ??= testingShip.ID;
            ShipData shipData = ShipFactory.GetShipData(SpaceData.playerShipID);
            currentLevel.SpawnPlayer(shipData, playerStanding);
        }

        
        
        private void UnloadLevel()
        {
            // Destroy the current level object
            Destroy(currentLevel);
        }

        private void LoadLevel(string levelName)
        {
            // Loads a new level object
            levelData.TryGetValue(levelName, out Level newLevel);
            if (newLevel == null)
                Debug.LogError("No such level: " + levelName);

            UnloadLevel();
            currentLevel = Instantiate(newLevel);
        }
    }
}