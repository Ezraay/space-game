using Spaceships.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Spaceships.UI.Windows
{
    public class ShopSlot : Element
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text tierText;
        [SerializeField] private TMP_Text costText;

        public void Setup(ShipData shipData)
        {
            Debug.Log("Test");
            titleText.text = shipData.Name;
            tierText.text = $"Tier {shipData.Tier}";
            costText.text = shipData.CreditCost.ToString();
            icon.sprite = shipData.Sprite;
            costText.gameObject.SetActive(false);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            titleText.gameObject.SetActive(false);
            costText.gameObject.SetActive(true);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            titleText.gameObject.SetActive(true);
            costText.gameObject.SetActive(false);
        }
    }
}