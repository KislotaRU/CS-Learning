using System;
using System.Collections.Generic;
using System.Linq;

/*
 * Есть набор тушенки. У тушенки есть название, год производства и срок годности.
 * Написать запрос для получения всех просроченных банок тушенки.
 * Чтобы не заморачиваться, можете думать, что считаем только года, без месяцев.
*/

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
    private readonly List<Conserva> _conserves;

    public ConservaDatabase()
    {
        _conserves = new List<Conserva>()
        {
            new Conserva("Тушёнка", 2023, 3),
            new Conserva("Тушёнка", 2020, 3),
            new Conserva("Тунец", 2018, 1),
            new Conserva("Килька", 2022, 2),
            new Conserva("Килька", 2022, 2),
            new Conserva("Тушёнка", 2024, 3),
            new Conserva("Бобы", 2020, 4),
            new Conserva("Огурцы", 2015, 2),
            new Conserva("Томаты", 2019, 2),
            new Conserva("Тунец", 2010, 1),
            new Conserva("Корнишон", 2022, 3),
            new Conserva("Килька", 2019, 2),
            new Conserva("Бобы", 2008, 4),
            new Conserva("Тушёнка", 1999, 3)
        };
    }

    public void Work()
    {
        const string CommandShow = "Показать всех";
        const string CommandShowDelayConserves = "Показать просрочку";
        const string CommandExit = "Завершить работу";

        string[] menu = new string[]
        {
            CommandShow,
            CommandShowDelayConserves,
            CommandExit
        };

        string userInput;

        bool isWorking = true;

        while (isWorking)
        {
            Console.Write("\t\tМеню базы данных консерв\n\n");

            Console.Write("Доступные команды:\n");
            PrintMenu(menu);

            Console.Write("Ожидается ввод: ");
            userInput = GetCommandMenu(menu);

            Console.Clear();

            switch (userInput)
            {
                case CommandShow:
                    Show(_conserves);
                    break;

                case CommandShowDelayConserves:
                    ShowDelayConserves();
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

    private void Show(List<Conserva> conserves)
    {
        int numberConserva = 1;

        if (conserves.Count > 0)
        {
            foreach (Conserva conserva in conserves)
            {
                Console.Write($"\t{numberConserva}. ".PadRight(5));
                conserva.Show();

                numberConserva++;
            }
        }
        else
        {
            Console.Write("Нет ни одной записи.\n");
        }

        Console.WriteLine();
    }

    private void ShowDelayConserves()
    {
        List<Conserva> temporaryConserves;
        int dateTimeNow = DateTime.Now.Year;

        temporaryConserves = _conserves.Where(conserva => conserva.YearOfRelease + conserva.ExpirationDateInYears < dateTimeNow).ToList();

        Console.Write($"Текущий год: {dateTimeNow}\n");
        Console.Write("Просроченные консервы:\n");
        Show(temporaryConserves);
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

public class Conserva
{
    private readonly string _name;

    public Conserva(string name, int yearOfRelease, int expirationDate)
    {
        _name = name;
        YearOfRelease = yearOfRelease;
        ExpirationDateInYears = expirationDate;
    }

    public int YearOfRelease { get; }
    public int ExpirationDateInYears { get; }

    public void Show()
    {
        Console.Write($"{_name}".PadRight(9) +
                      $"| Год производства: {YearOfRelease}".PadRight(25) +
                      $"| Срок годности: {ExpirationDateInYears}\n");
    }
}