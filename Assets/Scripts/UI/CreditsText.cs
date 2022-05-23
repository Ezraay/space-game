using System;
using Spaceships.Economy;
using UnityEngine;
using UnityEngine.UI;

namespace Spaceships.UI.Hangar
{
    public class CreditsText : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private Vector2 direction;
        [SerializeField] private CreditsChangeText creditsChangeText;
        private const float Speed = 100;
        private const float FadeTime = 1.5f;
        private const float Offset = 50;

        private void Start()
        {
            Wallet.OnCreditsChange.AddListener(OnCreditsChange);
        }

        private void OnDestroy()
        {
            Wallet.OnCreditsChange.RemoveListener(OnCreditsChange);
        }

        private void OnCreditsChange(int change)
        {
            Vector3 position = transform.position + (Vector3)direction * Offset;
            CreditsChangeText newText = Instantiate(creditsChangeText, position, Quaternion.identity, transform.parent);
            newText.Setup(change, Speed, FadeTime, direction);
            text.text = $"CR {Wallet.Credits}";
        }
    }
}