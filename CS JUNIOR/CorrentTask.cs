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

    private readonly Dictionary<string, int> _characteristic;

    private List<Soldier> _soldiers;

    public Squad()
    {
        _soldiers = new List<Soldier>();

        _characteristic = new Dictionary<string, int>()
        {
            { Soldier.KeyShootingAttack, 0},
            { Soldier.KeyMeleeAttack, 0},
            { Soldier.KeyHealthPoints, 0},
            { Soldier.KeyArmor, 0},
            { Soldier.KeyFightingSpirit, 0},
            { Soldier.KeyMedication, 0},
        };
    }

    public int ShootingAttack { get => _characteristic[Soldier.KeyShootingAttack]; }
    public int MeleeAttack { get => _characteristic[Soldier.KeyMeleeAttack]; }
    public int HealthPoints { get => _characteristic[Soldier.KeyHealthPoints]; private set => _characteristic[Soldier.KeyHealthPoints] = value; }
    public int Armor { get => _characteristic[Soldier.KeyArmor]; }
    public int FightingSpirit { get => _characteristic[Soldier.KeyFightingSpirit]; }
    public int Medication { get => _characteristic[Soldier.KeyMedication]; private set => _characteristic[Soldier.KeyMedication] = value; }

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

    public void ShowAttack(float takeDamage, int takeHealth, string typeAttack)
    {
        Console.Write($"Атаковал противника {typeAttack} на {(int)takeDamage} единиц урона.\n");

        if (takeHealth > 0)
           Console.Write($"Противник восстановил себе здоровье на {takeHealth} единиц.");
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

    public void CollectSquad(List<Soldier> soldiers)
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

            _characteristic[characteristic] += value;
        }
    }
}

class Team
{
    private readonly Barrack _barrack;
    private readonly Squad _squad;

    public Team()
    {
        _barrack = new Barrack();
        _squad = new Squad();
    }

    public void CreateSquad()
    {
        _barrack.CreateSoldier();
        _squad.CollectSquad(_barrack.GetSoldiers());
    }

    public Squad GetSquad()
    {
        return _squad;
    }
}

class War
{
    private readonly Team _teamRed;
    private readonly Team _teamBlue;

    private Battle _battle;

    private string _squadWinner;

    public War()
    {
        _teamRed = new Team();
        _teamBlue = new Team();
    }

    public void Begin()
    {
        _teamRed.CreateSquad();
        _teamBlue.CreateSquad();

        Console.Write("Началась Война между двумя странами!\n" +
                      "*Нажмите любую кнопку*\n");

        Console.ReadKey();
        Console.Clear();

        _battle = new Battle(_teamRed.GetSquad(), _teamBlue.GetSquad());

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

    private int _countMoves = 0;

    public Battle(Squad squadRed, Squad squadBlue)
    {
        _squadRed = squadRed;
        _squadBlue = squadBlue;
    }

    public void Show(int distanceSquadRed, int distanceSquadBlue)
    { 
        int freeDistance;

        Console.Write("Дистанция между взводами:\n");

        Console.Write("┌" + new string('─', _distanceBattle) + "┐\n");
        Console.Write("│");

        freeDistance = _distanceBattle - (distanceSquadRed + distanceSquadBlue);

        Console.ForegroundColor = ColorRed;
        Console.Write(new string('#', distanceSquadRed));

        if (freeDistance > 0)
        {
            Console.ForegroundColor = ColorDefault;
            Console.Write(new string('#', freeDistance));
        }

        Console.ForegroundColor = ColorBlue;
        Console.Write(new string('#', distanceSquadBlue));

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
        }

        if (_squadRed.HealthPoints <= 0)
            squadWinner = NameSquadBlue;
        else
            squadWinner = NameSquadRed;
    }

    private void MakeMove(string nameSquad, Squad squadAttacking, Squad squadDefending, ref int distanceSquadAttacking, ref int distanceSquadDefending, Random random)
    {
        string typeAttack;

        int distanceEnemys = _distanceBattle - (distanceSquadAttacking + distanceSquadDefending);

        int minStepDistance = -2;
        int maxStepDistance = distanceEnemys + 1;

        int step = random.Next(minStepDistance, maxStepDistance);

        Console.Write($"Ходит {nameSquad}:\n\n");

        squadDefending.TakeDamage(squadAttacking.Attack(distanceEnemys, out typeAttack), out float takeDamage, out int takeHealth);
        squadAttacking.ShowAttack(takeDamage, takeHealth, typeAttack);

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

        if (distanceEnemys < 0)
            distanceEnemys = _distanceBattle - (distanceSquadAttacking + distanceSquadDefending);
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

        SetCharacteristic();
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
    private int _medication = 12;

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
    private int _healthPoints = 90;
    private int _shootingAttack = 60;
    private int _fightingSpirit = 10;

    public Artilleryman()
    {
        HealthPoints = _healthPoints;
        ShootingAttack = _shootingAttack;
        FightingSpirit = _fightingSpirit;

        SetCharacteristic();
    }
}