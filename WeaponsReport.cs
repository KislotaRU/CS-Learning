using System;
using System.Collections.Generic;
using System.Linq;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            ConservaDatabase conservaDatabase = new ConservaDatabase();

            Console.ForegroundColor = ConsoleColor.White;

            conservaDatabase.Work();
        }
    }
}

public class ConservaDatabase
{
    private readonly List<Soldier> _soldiers;

    public ConservaDatabase()
    {
        _soldiers = new List<Soldier>()
        {
            new Soldier("Эшли", "Винтовка", "Капитан", 60),
            new Soldier("Брайан", "Болтовка", "Капрал", 24),
            new Soldier("Арнольд", "Пулемёт", "Подполковник", 84),
            new Soldier("Кэйлеб", "Винтовка", "Капитан", 60),
            new Soldier("Эндрю", "Винтовка", "Сержант", 36),
            new Soldier("Гарет", "Пулемёт", "Капрал", 24),
            new Soldier("Итан", "Пулемёт", "Рядовой", 13),
            new Soldier("Джейм", "Винтовка", "Подполковник", 87),
            new Soldier("Майкл", "Болтовка", "Генерал", 124),
            new Soldier("Эрик", "Пулемёт", "Генерал армий", 189),
            new Soldier("Адам", "Болтовка", "Подполковник", 96),
            new Soldier("Гарри", "Винтовка", "Майор", 72),
            new Soldier("Джек", "Пулемёт", "Сержант", 36),
            new Soldier("Хорас", "Винтовка", "Подполковник", 108),
            new Soldier("Кевин", "Болтовка", "Капитан", 60),
            new Soldier("Грант", "Винтовка", "Рядовой", 12),
            new Soldier("Дуайт", "Винтовка", "Капитан", 59),
        };
    }

    public void Work()
    {
        const string CommandShow = "Показать всех";
        const string CommandShowNameAndRank = "Показать имя и звание";
        const string CommandExit = "Завершить работу";

        string[] menu = new string[]
        {
            CommandShow,
            CommandShowNameAndRank,
            CommandExit
        };

        string userInput;

        bool isWorking = true;

        while (isWorking)
        {
            Console.Write("\t\tМеню базы данных солдат\n\n");

            Console.Write("Доступные команды:\n");
            PrintMenu(menu);

            Console.Write("Ожидается ввод: ");
            userInput = GetCommandMenu(menu);

            Console.Clear();

            switch (userInput)
            {
                case CommandShow:
                    Show();
                    break;

                case CommandShowNameAndRank:
                    ShowNameAndRank();
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

    private void Show()
    {
        int numberSoldier = 1;

        if (_soldiers.Count > 0)
        {
            foreach (Soldier soldier in _soldiers)
            {
                Console.Write($"\t{numberSoldier}. ".PadRight(5));
                soldier.Show();

                numberSoldier++;
            }
        }
        else
        {
            Console.Write("Нет ни одной записи.\n");
        }

        Console.WriteLine();
    }

    private void ShowNameAndRank()
    {
        int numberSoldier = 1;
        var temporarySoldiers = _soldiers.Select(soldier => new { soldier.Name, soldier.Rank } ).ToList();

        Console.Write("Список солдат с именем и званием:\n");

        foreach (var soldier in temporarySoldiers)
        {
            Console.Write($"\t{numberSoldier}. ".PadRight(5) +
                            $"Имя: {soldier.Name}".PadRight(13) +
                            $"Звание: {soldier.Rank}\n");

            numberSoldier++;
        }

        Console.WriteLine();
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
        Console.Write($"{Name}".PadRight(8) +
                      $"Оружие: {_weapon}".PadRight(18) +
                      $"Звание: {Rank}".PadRight(22) +
                      $"Срок службы: {_serviceLifeInMonths} месяц\n");
    }
}