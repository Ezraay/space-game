﻿using System;
using Spaceships.Entities;
using Spaceships.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Spaceships.UI.Windows
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
            this.ShipData = shipData;
            
            titleText.text = shipData.Name;
            tierText.text = $"Tier {RomanNumeral.Convert(shipData.Tier)}";
            costText.text = shipData.CreditCost.ToString();
            button.onClick.AddListener(() => onClick.Invoke());
        }
    }
}