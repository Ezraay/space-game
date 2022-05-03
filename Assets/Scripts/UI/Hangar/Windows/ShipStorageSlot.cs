using Spaceships.Entities;
using Spaceships.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Spaceships.UI.Hangar.Windows
{
    public class ShipStorageSlot : MonoBehaviour
    {
        [HideInInspector] public UnityEvent onClick = new UnityEvent();

        [SerializeField] private Text titleText;
        [SerializeField] private Text tierText;
        [SerializeField] private Text equippedText;

        private Button button;


        public void Setup(ShipData shipData)
        {
            button = GetComponent<Button>();

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