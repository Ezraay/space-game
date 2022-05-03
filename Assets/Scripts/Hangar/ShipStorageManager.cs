using System.Collections.Generic;
using UnityEngine;

namespace Spaceships.Hangar
{
    public class ShipStorageManager : MonoBehaviour
    {
        private static readonly Dictionary<string, ShipStorage> stationShipStorages =
            new Dictionary<string, ShipStorage>();

        public static ShipStorage GetStorage(string hangarName)
        {
            if (!stationShipStorages.ContainsKey(hangarName))
            {
                ShipStorage newStorage = new ShipStorage();
                stationShipStorages.Add(hangarName, newStorage);
            }

            return stationShipStorages[hangarName];
        }
    }
}