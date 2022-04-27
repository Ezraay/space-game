using Spaceships.Entities;
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
            }
        }
    }
}
