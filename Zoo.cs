using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Zoo zoo = new Zoo();

            Console.ForegroundColor = ConsoleColor.White;

            zoo.Work();
        }
    }
}

static class UserUtils
{
    private static readonly Random s_random;

    static UserUtils()
    {
        s_random = new Random();
    }

    public static int GenerateRandomNumber(int minNumber = 0, int maxNumber = 0) =>
        s_random.Next(minNumber, maxNumber);

    public static double GenerateRandomNumberDouble() =>
        s_random.NextDouble();

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

class Zoo
{
    private readonly List<Enclosure> _enclosures;

    private readonly int _maxCountEnclosures = 10;

    public Zoo()
    {
        _enclosures = new List<Enclosure>();

        CreateEnclosures();
    }

    public void Work()
    {
        const string CommandChooseEnclosure = "Выбрать вольер";
        const string CommandExit = "Выйти";

        string[] menu = new string[]
        {
            CommandChooseEnclosure,
            CommandExit
        };

        string userInput;

        bool isWorking = true;

        while (isWorking)
        {
            Console.Write("\t\tМеню зоопарка\n\n");

            Console.Write("Доступные команды:\n");
            PrintMenu(menu);

            Console.Write("Ожидается ввод: ");
            userInput = GetCommandMenu(menu);

            Console.Clear();

            switch (userInput)
            {
                case CommandChooseEnclosure:
                    ChooseEnclosure();
                    break;

                case CommandExit:
                    Console.Write("Вы покинули Зоопарк.\n\n");
                    isWorking = false;
                    continue;

                default:
                    Console.Write("Требуется ввести номер команды или саму команду.\n\n");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    private void ChooseEnclosure()
    {
        ShowEnclosures();

        Console.Write("Выберите клетку, которую хотите посмотреть: ");

        if (TryGetEnclosure(out Enclosure foundEnclosure))
        {
            Console.Clear();
            foundEnclosure.ShowAnimals();
        }
        else
        {
            Console.Write("Не удалось найти указанную клетку.\n");
        }
    }

    private void CreateEnclosures()
    {
        while (_enclosures.Count < _maxCountEnclosures)
            _enclosures.Add(new Enclosure());
    }

    private void ShowEnclosures()
    {
        int numberEnclosure = 1;

        Console.Write("Вольеры зоопарка:\n");

        foreach (Enclosure enclosure in _enclosures)
        {
            Console.Write($"\t{numberEnclosure}. ".PadRight(5));
            enclosure.ShowInfo();

            numberEnclosure++;
        }

        Console.WriteLine();
    }

    private bool TryGetEnclosure(out Enclosure foundEnclosure)
    {
        int numberEnclosure = UserUtils.ReadInt();

        foundEnclosure = null;

        if (numberEnclosure > 0 && numberEnclosure <= _enclosures.Count)
        {
            foundEnclosure = _enclosures[numberEnclosure - 1];
            return true;
        }
        else
        {
            Console.Write("Под таким номером нет клетки.\n");
        }
        
        return false;
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

class Enclosure
{
    private readonly List<Animal> _animals;
    private readonly int _maxCountAnimals = 10;

    private int _malesCount = 0;
    private int _femalesCount = 0;

    private string _typeAnimal;
    private string _soundAnimals;

    public Enclosure()
    {
        _animals = new List<Animal>();

        CreateAnimal();
    }

    public void ShowAnimals()
    {
        if (_animals.Count > 0)
        {
            int numberAnimal = 1;

            Console.Write($"Вольер, где содержится вид животных: {_typeAnimal ?? "..."}\n" +
                          $"Кол-во особей: {_animals.Count}\n" +
                          $"Из них мужского пола {_malesCount} и женского пола {_femalesCount}\n" +
                          $"Животные данного вида в основном издают звук: {_soundAnimals}\n\n");

            foreach (Animal animal in _animals)
            {
                Console.Write($"\t{numberAnimal}. ".PadRight(5));
                animal.Show();

                numberAnimal++;
            }

            Console.WriteLine();
        }
        else
        {
            Console.Write("Данный вольер ещё не заселён.\n");
        }
    }

    public void ShowInfo() =>
        Console.Write($"Вольер, где содержится вид животных: {_typeAnimal ?? "..."}\n");

    private void CreateAnimal()
    {
        AnimalFactory animalFactory = new AnimalFactory();
        List<Animal> animals = animalFactory.GetAnimals();

        int animalsCount = UserUtils.GenerateRandomNumber(maxNumber: _maxCountAnimals);
        int index = UserUtils.GenerateRandomNumber(maxNumber: animals.Count);

        Animal temporaryAnimal = animals[index];

        if (animalsCount > 0)
        {
            _typeAnimal = temporaryAnimal.Name;
            _soundAnimals = temporaryAnimal.Sound;

            while (_animals.Count < animalsCount)
                _animals.Add(temporaryAnimal.Clone());

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

class AnimalFactory
{
    private readonly List<Animal> _animals;

    public AnimalFactory()
    {
        _animals = new List<Animal>()
        {
            new Bear(),
            new Wolf(),
            new Capybara(),
            new Penguin()
        };
    }

    public List<Animal> GetAnimals()
    {
        List<Animal> temporaryAnimals = new List<Animal>();

        foreach (Animal animal in _animals)
            temporaryAnimals.Add(animal.Clone());

        return temporaryAnimals;
    }
}

abstract class Animal
{
    public const string GenderMale = "М";
    public const string GenderFemale = "Ж";

    public string Name { get; protected set; }
    public string Sound { get; protected set; }
    public string Gender { get; protected set; }

    public abstract Animal Clone();

    public void Show() =>
        Console.Write($"{Name}".PadRight(10) + $"Пол: {Gender}".PadRight(7) + $"Издаёт звук: {Sound}\n");

    protected string GetGender()
    {
        float coefficientGender = 0.5f;

        if (coefficientGender >= UserUtils.GenerateRandomNumberDouble())
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