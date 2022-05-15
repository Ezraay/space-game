using System.Collections.Generic;
using Spaceships.ItemSystem.Items;
using UnityEngine.Events;

namespace Spaceships.ItemSystem
{
    public class Inventory<T> where T : Item
    {
        public UnityEvent<int, Item> OnItemAdded { get; }= new UnityEvent<int, Item>();
        public UnityEvent<int, Item> OnItemRemoved { get; } = new UnityEvent<int, Item>();
        protected List<T> items = new List<T>();

        public virtual string Name => "Inventory";

        public virtual void AddItem(T item)
        {
            if (!CanAddItem(item))
                return;
            items.Add(item);
            OnItemAdded.Invoke(items.Count-1, item);
        }

        public virtual void RemoveItem(T item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == item)
                {
                    items.Remove(item);
                    OnItemRemoved.Invoke(i, item);
                }
            }
        }

        public virtual bool ContainsItem(string id, int count)
        {
            foreach (T item in items)
            {
                if (item.ID == id)
                    return true;
            }

            return false;
        }

        public virtual bool CanAddItem(T item)
        {
            return true;
        }

        public T GetItem(int index)
        {
            return items[index];
        }

        public List<T> GetItems()
        {
            return items;
        }
    }
}