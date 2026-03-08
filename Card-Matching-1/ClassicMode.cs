using System;

namespace CardMatching
{
    class ClassicMode : IGameMode
    {
        public void Run(GameManager game)
        {
            while (!game.GameClear())
            {
                game.PrintBoard();

                Console.WriteLine($"\n시도: {game.TryCount}/{game.MaxTryCount} | 찾은 쌍: {game.MatchCount}/{game.TotalPairs}");

                game.PlayTurn();

                if (game.TryCount >= game.MaxTryCount)
                {
                    Console.WriteLine("\n게임 오버");
                    return;
                }
            }

            Console.WriteLine("\n게임 클리어!");
        }
    }
}