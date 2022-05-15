using System;
using Spaceships.Entities;
using Spaceships.ItemSystem;
using Spaceships.ItemSystem.Items;
using UnityEngine;

namespace Spaceships.UI.Space
{
    public class InventoryWindowFactory : MonoBehaviour
    {
        private static InventoryWindowFactory instance;
        
        [SerializeField] private RectTransform windowParent;
        [SerializeField] private InventoryWindow window;
        private InventoryWindow playerInventoryWindow;
            
        private void Start()
        {
            instance = this;
            ItemContainer.OnInteract.AddListener(container =>
            {
                ShowInventory(container.Inventory);
            });
        }

        private void Update()
        {
            if (InputController.inventoryInput)
            {
                if (playerInventoryWindow == null)
                {
                    playerInventoryWindow = ShowInventory(Player.Inventory);
                    playerInventoryWindow.OnClose.AddListener(() => playerInventoryWindow = null);
                }
                else
                {
                    playerInventoryWindow.Close();
                }
            }
        }

        private static InventoryWindow ShowInventory(Inventory<Item> inventory)
        {
            InventoryWindow newWindow = Instantiate(instance.window, instance.windowParent);
            newWindow.Setup(inventory);
            return newWindow;
        }
    }
}