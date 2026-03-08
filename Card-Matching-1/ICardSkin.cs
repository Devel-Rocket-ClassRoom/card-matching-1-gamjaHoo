using System;

namespace CardMatching
{
    interface ICardSkin
    {
        string GetDisplay(int value);
        ConsoleColor GetColor(int value);
    }
}