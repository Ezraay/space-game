using Spaceships.ItemSystem;
using Spaceships.ItemSystem.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Spaceships.UI.Space
{
    public class ItemSlot : DraggableSlot
    {
        [SerializeField] private Image image;
        [SerializeField] private Text titleText;
        [SerializeField] private Text countText;
        public Item Item { get; private set; }
        public Inventory<Item> Inventory { get; private set; }

        public void Setup(Item item, Inventory<Item> inventory)
        {
            Item = item;
            Inventory = inventory;

            image.sprite = item.Sprite;
            titleText.text = item.Name;
            if (item.Count > 1)
                countText.text = item.Count.ToString();
        }

        // public override bool CanDragInto(Window window)
        // {
            // InventoryWindow inventoryWindow = window as InventoryWindow;
            // if (inventoryWindow != null)
            // {
            //     return inventoryWindow.CanAddItem(Item);
            // }
            //
            // return false;
        // }
    }
}