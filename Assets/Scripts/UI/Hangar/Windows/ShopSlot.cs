using Spaceships.Entities;
using Spaceships.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Spaceships.UI.Hangar.Windows
{
    [RequireComponent(typeof(Button))]
    public class ShopSlot : Element
    {
        [HideInInspector] public UnityEvent onClick = new UnityEvent();

        [SerializeField] private Text titleText;
        [SerializeField] private Text tierText;
        [SerializeField] private Text costText;

        private Button button;
        public ShipData ShipData { get; private set; }


        public void Setup(ShipData shipData)
        {
            button = GetComponent<Button>();
            ShipData = shipData;

            titleText.text = shipData.Name;
            tierText.text = $"Tier {RomanNumeral.Convert(shipData.Tier)}";
            costText.text = $"CR {shipData.CreditCost.ToString()}";
            button.onClick.AddListener(() => onClick.Invoke());
        }
    }
}