using UnityEngine;

namespace Spaceships.ItemSystem.Items
{
    [CreateAssetMenu(menuName = "Create Item", fileName = "New Item", order = 0)]
    public class ItemData : ScriptableObject
    {
        public float Weight => weight;
        public string Name => name;
        public string ID => id;
        public Sprite Sprite => sprite;
        public int SellCost => sellCost;

        [SerializeField] private string id = "new_item";
        [SerializeField] private new string name = "New Item";
        [SerializeField] private float weight = 1;
        [SerializeField] private Sprite sprite;
        [SerializeField] private int sellCost = 1;
    }
}