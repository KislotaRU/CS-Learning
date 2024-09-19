using System;
using System.Collections.Generic;

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
    public const int HundredPercent = 100;

    private readonly static Random s_random;

    static UserUtils()
    {
        s_random = new Random();
    }

    public static int GenerateRandomNumber(int minNumber = 0, int maxNumber = 0) =>
        s_random.Next(minNumber, maxNumber);

    public static int GenerateRandomNumber(int maxNumber) =>
        s_random.Next(maxNumber);

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
            float damage;

            Console.Write("\t\tБой\n");


            if (_firstFighter.HealthPoints > 0)
            {
                Console.Write("\tХодит первый боец:\n");
                Console.WriteLine();

                damage = _firstFighter.Attack();

                _secondFighter.TakeDamage(damage);

                if (_secondFighter.HealthPoints > 0)
                {
                    Console.Write("\tХодит второй боец:\n");

                    _secondFighter.TakeDamage(_firstFighter.Attack());
                }
                else
                {
                    isFighting = false;
                }
            }
            else
            {
                isFighting = false;
            }

            Console.ReadLine();
            Console.Clear();
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
    public float HealthPoints { get; protected set; }
    public int Armor { get; protected set; }
    public float Damage { get; protected set; }

    public void Show() =>
        Console.Write($"Боец: {Name}".PadRight(17) + $"Хп: {HealthPoints}".PadRight(8) + $"Броня: {Armor}".PadRight(10) + $"Урон: {Damage}\n");

    public virtual float Attack() =>
        Damage;

    public virtual void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            damage -= damage / UserUtils.HundredPercent * Armor;
            HealthPoints -= damage;
        }

        if (HealthPoints <= 0)
            HealthPoints = 0;
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

    private readonly int _chanceDoubleAttack = 40;
    private readonly int _damageСoefficient = 2;

    public Warrior()
    {
        Name = _name;
        HealthPoints = UserUtils.GenerateRandomNumber(_minHealthPoints, _maxHealthPoints);
        Armor = UserUtils.GenerateRandomNumber(_minArmor, _maxArmor);
        Damage = UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);
    }

    public Warrior(float healthPoints, int armor, float damage)
    {
        Name = _name;
        HealthPoints = healthPoints;
        Armor = armor;
        Damage = damage;
    }

    public override float Attack()
    {
        int chanceDropped = UserUtils.GenerateRandomNumber(UserUtils.HundredPercent);
        float damage = 0f;

        damage += Attack();

        if (_chanceDoubleAttack <= chanceDropped)
            damage *= _damageСoefficient;

        return damage;
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

    private readonly int _attackTriggerNumber = 3;

    private int _attacksCount = 0;

    public Crossbowman()
    {
        Name = _name;
        HealthPoints = UserUtils.GenerateRandomNumber(_minHealthPoints, _maxHealthPoints);
        Armor = UserUtils.GenerateRandomNumber(_minArmor, _maxArmor);
        Damage = UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);
    }

    public Crossbowman(float healthPoints, int armor, float damage)
    {
        Name = _name;
        HealthPoints = healthPoints;
        Armor = armor;
        Damage = damage;
    }

    public override float Attack()
    {
        float damage = 0f;

        damage += Attack();
        _attacksCount++;

        if (_attacksCount % _attackTriggerNumber == 0)
            damage += Attack();

        return damage;
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

    private readonly int _maxNumberRage = 40;
    private readonly int _percentRecovery = 25;

    private float _rageCount = 0f;
    private float _healPoints = 0f;

    public Berserk()
    {
        Name = _name;
        HealthPoints = UserUtils.GenerateRandomNumber(_minHealthPoints, _maxHealthPoints);
        Armor = UserUtils.GenerateRandomNumber(_minArmor, _maxArmor);
        Damage = UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);
    }

    public Berserk(float healthPoints, int armor, float damage)
    {
        Name = _name;
        HealthPoints = healthPoints;
        Armor = armor;
        Damage = damage;
    }

    public override float Attack()
    {
        if (_rageCount >= _maxNumberRage)
        {
            TakeHealth(_healPoints);
            _rageCount = 0;
        }

        return base.Attack();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        _rageCount += damage;
        _healPoints += damage / UserUtils.HundredPercent * _percentRecovery;
    }

    public override Fighter Clone(Fighter fighter) =>
        new Berserk(fighter.HealthPoints, fighter.Armor, fighter.Damage);

    private void TakeHealth(float healPoints)
    {
        if (healPoints > 0)
        {
            HealthPoints += healPoints;
            _healPoints = 0;
        }
    }
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

    private int _magicPoints = 80;

    private int _priceSpellFireball = 25;
    private float _damageFireball = 20f;

    public Wizard()
    {
        Name = _name;
        HealthPoints = UserUtils.GenerateRandomNumber(_minHealthPoints, _maxHealthPoints);
        Armor = UserUtils.GenerateRandomNumber(_minArmor, _maxArmor);
        Damage = UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);
    }

    public Wizard(float healthPoints, int armor, float damage)
    {
        Name = _name;
        HealthPoints = healthPoints;
        Armor = armor;
        Damage = damage;
    }

    public override float Attack()
    {
        float damage = 0f;

        if (_magicPoints >= _priceSpellFireball)
            damage += AttackSpellFireball();
        else
            damage += Attack();

        return damage;
    }

    public override Fighter Clone(Fighter fighter) =>
        new Wizard(fighter.HealthPoints, fighter.Armor, fighter.Damage);

    private float AttackSpellFireball()
    {
        _magicPoints -= _priceSpellFireball;

        return _damageFireball;
    }
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

    private readonly int _chanceToDodge = 35;

    public Assassin()
    {
        Name = _name;
        HealthPoints = UserUtils.GenerateRandomNumber(_minHealthPoints, _maxHealthPoints);
        Armor = UserUtils.GenerateRandomNumber(_minArmor, _maxArmor);
        Damage = UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);
    }

    public Assassin(float healthPoints, int armor, float damage)
    {
        Name = _name;
        HealthPoints = healthPoints;
        Armor = armor;
        Damage = damage;
    }

    public override void TakeDamage(float damage)
    {
        int chanceDropped = UserUtils.GenerateRandomNumber(UserUtils.HundredPercent);

        if (chanceDropped > _chanceToDodge)
            base.TakeDamage(damage);
    }

    public override Fighter Clone(Fighter fighter) =>
        new Assassin(fighter.HealthPoints, fighter.Armor, fighter.Damage);
}