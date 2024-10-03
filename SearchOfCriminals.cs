using System;
using System.Collections.Generic;
using System.Linq;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            CriminalDataBase criminalDataBase = new CriminalDataBase();

            Console.ForegroundColor = ConsoleColor.White;

            criminalDataBase.Work();
        }
    }
}

public static class UserUtils
{
    public static int ReadInt()
    {
        int number;

        string userInput = Console.ReadLine();

        while (int.TryParse(userInput, out number) == false)
        {
            Console.Write("Требуется ввести число: ");
            userInput = Console.ReadLine();
        }

        return number;
    }
}

public class CriminalDataBase
{
    private readonly List<Criminal> _criminals;

    public CriminalDataBase()
    {
        _criminals = new List<Criminal>()
        {
            new Criminal("Блохин", "Юстиниан", "Петрович", 180, 70, "русский", true),
            new Criminal("Беляев", "Лев", "Станиславович", 167, 78, "русский", false),
            new Criminal("Тимофеева", "Алёна", "Федосеевна", 160, 56, "русский", false),
            new Criminal("Семёнова", "Серафима", "Иринеевна", 175, 74, "белорус", true),
            new Criminal("Якушев", "Виктор", "Агафонович", 173, 80, "белорус", true),
            new Criminal("Королёв", "Игорь", "Альвианович", 187, 81, "белорус", false),
            new Criminal("Яковлев", "Ипполит", "Куприянович", 174, 70, "украинец", false),
            new Criminal("Ефремов", "Алан", "Александрович", 180, 75, "еврей", false),
            new Criminal("Пономарёва", "Олеся", "Евгеньевна", 168, 68, "украинец", true),
            new Criminal("Захаров", "Юстин", "Андреевич", 188, 78, "украинец", true),
            new Criminal("Орехов", "Адам", "Валентинович", 190, 78, "русский", false),
            new Criminal("Власова", "Даниэла", "Александровна", 182, 82, "русский", false),
            new Criminal("Киселёв", "Митрофан", "Арсеньевич", 178, 76, "русский", true),
            new Criminal("Жданова", "Елизавета", "Наумовна", 163, 54, "русский", true)
        };
    }

    public void Work()
    {
        const string CommandShow = "Показать всех";
        const string CommandSearch = "Найти по запросу";
        const string CommandExit = "Завершить работу";

        string[] menu = new string[]
        {
            CommandShow,
            CommandSearch,
            CommandExit
        };

        string userInput;

        bool isWorking = true;

        while (isWorking)
        {
            Console.Write("\t\tМеню базы данных преступников\n\n");

            Console.Write("Доступные команды:\n");
            PrintMenu(menu);

            Console.Write("Ожидается ввод: ");
            userInput = GetCommandMenu(menu);

            Console.Clear();

            switch (userInput)
            {
                case CommandShow:
                    Show(_criminals);
                    break;

                case CommandSearch:
                    Search();
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

    private void Show(List<Criminal> criminals)
    {
        int numberCriminal = 1;

        foreach (Criminal criminal in criminals)
        {
            Console.Write($"{numberCriminal}. ".PadRight(5));
            criminal.Show();

            numberCriminal++;
        }

        Console.WriteLine();
    }

    private void Search()
    {
        List<Criminal> temporaryCriminals;
        int height;
        int weight;
        string nationality;

        Console.Write("Поиск преступников на свободе:\n");

        Console.Write("Введите рост: ");
        height = UserUtils.ReadInt();

        Console.Write("Введите вес: ");
        weight = UserUtils.ReadInt();

        Console.Write("Введите национальность: ");
        nationality = Console.ReadLine();

        temporaryCriminals = GetCriminalsBySearch(height, weight, nationality);

        Show(temporaryCriminals);
    }

    private List<Criminal> GetCriminalsBySearch(int height, int weight, string nationality)
    {
        List<Criminal> temporaryCriminals = new List<Criminal>();

        temporaryCriminals = _criminals.Where(criminal => criminal.Height == height).Select(criminal => criminal).ToList();
        temporaryCriminals = temporaryCriminals.Where(criminal => criminal.Weight == weight).Select(criminal => criminal).ToList();
        temporaryCriminals = temporaryCriminals.Where(criminal => criminal.Nationality == nationality).Select(criminal => criminal).ToList();
        temporaryCriminals = temporaryCriminals.Where(criminal => criminal.IsPrisoner == false).Select(criminal => criminal).ToList();

        return temporaryCriminals;
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

public class Criminal
{
    private readonly string _lastName;
    private readonly string _firstName;
    private readonly string _middleName;

    public Criminal(string lastName, string firstName, string middleName, int height, int weight, string nationality, bool isPrisoner)
    {
        _lastName = lastName;
        _firstName = firstName;
        _middleName = middleName;
        Height = height;
        Weight = weight;
        Nationality = nationality;
        IsPrisoner = isPrisoner;
    }

    public int Height { get; }
    public int Weight { get; }
    public string Nationality { get; }
    public bool IsPrisoner { get; }

    public void Show()
    {
        Console.Write($"{_lastName}".PadRight(15) +
            $"{_firstName}".PadRight(15) +
            $"{_middleName}".PadRight(15) +
            $"Рост: {Height}".PadRight(10) +
            $"Вес: {Weight}".PadRight(10) +
            $"Национальность: {Nationality}".PadRight(25) +
            $"Статус: {(IsPrisoner ? "Заключён" : "Свободен")}\n");
    }
}