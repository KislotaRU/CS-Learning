using System;
using System.Collections.Generic;
using System.Linq;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            PatientDatabase patientDatabase = new PatientDatabase();

            Console.ForegroundColor = ConsoleColor.White;

            patientDatabase.Work();
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

public class PatientDatabase
{
    private readonly List<Patient> _patients;

    public PatientDatabase()
    {
        _patients = new List<Patient>()
        {
            new Patient("Блохин", "Юстиниан", "Петрович", 18, "ОРВИ"),
            new Patient("Беляев", "Лев", "Станиславович", 20, "ОРВИ"),
            new Patient("Тимофеева", "Алёна", "Федосеевна", 19, "ОРВИ"),
            new Patient("Семёнова", "Серафима", "Иринеевна", 54, "Грыжа"),
            new Patient("Якушев", "Виктор", "Агафонович", 73, "Грипп"),
            new Patient("Королёв", "Игорь", "Альвианович", 34, "ОРВИ"),
            new Patient("Яковлев", "Ипполит", "Куприянович", 34, "ОРВИ"),
            new Patient("Ефремов", "Алан", "Александрович", 28, "Аденоидит"),
            new Patient("Пономарёва", "Олеся", "Евгеньевна", 18, "ОРВИ"),
            new Patient("Захаров", "Юстин", "Андреевич", 65, "Ожоги"),
            new Patient("Орехов", "Адам", "Валентинович", 22, "ОРВИ"),
            new Patient("Власова", "Даниэла", "Александровна", 25, "ОРВИ"),
            new Patient("Киселёв", "Митрофан", "Арсеньевич", 34, "Ангина"),
            new Patient("Жданова", "Елизавета", "Наумовна", 28, "Остеосклероз")
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
            Console.Write("\t\tМеню базы данных пациентов\n\n");

            Console.Write("Доступные команды:\n");
            PrintMenu(menu);

            Console.Write("Ожидается ввод: ");
            userInput = GetCommandMenu(menu);

            Console.Clear();

            switch (userInput)
            {
                case CommandShow:
                    Show(_patients);
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

    private void Show(List<Patient> patients)
    {
        int numberPatient = 1;

        if (patients.Count > 0)
        {
            foreach (Patient patient in patients)
            {
                Console.Write($"\t{numberPatient}. ".PadRight(5));
                patient.Show();

                numberPatient++;
            }
        }
        else
        {
            Console.Write("Нет ни одной записи.\n");
        }

        Console.WriteLine();
    }

    private void Search()
    {
        const string CommandSearchByLastName = "Фамилия";
        const string CommandSearchByAge = "Возраст";
        const string CommandSearchByDisease = "Заболевание";

        string[] menu = new string[]
        {
            CommandSearchByLastName,
            CommandSearchByAge,
            CommandSearchByDisease,
        };

        string userInput;

        List<Patient> foundPatients = new List<Patient>();

        Console.Write("\t\tПараметры поиска.\n\n");

        Console.Write("Доступные параметры:\n");
        PrintMenu(menu);

        Console.Write("Ожидается ввод:\n");
        userInput = GetCommandMenu(menu);

        Console.Clear();

        switch (userInput)
        {
            case CommandSearchByLastName:
                foundPatients = SearchByLastName();
                break;

            case CommandSearchByAge:
                foundPatients = SearchByAge();
                break;

            case CommandSearchByDisease:
                foundPatients = SearchByDisease();
                break;

            default:
                Console.Write("Требуется ввести номер параметра или сам параметр.\n");
                break;
        }

        Console.Write("Найденные пациенты по запросу:\n");
        Show(foundPatients);
    }

    private List<Patient> SearchByLastName()
    {
        string lastName;

        Console.Write("Введите фамилию: ");
        lastName = Console.ReadLine();

        return _patients.Where(patient => patient.LastName.ToLower() == lastName.ToLower()).ToList();
    }

    private List<Patient> SearchByAge()
    {
        int age;

        Console.Write("Введите возраст: ");
        age = UserUtils.ReadInt();

        return _patients.Where(patient => patient.Age == age).ToList();
    }

    private List<Patient> SearchByDisease()
    {
        string disease;

        Console.Write("Введите заболевание: ");
        disease = Console.ReadLine();

        return _patients.Where(patient => patient.Disease.ToLower() == disease.ToLower()).ToList();
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

public class Patient
{ 
    private readonly string _firstName;
    private readonly string _middleName;

    public Patient(string lastName, string firstName, string middleName, int age, string disease)
    {
        LastName = lastName;
        _firstName = firstName;
        _middleName = middleName;
        Age = age;
        Disease = disease;
    }

    public string LastName { get; }
    public int Age { get; }
    public string Disease { get; }

    public void Show()
    {
        Console.Write($"{LastName}".PadRight(12) +
                      $"| {_firstName}".PadRight(12) +
                      $"| {_middleName}".PadRight(16) +
                      $"| Возраст: {Age}".PadRight(14) +
                      $"| Заболевание: {Disease}\n");
    }
}