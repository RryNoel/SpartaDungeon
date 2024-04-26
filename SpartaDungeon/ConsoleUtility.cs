using System;

namespace SpartaDungeon
{
	public class ConsoleUtility
    {

        // 강의를 보고 따라 만든 함수
        public static int PromptMenuChoice(int min, int max)
        {
            while (true)
            {
                Console.Write("원하시는 번호를 입력해주세요: ");
                Console.WriteLine(">> ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요");
            }
        }
    }
}
