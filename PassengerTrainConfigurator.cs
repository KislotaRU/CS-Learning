using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {

        }
    }
}

static class UserUtils
{
    private readonly static Random s_random;

    static UserUtils()
    {
        s_random = new Random();
    }

    public static int GenerateRandomNumber(int minNumber, int maxNumber) =>
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

class Dispatcher
{
    private readonly Train _train;
    private readonly List<string> _directions;

    public Dispatcher()
    {
        _train = new Train();
        _directions = new List<string>()
        {
            "Бийск - Барнаул",
            "Санкт-Петербург - Псков",
            "Екатеренбург - Барнаул",
            "Санкт-Петербург — Москва",
            "Москва - Сочи",
            "Санкт-Петербург - Симферополь",
        };
        
    }

    public void ShowTrain() =>
        _train.Show();
}

class Train
{
    private readonly List<Wagon> _wagons;

    public Train()
    {

    }

    public void Show()
    {
        Console.Write("Вагоны поезда: \n");

        foreach (Wagon wagon in _wagons)
            wagon.Show();

        Console.WriteLine();
    }
}

class Wagon
{
    private readonly int _maxCountSeats;
    private int _seatsCount = 0;

    public Wagon()
    {
        int randomMinCountSeats = 10;
        int randomMaxCountSeats = 20;

        _maxCountSeats = UserUtils.GenerateRandomNumber(randomMinCountSeats, randomMaxCountSeats);
    }

    public void Show() =>
        Console.Write($"Вагон, кол-во мест: {_seatsCount}/{_maxCountSeats}\n");

    public void TakeSeat()
    {

    }
}