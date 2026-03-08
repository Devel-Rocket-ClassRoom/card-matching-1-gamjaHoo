using System;

namespace CardMatching
{
    class NumberSkin : ICardSkin
    {
        public string GetDisplay(int value)
        {
            return value.ToString();
        }

        public ConsoleColor GetColor(int value)
        {
            return ConsoleColor.White;
        }
    }
}