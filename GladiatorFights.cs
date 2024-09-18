using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Arena arena = new Arena();

            Console.ForegroundColor = ConsoleColor.White;

            arena.Work();
        }
    }
}

static class UserUtils
{
    private readonly static Random s_random;

    static UserUtils()
    {
        s_random = new Random();
    }

    public static int GenerateRandomNumber(int minNumber = 0, int maxNumber = 0) =>
        s_random.Next(minNumber, maxNumber);

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

class Arena
{
    private readonly List<Fighter> _fighters;

    private Fighter _firstFighter;
    private Fighter _secondFighter;

    public Arena()
    {
        _fighters = new List<Fighter>()
        {
            new Warrior(),
            new Crossbowman(),
            new Berserk(),
            new Wizard(),
            new Assassin(),
        };
    }

    public void Work()
    {
        const string CommandFight = "Начать бой";
        const string CommandExit = "Выйти";

        string[] menu = new string[]
        {
            CommandFight,
            CommandExit
        };

        string userInput;
        bool isRunning = true;

        while (isRunning)
        {
            Console.Write("\t\tМеню Арены.\n\n");

            Console.Write("Доступные команды:\n");
            PrintMenu(menu);

            Console.Write("\nОжидается ввод: ");
            userInput = GetCommandMenu(menu);

            Console.Clear();

            switch (userInput)
            {
                case CommandFight:
                    Fight();
                    break;

                case CommandExit:
                    Console.Write("Вы завершили работу программы.\n\n");
                    isRunning = false;
                    continue;

                default:
                    Console.Write("Требуется ввести номер команды или саму команду.\n\n");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    private void Fight()
    {
        Console.Write("Первый боец:\n");
        _firstFighter = ChooseFighter();
        Console.Clear();

        Console.Write("Второй боец:\n");
        _secondFighter = ChooseFighter();
        Console.Clear();

        bool isFighting = true;

        while (isFighting)
        {
            Console.Write("\t\tБой\n");

            if (_firstFighter.HealthPoints <= 0 || _secondFighter.HealthPoints <= 0)
                isFighting = false;
        }

        AnnounceResults();
    }

    private Fighter ChooseFighter()
    {
        Fighter fighter;
        int numberFighter;

        ShowFighters();

        Console.Write("Выберите бойца: ");
        numberFighter = UserUtils.ReadInt();

        while (numberFighter <= 0 || numberFighter > _fighters.Count)
        {
            Console.Write("Под таким номером нет бойца.\n");
            Console.Write("Выберите бойца: ");
            numberFighter = UserUtils.ReadInt();
        }

        fighter = _fighters[numberFighter - 1];

        return fighter.Clone(fighter);
    }

    private void ShowFighters()
    {
        int numberFighter = 1;

        foreach (Fighter fighter in _fighters)
        {
            Console.Write($"\t{numberFighter}. ".PadRight(4));
            fighter.Show();

            numberFighter++;
        }

        Console.WriteLine();
    }

    private void AnnounceResults()
    {
        if (_firstFighter.HealthPoints > 0)
        {
            Console.Write("Победил первый боец:\n");
            _firstFighter.Show();
        }
        else if (_secondFighter.HealthPoints > 0)
        {
            Console.Write("Победил второй боец:\n");
            _secondFighter.Show();
        }
    }

    private void PrintMenu(string[] menu)
    {
        int numberCommand = 1;

        for (int i = 0; i < menu.Length; i++)
        {
            Console.Write($"\t{numberCommand}. {menu[i]}\n");
            numberCommand++;
        }
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

abstract class Fighter
{
    public string Name { get; protected set; }
    public int HealthPoints { get; protected set; }
    public int Armor { get; protected set; }
    public int Damage { get; protected set; }

    public void Show() =>
        Console.Write($"Боец: {Name}".PadRight(17) + $"Хп: {HealthPoints}".PadRight(8) + $"Броня: {Armor}".PadRight(10) + $"Урон: {Damage}\n");

    public int Attack()
    {
        return 1;
    }

    public void TakeDamage(int damage)
    {

    }

    public abstract Fighter Clone(Fighter fighter);
}

class Warrior : Fighter
{
    private readonly string _name = "Воин";
    private readonly int _minHealthPoints = 80;
    private readonly int _maxHealthPoints = 101;
    private readonly int _minArmor = 10;
    private readonly int _maxArmor = 21;
    private readonly int _minDamage = 10;
    private readonly int _maxDamage = 21;

    public Warrior()
    {
        Name = _name;
        HealthPoints = UserUtils.GenerateRandomNumber(_minHealthPoints, _maxHealthPoints);
        Armor = UserUtils.GenerateRandomNumber(_minArmor, _maxArmor);
        Damage = UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);
    }

    public Warrior(int healthPoints, int armor, int damage)
    {
        Name = _name;
        HealthPoints = healthPoints;
        Armor = armor;
        Damage = damage;
    }

    public override Fighter Clone(Fighter fighter) =>
        new Warrior(fighter.HealthPoints, fighter.Armor, fighter.Damage);
}

class Crossbowman : Fighter
{
    private readonly string _name = "Арбалетчик";
    private readonly int _minHealthPoints = 80;
    private readonly int _maxHealthPoints = 101;
    private readonly int _minArmor = 5;
    private readonly int _maxArmor = 16;
    private readonly int _minDamage = 15;
    private readonly int _maxDamage = 26;

    public Crossbowman()
    {
        Name = _name;
        HealthPoints = UserUtils.GenerateRandomNumber(_minHealthPoints, _maxHealthPoints);
        Armor = UserUtils.GenerateRandomNumber(_minArmor, _maxArmor);
        Damage = UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);
    }

    public Crossbowman(int healthPoints, int armor, int damage)
    {
        Name = _name;
        HealthPoints = healthPoints;
        Armor = armor;
        Damage = damage;
    }

    public override Fighter Clone(Fighter fighter) =>
        new Crossbowman(fighter.HealthPoints, fighter.Armor, fighter.Damage);
}

class Berserk : Fighter
{
    private readonly string _name = "Берсерк";
    private readonly int _minHealthPoints = 100;
    private readonly int _maxHealthPoints = 121;
    private readonly int _minArmor = 20;
    private readonly int _maxArmor = 31;
    private readonly int _minDamage = 20;
    private readonly int _maxDamage = 31;

    public Berserk()
    {
        Name = _name;
        HealthPoints = UserUtils.GenerateRandomNumber(_minHealthPoints, _maxHealthPoints);
        Armor = UserUtils.GenerateRandomNumber(_minArmor, _maxArmor);
        Damage = UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);
    }

    public Berserk(int healthPoints, int armor, int damage)
    {
        Name = _name;
        HealthPoints = healthPoints;
        Armor = armor;
        Damage = damage;
    }

    public override Fighter Clone(Fighter fighter) =>
        new Berserk(fighter.HealthPoints, fighter.Armor, fighter.Damage);
}

class Wizard : Fighter
{
    private readonly string _name = "Волшебник";
    private readonly int _minHealthPoints = 90;
    private readonly int _maxHealthPoints = 101;
    private readonly int _minArmor = 5;
    private readonly int _maxArmor = 16;
    private readonly int _minDamage = 25;
    private readonly int _maxDamage = 36;

    public Wizard()
    {
        Name = _name;
        HealthPoints = UserUtils.GenerateRandomNumber(_minHealthPoints, _maxHealthPoints);
        Armor = UserUtils.GenerateRandomNumber(_minArmor, _maxArmor);
        Damage = UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);
    }

    public Wizard(int healthPoints, int armor, int damage)
    {
        Name = _name;
        HealthPoints = healthPoints;
        Armor = armor;
        Damage = damage;
    }

    public override Fighter Clone(Fighter fighter) =>
        new Wizard(fighter.HealthPoints, fighter.Armor, fighter.Damage);
}

class Assassin : Fighter
{
    private readonly string _name = "Ассасин";
    private readonly int _minHealthPoints = 100;
    private readonly int _maxHealthPoints = 111;
    private readonly int _minArmor = 20;
    private readonly int _maxArmor = 26;
    private readonly int _minDamage = 20;
    private readonly int _maxDamage = 26;

    public Assassin()
    {
        Name = _name;
        HealthPoints = UserUtils.GenerateRandomNumber(_minHealthPoints, _maxHealthPoints);
        Armor = UserUtils.GenerateRandomNumber(_minArmor, _maxArmor);
        Damage = UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);
    }

    public Assassin(int healthPoints, int armor, int damage)
    {
        Name = _name;
        HealthPoints = healthPoints;
        Armor = armor;
        Damage = damage;
    }

    public override Fighter Clone(Fighter fighter) =>
        new Assassin(fighter.HealthPoints, fighter.Armor, fighter.Damage);
}