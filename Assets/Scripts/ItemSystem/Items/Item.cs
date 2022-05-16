using UnityEngine;

namespace Spaceships.ItemSystem.Items
{
    public class Item
    {
        private readonly ItemData data;
        public string ID => data.ID;
        public string Name => data.Name;
        public Sprite Sprite => data.Sprite;
        public int Count { get; private set; }

        public float Weight => data.Weight;
        public float TotalWeight => Count * Weight;

        public Item(ItemData data, int count)
        {
            this.data = data;
            Count = count;
        }

        public int AddCount(int count)
        {
            Count += count;
            return Count;
        }

        public int RemoveCount(int count)
        {
            Count = Mathf.Max(0, Count - count);
            return Count;
        }
    }
}