using NaughtyAttributes;
using UnityEngine;

namespace Spaceships.Economy
{
    public class Wallet : MonoBehaviour
    {
        [ShowNativeProperty]
        public static int Credits { get; private set; }

        private void Start()
        {
            Credits = 100000; // Temporary for testing
        }

        public static void AddCredits(int amount)
        {
            Credits += amount;
        }

        public static void RemoveCredits(int amount)
        {
            Credits -= amount;
            Credits = Mathf.Max(Credits, 0);
        }
    }
}