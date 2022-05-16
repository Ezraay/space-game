using NaughtyAttributes;
using Spaceships.ItemSystem;
using Spaceships.ItemSystem.Items;
using UnityEngine;

namespace Spaceships.Entities.AI.Loot
{
    [System.Serializable]
    public class LootItem
    {
        [SerializeField] [Range(0, 1f)] private float dropChance;
        public float DropChance => dropChance;

        [SerializeField, OnValueChanged("UpdateTitle")] private ItemData itemData;
        [SerializeField] private int minCount = 1;
        [SerializeField] private int maxCount = 10;

 
        public Item AddItem()
        {
            if (itemData == null)
                return null;
            int count = Random.Range(minCount, maxCount + 1);
            return ItemFactory.CreateItem(itemData, count);
        }
    }
}