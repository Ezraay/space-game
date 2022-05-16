using System.Collections.Generic;
using UnityEngine;

namespace Spaceships.ItemSystem.Items
{
    public class ItemFactory : MonoBehaviour
    {
        private static readonly Dictionary<string, ItemData> ItemData = new Dictionary<string, ItemData>();
        [SerializeField] private List<ItemData> data;

        private void Start()
        {
            foreach (ItemData item in data)
            {
                ItemData.Add(item.ID, item);
            }
        }

        public static Item CreateItem(string id, int count = 1)
        {
            if (!ItemData.ContainsKey(id))
                Debug.LogError("No such item ID: " + id);
            ItemData data = ItemData[id];
            Item item = new Item(data, count);
            return item;
        }

        public static Item CreateItem(ItemData data, int count = 1)
        {
            return CreateItem(data.ID, count);
        }
    }
}