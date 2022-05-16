using System.Collections.Generic;
using NaughtyAttributes;
using Spaceships.Economy;
using Spaceships.ItemSystem.Items;
using UnityEngine;

namespace Spaceships.Entities.AI.Loot
{
    [CreateAssetMenu(menuName = "Create Loot Table", fileName = "New Loot Table", order = 0)]
    public class LootTable : ScriptableObject
    {
        [SerializeField] private List<LootItem> lootItems;

        [SerializeField] private bool dropsCredits;

        [SerializeField] [ShowIf("dropsCredits")] [Min(1)]
        private int minCredits = 1;

        [SerializeField] [ShowIf("dropsCredits")]
        private int maxCredits = 1;

        public List<Item> GetItems()
        {
            List<Item> result = new List<Item>();

            foreach (LootItem lootItem in lootItems)
            {
                float random = Random.Range(0f, 1f);
                if (random <= lootItem.DropChance)
                {
                    Item item = lootItem.AddItem();
                    if (item != null)
                        result.Add(item);
                }
            }

            if (dropsCredits)
            {
                int credits = Random.Range(minCredits, maxCredits + 1);
                Wallet.AddCredits(credits);
            }

            return result;
        }
    }
}