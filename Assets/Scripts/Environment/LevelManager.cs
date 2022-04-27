using System;
using System.Collections.Generic;
using Spaceships.Entities;
using UnityEngine;

namespace Spaceships.Environment
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Level[] levels;
        [SerializeField] private string levelToLoad = "New Level";
        
        private Level currentLevel;
        private Dictionary<string, Level> levelData = new Dictionary<string, Level>();

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
            string playerShipID = SceneTransitions.SpaceData.playerShipID ?? Player.DefaultShip;
            // playerShip = ShipFactory.shipData[playerShipID].ShipData;
            currentLevel.SpawnPlayer(playerShipID);
        }

        private void UnloadLevel()
        {
            Destroy(currentLevel);
        }
        
        private void LoadLevel(string levelName)
        {
            levelData.TryGetValue(levelName, out Level newLevel);
            if (newLevel == null) 
                Debug.LogError("No such level: " + levelName);
            
            UnloadLevel();
            currentLevel = Instantiate(newLevel);
        }
    }
}