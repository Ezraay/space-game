using Spaceships.Entities;
using Spaceships.SceneTransitions;
using UnityEngine;

namespace Spaceships.Hangar
{
    public class HangarManager : MonoBehaviour
    {
        [SerializeField] private ShipData defaultShip;
        public static string hangarName;
        public static ShipData ActiveShip { get; private set; }
        public static ShipStorage ShipStorage { get; private set; }

        private void Start()
        {
            hangarName = HangarData.stationName;
            ShipStorage = ShipStorageManager.GetStorage(hangarName);
            if (PlayerData.ShipData == null)
            {
                PlayerData.SetShip(defaultShip);
            }
            
            ActiveShip = ShipFactory.GetShipData(PlayerData.ShipData.ID);
            int shipIndex = ShipStorage.AddShip(ActiveShip);
            ShipStorage.SetActiveShip(shipIndex);
        }
    }
}