using Spaceships.Economy;
using UnityEngine;
using UnityEngine.UI;

namespace Spaceships.UI.Hangar
{
    public class CreditsText : MonoBehaviour
    {
        [SerializeField] private Text text;

        private void Update()
        {
            text.text = $"CR {Wallet.Credits}";
        }
    }
}