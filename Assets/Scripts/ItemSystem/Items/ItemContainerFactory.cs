using System.Collections.Generic;
using Spaceships.Entities;
using UnityEngine;

namespace Spaceships.ItemSystem.Items
{
    public class ItemContainerFactory : MonoBehaviour
    {
        public static ItemContainerFactory instance;
        [SerializeField] private ItemContainer itemContainerPrefab;

        private void Start()
        {
            instance = this;
        }

        public static ItemContainer CreateContainer(ContainerInventory inventory, Vector3 position, Ship ship)
        {
            ItemContainer container = Instantiate(instance.itemContainerPrefab, position, Quaternion.identity);
            container.SetInventory(inventory);

            return container;
        }

        public static ItemContainer CreateContainer(List<Item> items, Vector3 position, Ship ship)
        {
            ContainerInventory inventory = new ContainerInventory(items, ship);
            return CreateContainer(inventory, position, ship);
        }
    }
}