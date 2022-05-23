using System;
using Spaceships.Entities;
using Spaceships.ItemSystem;
using Spaceships.ItemSystem.Items;
using Spaceships.SceneTransitions;
using Spaceships.UI.Space;
using UnityEngine;

namespace Spaceships.UI.Hangar.Windows
{
    public class HangarInventoryWindow : InventoryWindow
    {
        protected override void Start()
        {
            base.Start();

            Hide();
        }

        public override void Show()
        {
            if (PlayerData.ShipInventory != null)
            {
                base.Show();
                Setup(PlayerData.ShipInventory);
            }
        }
    }
}