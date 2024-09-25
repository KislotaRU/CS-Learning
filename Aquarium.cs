using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Aquarium aquarium = new Aquarium();

            Console.ForegroundColor = ConsoleColor.White;

            aquarium.Work();
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

    public static int GenerateRandomNumber(int minNumber = 0, int maxNumber = 0) =>
        s_random.Next(minNumber, maxNumber);

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

class Aquarium
{
    private readonly List<Fish> _fishesShop;
    private readonly List<Fish> _fishes;

    private readonly int _maxCountFishes = 20;

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
        const string CommandCelebrateNewYear = "Отпраздновать новый год";
        const string CommandAddFish = "Добавить рыбу";
        const string CommandRemoveFish = "Убрать рыбу";
        const string CommandExit = "Завершить работу аквариума";

        string[] menu = new string[]
        {
            CommandCelebrateNewYear,
            CommandAddFish,
            CommandRemoveFish,
            CommandExit
        };

        string userInput;

        bool isWorking = true;

        while (isWorking)
        {
            Console.Write("\t\tВаш аквариум\n\n");

            Console.Write($"Кол-во лет работы вашего Аквариума: {_workTime}\n" +
                          $"Кол-во уникальных рыб за это время: {_uniqueCountFishes}\n\n");

            Show(_fishes);

            Console.Write("Доступные команды:\n");
            PrintMenu(menu);

            Console.Write("Ожидается ввод: ");
            userInput = GetCommandMenu(menu);

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
                    Console.Write("Вы завершили работу аквариума.\n");
                    isWorking = false;
                    continue;

                default:
                    Console.Write("Ввод не соответствует номеру команды.");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    private void CelebrateNewYear()
    {
        foreach (Fish fish in _fishes)
            fish.GrowUp();

        UpdateFishesShop();

        _workTime++;
        Console.Write("Вы отпраздновали Новый Год.\n");
    }

    private void AddFish()
    {
        if (_fishes.Count < _maxCountFishes)
        {
            Console.Write("\t\tМагазин рыб\n\n");

            Show(_fishesShop);

            Console.Write("Выберите рыбу, которую хотите добавить: ");

            if (TryGetFish(_fishesShop, out Fish fishFound))
            {
                _uniqueCountFishes++;
                _fishes.Add(fishFound);
                _fishesShop.Remove(fishFound);
                Console.Write("Вы успешно добавили рыбу в аквариум.\n");
            }
            else
            {
                Console.Write("Не удалось добавить рыбу.\n");
            }
        }
        else
        {
            Console.Write("В вашем аквариуме нет места для новой рыбы.\n");
        }
    }

    private void RemoveFish()
    {
        if (_fishes.Count > 0)
        {
            Show(_fishes);

            Console.Write("Выберите рыбу, которую хотите убрать: ");

            if (TryGetFish(_fishes, out Fish fishFound))
            {
                _fishes.Remove(fishFound);
                Console.Write("Вы успешно убрали рыбу из аквариума.\n");
            }
            else
            {
                Console.Write("Не удалось убрать рыбу из аквариума.\n");
            }
        }
        else
        {
            Console.Write("Ваш аквариум пуст.\n");
        }
    }

    private void UpdateFishesShop()
    {
        _fishesShop.Clear();

        CreateFishes();
    }

    private void CreateFishes()
    {
        FishFactory fishFactory = new FishFactory();

        List<Fish> fishes = fishFactory.GetFishes();

        int maxCountFishesShop = 10;

        while (_fishesShop.Count < maxCountFishesShop)
        {
            int index = UserUtils.GenerateRandomNumber(maxNumber: fishes.Count);

            _fishesShop.Add(fishes[index].Clone());
        }
    }

    private void Show(List<Fish> fishes)
    {
        Console.Write("Рыбы в аквариуме:\n");

        if (fishes.Count > 0)
        {
            int numberFish = 1;

            foreach (Fish fish in fishes)
            {
                Console.Write($"\t{numberFish}. ".PadRight(5));
                fish.Show();

                numberFish++;
            }

            Console.WriteLine();
        }
        else
        {
            Console.Write("\tАквариум пуст.\n\n");
        }
    }

    private bool TryGetFish(List<Fish> fishes, out Fish foundFish)
    {
        int numberFish = UserUtils.ReadInt();

        foundFish = null;

        if (numberFish > 0 && numberFish <= fishes.Count)
        {
            foundFish = fishes[numberFish - 1];
            return true;
        }
        else
        {
            Console.Write("Под таким номером нет рыбы.\n");
        }

        return false;
    }

    private void PrintMenu(string[] menu)
    {
        int numberCommand = 1;

        for (int i = 0; i < menu.Length; i++)
        {
            Console.Write($"\t{numberCommand}. {menu[i]}\n");
            numberCommand++;
        }

        Console.WriteLine();
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

class FishFactory
{
    private readonly List<Fish> _fishes;

    public FishFactory()
    {
        _fishes = new List<Fish>()
        {
            new Salmon(),
            new Perch(),
            new Crucian(),
            new Goldfish()
        };
    }

    public List<Fish> GetFishes()
    {
        List<Fish> temporaryFishes = new List<Fish>();

        foreach (Fish fish in _fishes)
            temporaryFishes.Add(fish.Clone());

        return temporaryFishes;
    }
}

abstract class Fish
{
    private const string StateOld = "Старая";
    private const string StateAdult = "Взрослая";
    private const string StateYoung = "Молодая";
    private const string StateTadpole = "Головастик";
    private const string StateDead = "Мёртвая";

    public string Type { get; protected set; }
    public string State { get; protected set; }
    public int Lifespan { get; protected set; }
    public int Age { get; protected set; }

    public abstract Fish Clone();

    public void Show() =>
        Console.Write($"Вид рыбы: {Type}".PadRight(20) + $"Возраст: {Age}".PadRight(13) + $"Состояние: {State}\n");

    public void GrowUp()
    {
        if (State != StateDead)
        {
            Age++;
            UpdateState();
        }
    }

    protected void UpdateState()
    {
        float coefficientLifespan = 2;
        int middleAge = (int)(Lifespan / coefficientLifespan);
        int oldAge = (int)(middleAge + middleAge / coefficientLifespan);

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
        Age = UserUtils.GenerateRandomNumber(maxNumber: Lifespan);

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
        Age = UserUtils.GenerateRandomNumber(maxNumber: Lifespan);

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
        Age = UserUtils.GenerateRandomNumber(maxNumber: Lifespan);

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
        Age = UserUtils.GenerateRandomNumber(maxNumber: Lifespan);

        UpdateState();
    }

    public override Fish Clone() =>
        new Goldfish();
}