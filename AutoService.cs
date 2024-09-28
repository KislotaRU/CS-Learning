using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            AutoService autoService = new AutoService();

            Console.ForegroundColor = ConsoleColor.White;

            autoService.Work();
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

class PartFactory
{
    private readonly List<Part> _parts;

    public PartFactory()
    {
        _parts = new List<Part>()
        {
            new Part("Трансмиссия", 70, 80),
            new Part("Колесо", 15, 25),
            new Part("Лобовое стекло", 30, 40),
            new Part("Аккумулятор", 5, 15),
            new Part("Свеча зажигания", 5, 10),
            new Part("Моторное масло", 10, 20),
            new Part("Выхлопной коллектор", 20, 30),
            new Part("Бампер", 25, 35),
        };
    }

    public List<Part> Create(bool isCar = false)
    {
        List<Part> temporaryParts = new List<Part>();

        if (isCar)
        {
            foreach (Part part in _parts)
                temporaryParts.Add(part.Clone(part.Name, 0));
        }
        else
        {
            foreach (Part part in _parts)
                temporaryParts.Add(part.Clone(part.Name, part.Price));
        }

        return temporaryParts;
    }
}

class AutoService
{
    private readonly PartFactory _partFactory;
    private readonly Inventory _storage;
    private readonly Queue<Car> _cars;

    private readonly int _priceFineForReject = 100;
    private readonly int _priceFineForMistake = 50;
    private readonly int _priceWork = 30;

    private int _money = 1000;

    private int _sumMoneyForRepair = 0;
    private int _sumMoneyOfFines = 0;

    public AutoService()
    {
        _partFactory = new PartFactory();
        _storage = CreateStorage();
        _cars = new Queue<Car>();
    }

    public void Work()
    {
        const string CommandUpdateData = "Обновить данные";
        const string CommandServeCar = "Обслужить клиента";
        const string CommandShowStorage = "Посмотреть склад";
        const string CommandOrderPart = "Заказать запчасть";
        const string CommandExit = "Завершить работу";

        string[] menu = new string[]
        {
            CommandUpdateData,
            CommandServeCar,
            CommandShowStorage,
            CommandOrderPart,
            CommandExit
        };

        string userInput;

        bool isWorking = true;

        while (isWorking)
        {
            Console.Write("\t\tМеню автосервиса\n\n");

            ShowBalance();

            Console.Write("Доступные команды:\n");
            PrintMenu(menu);

            Console.Write("Ожидается ввод: ");
            userInput = GetCommandMenu(menu);

            Console.Clear();

            switch (userInput)
            {
                case CommandUpdateData:
                    UpdateData();
                    break;

                case CommandServeCar:
                    ServeCar();
                    break;

                case CommandShowStorage:
                    ShowStorage();
                    break;

                case CommandOrderPart:
                    OrderPart();
                    break;

                case CommandExit:
                    Console.Write("Вы завершии работу автосервиса.\n\n");
                    isWorking = false;
                    continue;

                default:
                    Console.Write("Требуется ввести номер команды или саму команду.\n");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    } 

    private void ShowStorage()
    {
        Console.Write("Склад автосервиса.\n");
        _storage.Show();
    }

    private void ShowBalance() =>
        Console.Write($"Кол-во денег на счету автосервиса: {_money}$\n\n");

    private void ShowResult(Car car)
    {
        int profit = _sumMoneyForRepair - _sumMoneyOfFines;

        Console.Write($"Машина исправна: {(car.IsFixedCar ? "Да" : "Нет")}\n" +
                      $"Кол-во денег за проведённую работу: {_sumMoneyForRepair}$\n" +
                      $"Кол-во денег за штрафы: {_sumMoneyOfFines}$\n" +
                      $"{new string('-', 20)}\n"+
                      $"Итог: {profit}$\n\n");
    }

    private void ShowCar(Car car)
    {
        Console.Write("Машина клиента:\n");
        car.ShowBrokenParts();
    }

    private void UpdateData()
    {
        CreateCar();
        Console.Write("Данные обновлены.\n");
    }

    private void ServeCar()
    {
        if (_cars.Count > 0)
        {
            Car car = _cars.Peek();

            if (TryFixCar(car, out int timeCount) == false)
                _sumMoneyOfFines += CalculateFines(timeCount, car.BrokenPartsCount);

            ShowResult(car);
            PayJob();

            _cars.Dequeue();
        }
        else
        {
            Console.Write("Очередь пуста. Обновите данные.\n");
        }
    }

    private bool CanServeCar(Car car)
    {
        string CommandYes = "Да";
        string userInput;

        ShowCar(car);

        Console.Write("Отказать клиенту в обслуживани?\n");

        Console.Write("Доступные команды:\n");
        Console.Write($"\t{CommandYes} - отказать\n" +
                      $"\tЛюбая другая кнопка - нет\n\n");
        
        Console.Write("Ожидается ввод: ");
        userInput = Console.ReadLine();

        Console.Clear();

        if (userInput == CommandYes)
            return false;
        else
            return true;
    }

    private bool TryFixCar(Car car, out int timeCount)
    {
        bool isServiced = true;

        timeCount = 0;

        while (isServiced)
        {
            isServiced = CanServeCar(car);

            if (isServiced == false)
                continue;

            FixCar(car);

            if (car.IsFixedCar)
                isServiced = false;

            timeCount++;

            Console.ReadKey();
            Console.Clear();
        }

        return car.IsFixedCar;
    }

    private void FixCar(Car car)
    {
        int priceRepair;

        Console.Clear();
        ShowCar(car);

        if (car.TryGetPart(out Part partCar) == false)
        {
            Console.Write("Не удалось получить запчасть из машины.\n");
            return;
        }

        Console.Clear();
        Console.Write("Теперь выберите запчасть со своего склада.\n");
        ShowStorage();

        if (_storage.TryGetPart(out Part partAutoService) == false)
        {
            Console.Write("Не удалось получить запчасть со склада.\n");
            return;
        }
        
        if (_storage.CanUsedPart(partAutoService) == false)
        {
            Console.Write("Запчать на складе закончилась.\n");
            return;
        }

        if (car.TryFixPart(partCar, partAutoService))
        {
            Console.Write("Запчасть машины успешна заменена на новую.\n");
            priceRepair = _priceWork + partAutoService.Price;
            _sumMoneyForRepair += priceRepair;

            Console.Write($"Цена починки составила {partAutoService.Price}$ + {_priceWork}$ = {priceRepair}$.\n");
        }
        else
        {
            Console.Write("Была заменена не та запчасть.\n");
            _sumMoneyOfFines += _priceFineForMistake;

            Console.Write($"Автосервису выписан штраф. Размер штрафа: {_priceFineForMistake}$\n");
        }

        _storage.RemovePart(partAutoService);
    }

    private int CalculateFines(int timeCount, int brokenPartsCount)
    {
        int sumMoneyOfFines = 0;

        sumMoneyOfFines += _priceFineForReject;

        if (timeCount > 0)
            sumMoneyOfFines += brokenPartsCount * _priceFineForMistake;

        return sumMoneyOfFines;
    }

    private bool TryToPayFine(int moneyOfFines)
    {
        if (CanToPay(moneyOfFines))
        {
            if (moneyOfFines > 0)
            {
                Pay(moneyOfFines);
                Console.Write("Штраф: Оплачен.\n");
            }
            else
            {
                Console.Write("Штраф: Не требуется оплачивать.\n");
            }

            return true;
        }
        else
        {
            Console.Write("Штраф: Не опалчен.\n");
        }

        return false;
    }

    private void PayJob()
    {
        if (TryToPayFine(_sumMoneyOfFines))
        {
            if (_sumMoneyForRepair > 0)
            {
                TakeMoney(_sumMoneyForRepair);
                Console.Write("Деньги: Получены.\n");
            }
            else
            {
                Console.Write("Деньги: Не требуется оплачивать.\n");
            }

            _sumMoneyForRepair = 0;
            _sumMoneyOfFines = 0;
        }
        else
        {
            Console.Write("Деньги: Не получены.\n");
        }
    }

    private void OrderPart()
    {
        int moneyToPay;

        ShowStorage();
        ShowBalance();

        if (_storage.TryGetPart(out Part foundPart) == false)
            return;

        if (_storage.CanAddPart(foundPart, out int partCount) == false)
            return;

        moneyToPay = foundPart.Price * partCount;
        Console.Write($"Сумма заказа составляет: {moneyToPay}$\n");

        if (CanToPay(moneyToPay))
        {
            Pay(moneyToPay);

            _storage.AddPart(foundPart, partCount);
            Console.Write("Заказ успешно оформлен.\n");
        }
        else
        {
            Console.Write("Не хватает денег.\n");
        }
    }

    private bool CanToPay(int moneyToPay) =>
        _money >= moneyToPay;

    private void Pay(int moneyToPay) =>
        _money -= moneyToPay;

    private void TakeMoney(int money) =>
        _money += money > 0 ? money : 0;

    private void CreateCar()
    {
        bool isCar = true;
        Car car = new Car(_partFactory.Create(isCar));

        _cars.Enqueue(car);
    }

    private Inventory CreateStorage() =>
        new Inventory(_partFactory.Create());

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

class Car
{
    private readonly List<Part> _parts;
    private readonly List<Part> _brokenParts;

    public Car(List<Part> parts)
    {
        _parts = parts;
        _brokenParts = BreakParts();
    }

    public int BrokenPartsCount => _brokenParts.Count;
    public bool IsFixedCar => _brokenParts.Count == 0;

    public void ShowBrokenParts()
    {
        int numberPart = 1;

        Console.Write("Сломанные запчасти:\n");

        foreach (Part part in _brokenParts)
        {
            Console.Write($"\t{numberPart}. ".PadRight(5));
            part.Show();

            numberPart++;
        }

        Console.WriteLine();
    }

    public bool TryGetPart(out Part foundItem)
    {
        int numberItem;

        foundItem = null;

        Console.Write("Введите номер запчасти: ");
        numberItem = UserUtils.ReadInt();

        if (numberItem > 0 && numberItem <= _brokenParts.Count)
        {
            foundItem = _brokenParts[numberItem - 1];
            return true;
        }
        else
        {
            Console.Write("Под таким номером нет запчасти.\n");
        }

        return false;
    }

    public bool TryFixPart(Part partCar, Part partAutoService)
    {
        if (partCar.Name == partAutoService.Name && partCar.IsBroken)
        {
            _brokenParts.Remove(partCar);
            _parts.Add(partAutoService);

            return true;
        }

        return false;
    }

    private List<Part> BreakParts()
    {
        List<Part> brokenParts = new List<Part>();
        Part part;
        int index;

        int minCountBrokenParts = 1;
        int maxCountBrokenParts = 3;
        int partsCount = UserUtils.GenerateRandomNumber(minCountBrokenParts, maxCountBrokenParts);

        for (int i = 0; i < partsCount; i++)
        {
            index = UserUtils.GenerateRandomNumber(maxNumber: _parts.Count);
            part = _parts[index];

            if (brokenParts.Contains(part) == false)
            {
                brokenParts.Add(part.Clone(part.Name, part.Price, Part.ConditionBroken));
                _parts.Remove(part);
            }
        }

        return brokenParts;
    }
}

class Inventory
{
    private readonly List<Cell> _cells;

    public Inventory(List<Part> parts)
    {
        _cells = new List<Cell>();

        ReadParts(parts);
    }

    public void Show()
    {
        int numberCell = 1;

        Console.Write("Запчасти:\n");

        foreach (Cell cell in _cells)
        {
            Console.Write($"\t{numberCell}. ".PadRight(5));
            cell.Show();

            numberCell++;
        }

        Console.WriteLine();
    }

    public void AddPart(Part part, int partCount)
    {
        foreach (Cell cell in _cells)
        {
            if (cell.Part == part)
            {
                cell.IncreaseCount(partCount);
                break;
            }
        }
    }

    public void RemovePart(Part part)
    {
        foreach (Cell cell in _cells)
        {
            if (cell.Part == part)
            {
                cell.DecreaseCount();
                break;
            }
        }
    }

    public bool CanAddPart(Part part, out int partCount)
    {
        partCount = 0;

        foreach (Cell cell in _cells)
        {
            if (cell.Part != part)
                continue;

            if (cell.CanIncreaseCount(out partCount))
                return true;
        }

        return false;
    }

    public bool CanUsedPart(Part part)
    {
        foreach (Cell cell in _cells)
        {
            if (cell.Part != part)
                continue;

            if (cell.PartCount > 0)
                return true;
        }

        return false;
    }
        
    public bool TryGetPart(out Part foundItem)
    {
        int numberCell;

        foundItem = null;

        Console.Write("Введите номер запчасти: ");
        numberCell = UserUtils.ReadInt();

        if (numberCell > 0 && numberCell <= _cells.Count)
        {
            foundItem = _cells[numberCell - 1].Part;
            return true;
        }
        else
        {
            Console.Write("Под таким номером нет запчасти.\n");
        }
        
        return false;
    }

    private void ReadParts(List<Part> parts) =>
        parts.ForEach(part => _cells.Add(new Cell(part)));
}

class Cell
{
    private readonly int _maxCountPart = 10;

    public Cell(Part part, int partCount = 0)
    {
        Part = part;
        PartCount = partCount;
    }

    public Cell(Part part)
    {
        Part = part;
        PartCount = _maxCountPart;
    }

    public Part Part { get; private set; }
    public int PartCount { get; private set; }

    public void Show()
    {
        Console.Write($"{PartCount}/{_maxCountPart} ".PadLeft(7));
        Part.Show();
    }

    public void IncreaseCount(int partCount) =>
        PartCount += partCount > 0 ? partCount : 0;

    public void DecreaseCount() =>
        PartCount--;

    public bool CanIncreaseCount(out int partCount)
    {
        int temporaryPartCount;

        if (PartCount < _maxCountPart)
        {
            Console.Write("Введите кол-во: ");
            partCount = UserUtils.ReadInt();

            temporaryPartCount = PartCount + partCount;

            if (temporaryPartCount <= _maxCountPart)
                return true;
            else
                Console.Write($"Вы хотите прибавить больше максимума. Максимум: {_maxCountPart}\n");
        }
        else
        {
            Console.Write($"Нет места. {PartCount}/{_maxCountPart}\n");
        }

        partCount = 0;
        return false;
    }
}

class Part
{
    public const string ConditionBroken = "Сломана";
    public const string ConditionWorking = "Исправна";

    public Part(string name, int minPrice = 0, int maxPrice = 0)
    {
        Name = name;
        Price = UserUtils.GenerateRandomNumber(minPrice, maxPrice );
        Condition = ConditionWorking;
    }

    public Part(string name, int price = 0, string condition = ConditionWorking)
    {
        Name = name;
        Price = price;
        Condition = condition;
    }

    public string Name { get; private set; }
    public int Price { get; private set; }
    public string Condition { get; private set; }
    public bool IsBroken => Condition == ConditionBroken;

    public void Show()
    {
        if (Price == 0)
            Console.Write($"{Name}".PadRight(20) + $"Состояние: {Condition}\n");
        else
            Console.Write($"{Name}".PadRight(20) + $"Цена: {Price}$".PadRight(10) + $"Состояние: {Condition}\n");
    }

    public Part Clone(string name, int price, string condition = ConditionWorking) =>
        new Part(name, price, condition);
}