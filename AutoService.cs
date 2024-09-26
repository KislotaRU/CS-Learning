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
    public const int FullValue = 100;
    public const int HalfValue = 50;

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

    public static void ShuffleList<T>(List<T> list)
    {
        int firstIndex;
        int secondIndex;
        T temporaryElement;

        for (int i = 0; i < list.Count; i++)
        {
            firstIndex = i;
            secondIndex = s_random.Next(list.Count);

            temporaryElement = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temporaryElement;
        }
    }
}

class AutoService
{
    private readonly Inventory _storage;

    private readonly int _pricePenatly = 100;
    private readonly int _priceWork = 30;

    private Car _carCustomer;

    private int _money = 1000;

    public AutoService()
    {

        _storage = new Inventory();
    }

    public void Work()
    {
        const string CommandShowStorage = "Посмотреть склад";
        const string CommandServeCustomer = "Обслужить клиента";
        const string CommandOrderPart = "Заказать запчасть";
        const string CommandExit = "Завершить работу";

        string[] menu = new string[]
        {
            CommandShowStorage,
            CommandServeCustomer,
            CommandOrderPart,
            CommandExit
        };

        string userInput = null;

        bool isWorking = true;

        while (isWorking)
        {
            Console.Write("\t\tМеню автосервиса\n\n");

            ShowBalance();

            Console.Write("\nДоступные команды:\n");
            PrintMenu(menu);

            Console.Write("\nОжидается ввод: ");
            userInput = GetCommandMenu(menu);

            Console.Clear();

            switch (userInput)
            {
                case CommandShowStorage:
                    ShowStorage();
                    break;

                case CommandServeCustomer:
                    ServeCustomer();
                    break;

                case CommandOrderPart:
                    OrderPart();
                    break;

                case CommandExit:
                    Console.Write("Вы завершии работу автосервиса.\n");
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

    private void ShowStorage()
    {
        Console.Write("Склад автосервиса.\n");
        _storage.Show();
    }

    private void ShowBalance() =>
        Console.Write($"Кол-во денег на счету автосервиса: {_money}$\n");

    private void ShowResult()
    {
        if (_carCustomer.IsFixedCar)
            Console.Write("Машина починена.\n");
        else
            Console.Write("Машина осталась непочиненной.\n");

        Console.Write("Клиент ушёл.\n");
    }

    private void ServeCustomer()
    {
        const string CommandDeny = "Отказать";
        const string CommandAgree = "Согласиться";

        string[] menu = new string[]
        {
            CommandDeny,
            CommandAgree
        };

        string userInput;
        bool isServiced = true;

        int pricePenalty = 0;

        CreateCustomer();

        Console.Write("Машина клиента:\n");
        _carCustomer.Show();

        while (isServiced)
        {
            Console.Clear();

            ShowBalance();

            Console.Write("Машина клиента:\n");
            _carCustomer.Show();

            if (TryFixBrokenPart(_carCustomer, out int priceRepair, out pricePenalty))
            {
                Console.Write($"Полученна сумма за проделанную работу в размере {priceRepair}$.\n");
                CollectCoins(priceRepair);
            }
            else
            {
                Console.Write("Отказать клиенту в обслуживани?\n");
                PrintMenu(menu);

                Console.Write("Ожидается ввод: ");
                userInput = GetCommandMenu(menu);

                if (userInput == CommandDeny)
                {
                    Console.Write("Клиенту отказано в обслуживание.\n");
                    pricePenalty += _pricePenatly;
                }
            }

            if (pricePenalty > 0)
            {
                Console.Write($"Итоговая сумма штрафа составила {pricePenalty}$.\n");

                if (CanToPay(pricePenalty))
                {
                    Pay();

                    pricePenalty = 0;
                    Console.Write("Штраф оплачен.\n");
                }
                else
                {
                    Console.Write("Штраф не удалось оплатить.\n");
                }
            }

            Console.ReadKey();
        }

        Console.Clear();

        ShowResult();
    }

    private void CreateCustomer()
    {
        PartFactory partFactory = new PartFactory();
        List<Part> parts = partFactory.CreateForCar();

        _carCustomer = new Car(parts);
    }

    private bool TryFixCar(Car carCustomer, out int priceRepair, out int pricePenalty)
    {
        string userInput;

        priceRepair = 0;
        pricePenalty = 0;

        Console.Write("\nВыберити какую запчасть хотите заменить: ");
        userInput = Console.ReadLine();

        if (carCustomer.TryGetPart(userInput, out Part partCar))
        {
            Console.Clear();
            ShowStorage();

            Console.Write("\nТеперь выберите запчасть со своего склада: ");
            userInput = Console.ReadLine();

            if (_storage.TryGetItem(userInput, out Cell foundShelf))
            {
                Part partAutoService = foundShelf.GetPart();

                if (carCustomer.TryFixBrokenPart(partCar, partAutoService))
                {
                    Console.Write("Запчасть машины успешна заменена на новую.\n");
                    priceRepair = _priceWork + partAutoService.Price;

                    Console.Write($"Цена починки составила {partAutoService.Price}$ + {_priceWork}$ = {priceRepair}$.\n");
                }
                else
                {
                    Console.Write("Была заменена не та запчасть.\nАвтосервису выписан штраф.\n");

                    pricePenalty = (int)((float)partAutoService.Price / UserUtils.FullValue * UserUtils.HalfValue);

                    Console.Write($"Размер штрафа {pricePenalty}$\n");
                }

                _storage.RemovePart(partAutoService.Name);
                return true;
            }
            else
            {
                Console.Write("Не удалось получить запчасть со склада.\n");
            }
        }
        else
        {
            Console.Write("Не удалось получить запчасть из машины.\n");
        }

        return false;
    }

    private void OrderPart()
    {
        int moneyToPay;

        ShowStorage();
        ShowBalance();

        if (_storage.TryGetPart(out Part foundPart))
        {
            if (_storage.CanAddPart(foundPart, out int partCount))
            {
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
        }
    }

    private bool CanToPay(int moneyToPay) =>
        _money >= moneyToPay;

    private void Pay(int moneyToPay) =>
        _money -= moneyToPay;

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

    private readonly int _minCountBrokenParts = 1;
    private readonly int _maxCountBrokenParts = 3;

    private int _countBrokenParts;

    public Car(List<Part> parts)
    {
        _parts = parts;
        _countBrokenParts = UserUtils.GenerateRandomNumber(_minCountBrokenParts, _maxCountBrokenParts);

        BreakParts();
    }

    public bool IsFixedCar => _countBrokenParts == 0;

    public void Show()
    {
        int numberPart = 1;

        Console.Write("Внутренности машины:\n");

        foreach (Part part in _parts)
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

        if (numberItem > 0 && numberItem <= _parts.Count)
        {
            foundItem = _parts[numberItem - 1];
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
            partCar.Fix();
            _countBrokenParts--;
            return true;
        }

        return false;
    }

    private void BreakParts()
    {
        int index;

        for (int i = 0; i < _countBrokenParts; i++)
        {
            index = UserUtils.GenerateRandomNumber(maxNumber: _parts.Count);
            _parts[index].Break();
        }

        UserUtils.ShuffleList(_parts);
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
            if (cell.Part == part)
            {
                if (cell.CanIncreaseCount(out partCount))
                    return true;

                break;
            }
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

    public List<Part> Create()
    {
        List<Part> temporaryParts = new List<Part>();

        foreach (Part part in _parts)
            temporaryParts.Add(part.Clone(part.Name, part.Price));

        return temporaryParts;
    }

    public List<Part> CreateForCar()
    {
        List<Part> temporaryParts = new List<Part>();

        foreach (Part part in _parts)
            temporaryParts.Add(part.Clone(part.Name, 0));

        return temporaryParts;
    }
}

class Part
{
    private const string ConditionBroken = "Сломана";
    private const string ConditionWorking = "Исправна";

    public Part(string name, int minPrice = 0, int maxPrice = 0)
    {
        Name = name;
        Price = UserUtils.GenerateRandomNumber(minPrice, maxPrice );
        Condition = ConditionWorking;
    }

    public Part(string name, int price = 0)
    {
        Name = name;
        Price = price;
        Condition = ConditionWorking;
    }

    public string Name { get; private set; }
    public int Price { get; private set; }
    public string Condition { get; private set; }
    public bool IsFixed => Condition == ConditionWorking;
    public bool IsBroken => Condition == ConditionBroken;

    public void Show()
    {
        if (Price == 0)
            Console.Write($"{Name}".PadRight(20) + $"Состояние: {Condition}\n");
        else
            Console.Write($"{Name}".PadRight(20) + $"Цена: {Price}$".PadRight(10) + $"Состояние: {Condition}\n");
    }

    public void Fix() =>
        Condition = ConditionWorking;

    public void Break() =>
        Condition = ConditionBroken;

    public Part Clone(string name, int price) =>
        new Part(name, price);
}