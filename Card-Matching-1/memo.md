while(end flag)
{
카드 shuffle
화면 출력


}

class

카드 class -> 숫자 필드, 쌍이 맞춰졌는지 bool
게임매니저 class -> 게임 클리어했는지, 시도 횟수 저장, 찾은 쌍 개수 저장, 카드 배열
				화면 출력메서드


필요한거 
현재 테이블 위의 카드 배열
해당 카드들


Program
 └ GameManager
      ├ Card[,] board
      ├ ShuffleCards()
      ├ PrintBoard()
      ├ CheckMatch()
      ├ StartGame()

Card
 ├ Number
 ├ Opened
 └ Matched


using System;
using System.Collections.Generic;
using System.Threading;

class GameManager
{
    Card[,] board = new Card[4, 4];

    int tryCount = 0;
    int pairCount = 0;

    public void StartGame()
    {
        ShuffleCards();

        while (pairCount < 8)
        {
            PrintBoard();

            Console.WriteLine($"\n시도 횟수: {tryCount} | 찾은 쌍: {pairCount}/8");

            Console.Write("\n첫 번째 카드 선택 (행 열): ");
            int r1 = int.Parse(Console.ReadLine().Split()[0]) - 1;
            int c1 = int.Parse(Console.ReadLine().Split()[0]) - 1;
        }
    }

    private void ShuffleCards()
    {
        List<int> numbers = new List<int>();

        for (int i = 1; i <= 8; i++)
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

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                board[i, j] = new Card(numbers[index++]);
            }
        }
    }

    private void PrintBoard()
    {
        Console.WriteLine("\n    1열 2열 3열 4열");

        for (int i = 0; i < 4; i++)
        {
            Console.Write($"{i + 1}행 ");

            for (int j = 0; j < 4; j++)
            {
                Card card = board[i, j];

                if (card.Matched || card.Opened)
                    Console.Write($" {card.Number} ");
                else
                    Console.Write(" ** ");
            }

            Console.WriteLine();
        }
    }

    public void PlayTurn()
    {
        Console.Write("\n첫 번째 카드 선택 (행 열): ");
        var input1 = Console.ReadLine().Split();
        int r1 = int.Parse(input1[0]) - 1;
        int c1 = int.Parse(input1[1]) - 1;

        board[r1, c1].Opened = true;
        PrintBoard();

        Console.Write("\n두 번째 카드 선택 (행 열): ");
        var input2 = Console.ReadLine().Split();
        int r2 = int.Parse(input2[0]) - 1;
        int c2 = int.Parse(input2[1]) - 1;

        board[r2, c2].Opened = true;
        PrintBoard();

        tryCount++;

        if (CheckMatch(r1, c1, r2, c2))
        {
            Console.WriteLine("\n짝을 찾았습니다!");

            board[r1, c1].Matched = true;
            board[r2, c2].Matched = true;

            pairCount++;
        }
        else
        {
            Console.WriteLine("\n짝이 맞지 않습니다!");

            Thread.Sleep(1500);

            board[r1, c1].Opened = false;
            board[r2, c2].Opened = false;
        }
    }

    private bool CheckMatch(int r1, int c1, int r2, int c2)
    {
        return board[r1, c1].Number == board[r2, c2].Number;
    }

    public bool IsGameClear()
    {
        return pairCount == 8;
    }
}

class Card
{
    public int Number { get; set; }
    public bool Opened { get; set; }
    public bool Matched { get; set; }

    public Card(int number)
    {
        Number = number;
        Opened = false;
        Matched = false;
    }
}