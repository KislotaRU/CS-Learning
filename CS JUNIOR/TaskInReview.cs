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
            Console.ForegroundColor = ConsoleColor.White;

            Battle battle = new Battle();
            
            battle.Begin();
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

    public static int GenerateRandomNumber(int minNumber, int maxNumber)
    {
        return s_random.Next(minNumber, maxNumber);
    }
}

class Squad
{
    private const string ProfessionRifleman = "Rifleman";
    private const string ProfessionTankman = "Tankman";
    private const string ProfessionBorderman = "Borderman";
    private const string ProfessionDoctor = "Doctor";
    private const string ProfessionArtilleryman = "Artilleryman";

    private const int MaxCountSoldiers = 4;

    private readonly string[] _professionsSoldiers;

    private List<Soldier> _soldiers;

    public Squad()
    {
        _soldiers = new List<Soldier>();

        _professionsSoldiers = new string[]
        {
            ProfessionRifleman,
            ProfessionTankman,
            ProfessionBorderman,
            ProfessionDoctor,
            ProfessionArtilleryman
        };
    }

    public int ShootingAttack { get; private set; }
    public int MeleeAttack { get; private set; }
    public int HealthPoints { get; private set; }
    public int Armor { get; private set; }
    public int FightingSpirit { get; private set; }
    public int Medication { get; private set; }
    public int DistanceInFight { get; private set; }

    public void ShowStatus(ConsoleColor colorSquad, ConsoleColor colorDefault)
    {
        string[] status = new string[]
        {
            $"Здоровье: {HealthPoints}",
            $"Дальняя атака: {ShootingAttack}",
            $"Ближняя атака: {MeleeAttack}",
            $"Броня: {Armor}",
            $"Боевой дух: {FightingSpirit}",
            $"Аптеки: {Medication}"
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
            Console.ForegroundColor = colorSquad;
            Console.Write(status[i].PadRight(status[i].Length));
            Console.ForegroundColor = colorDefault;
            Console.Write(" │ ");
        }

        Console.Write("\n└" + new string('─', messageLength) + "┘\n");
    }

    public void ShowAttack(float damageTaken, int healthTaken, string typeAttack)
    {
        Console.Write($"Атаковал противника {typeAttack} на {(int)damageTaken} единиц урона.\n");

        if (healthTaken > 0)
            Console.Write($"Противник восстановил себе здоровье на {healthTaken} единиц.");
    }

    public int Attack(int distanceEnemy, out string typeAttack)
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

    public void TakeDamage(int damage, out float takeDamage, out int takeHealth)
    {
        int fullAmount = 100;
        takeHealth = 0;
        takeDamage = damage - (float)damage / fullAmount * Armor;

        if (HealthPoints - (int)takeDamage > 0)
        {
            HealthPoints -= (int)takeDamage;

            takeHealth = (int)(takeDamage / fullAmount * Medication) < 0 ? 1 : (int)(takeDamage / fullAmount * Medication);

            HealthPoints += takeHealth;

            if (Medication > 0)
                Medication--;
        }
        else
        {
            HealthPoints = 0;
        }
    }

    public void Walk(int step)
    {
        DistanceInFight += step;

        if (DistanceInFight < 0)
            DistanceInFight = 0;
    }

    public void Create()
    {
        Soldier temporarySoldier;

        int minNumber = 0;
        int maxNumber = _professionsSoldiers.Length;

        while (_soldiers.Count < MaxCountSoldiers)
        {
            int indexProfession = UserUtils.GenerateRandomNumber(minNumber, maxNumber);

            string professionsSoldier = _professionsSoldiers[indexProfession];

            switch (professionsSoldier)
            {
                case ProfessionRifleman:
                    temporarySoldier = new Rifleman();
                    break;

                case ProfessionTankman:
                    temporarySoldier = new Tankman();
                    break;

                case ProfessionBorderman:
                    temporarySoldier = new Borderman();
                    break;

                case ProfessionDoctor:
                    temporarySoldier = new Doctor();
                    break;

                case ProfessionArtilleryman:
                    temporarySoldier = new Artilleryman();
                    break;

                default:
                    temporarySoldier = null;
                    break;
            }

            _soldiers.Add(temporarySoldier);

            ShootingAttack += temporarySoldier.ShootingAttack / MaxCountSoldiers;
            MeleeAttack += temporarySoldier.MeleeAttack;
            HealthPoints += temporarySoldier.HealthPoints;
            Armor += temporarySoldier.Armor / MaxCountSoldiers;
            FightingSpirit += temporarySoldier.FightingSpirit;
            Medication += temporarySoldier.Medication;
        }
    }
}

class Battle
{
    private readonly Squad _squadRed;
    private readonly Squad _squadBlue;

    private int _distanceBattle = 10;
    private int _distanceEnemys = 0;

    public Battle()
    {
        _squadRed = new Squad();
        _squadBlue = new Squad();

        _distanceEnemys = _distanceBattle - (_squadRed.DistanceInFight + _squadBlue.DistanceInFight);
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

    public void Show()
    {
        ConsoleColor ColorDefault = ConsoleColor.White;
        ConsoleColor ColorRed = ConsoleColor.Red;
        ConsoleColor ColorBlue = ConsoleColor.Blue;

        int freeDistance;

        Console.Write("Дистанция между взводами:\n");

        Console.Write("┌" + new string('─', _distanceBattle) + "┐\n");
        Console.Write("│");

        freeDistance = _distanceBattle - (_squadRed.DistanceInFight + _squadBlue.DistanceInFight);

        Console.ForegroundColor = ColorRed;
        Console.Write(new string('#', _squadRed.DistanceInFight));

        if (freeDistance > 0)
        {
            Console.ForegroundColor = ColorDefault;
            Console.Write(new string('#', freeDistance));
        }

        Console.ForegroundColor = ColorBlue;
        Console.Write(new string('#', _squadBlue.DistanceInFight));

        Console.ForegroundColor = ColorDefault;

        Console.Write("│");
        Console.Write("\n└" + new string('─', _distanceBattle) + "┘\n");
    }

    public void Fight()
    {
        ConsoleColor colorDefault = ConsoleColor.White;
        ConsoleColor colorRed = ConsoleColor.Red;
        ConsoleColor colorBlue = ConsoleColor.Blue;

        string nameSquadRed = "Красный взвод";
        string nameSquadBlue = "Синий взвод";

        string squadWinner;

        int countMoves = 0;

        while (_squadRed.HealthPoints > 0 & _squadBlue.HealthPoints > 0)
        {
            Console.Clear();

            _squadRed.ShowStatus(colorRed, colorDefault);
            _squadBlue.ShowStatus(colorBlue, colorDefault);

            Show();

            Console.Write($"Ход #{++countMoves}\n");

            if (countMoves % 2 == 0)
            {
                Console.ForegroundColor = colorBlue;
                MakeMove(nameSquadBlue, _squadBlue, _squadRed);
            }
            else
            {
                Console.ForegroundColor = colorRed;
                MakeMove(nameSquadRed, _squadRed, _squadBlue);
            }

            Console.ForegroundColor = colorDefault;
            Console.ReadKey();
        }

        if (_squadRed.HealthPoints <= 0)
            squadWinner = nameSquadBlue;
        else
            squadWinner = nameSquadRed;

        Console.Write($"\n\nВ этой кровопролитной войне вверх одержал {squadWinner}.\n" +
                      $"Но какой ценной...\n\n");
    }

    private void MakeMove(string nameSquad, Squad squadAttacking, Squad squadDefending)
    {
        int minStepDistance = -2;
        int maxStepDistance = _distanceEnemys + 1;

        int step = UserUtils.GenerateRandomNumber(minStepDistance, maxStepDistance);

        Console.Write($"Ходит {nameSquad}:\n\n");

        squadDefending.TakeDamage(squadAttacking.Attack(_distanceEnemys, out string typeAttack), out float damageTaken, out int HealthTaken);

        squadAttacking.ShowAttack(damageTaken, HealthTaken, typeAttack);

        if (step > 0)
            Console.Write($"\n{nameSquad} продвинулся к противнику на кол-во клеток ({step}).\n");
        else if (step == 0)
            Console.Write($"\n{nameSquad} удерживает позицию.\n");
        else
            Console.Write($"\n{nameSquad} отступил от противника на кол-во клеток ({step}).\n");

        squadAttacking.Walk(step);

        _distanceEnemys = _distanceBattle - (squadAttacking.DistanceInFight + squadDefending.DistanceInFight);
    }
}

abstract class Soldier
{
    private int _shootingAttack = 0;
    private int _meleeAttack = 15;
    private int _healthPoints = 100;
    private int _armor = 10;
    private int _fightingSpirit = 0;
    private int _medication = 0;

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

    //public int Attack()
    //{
    //    return
    //}
}

class Rifleman : Soldier
{
    private int _healthPoints = 110;
    private int _shootingAttack = 30;
    private int _meleeAttack = 20;
    private int _armor = 25;

    public Rifleman()
    {
        HealthPoints = _healthPoints;
        ShootingAttack = _shootingAttack;
        MeleeAttack = _meleeAttack;
        Armor = _armor;
    }
}

class Tankman : Soldier
{
    private int _healthPoints = 130;
    private int _shootingAttack = 40;
    private int _armor = 60;
    private int _fightingSpirit = 5;

    public Tankman()
    {
        HealthPoints = _healthPoints;
        ShootingAttack = _shootingAttack;
        Armor = _armor;
        FightingSpirit = _fightingSpirit;
    }
}

class Borderman : Soldier
{
    private int _shootingAttack = 25;
    private int _meleeAttack = 20;
    private int _armor = 20;
    private int _fightingSpirit = -2;

    public Borderman()
    {
        ShootingAttack = _shootingAttack;
        MeleeAttack = _meleeAttack;
        Armor = _armor;
        FightingSpirit = _fightingSpirit;
    }
}

class Doctor : Soldier
{
    private int _shootingAttack = 10;
    private int _meleeAttack = 5;
    private int _medication = 12;

    public Doctor()
    {
        ShootingAttack = _shootingAttack;
        MeleeAttack = _meleeAttack;
        Medication = _medication;
    }
}

class Artilleryman : Soldier
{
    private int _healthPoints = 90;
    private int _shootingAttack = 60;
    private int _fightingSpirit = 10;

    public Artilleryman()
    {
        HealthPoints = _healthPoints;
        ShootingAttack = _shootingAttack;
        FightingSpirit = _fightingSpirit;
    }
}