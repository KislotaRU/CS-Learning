﻿using System;
using System.Collections.Generic;

//У вас есть автосервис, в который приезжают люди, чтобы починить свои автомобили.
//У вашего автосервиса есть баланс денег и склад деталей.
//Когда приезжает автомобиль, у него сразу ясна его поломка, и эта поломка отображается у вас в
//консоли вместе с ценой за починку(цена за починку складывается из цены детали + цена за работу).
//Поломка всегда чинится заменой детали, но количество деталей ограничено тем, что находится на вашем складе деталей.
//Если у вас нет нужной детали на складе, то вы можете отказать клиенту, и в этом случае вам придется выплатить штраф.
//Если вы замените не ту деталь, то вам придется возместить ущерб клиенту.
//За каждую удачную починку вы получаете выплату за ремонт, которая указана в чек-листе починки.
//Класс Деталь не может содержать значение “количество”. Деталь всего одна, за количество отвечает тот, кто хранит детали.
//При необходимости можно создать дополнительный класс для конкретной детали и работе с количеством.

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;

            AutoService autoService = new AutoService();

            autoService.Work();
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

    public static string GetParagraphMenu(string[] arrayMenu)
    {
        string userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int index))
            if (index > 0 && index <= arrayMenu.Length)
                return arrayMenu[index - 1];

        return null;
    }

    public static void ShuffleList<T>(List<T> list)
    {
        int firstIndex;
        int secondIndex;

        T temporaryItem;

        for (int i = 0; i < list.Count; i++)
        {
            firstIndex = s_random.Next(0, list.Count);
            secondIndex = s_random.Next(0, list.Count);

            temporaryItem = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temporaryItem;
        }
    }
}

class AutoService
{
    private const string CommandShowStorage = "Посмотреть свой склад";
    private const string CommandServeCustomer = "Обслужить машину";
    private const string CommandOrderPart = "Заказать запчасть";
    private const string CommandExit = "Завершить работу";
    private const string CommandDeny = "Отказать";
    private const string CommandContinue = "Продолжить";

    private readonly Storage _storage;

    private readonly int _pricePenatly = 100;
    private readonly int _priceWork = 30;

    private Car _carCustomer;

    private int _coins = 1000;

    public AutoService()
    {
        _storage = new Storage();
    }

    public int CoinsToPay { get; private set; }

    public void Work()
    {
        string[] menu = new string[]
        {
            CommandShowStorage,
            CommandServeCustomer,
            CommandOrderPart,
            CommandExit
        };

        string userInput = null;

        while (userInput != CommandExit)
        {
            Console.Write("\t\tАвтосервис \"100 по 100\"\n\n");

            ShowBalance();

            Console.Write("\nДоступные команды:\n");
            UserUtils.PrintMenu(menu);

            Console.Write("\nОжидается ввод: ");
            userInput = UserUtils.GetParagraphMenu(menu);

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
                    Console.Write("Вы завершии работу Автосервиса.\n");
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
        Console.Write("\t\tСклад Автосервиса \"100 по 100\"\n");

        _storage.ShowShelfs();
    }

    private void ShowBalance() =>
        Console.Write($"Кол-во денег на счету Автосервиса: {_coins}$\n");

    private void ShowResult(bool isFixedCar, string userInput, int pricePenalty)
    {
        if (_carCustomer.IsFixedCar)
            Console.Write("Машина полностью исправна.\n");
        else
            Console.Write("Машина осталась непочиненной.\n");

        if (userInput == CommandDeny)
            Console.Write("Автосервис отказал клиенту в обслуживании.\n");

        if (pricePenalty > 0)
            Console.Write("Автосервис не в силах оплачивать штрафы.\n");

        Console.Write("Клиент ушёл.\n");
    }

    private void ServeCustomer()
    {
        _carCustomer = new Car();

        string[] menu = new string[]
        {
            CommandDeny,
            CommandContinue
        };

        string userInput = null;

        int pricePenalty = 0;

        while ((_carCustomer.IsFixedCar == false) && (userInput != CommandDeny) && (pricePenalty == 0))
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
                Console.Write("\nОтказать клиенту в обслуживани?\n");
                UserUtils.PrintMenu(menu);

                Console.Write("\nОжидается ввод: ");
                userInput = UserUtils.GetParagraphMenu(menu);

                if (userInput == CommandDeny)
                {
                    Console.Write("Клиенту отказано в обслуживание.\n");
                    pricePenalty += _pricePenatly;
                }
            }

            if (pricePenalty > 0)
            {
                Console.Write($"Итоговая сумма штрафа составила {pricePenalty}$.\n");

                if (TryToPay(pricePenalty))
                {
                    ToPay();

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

        ShowResult(_carCustomer.IsFixedCar, userInput, pricePenalty);
    }

    private bool TryFixBrokenPart(Car carCustomer, out int priceRepair, out int pricePenalty)
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

            if (_storage.TryGetShelf(userInput, out Shelf foundShelf))
            {
                Part partAutoService = foundShelf.GetPart();

                if (carCustomer.TryFixBrokenPart(partCar, partAutoService))
                {
                    Console.Write("Запчасть машины успешна заменена на новую.\n");
                    priceRepair = _priceWork + partAutoService.Price;

                    Console.Write($"Цена починки составила {partAutoService.Price}$ + {_priceWork}$ = {priceRepair}$.\n");
                    return true;
                }
                else
                {
                    Console.Write("Была заменена не та запчасть.\n Автосервису выписан штраф.\n");

                    pricePenalty = (int)(partAutoService.Price * UserUtils.HalfValue);

                    Console.Write($"Размер штрафа {pricePenalty}$\n");
                }

                _storage.RemovePart(partAutoService);
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
        int coinsToPay;

        ShowStorage();
        ShowBalance();

        if (_storage.TryOrderPart(out Shelf foundShelf, out int partCount))
        {
            Part orderPart = foundShelf.GetPart();

            coinsToPay = orderPart.Price * partCount;

            Console.Write($"Сумма заказа составляет: {coinsToPay}$\n");

            if (TryToPay(coinsToPay))
            {
                ToPay();

                _storage.AddPart(orderPart, partCount);
                Console.Write("Заказ успешно оформлен.\n");
            }
            else
            {
                Console.Write("Не хватает денег на такой заказ.\n");
            }
        }
        else
        {
            Console.Write("Не получилось заказать деталь.\n");
        }
    }

    private bool TryToPay(int coinsToPay)
    {
        CoinsToPay = _coins >= coinsToPay ? coinsToPay : 0;
        return _coins >= coinsToPay;
    }

    private void ToPay() =>
        _coins -= CoinsToPay;

    private void CollectCoins(int coinsCollected)
    {
        if (coinsCollected > 0)
            _coins += coinsCollected;
    }
}

class Storage
{
    private readonly List<Shelf> _shelfs;

    public Storage()
    {
        _shelfs = new List<Shelf>();

        CreateShelfs();
    }

    public void ShowShelfs()
    {
        int indexShelf = 0;

        foreach (Shelf shelf in _shelfs)
        {
            Console.Write($"\t{++indexShelf}. ");
            shelf.Show();
        }
    }

    public bool TryOrderPart(out Shelf shelf, out int partCount)
    {
        string userInput;

        partCount = 0;

        Console.Write("Введите номер необходимой запчасти, которую хотите заказать: ");
        userInput = Console.ReadLine();

        if (TryGetShelf(userInput, out shelf))
        {
            if (shelf.TryIncrementCount(out partCount))
            {
                Console.Write("Теперь необходимо оплатить заказ.\n");
                return true;
            }
            else
            {
                Console.Write("Не удалось выбрать кол-во запчасти.\n");
            }
        }
        else
        {
            Console.Write("Не удалось получить запчасть.\n");
        }

        return false;
    }

    public void AddPart(Part part, int partCount)
    {
        foreach (Shelf shelf in _shelfs)
        {
            if (shelf.PartName == part.Name)
            {
                shelf.IncrementCount(partCount);
                break;
            }
        }
    }

    public void RemovePart(Part part)
    {
        foreach (Shelf shelf in _shelfs)
        {
            if (shelf.PartName == part.Name)
            {
                shelf.DecrementCount();
                break;
            }
        }
    }

    public bool TryGetShelf(string userInput, out Shelf foundShelf)
    {
        if (int.TryParse(userInput, out int indexShelf))
        {
            if (indexShelf > 0 && indexShelf <= _shelfs.Count)
            {
                foreach (Shelf shelf in _shelfs)
                {
                    if (shelf.PartName == _shelfs[indexShelf - 1].PartName)
                    {
                        foundShelf = shelf;
                        return true;
                    }
                }
            }
            else
            {
                Console.Write("Такого стеллажа не существует.\n");
            }
        }
        else
        {
            Console.Write("Требуется ввести номер стеллажа.\n");
        }

        foundShelf = null;
        return false;
    }

    private void CreateShelfs()
    {
        List<Part> possibleParts = new List<Part>()
        {
            new Transmission (),
            new Wheel (),
            new Windshield (),
            new Battery (),
            new SparkPlug (),
            new EngineOil (),
            new ExhaustManifold (),
            new Bumper ()
        };

        foreach (Part part in possibleParts)
            _shelfs.Add(new Shelf(part));
    }
}

class Shelf
{
    private readonly int _maxCountPart = 10;
    private readonly Part _part;

    public Shelf(Part part, int partCount = 0)
    {
        _part = part;
        PartCount = partCount;
    }

    public Shelf(Part part)
    {
        _part = part;

        Fill();
    }

    public string PartName { get { return _part.Name; } }
    public int PartPrice { get { return _part.Price; } }
    public int PartCount { get; private set; }

    public void Show()
    {
        Console.Write($"{PartCount}/{_maxCountPart} ");

        _part.Show();
    }

    public Part GetPart() =>
        PartCount > 0 ? _part : null;

    public void IncrementCount(int partCount)
    {
        if (partCount > 0)
            PartCount += partCount;

        if (PartCount > _maxCountPart)
            PartCount = _maxCountPart;
    }

    public void DecrementCount()
    {
        PartCount--;

        if (PartCount < 0)
            PartCount = 0;
    }

    public bool TryIncrementCount(out int partCount)
    {
        string userInput;
        int temporaryPartCount;

        if (PartCount != _maxCountPart)
        {
            Console.Write("Введите кол-во этой запчасти, которое хотите заказать: ");
            userInput = Console.ReadLine();

            if (int.TryParse(userInput, out partCount))
            {
                temporaryPartCount = PartCount + partCount;

                if (temporaryPartCount <= _maxCountPart)
                {
                    return true;
                }
                else
                {
                    Console.Write($"Вы хотите заказать больше, чем это возможно.\n" +
                                  $"Максимальное кол-во запчасти на стеллаже: {_maxCountPart}\n");
                }
            }
        }
        else
        {
            Console.Write("Кол-во этой запчасти на стеллаже достигло максимума.\n");
        }

        partCount = 0;
        return false;
    }

    private void Fill() =>
        PartCount = _maxCountPart;
}

class Car
{
    private readonly List<Part> _parts;

    private readonly int _minCountBrokenParts = 2;
    private readonly int _maxCountBrokenParts = 6;

    private int _countBrokenParts;

    public Car()
    {
        _parts = new List<Part>();

        CreateParts();
    }

    public bool IsFixedCar { get { return _countBrokenParts == 0; } }

    public void Show()
    {
        int numberPart = 0;

        foreach (Part part in _parts)
        {
            Console.Write($"\t{++numberPart}. ");
            part.Show();
        }
    }

    public bool TryGetPart(string userInput, out Part partCar)
    {
        if (int.TryParse(userInput, out int indexPartCar))
        {
            if (indexPartCar > 0 && indexPartCar <= _parts.Count)
            {
                partCar = _parts[indexPartCar - 1];
                return true;
            }
            else
            {
                Console.Write("Под таким номером нет запчасти.\n");
            }
        }
        else
        {
            Console.Write("Требуется ввести номер запчасти машины.\n");
        }

        partCar = null;
        return false;
    }

    public bool TryFixBrokenPart(Part partCar, Part partAutoService)
    {
        if (partCar.Name == partAutoService.Name && partCar.Condition == Part.ConditionBroken)
        {
            _parts.Remove(partCar);
            _parts.Add(partAutoService.Clone(partAutoService.Condition));
            _countBrokenParts--;
            return true;
        }

        return false;
    }

    private void CreateParts()
    {
        List<Part> possibleParts = new List<Part>()
        {
            new Transmission (Part.ConditionWorking),
            new Wheel (Part.ConditionWorking),
            new Windshield (Part.ConditionWorking),
            new Battery (Part.ConditionWorking),
            new SparkPlug (Part.ConditionWorking),
            new EngineOil (Part.ConditionWorking),
            new ExhaustManifold (Part.ConditionWorking),
            new Bumper (Part.ConditionWorking)
        };

        int indexPart;
        int countBrokenParts = UserUtils.GenerateRandomNumber(_minCountBrokenParts, _maxCountBrokenParts);

        foreach (Part part in possibleParts)
        {
            _parts.Add(part.Clone(Part.ConditionWorking));
        }

        for (int i = 0; i < countBrokenParts; i++)
        {
            indexPart = UserUtils.GenerateRandomNumber(_parts.Count);

            _parts.Add(_parts[indexPart].Clone(Part.ConditionBroken));
            _parts.Remove(_parts[indexPart]);
        }

        foreach (Part part in _parts)
        {
            if (part.Condition == Part.ConditionBroken)
                _countBrokenParts++;
        }

        UserUtils.ShuffleList(_parts);
    }
}

abstract class Part
{
    public const string ConditionBroken = "Сломанное";
    public const string ConditionWorking = "Рабочее";

    public string Name { get; protected set; }
    public int Price { get; protected set; }
    public string Condition { get; protected set; }

    abstract public Part Clone(string condition);

    public void Show()
    {
        if (Price == 0)
            Console.Write($"{Name}".PadRight(20) + $"Состояние: {Condition}\n");
        else
            Console.Write($"{Name}".PadRight(20) + $"Цена: {Price}$".PadRight(10) + $"Состояние: {Condition}\n");
    }
}

class Transmission : Part
{
    private readonly string _name = "Трансмиссия";

    private readonly int _minPrice = 70;
    private readonly int _maxPrice = 80;

    public Transmission(string condition = ConditionWorking)
    {
        Name = _name;
        Condition = condition;
    }

    public Transmission()
    {
        Name = _name;
        Price = UserUtils.GenerateRandomNumber(_minPrice, _maxPrice);
        Condition = ConditionWorking;
    }

    public override Part Clone(string condition = ConditionWorking) =>
        new Transmission(condition);
}

class Wheel : Part
{
    private readonly string _name = "Колесо";

    private readonly int _minPrice = 15;
    private readonly int _maxPrice = 25;

    public Wheel(string condition = ConditionWorking)
    {
        Name = _name;
        Condition = condition;
    }

    public Wheel()
    {
        Name = _name;
        Price = UserUtils.GenerateRandomNumber(_minPrice, _maxPrice);
        Condition = ConditionWorking;
    }

    public override Part Clone(string condition = ConditionWorking) =>
        new Wheel(condition);
}

class Windshield : Part
{
    private readonly string _name = "Лобовое стекло";

    private readonly int _minPrice = 30;
    private readonly int _maxPrice = 40;

    public Windshield(string condition = ConditionWorking)
    {
        Name = _name;
        Condition = condition;
    }

    public Windshield()
    {
        Name = _name;
        Price = UserUtils.GenerateRandomNumber(_minPrice, _maxPrice);
        Condition = ConditionWorking;
    }

    public override Part Clone(string condition = ConditionWorking) =>
        new Windshield(condition);
}

class Battery : Part
{
    private readonly string _name = "Аккумулятор";

    private readonly int _minPrice = 5;
    private readonly int _maxPrice = 15;

    public Battery(string condition = ConditionWorking)
    {
        Name = _name;
        Condition = condition;
    }

    public Battery()
    {
        Name = _name;
        Price = UserUtils.GenerateRandomNumber(_minPrice, _maxPrice);
        Condition = ConditionWorking;
    }

    public override Part Clone(string condition = ConditionWorking) =>
        new Battery(condition);
}

class SparkPlug : Part
{
    private readonly string _name = "Свеча зажигания";

    private readonly int _minPrice = 5;
    private readonly int _maxPrice = 10;

    public SparkPlug(string condition = ConditionWorking)
    {
        Name = _name;
        Condition = condition;
    }

    public SparkPlug()
    {
        Name = _name;
        Price = UserUtils.GenerateRandomNumber(_minPrice, _maxPrice);
        Condition = ConditionWorking;
    }

    public override Part Clone(string condition = ConditionWorking) =>
        new SparkPlug(condition);
}

class EngineOil : Part
{
    private readonly string _name = "Моторное масло";

    private readonly int _minPrice = 10;
    private readonly int _maxPrice = 20;

    public EngineOil(string condition = ConditionWorking)
    {
        Name = _name;
        Condition = condition;
    }

    public EngineOil()
    {
        Name = _name;
        Price = UserUtils.GenerateRandomNumber(_minPrice, _maxPrice);
        Condition = ConditionWorking;
    }

    public override Part Clone(string condition = ConditionWorking) =>
        new EngineOil(condition);
}

class ExhaustManifold : Part
{
    private readonly string _name = "Выхлопной коллектор";

    private readonly int _minPrice = 20;
    private readonly int _maxPrice = 30;

    public ExhaustManifold(string condition = ConditionWorking)
    {
        Name = _name;
        Condition = condition;
    }

    public ExhaustManifold()
    {
        Name = _name;
        Price = UserUtils.GenerateRandomNumber(_minPrice, _maxPrice);
        Condition = ConditionWorking;
    }

    public override Part Clone(string condition = ConditionWorking) =>
        new ExhaustManifold(condition);
}

class Bumper : Part
{
    private readonly string _name = "Бампер";

    private readonly int _minPrice = 25;
    private readonly int _maxPrice = 35;

    public Bumper(string condition = ConditionWorking)
    {
        Name = _name;
        Condition = condition;
    }

    public Bumper()
    {
        Name = _name;
        Price = UserUtils.GenerateRandomNumber(_minPrice, _maxPrice);
        Condition = ConditionWorking;
    }

    public override Part Clone(string condition = ConditionWorking) =>
        new Bumper(condition);
}