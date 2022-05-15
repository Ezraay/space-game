using System.Collections.Generic;
using NaughtyAttributes;
using Spaceships.ItemSystem.Items;
using UnityEngine;

namespace Spaceships.Entities
{
    [CreateAssetMenu(menuName = "Create Loot Table", fileName = "New Loot Table", order = 0)]
    public class LootTable : ScriptableObject
    {
        [SerializeField] private List<Loot> loot;

        public List<Item> GetItems()
        {
            List<Item> result = new List<Item>();

            foreach (Loot lootChance in loot)
            {
                float random = Random.Range(0f, 1f);
                if (random <= lootChance.dropChance)
                {
                    int count = Random.Range(lootChance.countRange.x, lootChance.countRange.y + 1);
                    result.Add(ItemFactory.CreateItem(lootChance.item.ID, count));
                }
            }
            
            return result;
        }
        
        [System.Serializable]
        private struct Loot
        {
            public ItemData item;
            [Range(0,1f)] public float dropChance;
            [MinMaxSlider(1, 100)]
            public Vector2Int countRange;
        }
    }
}