using System;

namespace CardMatching
{
    class IconSkin : ICardSkin
    {
        string[] icons =
        {
            "★","♠","♥","♦","♣","●","■","▲"
        };

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
            return icons[value % icons.Length];
        }

        public ConsoleColor GetColor(int value)
        {
            return colors[value % colors.Length];
        }
    }
}