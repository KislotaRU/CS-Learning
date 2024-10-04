using System;
using System.Collections.Generic;
using System.Linq;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            SoldierDatabase soldierDatabase = new SoldierDatabase();

            Console.ForegroundColor = ConsoleColor.White;

            soldierDatabase.Work();
        }
    }
}

public class SoldierDatabase
{
    private List<Soldier> _firstSquad;
    private List<Soldier> _secondSquad;

    public SoldierDatabase()
    {
        _firstSquad = new List<Soldier>()
        {
            new Soldier("Бэшли", "Винтовка", "Капитан", 60),
            new Soldier("Брайан", "Болтовка", "Капрал", 24),
            new Soldier("Барнольд", "Пулемёт", "Подполковник", 84),
            new Soldier("Кэйлеб", "Винтовка", "Капитан", 60),
            new Soldier("Бэндрю", "Винтовка", "Сержант", 36),
            new Soldier("Гарет", "Пулемёт", "Капрал", 24),
            new Soldier("Битан", "Пулемёт", "Рядовой", 13),
            new Soldier("Джейм", "Винтовка", "Подполковник", 87),
            new Soldier("Майкл", "Болтовка", "Генерал", 124),
            new Soldier("Бернарт", "Болтовка", "Капитан", 60),
        };

        _secondSquad = new List<Soldier>()
        {
            new Soldier("Эрик", "Пулемёт", "Генерал армий", 189),
            new Soldier("Адам", "Болтовка", "Подполковник", 96),
            new Soldier("Барри", "Винтовка", "Майор", 72),
            new Soldier("Джек", "Пулемёт", "Сержант", 36),
            new Soldier("Хорас", "Винтовка", "Подполковник", 108),
            new Soldier("Грант", "Винтовка", "Рядовой", 12),
            new Soldier("Дуайт", "Винтовка", "Капитан", 59),
        };
    }

    public void Work()
    {
        const string CommandShow = "Показать все отряды";
        const string CommandJoinSquads = "Объединить отряды";
        const string CommandExit = "Завершить работу";

        string[] menu = new string[]
        {
            CommandShow,
            CommandJoinSquads,
            CommandExit
        };

        string userInput;

        bool isWorking = true;

        while (isWorking)
        {
            string letter = "Б";

            Console.Write("\t\tМеню базы данных солдат\n\n");

            Console.Write("Доступные команды:\n");
            PrintMenu(menu);

            Console.Write("Ожидается ввод: ");
            userInput = GetCommandMenu(menu);

            Console.Clear();

            switch (userInput)
            {
                case CommandShow:
                    Show(_firstSquad, _secondSquad);
                    break;

                case CommandJoinSquads:
                    JoinSquadsByLetter(letter);
                    break;

                case CommandExit:
                    Console.Write("Вы завершии работу базы данных.\n\n");
                    isWorking = false;
                    continue;

                default:
                    Console.Write("Требуется ввести номер команды или саму команду.\n");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    private void Show(params List<Soldier>[] soldiers)
    {
        if (soldiers.Length == 0)
        {
            Console.Write("Нет ни одного отряда.\n");
            return;
        }

        int numberSquad = 1;

        for (int i = 0; i < soldiers.Length ; i++)
        {
            int numberSoldier = 1;

            Console.Write($"Отряд #{numberSquad}.\n");

            numberSquad++;

            if (soldiers[i].Count == 0)
            {
                Console.Write("Нет ни одной записи.\n\n");
                continue;
            }

            foreach (Soldier soldier in soldiers[i])
            {
                Console.Write($"\t{numberSoldier}. ".PadRight(5));
                soldier.Show();

                numberSoldier++;
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private void JoinSquadsByLetter(string letter)
    {
        List<Soldier> temporarySoldiers;

        Console.Write("Перевод солдат из первого отряда во второй по фильтру:\n" +
                     $">Имя начинается с \"{letter}\"\n\n");

        Console.Write("До перевода:\n");
        Show(_firstSquad, _secondSquad);

        temporarySoldiers = _firstSquad.Where(soldier => soldier.Name.ToLower().StartsWith(letter.ToLower())).ToList();
        _secondSquad = _secondSquad.Union(temporarySoldiers).ToList();

        _firstSquad = _firstSquad.Where(soldier => soldier.Name.ToLower().StartsWith(letter.ToLower()) == false).ToList();

        Console.Write("После перевода:\n");
        Show(_firstSquad, _secondSquad);
    }
    
    private void PrintMenu(string[] menu)
    {
        int numberCommand = 1;

        for (int i = 0; i < menu.Length; i++)
        {
            Console.Write($"\t{numberCommand}. {menu[i]}\n");
            numberCommand++;
        }

        Console.WriteLine();
    }

    private string GetCommandMenu(string[] menu)
    {
        string userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int number))
            if (number > 0 && number <= menu.Length)
                return menu[number - 1];

        return userInput;
    }
}

public class Soldier
{
    private readonly string _weapon;
    private readonly int _serviceLifeInMonths;

    public Soldier(string name = null, string weapon = null, string rank = null, int serviceLife = 0)
    {
        Name = name;
        _weapon = weapon;
        Rank = rank;
        _serviceLifeInMonths = serviceLife;
    }

    public string Name { get; }
    public string Rank { get; }

    public void Show()
    {
        Console.Write($"{Name}".PadRight(10) +
                      $"Оружие: {_weapon}".PadRight(18) +
                      $"Звание: {Rank}".PadRight(22) +
                      $"Срок службы: {_serviceLifeInMonths} месяц\n");
    }
}