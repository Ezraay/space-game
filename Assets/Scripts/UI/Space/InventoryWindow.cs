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
        [SerializeField] private GameObject weightParent;
        [SerializeField] private Slider weightSlider;
        [SerializeField] private Text weightText;
        private Inventory<Item> inventory;
        private bool updateWeight;

        private void OnDestroy()
        {
            inventory.OnItemAdded.RemoveListener(OnItemAdded);
            inventory.OnItemRemoved.RemoveListener(OnItemRemoved);
        }


        public void Setup(Inventory<Item> inventory)
        {
            this.inventory = inventory;
            foreach (Item item in inventory.GetItems())
            {
                AddSlot(item);
            }

            inventory.OnItemAdded.AddListener(OnItemAdded);
            inventory.OnItemRemoved.AddListener(OnItemRemoved);

            updateWeight = inventory is ShipInventory;
            if (updateWeight)
                UpdateWeightView();
            else 
                Destroy(weightParent);

            SetTitle(inventory.Name);
        }

        public bool CanAddItem(Item item)
        {
            return inventory.CanAddItem(item);
        }

        private void OnItemAdded(int index, Item item)
        {
            AddSlot(item);
            UpdateWeightView();
        }

        private void OnItemRemoved(int index, Item item)
        {
            RemoveSlot(index);
            UpdateWeightView();
        }

        private void UpdateWeightView()
        {
            if (!updateWeight) return;
            
            ShipInventory shipInventory = inventory as ShipInventory;
            Debug.Log(shipInventory.Weight);
            float weight = (float) shipInventory.Weight;
            float maxWeight = (float) shipInventory.MaxWeight;
            weightSlider.value = weight / maxWeight;
            weightText.text = $"{shipInventory.Weight}/{shipInventory.MaxWeight}kg";
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
            if (!this.inventory.CanAddItem(item))
                Debug.LogError("Can't add item, inventory full");
            Inventory<Item> inventory = itemSlot.Inventory;
            inventory.RemoveItem(item);
            this.inventory.AddItem(item);
        }
    }
}