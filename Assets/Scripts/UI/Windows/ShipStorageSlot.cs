using Spaceships.Entities;
using Spaceships.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Spaceships.UI.Windows
{
    public class ShipStorageSlot : MonoBehaviour
    {
        [HideInInspector] public UnityEvent onClick = new UnityEvent();
        
        [SerializeField] private Text titleText;
        [SerializeField] private Text tierText;
        [SerializeField] private Text equippedText;

        private Button button;
        private ShipData shipData;
        

        public void Setup(ShipData shipData)
        {
            button = GetComponent<Button>();
            this.shipData = shipData;
            
            titleText.text = shipData.Name;
            tierText.text = $"Tier {RomanNumeral.Convert(shipData.Tier)}";
            button.onClick.AddListener(() => onClick.Invoke());
            UnsetActive();
        }

        public void SetActive()
        {
            equippedText.gameObject.SetActive(true);
        }

        public void UnsetActive()
        {
            equippedText.gameObject.SetActive(false);
        }
    }
}