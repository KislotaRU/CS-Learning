using System;
using System.Collections.Generic;

//Есть аквариум, в котором плавают рыбы. В этом аквариуме может быть максимум определенное кол-во рыб.
//Рыб можно добавить в аквариум или рыб можно достать из аквариума.
//(программу делать в цикле для того, чтобы рыбы могли “жить”)
//Все рыбы отображаются списком, у рыб также есть возраст.
//За 1 итерацию рыбы стареют на определенное кол-во жизней и могут умереть.
//Рыб также вывести в консоль, чтобы можно было мониторить показатели.

namespace CS_JUNIOR
{
    class CurrentTask
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White; 

            Aquarium aquarium = new Aquarium();

            aquarium.Work();
        }
    }
}

static class UserUtils
{
    private static readonly Random s_random;

    public const int HalfValue = 2;

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

    public static void PrintMenu(string[] arrayMenu)
    {
        for (int i = 0; i < arrayMenu.Length; i++)
            Console.Write($"\t{i + 1}. {arrayMenu[i]}\n");
    }

    public static void ReadInput(string[] arrayMenu, out string userInput)
    {
        userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int result))
            if ((result > 0) && (result <= arrayMenu.Length))
                userInput = arrayMenu[result - 1];
    }
}

class Aquarium
{
    private const string CommandCelebrateNewYear = "CelebrateNewYear";
    private const string CommandAddFish = "AddFish";
    private const string CommandRemoveFish = "RemoveFish";
    private const string CommandExit = "Exit";

    private readonly List<Fish> _fishesShop;
    private readonly List<Fish> _fishes;

    private int _maxCountFish = 20;
    private int _workTime = 0;

    public Aquarium()
    {
        _fishes = new List<Fish>();
        _fishesShop = new List<Fish>();

        CreateFishes();
    }

    public void Work()
    {
        string[] mainMenu = new string[]
        {
            CommandCelebrateNewYear,
            CommandAddFish,
            CommandRemoveFish,
            CommandExit
        };

        string userInput = null;

        while (userInput != CommandExit)
        {
            Console.Write("\tДоступные команды:\n\n");

            UserUtils.PrintMenu(mainMenu);

            Console.WriteLine();
            Console.Write("Ожидается ввод: ");

            UserUtils.ReadInput(mainMenu, out userInput);

            switch (userInput)
            {
                case CommandCelebrateNewYear:
                    CelebrateNewYear();
                    break;

                case CommandAddFish:
                    AddFish();
                    break;

                case CommandRemoveFish:
                    RemoveFish();
                    break;

                case CommandExit:
                    Exit();
                    break;

                default:
                    Console.Write("Ввод не соответствует номеру команды.");
                    continue;
            }
        }
    }

    private void Show(List<Fish> fishes)
    {
        int numberFish = 1;

        foreach (Fish fish in fishes)
        {
            Console.Write($"{numberFish++}. ");
            fish.Show();
        }
    }

    private void CelebrateNewYear()
    {
        _workTime++;
    }

    private void UpdateFishes()
    {

    }

    private void AddFish()
    {
        if (_fishes.Count < _maxCountFish)
            _fishes.Add(_spawn.GetFish());
    }

    private void RemoveFish()
    {
        
    }

    private void Exit()
    {
        Console.Write("\nВы решили уйти на пенсию и оставили рыбные дела своим приемникам,\n" +
                      "чтобы посвятить оставшееся время путешествиям по миру.\n\n");
    }

    private void CreateFishes()
    {
        List<Fish> typesFishes = new List<Fish>()
        {
            new Salmon(),
            new Perch(),
            new Crucian(),
            new Goldfish()
        };

        int maxCountFishesShop = 10;

        while (_fishesShop.Count < maxCountFishesShop)
        {
            int index = UserUtils.GenerateRandomNumber(typesFishes.Count);

            _fishesShop.Add(typesFishes[index].Clone());
        }
    }
}

abstract class Fish
{
    public string Type { get; protected set; }
    public string State { get; protected set; }
    public int Lifespan { get; protected set; }
    public int Age { get; private set; }

    public abstract Fish Clone();

    public void Show()
    {
        Console.Write($"Вид рыбы: {Type} | Возраст: {Age} | Статус: {State}");
    }

    public void Grow()
    {
        Age++;

        UpdateState();
    }

    protected void SetLifespan(int minLifespan, int maxLifespan)
    {
        Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        Lifespan = random.Next(minLifespan, maxLifespan);
    }

    private void Die()
    {
        
    }

    private void UpdateState()
    {
        int middleAge = Lifespan / UserUtils.HalfValue;
        int oldAge = middleAge + middleAge / UserUtils.HalfValue;

        if (Age >= oldAge)
            State = "Старенькая особь";
        else if (Age >= middleAge)
            State = "Взрослая особь";
        else if (Age > 0)
            State = "Молодняк";
        else if (Age == 0)
            State = "Головастик";
        else
            State = "Умерла";
    }
}

class Salmon : Fish
{
    private readonly string _type = "Salmon";

    private readonly int _minLifespan = 7;
    private readonly int _maxLifespan = 16;

    public Salmon()
    {
        Type = _type;

        Lifespan = UserUtils.GenerateRandomNumber(_minLifespan, _maxLifespan);
    }

    public override Fish Clone() =>
        new Salmon();
}

class Perch : Fish
{
    private readonly string _type = "Perch";

    private readonly int _minLifespan = 5;
    private readonly int _maxLifespan = 11;

    public Perch()
    {
        Type = _type;

        Lifespan = UserUtils.GenerateRandomNumber(_minLifespan, _maxLifespan);
    }

    public override Fish Clone() =>
        new Perch();
}

class Crucian : Fish
{
    private readonly string _type = "Crucian";

    private readonly int _minLifespan = 15;
    private readonly int _maxLifespan = 21;

    public Crucian()
    {
        Type = _type;

        Lifespan = UserUtils.GenerateRandomNumber(_minLifespan, _maxLifespan);
    }

    public override Fish Clone() =>
        new Crucian();
}

class Goldfish : Fish
{
    private readonly string _type = "Goldfish";

    private readonly int _minLifespan = 10;
    private readonly int _maxLifespan = 21;

    public Goldfish()
    {
        Type = _type;

        Lifespan = UserUtils.GenerateRandomNumber(_minLifespan, _maxLifespan);
    }

    public override Fish Clone() =>
        new Goldfish();
} 