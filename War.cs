using System;
using System.Collections.Generic;

/*
 * Реализовать 2 взвода и их сражение.
 * Каждый взвод внутри имеет солдат.
 * Каждый солдат - это уникальная единица, имеет способность и свои характеристики.
 * Солдаты атакуют случайных солдат во вражеском взводе.
 * Характеристики солдат состоят из здоровья, урона и брони.
 * Реализовать 4 типа солдат.
 * Первый - обычный солдат, без особенностей.
 * Второй - атакует только одного, но с множителем урона.
 * Третий - атакует сразу нескольких, без повторения атакованного за свою атаку.
 * Четвертый - атакует сразу нескольких, атакованные солдаты могут повторяться.
 * Сражение происходит “толпа на толпу”.
 * Первый взвод атакует второй взвод и потом наоборот. За время атаки каждый боец проводит свою атаку.
 * После атаки двух взводов остаются в каждом взводе только живые бойцы.
 * Побеждает тот взвод, в котором остались выжившие бойцы.
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            War war = new War();

            Console.ForegroundColor = ConsoleColor.White;

            war.Work();
        }
    }
}

static class UserUtils
{
    public const int HundredPercent = 100;

    private static readonly Random s_random;

    static UserUtils()
    {
        s_random = new Random();
    }

    public static int GenerateRandomNumber(int minNumber = 0, int maxNumber = 0) =>
        s_random.Next(minNumber, maxNumber);
}

class Squad
{
    private readonly List<Soldier> _soldiers;

    private readonly int _maxCountSoldiers = 4;

    public Squad(string name)
    {
        Name = name;
        _soldiers = new List<Soldier>();
        
        Create();
    }

    public string Name { get; private set; }
    public int SoldiersCount => _soldiers.Count;
    public bool IsAlive => SoldiersCount > 0;

    public void Show()
    {
        foreach (Soldier soldier in _soldiers)
        {
            string[] status = new string[]
            {
                $"{Name}",
                $"{soldier.GetType().Name} #{soldier.Id}",
                $"Здоровье: {soldier.HealthPoints: 0.##}",
                $"Урон: {soldier.Damage: 0.##}",
                $"Броня: {soldier.Armor}"
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
                Console.Write(status[i].PadRight(status[i].Length));
                Console.Write(" │ ");
            }

            Console.Write("\n└" + new string('─', messageLength) + "┘\n");
        }

        Console.WriteLine();
    }

    public void Attack(Squad squadEnemy)
    {
        Soldier attackerSoldier = GetSoldier();
        Soldier defenderSoldier;

        bool isAttacking = true;

        Console.Write($"\tСолдат: {attackerSoldier.GetType().Name} #{attackerSoldier.Id}\n");

        while (isAttacking)
        {
            defenderSoldier = squadEnemy.GetSoldier();

            attackerSoldier.Attack(defenderSoldier);

            isAttacking = attackerSoldier.IsAttacking;
        }
    }

    public void TryRemoveDeadSolder()
    {
        for (int i = SoldiersCount - 1; i >= 0; i--)
        {
            if (_soldiers[i].HealthPoints <= 0)
                _soldiers.Remove(_soldiers[i]);
        }
    }

    public Soldier GetSoldier()
    {
        int index = UserUtils.GenerateRandomNumber(maxNumber: SoldiersCount);

        return _soldiers[index];
    }

    private void Create()
    {
        SoldierFactory soldierFactory = new SoldierFactory();

        List<Soldier> soldiers = soldierFactory.GetSoldiers();

        while (SoldiersCount < _maxCountSoldiers)
        {
            int index = UserUtils.GenerateRandomNumber(maxNumber: soldiers.Count);

            _soldiers.Add(soldiers[index].Clone());
        }
    }
}

class War
{
    private Squad _firstSquad;
    private Squad _secondSquad;

    public void Work()
    {
        CreateSquads();
        Battle();
        AnnounceWinner();
    }

    private void CreateSquads()
    {
        string nameFirstSquad = "Первый взвод";
        string nameSecondSquad = "Второй взвод";

        _firstSquad = new Squad(nameFirstSquad);
        _secondSquad = new Squad(nameSecondSquad);
    }

    private void Battle()
    {
        bool isFighting = true;

        while (isFighting)
        {
            Console.Clear();

            Show();

            ExecuteBattle(_firstSquad, _secondSquad);

            isFighting = CanExecuteBattle(_secondSquad);

            if (isFighting == false)
                continue;

            ExecuteBattle(_secondSquad, _firstSquad);

            isFighting = CanExecuteBattle(_firstSquad);
        }
    }

    private void ExecuteBattle(Squad attacker, Squad defender)
    {
        Console.Write($"Ходит {attacker.Name}.\n");
        attacker.Attack(defender);
        defender.TryRemoveDeadSolder();

        Console.Write($"Походил {attacker.Name}.\n\n");
        Console.ReadKey();
    }

    private bool CanExecuteBattle(Squad squad) =>
        squad.IsAlive;

    private void AnnounceWinner()
    {
        if (_firstSquad.IsAlive == false)
            Console.Write($"В этой битве победил => {_secondSquad.Name}.\n\n");
        else if (_secondSquad.IsAlive == false)
            Console.Write($"В этой битве победил => {_firstSquad.Name}.\n\n");
        else
            Console.Write($"В этой битве нет победителя. Ничья.\n\n");
    }

    private void Show()
    {
        _firstSquad.Show();
        _secondSquad.Show();
    }
}

class SoldierFactory
{
    private readonly List<Soldier> _soldiers;

    public SoldierFactory()
    {
        _soldiers = new List<Soldier>()
        {
            new Rifleman(),
            new Sniper(),
            new Grenadier(),
            new Gunner()
        };
    }

    public List<Soldier> GetSoldiers()
    {
        List<Soldier> temporarySoldier = new List<Soldier>();

        foreach (Soldier soldier in _soldiers)
            temporarySoldier.Add(soldier.Clone());

        return temporarySoldier;
    }
}

abstract class Soldier
{
    private static int s_id = 0;

    public Soldier()
    {
        Id = ++s_id;
    }

    public float HealthPoints { get; protected set; }
    public int Armor { get; protected set; }
    public float Damage { get; protected set; }
    public float TotalDamage { get; protected set; }
    public bool IsAttacking { get; protected set; }
    public int Id { get; protected set; }

    public abstract Soldier Clone();

    public virtual void Attack(Soldier soldierEnemy)
    {
        Console.Write($"\tНанёс урон солдату: {soldierEnemy.GetType().Name} #{soldierEnemy.Id}\n");
        soldierEnemy.TakeDamage(TotalDamage);
        TotalDamage = Damage;
    }

    private void TakeDamage(float damage)
    {
        damage -= damage / UserUtils.HundredPercent * Armor;

        if (damage > 0)
            HealthPoints -= damage;
    }
}

class Rifleman : Soldier
{
    private readonly int _minHealthPoints = 90;
    private readonly int _maxHealthPoints = 110;
    private readonly int _minArmor = 10;
    private readonly int _maxArmor = 21;
    private readonly int _minDamage = 20;
    private readonly int _maxDamage = 31;

    public Rifleman()
    {
        HealthPoints = UserUtils.GenerateRandomNumber(_minHealthPoints, _maxHealthPoints);
        Armor = UserUtils.GenerateRandomNumber(_minArmor, _maxArmor);
        Damage = UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);
        TotalDamage = Damage;
    }

    public override Soldier Clone() =>
        new Rifleman();

    public override void Attack(Soldier soldierEnemy)
    {
        base.Attack(soldierEnemy);
        IsAttacking = false;
    }
}

class Sniper : Soldier
{
    private readonly int _minHealthPoints = 80;
    private readonly int _maxHealthPoints = 101;
    private readonly int _minArmor = 5;
    private readonly int _maxArmor = 11;
    private readonly int _minDamage = 30;
    private readonly int _maxDamage = 41;

    private readonly int _damageCoefficient = 2;

    public Sniper()
    {
        HealthPoints = UserUtils.GenerateRandomNumber(_minHealthPoints, _maxHealthPoints);
        Armor = UserUtils.GenerateRandomNumber(_minArmor, _maxArmor);
        Damage = UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);
        TotalDamage = Damage;
    }

    public override Soldier Clone() =>
        new Sniper();

    public override void Attack(Soldier soldierEnemy)
    {
        TotalDamage = Damage * _damageCoefficient;
        base.Attack(soldierEnemy);
        IsAttacking = false;
    }
}

class Grenadier : Soldier
{
    private readonly int _minHealthPoints = 80;
    private readonly int _maxHealthPoints = 111;
    private readonly int _minArmor = 15;
    private readonly int _maxArmor = 21;
    private readonly int _minDamage = 40;
    private readonly int _maxDamage = 61;

    private readonly List<Soldier> _attackedSoldiers;
    private readonly int _maxCountAttackedTargets = 3;
    private int _attackedTargetsCount = 0;

    public Grenadier()
    {
        HealthPoints = UserUtils.GenerateRandomNumber(_minHealthPoints, _maxHealthPoints);
        Armor = UserUtils.GenerateRandomNumber(_minArmor, _maxArmor);
        Damage = UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);
        TotalDamage = Damage;
        _attackedSoldiers = new List<Soldier>();
    }

    public override Soldier Clone() =>
        new Grenadier();

    public override void Attack(Soldier soldierEnemy)
    {
        IsAttacking = true;

        if (_attackedTargetsCount < _maxCountAttackedTargets)
        {
            if (_attackedSoldiers.Contains(soldierEnemy) == false)
            {
                base.Attack(soldierEnemy);
                _attackedSoldiers.Add(soldierEnemy);
            }

            _attackedTargetsCount++;
        }
        else
        {
            _attackedSoldiers.Clear();
            _attackedTargetsCount = 0;
            IsAttacking = false;
        }
    }
}

class Gunner : Soldier
{
    private readonly int _minHealthPoints = 100;
    private readonly int _maxHealthPoints = 131;
    private readonly int _minArmor = 30;
    private readonly int _maxArmor = 41;
    private readonly int _minDamage = 15;
    private readonly int _maxDamage = 21;

    private readonly int _maxCountAttacks = 3;
    private int _attacksCount = 0;

    public Gunner()
    {
        HealthPoints = UserUtils.GenerateRandomNumber(_minHealthPoints, _maxHealthPoints);
        Armor = UserUtils.GenerateRandomNumber(_minArmor, _maxArmor);
        Damage = UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);
        TotalDamage = Damage;
    }

    public override Soldier Clone() =>
        new Gunner();

    public override void Attack(Soldier soldierEnemy)
    {
        IsAttacking = true;

        if (_attacksCount < _maxCountAttacks)
        {
            base.Attack(soldierEnemy);
            _attacksCount++;
        }
        else
        {
            _attacksCount = 0;
            IsAttacking = false;
        }
    }
}