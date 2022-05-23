using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Spaceships.Economy
{
    public class Wallet : MonoBehaviour
    {
        [ShowNativeProperty] public static int Credits { get; private set; }
        public static UnityEvent<int> OnCreditsChange { get; } = new UnityEvent<int>();

        private void Start()
        {
            Credits = 100000; // Temporary for testing
        }

        public static void AddCredits(int amount)
        {
            Credits += amount;
            OnCreditsChange.Invoke(amount);
        }

        public static void RemoveCredits(int amount)
        {
            Credits -= amount;
            Credits = Mathf.Max(Credits, 0);
            OnCreditsChange.Invoke(-amount);
        }
    }
}