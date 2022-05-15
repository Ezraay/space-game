using System;
using Spaceships.ItemSystem;
using Spaceships.ItemSystem.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Spaceships.UI.Space
{
    public class InventoryWindow : ResizableWindow
    {
        [SerializeField] private ItemSlot slotPrefab;
        [SerializeField] private Transform slotParent;
        private Inventory<Item> inventory;


        public void Setup(Inventory<Item> inventory)
        {
            this.inventory = inventory;
            foreach (Item item in inventory.GetItems())
            {
                AddSlot(item);
            }

            inventory.OnItemAdded.AddListener(OnItemAdded);
            inventory.OnItemRemoved.AddListener(OnItemRemoved);
            
            SetTitle(inventory.Name);
        }

        public bool CanAddItem(Item item) => inventory.CanAddItem(item);

        private void OnDestroy()
        {
            inventory.OnItemAdded.RemoveListener(OnItemAdded);
            inventory.OnItemRemoved.RemoveListener(OnItemRemoved);
        }

        private void OnItemAdded(int index, Item item)
        {
            AddSlot(item);
        }

        private void OnItemRemoved(int index, Item item)
        {
            RemoveSlot(index);
        }

        private void AddSlot(Item item)
        {
            ItemSlot newSlot = Instantiate(slotPrefab, slotParent);
            newSlot.Setup(item, inventory);
        }

        private void RemoveSlot(int index)
        {
            Destroy(slotParent.GetChild(index).gameObject);
        }

        public override void OnDragOnto(DraggableSlot slot)
        {
            ItemSlot itemSlot = slot as ItemSlot;
            if (itemSlot == null)
                Debug.Log("Item slot is null");
            Item item = itemSlot.Item;
            Inventory<Item> inventory = itemSlot.Inventory;
            inventory.RemoveItem(item);
            this.inventory.AddItem(item);
        }
    }
}