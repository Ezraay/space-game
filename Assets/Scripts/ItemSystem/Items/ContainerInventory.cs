using System.Collections.Generic;
using System.Linq;
using Spaceships.Entities;

namespace Spaceships.ItemSystem.Items
{
    public class ContainerInventory : Inventory<Item>
    {
        public override bool CanAddItem(Item item) => false;

        public override string Name => "Loot - " + ship.ShipData.Name;
        private Ship ship;

        public ContainerInventory(List<Item> items, Ship ship)
        {
            this.ship = ship;
            this.items = items;
        }
    }
}