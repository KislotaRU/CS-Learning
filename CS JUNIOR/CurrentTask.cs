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

class Inventory
{
    private readonly List<Fish> _inventory;
}

class Aquarium
{
    private const int MaxCountFish = 20;

    private readonly List<Fish> _fishGroup;

    private int _workTime = 0;

    public Aquarium()
    {

    }

    public void Work()
    {
        while (Console.ReadKey(true).Key != ConsoleKey.Escape)
        {

            _workTime++;
        }

        Console.Write("\nВы решили уйти на пенсию и оставили рыбные дела своим приемникам,\n" +
                      "чтобы посвятить оставшееся время путешествиям по миру.\n\n");
    }

    public void Show()
    {

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
        Console.Write($"Вид рыбы: {Type} | Восраст: {Age} | Статус: {State}");
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