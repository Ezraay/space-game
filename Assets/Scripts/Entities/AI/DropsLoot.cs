using System.Collections.Generic;
using Spaceships.ItemSystem.Items;
using UnityEngine;

namespace Spaceships.Entities.AI
{
    public class DropsLoot : MonoBehaviour
    {
        public void Setup(ShipCombat shipCombat, LootTable lootTable)
        {
            shipCombat.OnDie.AddListener(() =>
            {
                List<Item> items = lootTable.GetItems();
                if (items.Count > 0)
                    ItemContainerFactory.CreateContainer(items, transform.position, shipCombat.GetComponent<Ship>());
            });
        }
    }
}