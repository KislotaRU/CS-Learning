using System;
using System.Collections.Generic;
using System.Linq;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            CriminalDatabase criminalDataBase = new CriminalDatabase();

            Console.ForegroundColor = ConsoleColor.White;

            criminalDataBase.Work();
        }
    }
}

public class CriminalDatabase
{
    private List<Criminal> _criminals;

    public CriminalDatabase()
    {
        _criminals = new List<Criminal>()
        {
            new Criminal("Блохин", "Юстиниан", "Петрович", true, "Вандализм"),
            new Criminal("Беляев", "Лев", "Станиславович", false, "Антиправительственное"),
            new Criminal("Тимофеева", "Алёна", "Федосеевна", false, "Хулиганство"),
            new Criminal("Семёнова", "Серафима", "Иринеевна", true, "Антиправительственное"),
            new Criminal("Якушев", "Виктор", "Агафонович", true, "Антиправительственное"),
            new Criminal("Королёв", "Игорь", "Альвианович", false, "Антиправительственное"),
            new Criminal("Яковлев", "Ипполит", "Куприянович", false, "Кража"),
            new Criminal("Ефремов", "Алан", "Александрович", false, "Антиправительственное"),
            new Criminal("Пономарёва", "Олеся", "Евгеньевна", true, "Антиправительственное"),
            new Criminal("Захаров", "Юстин", "Андреевич", true, "Кража"),
            new Criminal("Орехов", "Адам", "Валентинович", false, "Разбой"),
            new Criminal("Власова", "Даниэла", "Александровна", false, "Антиправительственное"),
            new Criminal("Киселёв", "Митрофан", "Арсеньевич", true, "Антиправительственное"),
            new Criminal("Жданова", "Елизавета", "Наумовна", true, "Антиправительственное")
        };
    }

    public void Work()
    {
        const string CommandShow = "Показать всех";
        const string CommandAmnesty = "Помиловать по запросу";
        const string CommandExit = "Завершить работу";

        string[] menu = new string[]
        {
            CommandShow,
            CommandAmnesty,
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
                    Show();
                    break;

                case CommandAmnesty:
                    Amnesty();
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
        int numberCriminal = 1;

        if (_criminals.Count > 0)
        {
            foreach (Criminal criminal in _criminals)
            {
                Console.Write($"\t{numberCriminal}. ".PadRight(5));
                criminal.Show();

                numberCriminal++;
            }
        }
        else
        {
            Console.Write("Нет ни одной записи.\n");
        }

        Console.WriteLine();
    }

    private void Amnesty()
    {
        string typeCrime = "Антиправительственное";

        Console.Write($"Преступники до амнистии:\n");
        Show();

        _criminals = _criminals.Where(criminal => criminal.TypeCrime != typeCrime).ToList();

        Console.Write($"Преступники после амнистии:\n");
        Show();
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

    public Criminal(string lastName, string firstName, string middleName, bool isPrisoner, string typeCrime = null)
    {
        _lastName = lastName;
        _firstName = firstName;
        _middleName = middleName;
        IsPrisoner = isPrisoner;
        TypeCrime = typeCrime;
    }

    public bool IsPrisoner { get; private set; }
    public string TypeCrime { get; private set; }

    public void Show()
    {
        Console.Write($"{_lastName}".PadRight(15) +
                      $"{_firstName}".PadRight(15) +
                      $"{_middleName}".PadRight(15) +
                      $"Статус: {(IsPrisoner ? "Заключён" : "Свободен")}".PadRight(17) +
                      $"Тип преступления: {TypeCrime ?? "Отсутствует"}\n");
    }
}