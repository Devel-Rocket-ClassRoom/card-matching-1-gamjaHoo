using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CardMatching
{
    internal class Card
    {
        public int Number {  get; set; }
        public bool Matched { get; set; } = false;
        public bool Opened { get; set; } = false;

        public Skin Skin { get; set; } = Skin.Number;
        static string[] icons =
        {
            "★", "♠", "♥", "♦", "♣", "●", "■", "▲"
        };
        public Card(int  number, Skin skin)
        {
            Number = number;
            Skin = skin;
        }
        private string ConvertToAlphabet(int number)
        {
            return ((char)('A' + number)).ToString();
        }

        public override string ToString()
        {
            switch (Skin)
            {
                case Skin.Number:
                    return Number.ToString();

                case Skin.Alphabet:
                    return ConvertToAlphabet(Number);

                case Skin.Icon:
                    return icons[Number % icons.Length];

                default:
                    return Number.ToString();
            }
        }
    }
}
