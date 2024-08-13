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

class Spawn
{
    private const string FishSalmon = "Salmon";
    private const string FishPerch = "Perch";
    private const string FishCrucian = "Crucian";
    private const string FishGoldfish = "Goldfish";

    private readonly List<string> _typesFish;

    private readonly Random _random;

    public Spawn()
    {
        _random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        _typesFish = new List<string>()
        {
            FishSalmon,
            FishPerch,
            FishCrucian,
            FishGoldfish
        };
    }

    public Fish GetFish()
    {
        int numberTypesFish = _random.Next(0, _typesFish.Count);

        switch (_typesFish[numberTypesFish])
        {
            case FishSalmon:
                return new Salmon();

            case FishPerch:
                return new Perch();

            case FishCrucian:
                return new Crucian();

            case FishGoldfish:
                return new Goldfish();

            default:
                return null;
        }
    }
}

class Aquarium
{
    private const string CommandCelebrateNewYear = "CelebrateNewYear";
    private const string CommandAddFish = "AddFish";
    private const string CommandRemoveFish = "RemoveFish";
    private const string CommandExit = "Exit";

    private const int MaxCountFish = 20;

    private readonly List<Fish> _fishGroup;

    private readonly Spawn _spawn;

    private readonly string[] _mainMenu; 
        
    private int _workTime = 0;

    private string _userInput = null;

    public Aquarium()
    {
        _fishGroup = new List<Fish>();
        _spawn = new Spawn();

        _mainMenu = new string[]
        {
            CommandCelebrateNewYear,
            CommandAddFish,
            CommandRemoveFish,
            CommandExit
        };
    }

    public void Work()
    {
        while (_userInput != CommandExit)
        {
            PrintMenu(_mainMenu);

            ReadInput(_mainMenu);

            _workTime++;
        }

        Console.Write("\nВы решили уйти на пенсию и оставили рыбные дела своим приемникам,\n" +
                      "чтобы посвятить оставшееся время путешествиям по миру.\n\n");
    }

    public void Show()
    {

    }

    public void AddFish()
    {
        if (_fishGroup.Count < MaxCountFish)
            _fishGroup.Add(_spawn.GetFish());
    }

    public void RemoveFish()
    {

    }

    private void ReadInput(string[] arrayMenu)
    {
        _userInput = Console.ReadLine();

        if (int.TryParse(_userInput, out int result))
            if ((result > 0) && (result <= arrayMenu.Length))
                _userInput = arrayMenu[result - 1];
    }

    private void PrintMenu(string[] arrayMenu)
    {
        Console.Write("\tДоступные команды:\n\n");

        for (int i = 0; i < arrayMenu.Length; i++)
            Console.Write($"\t{i + 1}. {arrayMenu[i]}\n");

        Console.WriteLine();
        Console.Write("Ожидается ввод: ");
    }
}

abstract class Fish
{
    private int _age;

    public Fish()
    {

    }

    public string Type { get; protected set; }
    public int Age { get { return _age; } private set { if (_age > Lifespan) Die(); } }
    public string State { get; protected set; }
    public int Lifespan { get; private set; }

    public void Show()
    {
        Console.Write($"Вид рыбы: {Type} | Возраст: {Age} | Статус: {State}");
    }

    public void Grow()
    {
        Age = ++_age;

        UpdateState();
    }

    protected void SetLifespan(int minLifespan, int maxLifespan)
    {
        Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        Lifespan = random.Next(minLifespan, maxLifespan);
    }

    private void Die()
    {
        Age = -1;
    }

    private void UpdateState()
    {
        int halfLife = Lifespan / 2;
        int oldAge = halfLife + halfLife / 2;

        if (Age >= oldAge)
            State = "Старенькая особь";
        else if (Age >= halfLife)
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
    private const int MinLifespan = 7;
    private const int MaxLifespan = 16;

    private readonly string _type = "Salmon";

    public Salmon()
    {
        Type = _type;

        SetLifespan(MinLifespan, MaxLifespan);
    }
}

class Perch : Fish
{
    private const int MinLifespan = 5;
    private const int MaxLifespan = 11;

    private readonly string _type = "Perch";

    public Perch()
    {
        Type = _type;

        SetLifespan(MinLifespan, MaxLifespan);
    }
}

class Crucian : Fish
{
    private const int MinLifespan = 15;
    private const int MaxLifespan = 21;

    private readonly string _type = "Crucian";

    public Crucian()
    {
        Type = _type;

        SetLifespan(MinLifespan, MaxLifespan);
    }
}

class Goldfish : Fish
{
    private const int MinLifespan = 10;
    private const int MaxLifespan = 21;

    private readonly string _type = "Goldfish";

    public Goldfish()
    {
        Type = _type;

        SetLifespan(MinLifespan, MaxLifespan);
    }
} 