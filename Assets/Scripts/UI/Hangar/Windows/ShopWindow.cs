using Spaceships.Economy;
using Spaceships.Entities;
using Spaceships.Hangar;
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