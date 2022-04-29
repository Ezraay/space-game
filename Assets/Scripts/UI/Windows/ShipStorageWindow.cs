using Spaceships.Entities;
using Spaceships.Hangar;
using UnityEngine;

namespace Spaceships.UI.Windows
{
    public class ShipStorageWindow : Window
    {
        [SerializeField] private ShipStorageSlot slotPrefab;
        [SerializeField] private Transform slotParent;
        private ShipStorageSlot activeShip;

        protected override void Start()
        {
            base.Start();
            
            CreateAllSlots();
            
            HangarManager.ShipStorage.onAdd.AddListener(shipData => AddSlot(shipData, HangarManager.ShipStorage.items.Count - 1));
        }

        private void CreateAllSlots()
        {
            foreach (Transform slot in slotParent)
            {
                Destroy(slot.gameObject);
            }
            
            for (int i = 0; i < HangarManager.ShipStorage.items.Count; i++)
            {
                ShipData shipData = HangarManager.ShipStorage.items[i];
                AddSlot(shipData, i);
            }
        }

        private void AddSlot(ShipData shipData, int index)
        {
            ShipStorageSlot newSlot = Instantiate(slotPrefab, slotParent);
            newSlot.Setup(shipData);
            if (index == HangarManager.ShipStorage.ActiveShip)
            {
                newSlot.SetActive();
                activeShip = newSlot;
            }

            newSlot.onClick.AddListener(() =>
            {
                if (activeShip != null)
                    activeShip.UnsetActive();
                    
                HangarManager.ShipStorage.SetActiveShip(index);
                newSlot.SetActive();
                activeShip = newSlot;
            });
        }
    }
}
