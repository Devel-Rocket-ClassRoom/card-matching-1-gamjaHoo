namespace CardMatching
{
    class Card
    {
        public int Number { get; set; }
        public bool Matched { get; set; }
        public bool Opened { get; set; }

        public ICardSkin Skin { get; }

        public Card(int number, ICardSkin skin)
        {
            Number = number;
            Skin = skin;
        }
    }
}