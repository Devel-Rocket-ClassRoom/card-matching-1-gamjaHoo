using System;

namespace CardMatching
{
    class TimeAttackMode : IGameMode
    {
        public void Run(GameManager game)
        {
            DateTime start = DateTime.Now;

            while (!game.GameClear())
            {
                int remain = game.TimeLimitSeconds - (int)(DateTime.Now - start).TotalSeconds;

                if (remain <= 0)
                {
                    Console.WriteLine("\n시간 초과! 게임 오버");
                    return;
                }

                game.PrintBoard();

                Console.WriteLine($"\n남은 시간: {remain}초 | 찾은 쌍: {game.MatchCount}/{game.TotalPairs}");

                game.PlayTurn();
            }

            Console.WriteLine("\n게임 클리어!");
        }
    }
}