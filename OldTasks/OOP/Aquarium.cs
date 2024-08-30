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
    class Program
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

    public const float HalfValue = 0.5f;

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

    public static void ReadInputMenu(string[] arrayMenu, out string userInput)
    {
        userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int result))
            if ((result > 0) && (result <= arrayMenu.Length))
                userInput = arrayMenu[result - 1];
    }
}

class Aquarium
{
    private const string CommandCelebrateNewYear = "Отпраздновать новый год";
    private const string CommandAddFish = "Добавить рыбу";
    private const string CommandRemoveFish = "Убрать рыбу";
    private const string CommandExit = "Выйти из программы";

    private readonly List<Fish> _fishesShop;
    private readonly List<Fish> _fishes;

    private readonly int _maxCountFish = 20;

    private int _workTime = 0;
    private int _uniqueCountFishes = 0;

    public Aquarium()
    {
        _fishes = new List<Fish>();
        _fishesShop = new List<Fish>();

        CreateFishes();
    }

    public void Work()
    {
        string[] menu = new string[]
        {
            CommandCelebrateNewYear,
            CommandAddFish,
            CommandRemoveFish,
            CommandExit
        };

        string userInput = null;

        while (userInput != CommandExit)
        {
            Console.Write("\t\t--- Ваш аквариум ---\n\n");

            Show(_fishes);

            Console.Write($"\nКол-во лет работы вашего Аквариума: {_workTime}" +
                          $"\nКол-во уникальных рыб за это время: {_uniqueCountFishes}");

            Console.Write("\t\n\nДоступные команды:\n\n");

            UserUtils.PrintMenu(menu);

            Console.WriteLine();
            Console.Write("Ожидается ввод: ");

            UserUtils.ReadInputMenu(menu, out userInput);

            Console.Clear();

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
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    private void Show(List<Fish> fishes)
    {
        int numberFish = 1;

        if (fishes.Count > 0)
        {
            foreach (Fish fish in fishes)
            {
                Console.Write($"\t{numberFish++}. ");
                fish.Show();
            }
        }
        else
        {
            Console.Write("Здесь будут отображаться ваши рыбы, но пока здесь пусто.\n");
        }
    }

    private void CelebrateNewYear()
    {
        _workTime++;

        foreach (Fish fish in _fishes)
            fish.Grow();

        UpdateFishesShop();
    }

    private void AddFish()
    {
        if (_fishes.Count < _maxCountFish)
        {
            Console.Write("\t\t--- МАГАЗИН РЫБ ---\n\n");

            Show(_fishesShop);

            Console.Write("\nВыберите рыбу, которую хотите добавить: ");

            if (TryGetFish(_fishesShop, out Fish fishFound))
            {
                _uniqueCountFishes++;
                _fishes.Add(fishFound);
                _fishesShop.Remove(fishFound);
            }
            else
            {
                Console.Write("\nНе удалось добавить рыбу.");
            }
        }
        else
        {
            Console.Write("\nВ вашем аквариуме нет места для новой рыбы.\n");
        }
    }

    private void RemoveFish()
    {
        if (_fishes.Count > 0)
        {
            Show(_fishes);

            if (TryGetFish(_fishes, out Fish fishFound))
            {
                _fishes.Remove(fishFound);
            }
            else
            {
                Console.Write("\nНе удалось удалить рыбу из аквариума.");
            }
        }
        else
        {
            Console.Write("Ваш аквариум пуст.");
        }
    }

    private void Exit()
    {
        Console.Write("Вы решили уйти на пенсию и оставили рыбные дела своим приемникам,\n" +
                      "чтобы посвятить оставшееся время путешествиям по миру.\n");
    }

    private bool TryGetFish(List<Fish> fishes, out Fish fishFound)
    {
        string userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int index))
        {
            if (index > 0 && index <= fishes.Count)
            {
                fishFound = fishes[index - 1];
                return true;
            }
            else
            {
                Console.Write("\nТакой рыбы нет");
            }
        }
        else
        {
            Console.Write("\nТребуется ввести номер рыбы.");
        }

        fishFound = null;
        return false;
    }

    private void UpdateFishesShop()
    {
        _fishesShop.Clear();

        CreateFishes();
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
    private const string StateOld = "Старенькая особь";
    private const string StateAdult = "Взрослая особь";
    private const string StateYoung = "Молодняк";
    private const string StateTadpole = "Головастик";
    private const string StateDead = "Мёртвая";

    public string Type { get; protected set; }
    public string State { get; protected set; }
    public int Lifespan { get; protected set; }
    public int Age { get; protected set; }

    public abstract Fish Clone();

    public void Show()
    {
        Console.Write($"Вид рыбы: {Type}| Возраст: {Age}| Состояние: {State}\n");
    }

    public void Grow()
    {
        if (State != StateDead)
            Age++;

        UpdateState();
    }

    protected void UpdateState()
    {
        int middleAge = (int)(Lifespan * UserUtils.HalfValue);
        int oldAge = (int)(middleAge + middleAge * UserUtils.HalfValue);

        if (Age <= Lifespan)
        {
            if (Age >= oldAge)
                State = StateOld;
            else if (Age >= middleAge)
                State = StateAdult;
            else if (Age == 0)
                State = StateTadpole;
            else if (Age < middleAge)
                State = StateYoung;
        }
        else
        {
            State = StateDead;
        }
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
        Age = UserUtils.GenerateRandomNumber(Lifespan);

        UpdateState();
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
        Age = UserUtils.GenerateRandomNumber(Lifespan);

        UpdateState();
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
        Age = UserUtils.GenerateRandomNumber(Lifespan);

        UpdateState();
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
        Age = UserUtils.GenerateRandomNumber(Lifespan);

        UpdateState();
    }

    public override Fish Clone() =>
        new Goldfish();
} 