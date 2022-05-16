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
        [SerializeField, MinMaxSlider(1, 100)] public Vector2Int countRange = new Vector2Int(1, 10);

 
        public Item AddItem()
        {
            if (itemData == null)
                return null;
            int count = Random.Range(countRange.x, countRange.y + 1);
            return ItemFactory.CreateItem(itemData, count);
        }
    }
}