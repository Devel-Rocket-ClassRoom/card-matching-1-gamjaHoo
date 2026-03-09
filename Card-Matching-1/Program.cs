using System;

namespace CardMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 카드 짝 맞추기 게임 ===");


            IGameMode mode = ChooseMode();
            Difficulty difficulty = ChooseDifficulty();
            BoardSize boardSize = ChooseBoardSize();
            ICardSkin skin = ChooseSkin();

            GameManager gm = new GameManager(difficulty, boardSize, skin, mode);
            gm.StartGame();
        }

        static Difficulty ChooseDifficulty()
        {
            while (true)
            {
                Console.Write("\n난이도 선택\n1. 쉬움\n2. 보통\n3. 어려움\n선택: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": return Difficulty.Easy;
                    case "2": return Difficulty.Normal;
                    case "3": return Difficulty.Hard;
                }
            }
        }

        static BoardSize ChooseBoardSize()
        {
            while (true)
            {
                Console.Write("\n보드 크기 선택\n1. 2x4\n2. 4x4\n3. 6x4\n선택: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": return BoardSize.Small;
                    case "2": return BoardSize.Medium;
                    case "3": return BoardSize.Large;
                }
            }
        }

        static ICardSkin ChooseSkin()
        {
            while (true)
            {
                Console.Write("\n스킨 선택\n1. 숫자\n2. 알파벳\n3. 기호\n선택: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": return new NumberSkin();
                    case "2": return new AlphabetSkin();
                    case "3": return new IconSkin();
                }
            }
        }

        static IGameMode ChooseMode()
        {
            while (true)
            {
                Console.Write("\n모드 선택\n1. 클래식\n2. 타임어택\n3. 서바이벌\n선택: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": return new ClassicMode();
                    case "2": return new TimeAttackMode();
                    case "3": return new SurvivalMode();
                }
            }
        }
    }
}