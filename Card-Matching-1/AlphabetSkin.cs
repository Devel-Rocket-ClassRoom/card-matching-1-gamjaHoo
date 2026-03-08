using System;

namespace CardMatching
{
    class AlphabetSkin : ICardSkin
    {
        ConsoleColor[] colors =
        {
            ConsoleColor.Yellow,
            ConsoleColor.Blue,
            ConsoleColor.Red,
            ConsoleColor.Green,
            ConsoleColor.Cyan,
            ConsoleColor.Magenta,
            ConsoleColor.White,
            ConsoleColor.DarkYellow
        };

        public string GetDisplay(int value)
        {
            return ((char)('A' + value)).ToString();
        }

        public ConsoleColor GetColor(int value)
        {
            return colors[value % colors.Length];
        }
    }
}