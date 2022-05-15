using System;
using Spaceships.Environment;
using UnityEngine;
using UnityEngine.Events;

namespace Spaceships.ItemSystem.Items
{
    public class ItemContainer : Interactable
    {
        [SerializeField] private float interactRadius = 2;
        [SerializeField] private float rotateSpeed = 1;
        public ContainerInventory Inventory { get; private set; }
        public static UnityEvent<ItemContainer> OnInteract { get; } = new UnityEvent<ItemContainer>();
        public override string InteractText => "Loot container";
        public override float InteractRadius => interactRadius;

        private void Start()
        {
            // foreach (Item item in inventory.GetItems())
            // {
            //     Debug.Log(item.Name); // Testing purposes
            // }
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, interactRadius);
        }

        public override void Interact()
        {
            if (Inventory == null)
                Debug.LogError("Inventory has not been set before interacting.");
            OnInteract.Invoke(this);
        }

        private void OnDestroy()
        {
            Inventory.OnItemRemoved.RemoveListener(OnItemRemoved);
        }

        public void SetInventory(ContainerInventory inventory)
        {
            Inventory = inventory;
            Inventory.OnItemRemoved.AddListener(OnItemRemoved);
        }

        private void OnItemRemoved(int index, Item item)
        {
            if (Inventory.GetItems().Count == 0) 
                Destroy(gameObject);
        }
    }
}