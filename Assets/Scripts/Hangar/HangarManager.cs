using Spaceships.Entities;
using Spaceships.SceneTransitions;
using UnityEngine;

namespace Spaceships.Hangar
{
    public class HangarManager : MonoBehaviour
    {
        public static string hangarName;
        public static ShipData ActiveShip { get; private set; }
        public static ShipStorage ShipStorage { get; private set; }

        private void Start()
        {
            hangarName = HangarData.stationName;
            ShipStorage = ShipStorageManager.GetStorage(hangarName);
            if (SpaceData.playerShipID != null)
            {
                ActiveShip = ShipFactory.GetShipData(SpaceData.playerShipID);
                int shipIndex = ShipStorage.AddShip(ActiveShip);
                ShipStorage.SetActiveShip(shipIndex);
            }
        }
    }
}