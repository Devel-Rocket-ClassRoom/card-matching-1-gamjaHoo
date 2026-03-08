using System;
using System.Collections.Generic;
using System.Threading;

namespace CardMatching
{
    class GameManager
    {
        private Card[,] board;
        private int row;
        private int col = 4;
        private Difficulty difficulty;

        public int TryCount { get; private set; }
        public int MatchCount { get; private set; }
        public int FailRowCount { get; private set; }

        public int MaxTryCount { get; private set; }
        public int MaxFailRow { get; private set; }
        public int TimeLimitSeconds { get; private set; }

        public int TotalPairs => row * col / 2;

        private ICardSkin skin;
        private IGameMode mode;

        public GameManager(Difficulty difficulty, BoardSize size, ICardSkin skin, IGameMode mode)
        {
            row = (int)size;
            board = new Card[row, col];

            this.skin = skin;
            this.mode = mode;
            this.difficulty = difficulty;

            if (size == BoardSize.Small)
            {
                if (difficulty == Difficulty.Easy)
                {
                    MaxTryCount = 12;
                    MaxFailRow = 6;
                    TimeLimitSeconds = 60;
                }
                else if (difficulty == Difficulty.Normal)
                {
                    MaxTryCount = 9;
                    MaxFailRow = 5;
                    TimeLimitSeconds = 50;
                }
                else
                {
                    MaxTryCount = 6;
                    MaxFailRow = 4;
                    TimeLimitSeconds = 40;
                }
            }
            else if (size == BoardSize.Medium)
            {
                if (difficulty == Difficulty.Easy)
                {
                    MaxTryCount = 20;
                    MaxFailRow = 8;
                    TimeLimitSeconds = 90;
                }
                else if (difficulty == Difficulty.Normal)
                {
                    MaxTryCount = 15;
                    MaxFailRow = 6;
                    TimeLimitSeconds = 75;
                }
                else
                {
                    MaxTryCount = 10;
                    MaxFailRow = 4;
                    TimeLimitSeconds = 60;
                }
            }
            else
            {
                if (difficulty == Difficulty.Easy)
                {
                    MaxTryCount = 30;
                    MaxFailRow = 10;
                    TimeLimitSeconds = 120;
                }
                else if (difficulty == Difficulty.Normal)
                {
                    MaxTryCount = 24;
                    MaxFailRow = 7;
                    TimeLimitSeconds = 100;
                }
                else
                {
                    MaxTryCount = 18;
                    MaxFailRow = 5;
                    TimeLimitSeconds = 80;
                }
            }
        }

        public void StartGame()
        {
            ShuffleCards();
            Preview();
            mode.Run(this);
        }

        public void ShuffleCards()
        {
            List<int> numbers = new List<int>();

            for (int i = 0; i < TotalPairs; i++)
            {
                numbers.Add(i);
                numbers.Add(i);
            }

            Random rand = new Random();

            for (int i = numbers.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                int temp = numbers[i];
                numbers[i] = numbers[j];
                numbers[j] = temp;
            }

            int index = 0;

            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                    board[i, j] = new Card(numbers[index++], skin);
        }

        public void Preview()
        {
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                    board[i, j].Matched = true;

            PrintBoard();

            int previewTime = 3;

            switch (difficulty)
            {
                case Difficulty.Easy:
                    previewTime = 5;
                    break;

                case Difficulty.Normal:
                    previewTime = 3;
                    break;

                case Difficulty.Hard:
                    previewTime = 2;
                    break;
            }

            Console.WriteLine($"\n잘 기억하세요! ({previewTime}초 후 뒤집힙니다)");

            Thread.Sleep(previewTime * 1000);

            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                    board[i, j].Matched = false;

            PrintBoard();
        }

        public void PrintBoard()
        {
            Console.Clear();

            Console.Write("\n    ");
            for (int i = 0; i < col; i++)
                Console.Write($" {i + 1}열");
            Console.WriteLine();

            for (int i = 0; i < row; i++)
            {
                Console.Write($"{i + 1}행 ");

                for (int j = 0; j < col; j++)
                {
                    Card card = board[i, j];

                    if (card.Matched || card.Opened)
                    {
                        Console.ForegroundColor = skin.GetColor(card.Number);

                        if (card.Matched)
                            Console.Write($" {skin.GetDisplay(card.Number)} ");
                        else
                            Console.Write($"[{skin.GetDisplay(card.Number)}]");

                        Console.ResetColor();
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
            Console.Write("\n첫 카드 선택 (행 열): ");
            var first = GetInput();

            board[first.r, first.c].Opened = true;
            PrintBoard();

            Console.Write("\n두 번째 카드 선택 (행 열): ");
            var second = GetInput();

            board[second.r, second.c].Opened = true;
            PrintBoard();

            TryCount++;

            if (board[first.r, first.c].Number == board[second.r, second.c].Number)
            {
                board[first.r, first.c].Matched = true;
                board[second.r, second.c].Matched = true;
                MatchCount++;
                FailRowCount = 0;

                Console.WriteLine("\n짝을 찾았습니다!");
            }
            else
            {
                FailRowCount++;
                Console.WriteLine("\n짝이 맞지 않습니다!");
                Thread.Sleep(1000);

                board[first.r, first.c].Opened = false;
                board[second.r, second.c].Opened = false;
            }
        }

        private (int r, int c) GetInput()
        {
            while (true)
            {
                string[] input = Console.ReadLine().Split();

                if (input.Length != 2) continue;

                if (int.TryParse(input[0], out int r) &&
                    int.TryParse(input[1], out int c))
                {
                    r--;
                    c--;

                    if (r >= 0 && r < row && c >= 0 && c < col)
                    {
                        if (!board[r, c].Matched && !board[r, c].Opened)
                            return (r, c);
                    }
                }
            }
        }

        public bool GameClear()
        {
            return MatchCount == TotalPairs;
        }
    }
}