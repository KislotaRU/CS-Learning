using System;
using System.Collections.Generic;

//Пользователь запускает приложение и перед ним находится меню, в котором он может выбрать,
//к какому вольеру подойти. При приближении к вольеру, пользователю выводится информация о том,
//что это за вольер, сколько животных там обитает, их пол и какой звук издает животное.
//Вольеров в зоопарке может быть много, в решении нужно создать минимум 4 вольера.

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Zoo zoo = new Zoo();

            zoo.Work();
        }
    }
}

static class UserUtils
{
    private static readonly Random s_random;

    public const int FullValue = 100;
    public const int HalfValue = 50;

    static UserUtils()
    {
        s_random = new Random();
    }

    public static int GenerateRandomNumber(int minNumber, int maxNumber)
    {
        return s_random.Next(minNumber, maxNumber);
    }

    public static int GenerateRandomNumber(int maxNumber)
    {
        return s_random.Next(maxNumber);
    }

    public static void PrintMenu(string[] arrayMenu)
    {
        for (int i = 0; i < arrayMenu.Length; i++)
            Console.Write($"\t{i + 1}. {arrayMenu[i]}\n");
    }

    public static void ReadInputMenu(string[] arrayMenu, out string userInput)
    {
        userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int result))
            if ((result > 0) && (result <= arrayMenu.Length))
                userInput = arrayMenu[result - 1];
    }
}

class Zoo
{
    private const string CommandChooseEnclosures = "Выбрать вольер";
    private const string CommandExit = "Выйти";

    private readonly List<Enclosure> _enclosures;

    private readonly int _maxCountEnclosures = 10;

    public Zoo()
    {
        _enclosures = new List<Enclosure>();

        CreateEnclosures();
    }

    public void Work()
    {
        string[] menu = new string[]
        {
            CommandChooseEnclosures,
            CommandExit
        };

        string userInput = null;

        while (userInput != CommandExit)
        {
            Console.Write("\t\t--- Добро пожаловать в Зоопарк ДИНО ---");

            Console.Write("\t\n\nДоступные команды:\n\n");

            UserUtils.PrintMenu(menu);

            Console.Write("\nОжидается ввод: ");

            UserUtils.ReadInputMenu(menu, out userInput);

            Console.Clear();

            switch (userInput)
            {
                case CommandChooseEnclosures:
                    ChooseEnclosure();
                    break;

                case CommandExit:
                    Exit();
                    continue;

                default:
                    Console.Write("Ввод не соответствует номеру команды.");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    private void ChooseEnclosure()
    {
        ShowEnclosures();

        Console.Write("\nВыберите клетку, которую хотите посмотреть: ");

        if (TryGetEnclosure(_enclosures, out Enclosure foundEnclosure))
        {
            Console.Clear();
            foundEnclosure.Show();
        }
        else
        {
            Console.Write("Не удалось найти указанную клетку.");
        }
    }

    private void ShowEnclosures()
    {
        int numberEnclosure = 0;

        Console.Write("Перед вами представлены все вольеры Зоопарка ДИНО:\n\n");

        foreach (Enclosure enclosure in _enclosures)
        {
            Console.Write($"\t{++numberEnclosure}. " +
                          $"Вольер, где содержится вид животных: {enclosure?.TypeAnimal ?? "..."}\n");
        }
    }

    private void Exit()
    {
        Console.Write("Вы решили покинуть Зоопарк.\n");
    }

    private void CreateEnclosures()
    {
        List<Animal> availableAnimals = new List<Animal>()
        {
            new Bear(),
            new Wolf(),
            new Capybara(),
            new Penguin()
        };

        while (_enclosures.Count < _maxCountEnclosures)
        {
            int indexAnimal = UserUtils.GenerateRandomNumber(availableAnimals.Count);

            _enclosures.Add(new Enclosure(availableAnimals[indexAnimal]));
        }
    }

    private bool TryGetEnclosure(List<Enclosure> enclosures, out Enclosure foundEnclosure)
    {
        string userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int index))
        {
            if (index > 0 && index <= enclosures.Count)
            {
                foundEnclosure = enclosures[index - 1];
                return true;
            }
            else
            {
                Console.Write("Под таким номером нет клетки.\n");
            }

        }
        else
        {
            Console.Write("Требуется ввести номер клетки.\n");
        }

        foundEnclosure = null;
        return false;
    }
}

class Enclosure
{
    private readonly List<Animal> _animals;

    private readonly int _maxCountAnimal = 10;


    private string _soundAnimals;

    private int _malesCount = 0;
    private int _femalesCount = 0;

    public Enclosure(Animal animal)
    {
        _animals = new List<Animal>();

        CreateAnimals(animal);
    }

    public string TypeAnimal{ get; private set; }

    public void Show()
    {
        if (_animals.Count > 0)
        {
            Console.Write($"Вы подошли к вольеру, где содержится вид животных: {TypeAnimal}\n" +
                          $"Всего в данном вольере сожержится {_animals.Count}\n" +
                          $"Из них мужского пола {_malesCount} и женского пола {_femalesCount}\n" +
                          $"Животные данного вида в основном издают звук: {_soundAnimals}\n\n");

            foreach (Animal animal in _animals)
                animal.Show();
        }
        else
        {
            Console.Write("Данный вольер ещё не успели заселить.\n");
        }
    }

    private void CreateAnimals(Animal modelAnimal)
    {
        int actualCountAnimal = UserUtils.GenerateRandomNumber(_maxCountAnimal);

        if (actualCountAnimal > 0)
        {
            TypeAnimal = modelAnimal.Name;
            _soundAnimals = modelAnimal.Sound;

            while (_animals.Count < actualCountAnimal)
                _animals.Add(modelAnimal.Clone());

            foreach (Animal animal in _animals)
            {
                if (animal.Gender == Animal.GenderMale)
                    _malesCount++;
                else
                    _femalesCount++;
            }
        }
    }
}

abstract class Animal
{
    public const string GenderMale = "Мужской";
    public const string GenderFemale = "Женский";

    public string Name { get ; protected set; }
    public string Sound { get; protected set; }
    public string Gender { get; protected set; }

    public void Show()
    {
        Console.Write($"{Name} | Пол: {Gender} | Издаёт звук: {Sound}\n");
    }

    public abstract Animal Clone();

    protected string GetGender()
    {
        if (UserUtils.HalfValue >= UserUtils.GenerateRandomNumber(UserUtils.FullValue))
            return GenderMale;
        else
            return GenderFemale;
    }
}

class Bear : Animal
{
    private readonly string _name = "Медведь";
    private readonly string _sound = "Рёв";

    public Bear()
    {
        Name = _name;
        Sound = _sound;
        Gender = GetGender();
    }

    public override Animal Clone() =>
        new Bear();
}

class Wolf : Animal
{
    private readonly string _name = "Волк";
    private readonly string _sound = "Рычание";

    public Wolf()
    {
        Name = _name;
        Sound = _sound;
        Gender = GetGender();
    }

    public override Animal Clone() =>
        new Wolf();
}

class Capybara : Animal
{
    private readonly string _name = "Капибара";
    private readonly string _sound = "Хрюканье";

    public Capybara()
    {
        Name = _name;
        Sound = _sound;
        Gender = GetGender();
    }

    public override Animal Clone() =>
        new Capybara();
}

class Penguin : Animal
{
    private readonly string _name = "Пингвин";
    private readonly string _sound = "Крик";

    public Penguin()
    {
        Name = _name;
        Sound = _sound;
        Gender = GetGender();
    }

    public override Animal Clone() =>
        new Penguin();
}