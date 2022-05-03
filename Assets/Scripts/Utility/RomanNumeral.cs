using System.Collections.Generic;

namespace Spaceships.Utility
{
    public static class RomanNumeral
    {
        private static readonly Dictionary<int, string> conversion = new Dictionary<int, string>
        {
            {1, "I"}, {2, "II"}, {3, "III"}, {4, "IV"}, {5, "V"},
            {6, "VI"}, {7, "VII"}, {8, "VIII"}, {9, "IX"}, {10, "X"},
        };

        public static string Convert(int value)
        {
            // Converts a tier 1-10 to a roman numeral I-X
            return conversion[value];
        }
    }
}