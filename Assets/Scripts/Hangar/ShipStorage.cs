using System.Collections.Generic;
using Spaceships.Entities;
using Spaceships.SceneTransitions;
using UnityEngine.Events;

namespace Spaceships.Hangar
{
    public class ShipStorage
    {
        public readonly List<ShipData> items;
        public UnityEvent<ShipData> onAdd = new UnityEvent<ShipData>();
        public UnityEvent<int> onDelete = new UnityEvent<int>();

        public ShipStorage()
        {
            items = new List<ShipData>();
        }

        public int ActiveShip { get; private set; } = -1;

        public int AddShip(ShipData shipData)
        {
            items.Add(shipData);
            if (items.Count == 1) // Added our first ship
                SetActiveShip(0);
            onAdd.Invoke(shipData);
            return items.Count - 1;
        }

        public void RemoveShip(int index)
        {
            items.RemoveAt(index);
            onDelete.Invoke(index);
        }

        public void RemoveActiveShip()
        {
            items.RemoveAt(ActiveShip);
            onDelete.Invoke(ActiveShip);
        }

        public void SetActiveShip(int index)
        {
            ActiveShip = index;
            // SpaceData.playerShipID = items[index].ID;
            PlayerData.SetShip(items[index]);
        }
    }
}