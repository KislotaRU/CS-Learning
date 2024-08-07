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
            
            battle.Begin();
        }
    }
}

static class UserUtils
{
    public const ConsoleColor ColorDefault = ConsoleColor.White;
    public const ConsoleColor ColorRed = ConsoleColor.Red;
    public const ConsoleColor ColorBlue = ConsoleColor.Blue;

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
        int halfAmount = 2;

        return s_random.Next(maxNumber / halfAmount, maxNumber);
    }

    public static int GenerateRandomPercent()
    {
        int fullAmount = 100;

        return s_random.Next(fullAmount);
    }

    public static void PaintForeground(ConsoleColor consoleColor)
    {
        Console.ForegroundColor = consoleColor;
    }
}

class Squad
{
    private const string ProfessionRifleman = "Rifleman";
    private const string ProfessionTankman = "Tankman";

    private const int MaxCountSoldiers = 4;

    private readonly Soldier[] _professionsSoldiers;

    private readonly List<Soldier> _soldiers;

    private Soldier _activeSoldier;

    public Squad()
    {
        _soldiers = new List<Soldier>();

        _professionsSoldiers = new Soldier[]
        {
            new Rifleman(),
            new Tankman(),
        };
    }

    public int CountSoldiers => _soldiers.Count;
    public int DistanceInFight { get; private set; }

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

            int separatorLength = 3;
            int messageLength = 0;

            for (int i = 0; i < status.Length; i++)
            {
                messageLength += status[i].Length + separatorLength;

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

    public void ShowAction(float damageTaken, int healthTaken, string typeAttack, string nameEnemy)
    {
        Console.Write($"{_activeSoldier} атаковал противника {nameEnemy} {typeAttack} на {(int)damageTaken} единиц урона.\n");

        if (healthTaken > 0)
            Console.Write($"Противник {nameEnemy} восстановил себе здоровье на {healthTaken} единиц.");
    }

    public void MakeMove(int distanceToEnemy, string nameSquad)
    {
        int minStepDistance = -2;
        int maxStepDistance = distanceToEnemy + 1;

        int step = UserUtils.GenerateRandomNumber(minStepDistance, maxStepDistance);

        DistanceInFight += step;

        if (DistanceInFight < 0)
            DistanceInFight = 0;

        if (step > 0)
            Console.Write($"\n{nameSquad} продвинулся к противнику на кол-во клеток ({step}).\n");
        else if (step == 0)
            Console.Write($"\n{nameSquad} удерживает позицию.\n");
        else
            Console.Write($"\n{nameSquad} отступил от противника на кол-во клеток ({step}).\n");
    }

    public int Attack(int distanceEnemy, out string typeAttack)
    {
        int indexSoldier = UserUtils.GenerateRandomNumber(_soldiers.Count);
        _activeSoldier = _soldiers[indexSoldier];

        return _activeSoldier.Attack(distanceEnemy, out typeAttack);
    }

    public void TakeDamage(int damage, out float damageTake, out int healthTake, out string nameSoldier)
    {
        int indexSoldier = UserUtils.GenerateRandomNumber(_soldiers.Count);
        _activeSoldier = _soldiers[indexSoldier];

        nameSoldier = _activeSoldier.GetType().Name;

        _activeSoldier.TakeDamage(damage, out damageTake, out healthTake);

        if (_activeSoldier.HealthPoints <= 0)
            _soldiers.Remove(_activeSoldier);
    }

    public void Create()
    {
        int minNumber = 0;
        int maxNumber = _professionsSoldiers.Length;

        while (CountSoldiers < MaxCountSoldiers)
        {
            int indexProfession = UserUtils.GenerateRandomNumber(minNumber, maxNumber);

            _soldiers.Add(_professionsSoldiers[indexProfession].Clone());
        }
    }
}

class Battle
{
    private const int DistanceBattle = 10;

    private readonly Squad _squadRed;
    private readonly Squad _squadBlue;

    public Battle()
    {
        _squadRed = new Squad();
        _squadBlue = new Squad();
    }

    public void Begin()
    {
        _squadRed.Create();
        _squadBlue.Create();

        Console.Write("Началась Война между двумя странами!\n" +
                      "*Нажмите любую кнопку*\n");

        Console.ReadKey();
        Console.Clear();

        Fight();
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
        string nameSquadRed = "Красный взвод";
        string nameSquadBlue = "Синий взвод";

        string squadWinner;

        int countOpponents = 2;
        int countMoves = 0;

        int damageToEnemy;
        int distanceToEnemy;

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
                Console.Write($"Ходит {nameSquadBlue}:\n");

                UserUtils.PaintForeground(UserUtils.ColorBlue);
                damageToEnemy = _squadBlue.Attack(distanceToEnemy, out string typeAttack);
                _squadBlue.MakeMove(distanceToEnemy, nameSquadBlue);
                _squadRed.TakeDamage(damageToEnemy, out float damageTake, out int healthTake, out string nameSoldier);

                _squadBlue.ShowAction(damageTake, healthTake, typeAttack, nameSoldier);
            }
            else
            {
                Console.Write($"Ходит {nameSquadRed}:\n");

                UserUtils.PaintForeground(UserUtils.ColorRed);
                damageToEnemy = _squadRed.Attack(distanceToEnemy, out string typeAttack);
                _squadRed.MakeMove(distanceToEnemy, nameSquadRed);
                _squadBlue.TakeDamage(damageToEnemy, out float damageTake, out int healthTake, out string nameSoldier);

                _squadRed.ShowAction(damageTake, healthTake, typeAttack, nameSoldier);
            }

            UserUtils.PaintForeground(UserUtils.ColorDefault);
            Console.ReadKey();
        }

        if (_squadRed.CountSoldiers <= 0)
            squadWinner = nameSquadBlue;
        else
            squadWinner = nameSquadRed;

        Console.Write($"\n\nВ этой кровопролитной войне вверх одержал {squadWinner}.\n" +
                      $"Но какой ценной...\n\n");
    }
}

abstract class Soldier
{
    private readonly int _shootingAttack = 0;
    private readonly int _meleeAttack = 10;
    private readonly int _healthPoints = 100;
    private readonly int _armor = 5;
    private readonly int _fightingSpirit = 0;
    private readonly int _medication = 0;

    public Soldier()
    {
        ShootingAttack = _shootingAttack;
        MeleeAttack = _meleeAttack;
        HealthPoints = _healthPoints;
        Armor = _armor;
        FightingSpirit = _fightingSpirit;
        Medication = _medication;
    }

    public int ShootingAttack { get; protected set; }
    public int MeleeAttack { get; protected set; }
    public int HealthPoints { get; protected set; }
    public int Armor { get; protected set; }
    public int FightingSpirit { get; protected set; }
    public int Medication { get; protected set; }

    public virtual int Attack(int distanceEnemy, out string typeAttack)
    {
        int fullAmount = 100;
        int distanceMeleeAttack = 1;
        float damage;

        if (distanceEnemy > distanceMeleeAttack)
        {
            damage = ShootingAttack + (float)ShootingAttack / fullAmount * FightingSpirit;
            typeAttack = "ДАЛЬНИМ оружием";
        }
        else
        {
            damage = MeleeAttack + (float)MeleeAttack / fullAmount * FightingSpirit;
            typeAttack = "БЛИЖНИМ оружием";
        }

        return (int)damage;
    }

    public virtual void TakeDamage(int damage, out float damageTake, out int healthTake)
    {
        int fullAmount = 100;
        int halfAmount = 50;
        healthTake = 0;
        damageTake = damage - (float)damage / fullAmount * Armor;

        if (HealthPoints - (int)damageTake > 0)
        {
            HealthPoints -= (int)damageTake;

            if (Medication > 0)
            {
                healthTake = (int)(damageTake / fullAmount * halfAmount);
                HealthPoints += healthTake;
                Medication--;
            }
        }
        else
        {
            HealthPoints = 0;
        }
    }

    public abstract Soldier Clone();
}

class Rifleman : Soldier
{
    private readonly int _shootingAttack = 30;
    private readonly int _meleeAttack = 20;
    private readonly int _healthPoints = 120;
    private readonly int _armor = 25;
    private readonly int _fightingSpirit = 5;
    private readonly int _medication = 2;

    public Rifleman()
    {
        ShootingAttack = UserUtils.GenerateRandomNumber(_shootingAttack);
        MeleeAttack = UserUtils.GenerateRandomNumber(_meleeAttack);
        HealthPoints = UserUtils.GenerateRandomNumber(_healthPoints);
        Armor = UserUtils.GenerateRandomNumber(_armor);
        FightingSpirit = UserUtils.GenerateRandomNumber(_fightingSpirit);
        Medication = UserUtils.GenerateRandomNumber(_medication);
    }

    public override Soldier Clone() => new Rifleman();
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
        ShootingAttack = UserUtils.GenerateRandomNumber(_shootingAttack);
        MeleeAttack = UserUtils.GenerateRandomNumber(_meleeAttack);
        HealthPoints = UserUtils.GenerateRandomNumber(_healthPoints);
        Armor = UserUtils.GenerateRandomNumber(_armor);
        FightingSpirit = UserUtils.GenerateRandomNumber(_fightingSpirit);
        Medication = UserUtils.GenerateRandomNumber(_medication);
    }

    public override Soldier Clone() => new Tankman();

    public override void TakeDamage(int damage, out float damageTake, out int healthTake)
    {
        if (_chanceBlockDamage > UserUtils.GenerateRandomPercent())
        {
            base.TakeDamage(damage, out damageTake, out healthTake);
        }
        else
        {
            damageTake = 0;
            healthTake = 0;
        }
    }
}