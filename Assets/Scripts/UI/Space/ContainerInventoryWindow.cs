using Spaceships.ItemSystem;
using Spaceships.ItemSystem.Items;
using UnityEngine;

namespace Spaceships.UI.Space
{
    public class ContainerInventoryWindow : InventoryWindow
    {
        public override void Setup(Inventory<Item> inventory)
        {
            base.Setup(inventory);
            inventory.OnItemRemoved.AddListener(OnItemRemoved);
        }

        private void OnItemRemoved(int index, Item item)
        {
            Debug.Log("Test");
            if (inventory.GetItems().Count == 0)
            {
                inventory.OnItemRemoved.RemoveListener(OnItemRemoved);
                Close();
            }
        }
    }
}