using System;
using System.Collections.Generic;

//Есть 2 взвода. 1 взвод страны один, 2 взвод страны два.
//Каждый взвод внутри имеет солдат.
//Нужно написать программу, которая будет моделировать бой этих взводов.
//Каждый боец - это уникальная единица, он может иметь уникальные способности или же уникальные характеристики, такие как повышенная сила.
//Побеждает та страна, во взводе которой остались выжившие бойцы.
//Не важно, какой будет бой, рукопашный, стрелковый.

namespace CS_JUNIOR
{
    class TaskInReview
    {
        static void Main()
        {
            UserUtils.PaintForeground(UserUtils.ColorDefault);

            Battle battle = new Battle();
            
            battle.Work();
        }
    }
}

static class UserUtils
{
    public const ConsoleColor ColorDefault = ConsoleColor.White;
    public const ConsoleColor ColorRed = ConsoleColor.Red;
    public const ConsoleColor ColorBlue = ConsoleColor.Blue;

    public const int FullValue = 100;
    public const int HalfValue = 50;

    private static readonly Random s_random;

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

    public static void PaintForeground(ConsoleColor consoleColor)
    {
        Console.ForegroundColor = consoleColor;
    }
}

class Squad
{
    private readonly List<Soldier> _soldiers;

    private readonly int _maxCountSoldiers = 4;

    private Soldier _activeSoldier;

    public Squad(string name)
    {
        _soldiers = new List<Soldier>();
        Name = name;
    }

    public int CountSoldiers => _soldiers.Count;
    public int DistanceInFight { get; private set; }
    public string Name { get; private set; }

    public void Create()
    {
        Soldier[] _professionsSoldiers = new Soldier[]
        {
            new Rifleman(),
            new Tankman(),
        };

        while (CountSoldiers < _maxCountSoldiers)
        {
            int indexProfession = UserUtils.GenerateRandomNumber(_professionsSoldiers.Length);

            _soldiers.Add(_professionsSoldiers[indexProfession].Clone());
        }
    }

    public void ShowStatus(ConsoleColor colorSquad, ConsoleColor colorDefault)
    {
        foreach(Soldier soldier in _soldiers)
        {
            string[] status = new string[]
            {
                $"{soldier.GetType().Name}",
                $"Здоровье: {soldier.HealthPoints}",
                $"Дальняя атака: {soldier.ShootingAttack}",
                $"Ближняя атака: {soldier.MeleeAttack}",
                $"Броня: {soldier.Armor}",
                $"Боевой дух: {soldier.FightingSpirit}",
                $"Аптеки: {soldier.Medication}"
            };

            int shiftLength = 3;
            int messageLength = 0;

            for (int i = 0; i < status.Length; i++)
            {
                messageLength += status[i].Length + shiftLength;

                if (i == status.Length - 1)
                    messageLength--;
            }

            Console.Write("┌" + new string('─', messageLength) + "┐\n");
            Console.Write("│ ");

            for (int i = 0; i < status.Length; i++)
            {
                UserUtils.PaintForeground(colorSquad);
                Console.Write(status[i].PadRight(status[i].Length));
                UserUtils.PaintForeground(colorDefault);
                Console.Write(" │ ");
            }

            Console.Write("\n└" + new string('─', messageLength) + "┘\n");
        }
    }

    public void ShowAction(int damageToEnemy, int healthTaken, string typeAttack, string nameEnemy)
    {
        Console.Write($"{_activeSoldier} атаковал противника {nameEnemy} {typeAttack} на {damageToEnemy} единиц урона.\n");

        if (healthTaken > 0)
            Console.Write($"Противник {nameEnemy} восстановил себе здоровье на {healthTaken} единиц.");
    }

    public void MakeMove(Squad squadEnemy, ConsoleColor colorSquad, int distanceToEnemy)
    {
        int minStepDistance = -2;
        int maxStepDistance = distanceToEnemy + 1;

        int step = UserUtils.GenerateRandomNumber(minStepDistance, maxStepDistance);

        Soldier soldierEnemy = squadEnemy.GetRandomSoldier();

        UserUtils.PaintForeground(colorSquad);
        Console.Write($"Ходит {Name}:\n");

        Attack(soldierEnemy, distanceToEnemy, out string typeAttack, out int damageTake, out int healthTake);

        DistanceInFight += step;

        if (DistanceInFight < 0)
            DistanceInFight = 0;

        if (step > 0)
            Console.Write($"\n{Name} продвинулся к противнику на кол-во клеток ({step}).\n");
        else if (step == 0)
            Console.Write($"\n{Name} удерживает позицию.\n");
        else
            Console.Write($"\n{Name} отступил от противника на кол-во клеток ({step}).\n");

        ShowAction(damageTake, healthTake, typeAttack, soldierEnemy.GetType().Name);
    }

    public void TryRemoveDeadSolder()
    {
        for (int i = CountSoldiers; i > 0; i--)
        {
            if (_soldiers[i].HealthPoints <= 0)
                _soldiers.Remove(_soldiers[i]);
        }
    }

    private void Attack(Soldier soldierEnemy, int distanceToEnemy, out string typeAttack, out int damageTake, out int healthTake)
    {
        _activeSoldier = GetRandomSoldier();

        _activeSoldier.Attack(soldierEnemy, distanceToEnemy, out typeAttack, out damageTake, out healthTake);
    }

    private Soldier GetRandomSoldier() =>
        _soldiers[UserUtils.GenerateRandomNumber(CountSoldiers)];
}

class Battle
{
    private const int DistanceBattle = 10;

    private readonly Squad _squadRed;
    private readonly Squad _squadBlue;

    public Battle()
    {
        string nameSquadRed = "Красный взвод";
        string nameSquadBlue = "Синий взвод";

        _squadRed = new Squad(nameSquadRed);
        _squadBlue = new Squad(nameSquadBlue);
    }

    public void Work()
    {
        CreateSquads();
        Fight();
        AnnounceWinner();
    }

    public void CreateSquads()
    {
        _squadRed.Create();
        _squadBlue.Create();
    }

    private void Show()
    {
        int freeDistance;

        Console.Write("Дистанция между взводами:\n");

        Console.Write("┌" + new string('─', DistanceBattle) + "┐\n");
        Console.Write("│");

        freeDistance = DistanceBattle - (_squadRed.DistanceInFight + _squadBlue.DistanceInFight);

        UserUtils.PaintForeground(UserUtils.ColorRed);
        Console.Write(new string('#', _squadRed.DistanceInFight));

        if (freeDistance > 0)
        {
            UserUtils.PaintForeground(UserUtils.ColorDefault);
            Console.Write(new string('#', freeDistance));
        }

        UserUtils.PaintForeground(UserUtils.ColorBlue);
        Console.Write(new string('#', _squadBlue.DistanceInFight));

        UserUtils.PaintForeground(UserUtils.ColorDefault);

        Console.Write("│");
        Console.Write("\n└" + new string('─', DistanceBattle) + "┘\n");
    }

    private void Fight()
    {
        int countOpponents = 2;
        int countMoves = 0;

        int distanceToEnemy;

        Console.Write("Началась Война между двумя странами!\n" +
                      "*Нажмите любую кнопку*\n");

        Console.ReadKey();
        Console.Clear();

        while (_squadRed.CountSoldiers > 0 & _squadBlue.CountSoldiers > 0)
        {
            Console.Clear();

            _squadRed.ShowStatus(UserUtils.ColorRed, UserUtils.ColorDefault);
            _squadBlue.ShowStatus(UserUtils.ColorBlue, UserUtils.ColorDefault);

            Show();

            Console.Write($"Ход #{++countMoves}\n");

            distanceToEnemy = DistanceBattle - (_squadRed.DistanceInFight + _squadBlue.DistanceInFight);

            if (countMoves % countOpponents == 0)
            {

                _squadBlue.MakeMove(_squadRed, UserUtils.ColorBlue, distanceToEnemy);
                _squadRed.TryRemoveDeadSolder();
            }
            else
            {
                _squadRed.MakeMove(_squadBlue, UserUtils.ColorRed, distanceToEnemy);
                _squadBlue.TryRemoveDeadSolder();
            }

            UserUtils.PaintForeground(UserUtils.ColorDefault);
            Console.ReadKey();
        }
    }

    private void AnnounceWinner()
    {
        string squadWinner;

        if (_squadRed.CountSoldiers <= 0)
            squadWinner = _squadBlue.Name;
        else
            squadWinner = _squadRed.Name;

        Console.Write($"\n\nВ этой кровопролитной войне вверх одержал {squadWinner}.\n" +
                      $"Но какой ценной...\n\n");
    }
}

abstract class Soldier
{
    public int ShootingAttack { get; protected set; }
    public int MeleeAttack { get; protected set; }
    public int HealthPoints { get; protected set; }
    public int Armor { get; protected set; }
    public int FightingSpirit { get; protected set; }
    public int Medication { get; protected set; }

    public abstract Soldier Clone();

    public virtual void Attack(Soldier soldierEnemy, int distanceToEnemy, out string typeAttack, out int damageTake, out int healthTake)
    {
        int distanceMeleeAttack = 1;
        int baseDamage;
        int damage;

        if (distanceToEnemy > distanceMeleeAttack)
        {
            baseDamage = ShootingAttack;
            typeAttack = "ДАЛЬНИМ оружием";
        }
        else
        {
            baseDamage = MeleeAttack;
            typeAttack = "БЛИЖНИМ оружием";
        }

        damage = (int)(baseDamage + (float)baseDamage / UserUtils.FullValue * FightingSpirit);

        soldierEnemy.TakeDamage(damage, out damageTake);
        soldierEnemy.TakeHealth(damageTake, out healthTake);
    }

    protected virtual void TakeDamage(int damage, out int damageTake)
    {
        damageTake = (int)(damage - (float)damage / UserUtils.FullValue * Armor);

        HealthPoints -= (HealthPoints - damageTake > 0) ? damageTake : HealthPoints;
    }

    protected virtual void TakeHealth(int damageTake, out int healthTake)
    {
        healthTake = (int)((float)damageTake / UserUtils.FullValue * UserUtils.HalfValue);

        if (Medication > 0)
        {
            HealthPoints += healthTake;
            Medication--;
        }
        else
        {
            healthTake = 0; 
        }
    }
}

class Rifleman : Soldier
{
    private readonly int _shootingAttack = 30;
    private readonly int _meleeAttack = 20;
    private readonly int _healthPoints = 120;
    private readonly int _armor = 25;
    private readonly int _fightingSpirit = 5;
    private readonly int _medication = 3;

    public Rifleman()
    {
        ShootingAttack = UserUtils.GenerateRandomNumber(_shootingAttack / (UserUtils.FullValue / UserUtils.HalfValue), _shootingAttack);
        MeleeAttack = UserUtils.GenerateRandomNumber(_meleeAttack / (UserUtils.FullValue / UserUtils.HalfValue), _meleeAttack);
        HealthPoints = UserUtils.GenerateRandomNumber(_healthPoints / (UserUtils.FullValue / UserUtils.HalfValue), _healthPoints);
        Armor = UserUtils.GenerateRandomNumber(_armor / (UserUtils.FullValue / UserUtils.HalfValue), _armor);
        FightingSpirit = UserUtils.GenerateRandomNumber(_fightingSpirit / (UserUtils.FullValue / UserUtils.HalfValue), _fightingSpirit);
        Medication = UserUtils.GenerateRandomNumber(_medication / (UserUtils.FullValue / UserUtils.HalfValue), _medication);
    }

    public override Soldier Clone() =>
        new Rifleman();
}

class Tankman : Soldier
{
    private readonly int _shootingAttack = 80;
    private readonly int _meleeAttack = 5;
    private readonly int _healthPoints = 100;
    private readonly int _armor = 80;
    private readonly int _fightingSpirit = 20;
    private readonly int _medication = 2;

    private readonly int _chanceBlockDamage = 50;

    public Tankman()
    {
        ShootingAttack = UserUtils.GenerateRandomNumber(_shootingAttack / (UserUtils.FullValue / UserUtils.HalfValue), _shootingAttack);
        MeleeAttack = UserUtils.GenerateRandomNumber(_meleeAttack / (UserUtils.FullValue / UserUtils.HalfValue), _meleeAttack);
        HealthPoints = UserUtils.GenerateRandomNumber(_healthPoints / (UserUtils.FullValue / UserUtils.HalfValue), _healthPoints);
        Armor = UserUtils.GenerateRandomNumber(_armor / (UserUtils.FullValue / UserUtils.HalfValue), _armor);
        FightingSpirit = UserUtils.GenerateRandomNumber(_fightingSpirit / (UserUtils.FullValue / UserUtils.HalfValue), _fightingSpirit);
        Medication = UserUtils.GenerateRandomNumber(_medication / (UserUtils.FullValue / UserUtils.HalfValue), _medication);
    }

    public override Soldier Clone() =>
        new Tankman();

    protected override void TakeDamage(int damage, out int damageTake)
    {
        if (_chanceBlockDamage > UserUtils.GenerateRandomNumber(UserUtils.FullValue))
            base.TakeDamage(damage, out damageTake);
        else
            damageTake = 0;
    }
}