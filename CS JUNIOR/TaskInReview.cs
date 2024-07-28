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

            War war = new War();

            war.Begin();
        }
    }
}

class Barrack
{
    private const string ProfessionRifleman = "Rifleman";
    private const string ProfessionTankman = "Tankman";
    private const string ProfessionBorderman = "Borderman";
    private const string ProfessionDoctor = "Doctor";
    private const string ProfessionArtilleryman = "Artilleryman";

    private const int CountSoldiers = 3;

    private readonly Random _random;

    private readonly List<Soldier> _soldiers;

    private readonly string[] _professionsSoldiers;

    public Barrack()
    {
        _random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

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

    public List<Soldier> GetSoldiers()
    {
        List<Soldier> temporarySoldiers = new List<Soldier>();

        foreach (Soldier soldier in _soldiers)
        {
            temporarySoldiers.Add(soldier);
        }

        return temporarySoldiers;
    }

    public void CreateSoldier()
    {
        int minRandom = 0;
        int maxRandom = _professionsSoldiers.Length;

        while (_soldiers.Count < CountSoldiers)
        {
            int indexProfession = _random.Next(minRandom, maxRandom);

            string temporaryProfession = _professionsSoldiers[indexProfession];

            switch (temporaryProfession)
            {
                case ProfessionRifleman:
                    _soldiers.Add(new Rifleman());
                    break;

                case ProfessionTankman:
                    _soldiers.Add(new Tankman());
                    break;

                case ProfessionBorderman:
                    _soldiers.Add(new Borderman());
                    break;

                case ProfessionDoctor:
                    _soldiers.Add(new Doctor());
                    break;

                case ProfessionArtilleryman:
                    _soldiers.Add(new Artilleryman());
                    break;
            }
        }
    }
}

class Squad
{
    private const int FullAmount = 100;

    private const int MaxCountSoldiers = 3;

    private const int DistanceMeleeAttack = 1;

    private readonly Barrack _barrack;
    private List<Soldier> _soldiers;

    public Squad()
    {
        _barrack = new Barrack();
        _soldiers = new List<Soldier>();
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
        float damage;
        typeAttack = null;

        if (distanceEnemy > DistanceMeleeAttack)
        {
            damage = ShootingAttack + (float)ShootingAttack / FullAmount * FightingSpirit;
            typeAttack = "ДАЛЬНИМ оружием";
        }
        else
        {
            damage = MeleeAttack + (float)MeleeAttack / FullAmount * FightingSpirit;
            typeAttack = "БЛИЖНИМ оружием";
        }

        return (int)damage;
    }

    public void TakeDamage(int damage, out float takeDamage, out int takeHealth)
    {
        takeHealth = 0;
        takeDamage = damage - (float)damage / FullAmount * Armor;

        if (HealthPoints - (int)takeDamage > 0)
        {
            HealthPoints -= (int)takeDamage;

            takeHealth = (int)(takeDamage / FullAmount * Medication) < 0 ? 1 : (int)(takeDamage / FullAmount * Medication);

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

    public void CreateSquad()
    {
        _barrack.CreateSoldier();

        List<Soldier> temporarySoldiers = _barrack.GetSoldiers();

        foreach (Soldier soldier in temporarySoldiers)
        {
            if (_soldiers.Count < MaxCountSoldiers)
            {
                _soldiers.Add(soldier);

                ShootingAttack += soldier.ShootingAttack / MaxCountSoldiers;
                MeleeAttack += soldier.MeleeAttack;
                HealthPoints += soldier.HealthPoints;
                Armor += soldier.Armor / MaxCountSoldiers;
                FightingSpirit += soldier.FightingSpirit;
                Medication += soldier.Medication;
            }
        }
    }
}

class War
{
    private readonly Squad _squadRed;
    private readonly Squad _squadBlue;

    private Battle _battle;

    private string _squadWinner;

    public War()
    {
        _squadRed = new Squad();
        _squadBlue = new Squad();
    }

    public void Begin()
    {
        _squadRed.CreateSquad();
        _squadBlue.CreateSquad();

        Console.Write("Началась Война между двумя странами!\n" +
                      "*Нажмите любую кнопку*\n");

        Console.ReadKey();
        Console.Clear();

        _battle = new Battle(_squadRed, _squadBlue);

        _battle.Fight(out _squadWinner);

        SendMessageResults(_squadWinner);
    }

    private void SendMessageResults(string squadWinner)
    {
        Console.Write($"\n\nВ этой кровопролитной войне вверх одержал {squadWinner}.\n" +
                      $"Но какой ценной...\n\n");
    }
}

class Battle
{
    private const ConsoleColor ColorDefault = ConsoleColor.White;
    private const ConsoleColor ColorRed = ConsoleColor.Red;
    private const ConsoleColor ColorBlue = ConsoleColor.Blue;

    private const string NameSquadRed = "Красный взвод";
    private const string NameSquadBlue = "Синий взвод";

    private readonly Squad _squadRed;
    private readonly Squad _squadBlue;

    private int _distanceBattle = 10;
    private int _distanceSquadRed = 0;
    private int _distanceSquadBlue = 0;
    private int _distanceEnemys = 0;


    private int _countMoves = 0;

    public Battle(Squad squadRed, Squad squadBlue)
    {
        _squadRed = squadRed;
        _squadBlue = squadBlue;

        _distanceEnemys = _distanceBattle - (squadRed.DistanceInFight + squadBlue.DistanceInFight);
    }

    public void Show()
    {
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

    public void Fight(out string squadWinner)
    {
        Random random;

        squadWinner = null;

        while (_squadRed.HealthPoints > 0 & _squadBlue.HealthPoints > 0)
        {
            Console.Clear();

            random = new Random((int)DateTime.Now.Ticks);

            _countMoves++;

            _squadRed.ShowStatus(ColorRed, ColorDefault);
            _squadBlue.ShowStatus(ColorBlue, ColorDefault);

            Show();

            Console.Write($"Ход #{_countMoves}\n");

            if (_countMoves % 2 == 0)
            {
                Console.ForegroundColor = ColorBlue;
                MakeMove(NameSquadBlue, _squadBlue, _squadRed, random);
            }
            else
            {
                Console.ForegroundColor = ColorRed;
                MakeMove(NameSquadRed, _squadRed, _squadBlue, random);
            }

            Console.ForegroundColor = ColorDefault;
            Console.ReadKey();
        }

        if (_squadRed.HealthPoints <= 0)
            squadWinner = NameSquadBlue;
        else
            squadWinner = NameSquadRed;
    }

    private void MakeMove(string nameSquad, Squad squadAttacking, Squad squadDefending, Random random)
    {
        int minStepDistance = -2;
        int maxStepDistance = _distanceEnemys + 1;

        int step = random.Next(minStepDistance, maxStepDistance);

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
    public const string KeyShootingAttack = "ShootingAttack";
    public const string KeyMeleeAttack = "MeleeAttack";
    public const string KeyHealthPoints = "HealthPoints";
    public const string KeyArmor = "Armor";
    public const string KeyFightingSpirit = "FightingSpirit";
    public const string KeyMedication = "Medication";

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