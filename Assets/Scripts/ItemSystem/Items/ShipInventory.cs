using System.Collections.Generic;
using Spaceships.Entities;

namespace Spaceships.ItemSystem.Items
{
    public class ShipInventory : Inventory<Item>
    {
        private readonly ShipData data;
        

        public ShipInventory(ShipData data)
        {
            this.data = data;
            MaxWeight = data.CargoSize;
        }

        public float Weight { get; private set; }
        // public override string Name => "Inventory";

        public float MaxWeight { get; }

        public override void AddItem(Item item)
        {
            Weight += item.TotalWeight;
            base.AddItem(item);
        }

        public override void RemoveItem(Item item)
        {
            Weight -= item.TotalWeight;
            base.RemoveItem(item);
        }

        public override bool ContainsItem(string id, int count)
        {
            foreach (Item item in items)
            {
                if (item.ID == id)
                {
                    count -= item.Count;
                    if (count <= 0) return true;
                }
            }

            return false;
        }

        public override bool CanAddItem(Item item)
        {
            return item.TotalWeight + Weight <= MaxWeight;
        }
    }
}