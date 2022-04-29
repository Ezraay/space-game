using Spaceships.Economy;
using Spaceships.Entities;
using Spaceships.Hangar;
using UnityEngine;

namespace Spaceships.UI.Windows
{
    public class ShopWindow : Window
    {
        [SerializeField] private ShopSlot slotPrefab;
        [SerializeField] private Transform slotParent;

        protected override void Start()
        {
            base.Start();
            
            foreach (Ship ship in ShipFactory.shipData.Values)
            {
                ShopSlot newSlot = Instantiate(slotPrefab, slotParent);
                newSlot.Setup(ship.ShipData);
                newSlot.onClick.AddListener(() =>
                {
                    TryBuy(newSlot.ShipData);
                });
            }
        }

        private void TryBuy(ShipData data)
        {
            if (Wallet.Credits < data.CreditCost)
                return; // Not enough money
            Wallet.RemoveCredits(data.CreditCost);
            HangarManager.ShipStorage.AddShip(data);
        }
    }
}