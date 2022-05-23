using Spaceships.Entities;
using Spaceships.ItemSystem.Items;
using UnityEngine.Events;

namespace Spaceships.SceneTransitions
{
    public static class PlayerData
    {
        public static ShipData ShipData { get; private set; }
        public static ShipInventory ShipInventory { get; private set; }

        public static UnityEvent<ShipData> OnShipChanged { get; } = new UnityEvent<ShipData>();
        
        public static void SetShip(ShipData shipData)
        {
            ShipData = shipData;
            ShipInventory = new ShipInventory(shipData); // TODO: Add persistence to inventory
            
            // TODO: Only for testing
            ShipInventory.AddItem(ItemFactory.CreateItem("metal_chunk", 100));
            
            OnShipChanged.Invoke(shipData);
        }

        public static ShipData GetShipOrDefault(ShipData other)
        {
            if (ShipData == null) 
                SetShip(other);
            return ShipData;
        }

        public static void ClearShip()
        {
            ShipData = null;
            ShipInventory = null;
            
            OnShipChanged.Invoke(null);
        }
    }
}