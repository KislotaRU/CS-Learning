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
    private static Random s_random;

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
}

class Zoo
{
    private const string CommandT = "Подойти к вольеру с животными";
    private const string CommandExit = "Выйти";

    private readonly List<Enclosure> _enclosures;

    public Zoo()
    {
        _enclosures = new List<Enclosure>();
    }

    public void Work()
    {
        string[] main = new string[]
        {
            CommandT,
            CommandExit
        };

        string userInput = null;

        while (userInput != CommandExit)
        {


            switch (userInput)
            {
                case CommandT:

                    break;

                case CommandExit:

                    break;

                default:
                    Console.Write("Ввод не соответствует номеру команды.");
                    break;
            }
        }
    }
}

class Enclosure
{
    private readonly List<Animal> _animals;

    public Enclosure()
    {
        _animals = new List<Animal>(); 
    }

    public int AnimalsCount { get { return _animals.Count; } }
}

abstract class Animal
{
    public string Name { get ; protected set; }
    public string Sounds { get; protected set; }
    public string Gender { get; protected set; }

    public void Show()
    {
        Console.Write($"{Name} ");
    }
}

class Bear : Animal
{
    private readonly string _name = "Медведь";

    public Bear()
    {
        Name = _name;
    }
}

class Wolf : Animal
{
    private readonly string _name = "Волк";

    public Wolf()
    {
        Name = _name;
    }
}

class Capybara : Animal
{
    private readonly string _name = "Капибара";

    public Capybara()
    {
        Name = _name;
    }
}

class Penguin : Animal
{
    private readonly string _name = "Пингвин";

    public Penguin()
    {
        Name = _name;
    }
}