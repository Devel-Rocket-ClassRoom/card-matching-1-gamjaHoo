using System;
using System.Threading;

namespace CardMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 카드 짝 맞추기 게임 ===");
            bool gameFlagInMain = true;

            Mode mode = new Mode();
            ChooseMode();

            Difficulty difficulty;
            ChooseDifficulty();


            Skin skin = new Skin();
            ChooseSkin();

            
            GameManager gm = new GameManager((int)difficulty, skin, mode);
            Thread.Sleep(1000);
            gm.StartGame();








            void ChooseMode()
            {
                Console.Write("\n모드를 선택하세요: \n1. 클래식 \n2. 타임어택 \n3. 서바이벌 \n선택: ");
                string input;
                while (true)
                {
                    input = Console.ReadLine();
                    if (int.TryParse(input, out int result))
                    {
                        if (result < 1 || result > 3)
                        {
                            Console.WriteLine("1-3 사이의 숫자를 입력하세요!");
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("숫자를 입력하세요!");
                    }
                }
                switch (input)
                {

                    case "1":
                        Console.WriteLine("클래식 모드를 선택하셨습니다!");
                        mode = Mode.Classic;
                        break;
                    case "2":
                        Console.WriteLine("타임어택 모드를 선택하셨습니다!");
                        mode = Mode.TimeAttack;
                        break;
                    case "3":
                        Console.WriteLine("서바이벌 모드를 선택하셨습니다!");
                        mode = Mode.Survival;
                        break;
                    default:
                        mode = Mode.Classic;
                        break;
                }
            }


            void ChooseDifficulty()
            {
                Console.Write("\n난이도를 선택하세요: \n1. 쉬움 (2x4)\n2. 보통 (4x4)\n3. 어려움 (6x4)\n선택: ");
                string input;
                while (true)
                {
                    input = Console.ReadLine();
                    if (int.TryParse(input, out int result))
                    {
                        if (result < 1 || result > 3)
                        {
                            Console.WriteLine("1-3 사이의 숫자를 입력하세요!");
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("숫자를 입력하세요!");
                    }
                }
                switch (input)
                {

                    case "1":
                        Console.WriteLine("쉬움 난이도를 선택하셨습니다!");
                        difficulty = Difficulty.Easy;
                        break;
                    case "2":
                        Console.WriteLine("보통 난이도를 선택하셨습니다!");
                        difficulty = Difficulty.Normal;
                        break;
                    case "3":
                        Console.WriteLine("어려움 난이도를 선택하셨습니다!");
                        difficulty = Difficulty.Hard;
                        break;
                    default:
                        difficulty = Difficulty.Normal;
                        break;
                }
            }
            void ChooseSkin()
            {
                Console.Write("\n스킨을 선택하세요: \n1. 숫자 \n2. 알파벳 \n3. 기호 \n선택: ");
                string inputSkin;
                while (true)
                {
                    inputSkin = Console.ReadLine();
                    if (int.TryParse(inputSkin, out int result))
                    {
                        if (result < 1 || result > 3)
                        {
                            Console.WriteLine("1-3 사이의 숫자를 입력하세요!");
                        }
                        else
                        {
                            if (result == 3 && difficulty == Difficulty.Hard)
                            {
                                Console.WriteLine("어려움 모드에서는 기호 스킨을 사용할 수 없습니다!");
                                continue;
                            }
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("숫자를 입력하세요!");
                    }
                }

                switch (inputSkin)
                {
                    case "1":
                        Console.WriteLine("숫자 스킨을 선택하셨습니다.");
                        skin = Skin.Number;
                        break;
                    case "2":
                        Console.WriteLine("알파벳 스킨을 선택하셨습니다.");
                        skin = Skin.Alphabet;
                        break;
                    case "3":
                        Console.WriteLine("기호 스킨을 선택하셨습니다.");
                        skin = Skin.Icon;
                        break;
                    default:
                        skin = Skin.Number;
                        break;
                }
            }
        }
    }
}