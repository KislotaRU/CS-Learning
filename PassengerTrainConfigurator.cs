﻿using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Dispatcher dispatcher = new Dispatcher();

            Console.ForegroundColor = ConsoleColor.White;

            dispatcher.Work();
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

    public static int GenerateRandomNumber(int maxNumber) =>
        s_random.Next(maxNumber);

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
    private readonly Queue<Train> _trains;
    private readonly List<string> _directions;

    public Dispatcher()
    {
        _trains = new Queue<Train>();

        _directions = new List<string>()
        {
            "Санкт-Петербург - Псков",
            "Екатеренбург - Барнаул",
            "Санкт-Петербург — Москва",
            "Москва - Сочи",
            "Санкт-Петербург - Симферополь",
        };
    }

    public void Work()
    {
        const string CommandCreateFlight = "Создать рейс";
        const string CommandSendTrain = "Отправить в рейс";
        const string CommandExit = "Выйти";

        string[] menu = new string[]
        {
            CommandCreateFlight,
            CommandSendTrain,
            CommandExit
        };

        string userInput;
        bool isRunning = true;

        while (isRunning)
        {
            Console.Write("\t\tМеню диспетчера поездов.\n\n");

            ShowFlightCount();
            ShowFlightInfo();

            Console.Write("Доступные команды:\n");
            PrintMenu(menu);

            Console.Write("\nОжидается ввод: ");
            userInput = GetCommandMenu(menu);

            Console.Clear();

            switch (userInput)
            {
                case CommandCreateFlight:
                    CreateFlight();
                    break;

                case CommandSendTrain:
                    SendTrain();
                    break;

                case CommandExit:
                    Console.Write("Вы завершили работу программы.\n\n");
                    isRunning = false;
                    continue;

                default:
                    Console.Write("Требуется ввести номер команды или саму команду.\n\n");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    private void CreateFlight()
    {
        string direction;
        int _peoplesCount;

        direction = GetDirection();
        Console.Write($"Получено направление: {direction}.\n");

        _peoplesCount = SellTickets();
        Console.Write("Билеты распроданы.\n");

        CreateTrain(direction, _peoplesCount);
        Console.Write("Сформирован поезд.\n");

        Console.Write("Поезд готов к отправки...\n");
    }

    private void SendTrain()
    {
        if (_trains.Count > 0)
        {
            ShowFlightInfo();
            Console.Write("Поезд отправлен в рейс.\n");
            _trains.Dequeue();
        }
        else
        {
            Console.Write("Нет готовых рейсов.\n");
        }
    }

    private string GetDirection()
    {
        int indexDirection = UserUtils.GenerateRandomNumber(_directions.Count);

        return _directions[indexDirection];
    }

    private int SellTickets()
    {
        int minNumberPeoples = 70;
        int maxNumberPeoples = 150;

        return UserUtils.GenerateRandomNumber(minNumberPeoples, maxNumberPeoples);
    }

    private void CreateTrain(string direction, int peoplesCount) =>
        _trains.Enqueue(new Train(direction, peoplesCount));

    private void ShowFlightInfo()
    {
        Train train = _trains.Count > 0 ? _trains.Peek() : null;

        Console.Write("\tТекущий рейс:\n" +
                     $"Направление: {train?.Direction ?? "Нет данных"}\n" +
                     $"Кол-во пассажиров: {train?.TakenPlacesCount ?? 0}\n" +
                     $"Кол-во всего мест: {train?.PlacesCount ?? 0}\n" +
                     $"Кол-во вагонов: {train?.WagonsCount ?? 0}\n\n");
    }

    private void ShowFlightCount() =>
        Console.Write($"Кол-во готовых рейсов: {_trains.Count}\n\n");

    private void PrintMenu(string[] menu)
    {
        for (int i = 0; i < menu.Length; i++)
            Console.Write($"\t{i + 1}. {menu[i]}\n");
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

class Train
{
    private readonly List<Wagon> _wagons;

    public Train(string direction, int peoplesCount)
    {
        _wagons = new List<Wagon>();
        Direction = direction;
        TakenPlacesCount = peoplesCount;

        CreateWagons();
    }

    public int PlacesCount { get; private set; }
    public int TakenPlacesCount { get; private set; }
    public int WagonsCount => _wagons.Count;
    public string Direction { get; private set; }

    public void Show()
    {
        Console.Write("Вагоны поезда: \n");

        foreach (Wagon wagon in _wagons)
            wagon.Show();

        Console.WriteLine();
    }

    private void CreateWagons()
    {
        Wagon wagon;

        while (PlacesCount <= TakenPlacesCount)
        {
            wagon = new Wagon();

            _wagons.Add(wagon);
            PlacesCount += wagon.MaxCountPassengers;
        }
    }
}

class Wagon
{
    public Wagon()
    {
        int _minNumberPassengers = 10;
        int _maxNumberPassengers = 20;

        MaxCountPassengers = UserUtils.GenerateRandomNumber(_minNumberPassengers, _maxNumberPassengers);
    }

    public int MaxCountPassengers { get; private set; }

    public void Show() =>
        Console.Write($"Вагон, кол-во мест: {MaxCountPassengers}\n");
}