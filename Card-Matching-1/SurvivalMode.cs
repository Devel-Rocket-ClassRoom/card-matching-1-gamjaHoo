using System;

namespace CardMatching
{
    class SurvivalMode : IGameMode
    {
        public void Run(GameManager game)
        {
            while (!game.GameClear())
            {
                game.PrintBoard();

                Console.WriteLine($"\n연속 실패: {game.FailRowCount}/{game.MaxFailRow}");

                game.PlayTurn();

                if (game.FailRowCount >= game.MaxFailRow)
                {
                    Console.WriteLine("\n게임 오버");
                    return;
                }
            }

            Console.WriteLine("\n게임 클리어!");
        }
    }
}