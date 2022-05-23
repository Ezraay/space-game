using Spaceships.Economy;
using Spaceships.Entities;
using Spaceships.Hangar;
using Spaceships.ItemSystem.Items;
using Spaceships.SceneTransitions;
using Spaceships.UI.Space;
using UnityEngine;

namespace Spaceships.UI.Hangar.Windows
{
    public class ShopWindow : Window
    {
        [SerializeField] private ShopSlot slotPrefab;
        [SerializeField] private Transform slotParent;

        protected override void Start()
        {
            base.Start();

            foreach (ShipData shipData in ShipFactory.GetAllShipData())
            {
                ShopSlot newSlot = Instantiate(slotPrefab, slotParent);
                newSlot.Setup(shipData);
                newSlot.onClick.AddListener(() => { TryBuy(newSlot.ShipData); });
            }
            
            SetTitle("Shop");
        }

        private void TryBuy(ShipData data)
        {
            if (Wallet.Credits < data.CreditCost)
                return; // Not enough money
            Wallet.RemoveCredits(data.CreditCost);
            HangarManager.ShipStorage.AddShip(data);
        }

        public override bool CanDragOnto(DraggableSlot slot)
        {
            ItemSlot itemSlot = slot as ItemSlot;
            return itemSlot != null;
        }
        
        public override void OnDragOnto(DraggableSlot slot)
        {
            ItemSlot itemSlot = slot as ItemSlot;
            Item item = itemSlot.Item;
            Wallet.AddCredits(item.TotalSellCost);
            PlayerData.ShipInventory.RemoveItem(item);
        }
    }
}