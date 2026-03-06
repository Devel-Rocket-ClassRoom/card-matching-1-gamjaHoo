using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;

namespace CardMatching
{
    internal class GameManager
    {
        private Card[,] board;

        int row;
        int col;

        int tryCount;
        int maxTryCount;
        int matchCount;

        int failRowCount;
        bool failedBefore = false;
        int maxFailRow;

        int timeLimitSeconds;
        DateTime startTime;

        Difficulty difficulty;
        Skin skin;
        Mode mode;
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
        BoardSize boardSize;


        public GameManager(Difficulty diff, Skin skin, Mode mode, BoardSize boardSize)
        {
            board = new Card[(int)boardSize, 4];
            row = (int)boardSize;
            col = 4;
            this.skin = skin;
            difficulty = diff;
            switch (difficulty)
            {
                case Difficulty.Easy:
                    maxTryCount = 10;
                    maxFailRow = 10;
                    timeLimitSeconds = 60;
                    break;
                case Difficulty.Normal:
                    maxTryCount = 20;
                    maxFailRow = 5;
                    timeLimitSeconds = 90;
                    break;
                case Difficulty.Hard:
                    maxTryCount = 30;
                    maxFailRow = 3;
                    timeLimitSeconds = 120;
                    break;
                default:
                    maxTryCount = 20;
                    maxFailRow = 5;
                    timeLimitSeconds = 90;
                    break;
            }

            this.mode = mode;
            this.boardSize = boardSize;
        }

        public void StartGame()
        {
            if(mode == Mode.Classic)
            {
                ClassicMode();
            }
            else if(mode == Mode.TimeAttack)
            {

            }
            else if(mode == Mode.Survival)
            {
                SurvivalMode();
            }
        }

        private void ShuffleCards()
        {
            List<int> numbers = new List<int>();
            for(int i = 0; i < row * col / 2; i++)
            {
                numbers.Add(i);
                numbers.Add(i);
            }

            Random rand = new Random();

            for(int i = numbers.Count - 1;i > 0; i--)
            {
                int j = rand.Next(i+1);
                int temp = numbers[i];
                numbers[i] = numbers[j];
                numbers[j] = temp;
            }

            int index = 0;

            for (int i = 0; i < row; i++)
            {
                for(int j = 0; j < col; j++)
                {
                    board[i, j] = new Card(numbers[index++], skin);
                }
            }
            Console.WriteLine("카드를 섞는 중...");
            Thread.Sleep(700);
        }

        private void PrintBoard()
        {
            Console.Clear();
            Console.Write("\n    ");

            for (int i = 0; i < col; i++)
            {
                Console.Write($" {i+1}열");
            }
            Console.WriteLine();

            for(int i = 0; i < row; i++)
            {
                Console.Write($"{i+1}행  ");

                for(int j = 0; j < col; j++)
                {
                    Card card = board[i, j];

                    if(card.Matched || card.Opened)
                    {
                        if(skin != Skin.Number)
                        {
                            Console.ForegroundColor = GetColor(card.Number);
                        }
                        if (card.Matched)
                        {
                            Console.Write($" {card} ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($" [{card}] ");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.Write(" ** ");
                    }
                }
                Console.WriteLine();
            }
        }
        public void PlayTurn()
        {
            Console.Write("\n첫 번째 카드 선택 (행 열): ");
            var first = GetValidCardInput();
            int r1 = first.Item1;
            int c1 = first.Item2;

            board[r1, c1].Opened = true;
            PrintBoard();

            Console.Write("\n두 번째 카드 선택 (행 열): ");
            var second = GetValidCardInput();
            int r2 = second.Item1;
            int c2 = second.Item2;

            board[r2, c2].Opened = true;
            PrintBoard();

            tryCount++;

            if (CheckMatch(r1, c1, r2, c2))
            {
                Console.WriteLine("\n짝을 찾았습니다!");

                board[r1, c1].Matched = true;
                board[r2, c2].Matched = true;

                matchCount++;
                failedBefore = true;
            }
            else
            {
                Console.WriteLine("\n짝이 맞지 않습니다!");
                failedBefore = true;
                failRowCount++;

                Thread.Sleep(1000);

                board[r1, c1].Opened = false;
                board[r2, c2].Opened = false;
            }
        }

        private bool CheckValidRow(int number)
        {
            return number < row;
        }
        private bool CheckValidCol(int number)
        {
            return number < col;
        }
        private bool CheckMatch(int r1, int c1, int r2, int c2)
        {
            return board[r1, c1].Number == board[r2, c2].Number;
        }
        public bool GameClear()
        {
            return matchCount == row * col / 2;
        }
        private void PrintTryGameOver()
        {
            Console.WriteLine("횟수제한 게임오버!");
            tryCount = 0;
            Thread.Sleep(800);
        }
        private void PrintTimeGameOver()
        {
            Console.WriteLine("\n⏰ 시간 초과! 게임 오버!");
            Thread.Sleep(800);
        }

        private void PrintSurvivalGameOver()
        {
            Console.WriteLine($"연속으로 {maxFailRow}번 틀렸습니다. 게임오버!");
            failRowCount = 0;
            Thread.Sleep(800);
        }

        private void startShow()
        {
            for(int i = 0; i < row; i++)
            {
                for(int j  = 0; j < col; j++)
                {
                    board[i,j].Matched = true;
                }
            }
            PrintBoard();
            switch (difficulty)
            {
                case Difficulty.Easy:
                    Console.WriteLine("\n잘 기억하세요! (5초 후 뒤집힙니다)");
                    Thread.Sleep(5000);
                    break;
                case Difficulty.Normal:
                    Console.WriteLine("\n잘 기억하세요! (3초 후 뒤집힙니다)");
                    Thread.Sleep(3000);
                    break;
                case Difficulty.Hard:
                    Console.WriteLine("\n잘 기억하세요! (2초 후 뒤집힙니다)");
                    Thread.Sleep(2000);
                    break;
                default:
                    Console.WriteLine("\n잘 기억하세요! (3초 후 뒤집힙니다)");
                    Thread.Sleep(3000);
                    break;
            }
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    board[i, j].Matched = false;
                }
            }
            PrintBoard();
        }
        private (int, int) GetValidCardInput()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.Write("입력이 비어있습니다. 다시 입력: ");
                    continue;
                }

                string[] parts = input.Split();

                if (parts.Length != 2)
                {
                    Console.Write("행과 열을 입력하세요 (예: 1 3): ");
                    continue;
                }

                if (!int.TryParse(parts[0], out int r) || !int.TryParse(parts[1], out int c))
                {
                    Console.Write("숫자를 입력하세요: ");
                    continue;
                }

                r--;
                c--;

                if (r < 0 || r >= row || c < 0 || c >= col)
                {
                    Console.Write("범위를 벗어났습니다. 다시 입력: ");
                    continue;
                }

                if (board[r, c].Matched)
                {
                    Console.Write("이미 맞춘 카드입니다. 다시 선택: ");
                    continue;
                }

                if (board[r, c].Opened)
                {
                    Console.Write("이미 뒤집힌 카드입니다. 다시 선택: ");
                    continue;
                }

                return (r, c);
            }
        }
        private bool AskRestart()
        {
            while (true)
            {
                Console.Write("\n새 게임을 하시겠습니까? (Y / N): ");

                string input = Console.ReadLine().ToLower();

                if (input == "y")
                    return true;

                if (input == "n")
                    return false;

                Console.WriteLine("Y 또는 N 으로 입력해주세요.");
            }
        }
        private ConsoleColor GetColor(int cardValue)
        {
            return colors[cardValue % 8];
        }
        private void ClassicMode()
        {
            bool gameFlag = true;

            while (gameFlag)
            {
                tryCount = 0;
                matchCount = 0;

                ShuffleCards();
                startShow();

                while (matchCount < row * col / 2)
                {
                    PrintBoard();

                    Console.WriteLine($"\n시도 횟수: {tryCount} | 찾은 쌍: {matchCount} / {board.Length / 2}");

                    PlayTurn();

                    if (tryCount > maxTryCount)
                    {
                        PrintTryGameOver();
                        break;
                    }
                }

                gameFlag = AskRestart();

            }
        }

        private void TimeAttackMode()
        {
            bool gameFlag = true;
            while (gameFlag)
            {
                startTime = DateTime.Now;

                tryCount = 0;
                matchCount = 0;

                ShuffleCards();
                startShow();


                while (matchCount < row * col / 2)
                {
                    TimeSpan elapsed = DateTime.Now - startTime;
                    //int remaining = timeLimitSeconds - (int)elapsed.TotalSeconds;

                    //if (remaining < 0) remaining = 0;

                    PrintBoard();
                    Console.WriteLine($"\n남은 시간: {DateTime.Now - startTime}초 / {timeLimitSeconds} | 찾은 쌍: {matchCount} / {board.Length / 2}");

                    if (elapsed.TotalSeconds >= timeLimitSeconds)
                    {
                        PrintTimeGameOver();
                        break;
                    }
                    PlayTurn();
                }

                gameFlag = AskRestart();
            }
        }

        private void SurvivalMode()
        {
            bool gameFlag = true;

            while (gameFlag)
            {
                tryCount = 0;
                matchCount = 0;

                ShuffleCards();
                startShow();

                while (matchCount < row * col / 2)
                {
                    PrintBoard();

                    Console.WriteLine($"\n연속 실패: {failRowCount} / {maxFailRow} | 찾은 쌍: {matchCount} / {board.Length / 2}");

                    PlayTurn();

                    if (failRowCount >= maxFailRow)
                    {
                        PrintSurvivalGameOver();
                        break;
                    }
                }

                gameFlag = AskRestart();

            }
        }
    }
}
