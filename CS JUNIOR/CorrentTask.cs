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
    class CorrentTask
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;

            War war = new War();
        }
    }
}

class Barrack
{
    private const string professionRifleman = "Rifleman";
    private const string professionTankman = "Tankman";
    private const string professionBorderman = "Borderman";
    private const string professionDoctor = "Doctor";
    private const string professionArtilleryman = "Artilleryman";

    private const int CountSoldiers = 5;

    private Random random;
    private List<Soldier> _soldiers;

    private readonly string[] _professionsSoldiers;

    public Barrack()
    {
        random = new Random();
        _soldiers = new List<Soldier>();

        _professionsSoldiers = new string[]
        {
            professionRifleman,
            professionTankman,
            professionBorderman,
            professionDoctor,
            professionArtilleryman
        };

        CreateSoldier(CountSoldiers);
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

    private void CreateSoldier(int countSoldiers = 1)
    {
        int minRandom = 0;
        int maxRandom = _professionsSoldiers.Length;

        while (_soldiers.Count < countSoldiers)
        {
            int indexProfession = random.Next(minRandom, maxRandom);

            string temporaryProfession = _professionsSoldiers[indexProfession];

            switch (temporaryProfession)
            {
                case professionRifleman:
                    _soldiers.Add(new Rifleman());
                    break;

                case professionTankman:
                    _soldiers.Add(new Tankman());
                    break;

                case professionBorderman:
                    _soldiers.Add(new Borderman());
                    break;

                case professionDoctor:
                    _soldiers.Add(new Doctor());
                    break;

                case professionArtilleryman:
                    _soldiers.Add(new Artilleryman());
                    break;
            }
        }
    }
}

class Squad
{
    private const int FullAmount = 100;

    private const int MaxCountSoldiers = 5;

    private readonly List<Soldier> _soldiers;

    private readonly Dictionary<string, int> Characteristic;

    public Squad(List<Soldier> soldiers)
    {
        _soldiers = new List<Soldier>();

        Characteristic = new Dictionary<string, int>()
        {
            { Soldier.KeyShootingAttack, 0},
            { Soldier.KeyMeleeAttack, 0},
            { Soldier.KeyHealthPoints, 0},
            { Soldier.KeyArmor, 0},
            { Soldier.KeyFightingSpirit, 0},
            { Soldier.KeyMedication, 0},
        };

        CollectSquad(soldiers);
    }

    public int ShootingAttack { get => Characteristic[Soldier.KeyShootingAttack]; }
    public int MeleeAttack { get => Characteristic[Soldier.KeyMeleeAttack]; }
    public int HealthPoints { get => Characteristic[Soldier.KeyHealthPoints]; private set => Characteristic[Soldier.KeyHealthPoints] = value; }
    public int Armor { get => Characteristic[Soldier.KeyArmor]; }
    public int FightingSpirit { get => Characteristic[Soldier.KeyFightingSpirit]; }
    public int Medication { get => Characteristic[Soldier.KeyMedication]; private set => Characteristic[Soldier.KeyMedication] = value; }

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

    public void ShowAttack(float takeDamage, float takeHealth)
    {
        Console.Write($"Атаковал противника на {(int)takeDamage} единиц урона.\n");
        Console.Write($"Противник восстановил себе здоровье на {(int)takeHealth} единиц.");
    }

    public int Attack(int distanceEnemy)
    {
        float damage;

        if (distanceEnemy > 0)
            damage = ShootingAttack + (float)ShootingAttack / FullAmount * FightingSpirit;
        else
            damage = MeleeAttack + (float)MeleeAttack / FullAmount * FightingSpirit;

        return (int)damage;
    }

    public void TakeDamage(int damage, out float takeDamage, out float takeHealth)
    {
        takeHealth = 0;
        takeDamage = damage - (float)damage / FullAmount * Armor;

        if (HealthPoints - (int)takeDamage > 0)
        {
            HealthPoints -= (int)takeDamage;

            takeHealth = takeDamage / FullAmount * Medication;
            HealthPoints += (int)takeHealth;

            if (Medication > 0)
                Medication--;
        }
        else
        {
            HealthPoints = 0;
        }
    }

    private void CollectSquad(List<Soldier> soldiers)
    {
        foreach (Soldier soldier in soldiers)
        {
            if (_soldiers.Count < MaxCountSoldiers)
            {
                _soldiers.Add(soldier);
                SetCharacteristic(soldier);
            }
        }
    }

    private void SetCharacteristic(Soldier soldier)
    {
        foreach (string characteristic in soldier.Characteristic.Keys)
        {
            soldier.Characteristic.TryGetValue(characteristic, out int value);

            if (characteristic == Soldier.KeyArmor || characteristic == Soldier.KeyShootingAttack)
                value /= MaxCountSoldiers;

            Characteristic[characteristic] += value;
        }
    }
}

class Team
{
    private readonly Barrack _barrack;

    public Team()
    {
        _barrack = new Barrack();
        Squad = new Squad(_barrack.GetSoldiers());
        CreateNewSquad();
    }

    public Squad Squad { get; private set; }

    private void CreateNewSquad()
    {

    }
}

class War
{
    private readonly Team _teamRed;
    private readonly Team _teamBlue;

    private readonly Battle _battle;

    public War()
    {
        _teamRed = new Team();
        _teamBlue = new Team();

        //Console.Write("\t\tНачалась Война между двумя странами!\n");
        //Console.ReadKey();
        //Console.Clear();

        _battle = new Battle(_teamRed.Squad, _teamBlue.Squad);
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

    private int _countMoves = 0;

    public Battle(Squad squadRed, Squad squadBlue)
    {
        _squadRed = squadRed;
        _squadBlue = squadBlue;

        Fight();
    }

    public void Show(int distanceSquad1, int distanceSquad2)
    {
        int freeDistance;

        Console.Write("Дистанция между взводами:\n");

        Console.Write("┌" + new string('─', _distanceBattle) + "┐\n");
        Console.Write("│");

        freeDistance = _distanceBattle - (distanceSquad1 + distanceSquad2);

        Console.ForegroundColor = ColorRed;
        Console.Write(new string('#', distanceSquad1));

        if (freeDistance > 0)
        {
            Console.ForegroundColor = ColorDefault;
            Console.Write(new string('#', freeDistance));
        }

        Console.ForegroundColor = ColorBlue;
        Console.Write(new string('#', distanceSquad2));

        Console.ForegroundColor = ColorDefault;

        Console.Write("│");
        Console.Write("\n└" + new string('─', _distanceBattle) + "┘\n");
    }

    private void Fight()
    {
        Random random;

        while (_squadRed.HealthPoints > 0 & _squadBlue.HealthPoints > 0)
        {
            random = new Random();

            _countMoves++;

            _squadRed.ShowStatus(ColorRed, ColorDefault);
            _squadBlue.ShowStatus(ColorBlue, ColorDefault);

            Show(_distanceSquadRed, _distanceSquadBlue);

            Console.Write($"Ход #{_countMoves}\n");

            if (_countMoves % 2 == 0)
            {
                Console.ForegroundColor = ColorBlue;
                MakeMove(NameSquadBlue, _squadBlue, _squadRed, ref _distanceSquadBlue, ref _distanceSquadRed, random);
            }
            else
            {
                Console.ForegroundColor = ColorRed;
                MakeMove(NameSquadRed, _squadRed, _squadBlue, ref _distanceSquadRed, ref _distanceSquadBlue, random);
            }

            Console.ForegroundColor = ColorDefault;
            Console.ReadKey();
            Console.Clear();
        }
    }

    private void MakeMove(string nameSquad, Squad squadAttacking, Squad squadDefending, ref int distanceSquadAttacking, ref int distanceSquadDefending, Random random)
    {
        int distanceEnemys = _distanceBattle - (distanceSquadAttacking + distanceSquadDefending);

        int minStepDistance = -2;
        int maxStepDistance = distanceEnemys + 1;

        int step = random.Next(minStepDistance, maxStepDistance);

        Console.Write($"Ходит {nameSquad}:\n");

        if (distanceEnemys > 0 && step > 0)
        {
            distanceSquadAttacking += step;

            Console.Write($"\n{nameSquad} продвинулся к противнику на кол-во клеток ({step}).\n");
        }
        else if (step == 0)
        {
            Console.Write($"\n{nameSquad} удерживает позицию.\n");
        }
        else
        {
            distanceSquadAttacking += step;

            if (distanceSquadAttacking < 0)
                distanceSquadAttacking = 0;

            Console.Write($"\n{nameSquad} отступил от противника на кол-во клеток ({step}).\n");
        }

        if (distanceEnemys <= 0)
        {
            distanceEnemys = _distanceBattle - (distanceSquadAttacking + distanceSquadDefending);
        }

        squadDefending.TakeDamage(squadAttacking.Attack(distanceEnemys), out float takeDamage, out float takeHealth);
        squadAttacking.ShowAttack(takeDamage, takeHealth);
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

    public Dictionary<string, int> Characteristic { get; private set; }

    public int ShootingAttack { get; protected set; }
    public int MeleeAttack { get; protected set; }
    public int HealthPoints { get; protected set; }
    public int Armor { get; protected set; }
    public int FightingSpirit { get; protected set; }
    public int Medication { get; protected set; }

    protected void SetCharacteristic()
    {
        Characteristic = new Dictionary<string, int>()
        {
            { KeyShootingAttack, ShootingAttack},
            { KeyMeleeAttack, MeleeAttack},
            { KeyHealthPoints, HealthPoints},
            { KeyArmor, Armor},
            { KeyFightingSpirit, FightingSpirit},
            { KeyMedication, Medication},
        };
    }
}

class Rifleman : Soldier
{
    private int _shootingAttack = 30;
    private int _meleeAttack = 20;
    private int _armor = 25;

    public Rifleman()
    {
        ShootingAttack = _shootingAttack;
        MeleeAttack = _meleeAttack;
        Armor = _armor;

        SetCharacteristic();
    }
}

class Tankman : Soldier
{
    private int _shootingAttack = 40;
    private int _armor = 60;
    private int _fightingSpirit = 5;

    public Tankman()
    {
        ShootingAttack = _shootingAttack;
        Armor = _armor;
        FightingSpirit = _fightingSpirit;

        SetCharacteristic();
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

        SetCharacteristic();
    }
}

class Doctor : Soldier
{
    private int _shootingAttack = 10;
    private int _meleeAttack = 5;
    private int _medication = 4;

    public Doctor()
    {
        ShootingAttack = _shootingAttack;
        MeleeAttack = _meleeAttack;
        Medication = _medication;

        SetCharacteristic();
    }
}

class Artilleryman : Soldier
{
    private int _shootingAttack = 60;
    private int _fightingSpirit = 10;

    public Artilleryman()
    {
        ShootingAttack = _shootingAttack;
        FightingSpirit = _fightingSpirit;

        SetCharacteristic();
    }
}