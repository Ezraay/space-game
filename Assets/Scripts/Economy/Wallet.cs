using System;
using UnityEngine;

namespace Spaceships.Economy
{
    public class Wallet : MonoBehaviour
    {
        public static int Credits => credits;

        private static int credits;

        private void Start()
        {
            credits = 100000; // Temporary for testing
        }

        public static void AddCredits(int amount)
        {
            credits += amount;
        }

        public static void RemoveCredits(int amount)
        {
            credits -= amount;
            credits = Mathf.Max(credits, 0);
        }
    }
}