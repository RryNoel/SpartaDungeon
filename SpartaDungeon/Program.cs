using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.InteropServices;

namespace SpartaDungeon
{
    // 수정하기 전에 다른 프로젝트를 만들어서 강의를 보고 따라서 실습해봤습니다.
    // 기존의 코드를 전부 바꾸면 시간도 오래걸리고, 제가 부족한 점이 많기 때문에,
    // 여러가지로 피드백을 받기 위해서 제가 구현하지 못했던 부분만 수정하겠습니다.

    // 해설 강의를 보고 GamaManager 클래스를 따로 생성
    // 이를 통해서 기존에 static을 많이 쓰게 된 것을 바꿀 수 있었습니다.


    
    public class GameManager
    {
        private Character player;

        List<Item> shopItems = new List<Item>
            {
                new Item("수련자 갑옷", "방어력 +", 5, "수련에 도움을 주는 갑옷입니다.", 1000),
                new Item("무쇠 갑옷", "방어력 +", 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000),
                new Item("스파르타의 갑옷", "방어력 +", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500),
                new Item("낡은 검", "공격력 +", 2, "쉽게 볼 수 있는 낡은 검입니다.", 600),
                new Item("청동 도끼", "공격력 +", 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500),
                new Item("스파르타의 창", "공격력 +", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2500),
            };

        // 게임 매니저 생성자에서 함수 실행
        public GameManager()
        {
            InitiallizeGame();
        }

        private void InitiallizeGame()
        {
            player = new Character("Diluc", "전사", 1, 10, 5, 100, 15000);

            
        }

        public void StartGame()
        {
            // 화면 정리 및 멘트
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

            Console.WriteLine("\n");

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");

            Console.WriteLine("\n");

            // 강의를 보고 실습한 들어가고 나가는 방식
            int choice = ConsoleUtility.PromptMenuChoice(1, 3);
            switch (choice)
            {
                case 1:
                    ShowStatus(player);
                    break;
                case 2:
                    ShowInventory(player);
                    break;
                case 3:
                    VisitStore(player);
                    break;
            }
            StartGame();

            // 기존에 썼던 나가고 들어가는 방식, 여러가지로 문제가 많습니다 ㅠㅠ

            //string input = Console.ReadLine();

            //switch (input)
            //{
            //    case "1":
            //        ShowStatus(player);
            //        do
            //        {
            //            Exit();
            //        } while (trigger == false);
            //        break;
            //        case "2":
            //        ShowInventory(player);
            //        do
            //        {
            //            Exit();
            //        } while (trigger == false);
            //        break;
            //        case "3":
            //        VisitStore(player);
            //        do
            //        {
            //            Exit();
            //        } while (trigger == false);
            //        break;
            //        default:
            //        Console.WriteLine("잘못된 입력입니다.");
            //        Task.Delay(800).Wait();
            //        trigger = true;
            //        break;
            //}
        }

        // 나가는 방식을 생각하다가 만든 함수
        // Switch문의 default랑 입력을 두번 받아서 인벤토리 or 상점 창에서 2번 입력해야 나가는 문제가 있었음
        //static void Exit()
        //{
        //    string userInput = Console.ReadLine();
        //    if (userInput == "0")
        //    {
        //        trigger = true;
        //    }
        //    else
        //    {
        //        Console.WriteLine("잘못된 입력입니다.");
        //        Console.Write(">> ");
        //        trigger = false;
        //    }
        //}


        private void ShowStatus(Character player)
        {
            Console.Clear();


            int totalAttack = player.BaseAttack;
            int totalDefense = player.BaseDefense;

            foreach (var item in player.Inventory)
            {
                if (item.Equipped)
                {
                    totalAttack += item.EffectValue;
                    totalDefense += item.EffectValue;
                }
            }

            // 공력력이랑 방어력을 받는 부분
            player.Atk = totalAttack;
            player.Def = totalDefense;


            // 상태 보기 화면 및 멘트
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine("\n");
            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"{player.Name} ( {player.Job} )");
            Console.WriteLine($"공격력 : {player.Atk} ({GetEquippedAttackBonus(player)})");
            Console.WriteLine($"방어력 : {player.Def} ({GetEquippedDefenseBonus(player)})");
            Console.WriteLine($"체 력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine("\n");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n");

            switch (ConsoleUtility.PromptMenuChoice(0, 0))
            {
                case 0:
                    StartGame();
                    break;
            }
        }

        private void ShowInventory(Character player)
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("\n");

            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < player.Inventory.Count; i++)
            {
                string equipped = player.Inventory[i].Equipped ? "[E] " : "";
                Console.WriteLine($"{equipped}{player.Inventory[i].Name}\t| {player.Inventory[i].Effect}{player.Inventory[i].EffectValue}\t| {player.Inventory[i].Explan}");
            }

            Console.WriteLine("\n");
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n");

            // 강의를 보고 수정한 나가고 들어가는 방식
            switch (ConsoleUtility.PromptMenuChoice(0, 1))
            {
                case 0:
                    StartGame();
                    break;
                case 1:
                    Inventory(player);
                    break;
            }

            // 기존에 썼던 나가고 들어가는 방식, 여러가지로 문제가 많습니다 ㅠㅠ
            //string input = Console.ReadLine();
            //switch (input)
            //{
            //    case "1":
            //        Inventory(player);
            //        break;
            //    case "0":
            //        return;
            //    default:
            //        if (input == "0")
            //        {
            //            Exit();
            //        }
            //        break;
            //}
        }

        private void Inventory(Character player)
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("\n");

            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < player.Inventory.Count; i++)
            {
                string equipped = player.Inventory[i].Equipped ? "[E] " : "";
                Console.WriteLine($"{i + 1}, {equipped}{player.Inventory[i].Name}\t| {player.Inventory[i].Effect}{player.Inventory[i].EffectValue}\t| {player.Inventory[i].Explan}");
            }

            Console.WriteLine("\n");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n");
            Console.WriteLine("착용하거나 해제할 아이템을 선택해주세요.");
            Console.Write(">> ");


            // 기존에 썻던 방식인데, 문제점이 한 둘이 아닙니다 ㅠㅠ
            bool trigger = true;
            while (trigger)
            {
                trigger = false;
                string input = Console.ReadLine();
                int selectedItemIndex;
                if (int.TryParse(input, out selectedItemIndex))
                {
                    if (selectedItemIndex >= 1 && selectedItemIndex <= player.Inventory.Count)
                    {
                        Item selectedItem = player.Inventory[selectedItemIndex - 1];
                        if (selectedItem.Equipped)
                        {
                            selectedItem.Unequip();
                            Console.WriteLine($"{selectedItem.Name}을(를) 장착 해제했습니다.");
                        }
                        else
                        {
                            selectedItem.Equip();
                            Console.WriteLine($"{selectedItem.Name}을(를) 장착했습니다.");
                        }
                    }
                    else if (selectedItemIndex == 0)
                    {
                        return; // 인벤토리 화면으로 돌아감
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");
                    Task.Delay(800).Wait();
                    trigger = true;
                }
            }
        }

        private void VisitStore(Character player)
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("\n");
            Console.WriteLine($"[보유 골드 {player.Gold} G");
            Console.WriteLine("\n");
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine("\n");

            for (int i = 0; i < shopItems.Count; i++)
            {
                string status = player.Inventory.Contains(shopItems[i]) ? "구매완료" : $"{shopItems[i].Price} G";
                Console.WriteLine($"{shopItems[i].Name}\t| {shopItems[i].Effect}{shopItems[i].EffectValue}\t| {shopItems[i].Explan}\t| {status}");
            }

            Console.WriteLine("\n");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("\n");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n");

            // 강의를 보고 만든 나가고 들어가는 방식
            switch (ConsoleUtility.PromptMenuChoice(0, 1))
            {
                case 0:
                    StartGame();
                    break;
                case 1:
                    BuyItem(player, shopItems);
                    break;

            }

            // 기존에 만들었던 방식
            //string input = Console.ReadLine();
            //switch (input)
            //{
            //    case "1":
            //        BuyItem(player, shopItems);
            //        break;
            //    case "0":
            //        return;
            //    default:
            //        Console.WriteLine("잘못된 입력입니다.");
            //        break;
            //}
        }

        private void BuyItem(Character player, List<Item> shopItems)
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine("\n");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < shopItems.Count; i++)
            {
                string status = player.Inventory.Contains(shopItems[i]) ? "구매완료" : $"{shopItems[i].Price} G";
                Console.WriteLine($"{i + 1}. {shopItems[i].Name}\t| {shopItems[i].Effect}{shopItems[i].EffectValue}\t| {shopItems[i].Explan}\t| {status}");
            }

            Console.WriteLine("\n");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n");
            Console.WriteLine("원하시는 품목 번호를 입력하세요.");


            // 아이템이 안사진다거나, 반복적인게 아니라서 다른거 입력하면 다시해야하는 문제가 많아요
            string input = Console.ReadLine();
            int selectedItemIndex;
            if (int.TryParse(input, out selectedItemIndex))
            {
                if (selectedItemIndex >= 1 && selectedItemIndex <= shopItems.Count)
                {
                    Item selectedItem = shopItems[selectedItemIndex - 1];
                    if (player.Inventory.Contains(selectedItem))
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                    }
                    else if (player.Gold >= selectedItem.Price)
                    {
                        player.Inventory.Add(selectedItem);
                        player.Gold -= selectedItem.Price;
                        Console.WriteLine("구매를 완료했습니다.");
                    }
                    else
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                    }
                }
                else if (selectedItemIndex == 0)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        // 장비를 장착하고 공격력을 어떻게 추가할까 생각하다가 만들었는데, 공격력이랑 방어력이랑 나눴는데 같이 올라갑니다
        // EffectValue의 값을 불러오는 게 같아서 생기는 문제겠죠?
        static string GetEquippedAttackBonus(Character player)
        {
            int totalBonus = 0;
            foreach (var item in player.Inventory)
            {
                if (item.Equipped)
                {
                    if (item.Effect.StartsWith("공격력"))
                    {
                        totalBonus += item.EffectValue;
                    }
                }
            }
            return totalBonus > 0 ? $"+{totalBonus}" : "";
        }

        // 장비를 장착하고 방어력을 어떻게 추가할까 생각하다가 만들었는데, 공격력이랑 방어력이랑 나눴는데 같이 올라갑니다.
        // EffectValue의 값을 불러오는 게 같아서 생기는 문제겠죠?
        static string GetEquippedDefenseBonus(Character player)
        {
            int totalBonus2 = 0;
            foreach (var item in player.Inventory)
            {
                if (item.Equipped)
                {
                    if (item.Effect.StartsWith("방어력"))
                    {
                        totalBonus2 += item.EffectValue;
                    }
                }
            }
            return totalBonus2 > 0 ? $"+{totalBonus2}" : "";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.StartGame();
        }
    }
}
