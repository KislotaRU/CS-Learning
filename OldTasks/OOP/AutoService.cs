using System;
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
    public const int FullValue = 100;
    public const int HalfValue = 50;

    private static readonly Random s_random;

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

        if (int.TryParse(userInput, out int number))
            if (number > 0 && number <= arrayMenu.Length)
                return arrayMenu[number - 1];

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
    private int _coinsToPay = 0;

    public AutoService()
    {
        _storage = new Storage();
    }

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

    private void ShowResult(string userInput, int pricePenalty)
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

        ShowResult(userInput, pricePenalty);
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
        int coinsToPay;

        ShowStorage();
        ShowBalance();

        if (_storage.TryOrderPart(out string partName, out int partPrice, out int partCount))
        {
            Console.Write("Теперь необходимо оплатить заказ.\n");

            coinsToPay = partPrice * partCount;

            Console.Write($"Сумма заказа составляет: {coinsToPay}$\n");

            if (CanToPay(coinsToPay))
            {
                Pay();

                _storage.AddPart(partName, partCount);
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

    private bool CanToPay(int coinsToPay)
    {
        _coinsToPay = _coins >= coinsToPay ? coinsToPay : 0;
        return _coins >= coinsToPay;
    }

    private void Pay() =>
        _coins -= _coinsToPay;

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
        int numberShelf = 1;

        foreach (Shelf shelf in _shelfs)
        {
            Console.Write($"\t{numberShelf++}. ");
            shelf.Show();
        }
    }

    public bool TryOrderPart(out string partName, out int partPrice, out int partCount)
    {
        string userInput;

        partName = null;
        partCount = 0;
        partPrice = 0;

        Console.Write("Введите номер необходимой запчасти, которую хотите заказать: ");
        userInput = Console.ReadLine();

        if (TryGetShelf(userInput, out Shelf shelf))
        {
            if (shelf.CanIncrementCount(out partCount))
            {
                partName = shelf.PartName;
                partPrice = shelf.PartPrice;
                return true;
            }
        }
        else
        {
            Console.Write("Не удалось получить запчасть.\n");
        }

        return false;
    }

    public void AddPart(string partName, int partCount)
    {
        foreach (Shelf shelf in _shelfs)
        {
            if (shelf.PartName == partName)
            {
                shelf.IncrementCount(partCount);
                break;
            }
        }
    }

    public void RemovePart(string partName)
    {
        foreach (Shelf shelf in _shelfs)
        {
            if (shelf.PartName == partName)
            {
                shelf.DecrementCount();
                break;
            }
        }
    }

    public bool TryGetShelf(string userInput, out Shelf foundShelf)
    {
        if (int.TryParse(userInput, out int numberShelf))
        {
            if (numberShelf > 0 && numberShelf <= _shelfs.Count)
            {
                foreach (Shelf shelf in _shelfs)
                {
                    if (shelf.PartName == _shelfs[numberShelf - 1].PartName)
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
        FactoryParts factoryParts = new FactoryParts();

        List<Part> possibleParts = factoryParts.GetParts();

        foreach (Part part in possibleParts)
            _shelfs.Add(new Shelf(part));
    }
}

class Shelf
{
    private readonly Part _part;

    private readonly int _maxCountPart = 10;

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
        Console.Write($"{PartCount}/{_maxCountPart} ".PadLeft(6));

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

    public bool CanIncrementCount(out int partCount)
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
    private readonly int _minCountBrokenParts = 2;
    private readonly int _maxCountBrokenParts = 6;

    private readonly List<Part> _parts;

    private int _countBrokenParts;

    public Car()
    {
        _parts = new List<Part>();

        CreateParts();
    }

    public bool IsFixedCar { get { return _countBrokenParts == 0; } }

    public void Show()
    {
        int numberPart = 1;

        foreach (Part part in _parts)
        {
            Console.Write($"\t{numberPart++}. ");
            part.Show();
        }
    }

    public bool TryGetPart(string userInput, out Part partCar)
    {
        if (int.TryParse(userInput, out int numberPartCar))
        {
            if (numberPartCar > 0 && numberPartCar <= _parts.Count)
            {
                partCar = _parts[numberPartCar - 1];
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
            _parts.Add(partAutoService.Clone(partAutoService.Name, partAutoService.Condition));
            _countBrokenParts--;
            return true;
        }

        return false;
    }

    private void CreateParts()
    {
        FactoryParts factoryParts = new FactoryParts();

        List<Part> temporaryParts = factoryParts.GetParts();

        int indexPart;
        int countBrokenParts = UserUtils.GenerateRandomNumber(_minCountBrokenParts, _maxCountBrokenParts);
        
        foreach (Part part in temporaryParts)
            _parts.Add(part.Clone(part.Name, Part.ConditionWorking));

        for (int i = 0; i < countBrokenParts; i++)
        {
            indexPart = UserUtils.GenerateRandomNumber(_parts.Count);

            _parts.Add(_parts[indexPart].Clone(_parts[indexPart].Name, Part.ConditionBroken));
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

class FactoryParts
{
    private readonly List<Part> _parts;

    public FactoryParts()
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

    public List<Part> GetParts()
    {
        List<Part> temporaryParts = new List<Part>();

        foreach (Part part in _parts)
            temporaryParts.Add(part.Clone(part.Name, part.Price));

        return temporaryParts;
    }
}

class Part
{
    public const string ConditionBroken = "Сломанное";
    public const string ConditionWorking = "Рабочее";

    public Part(string name, int minPrice, int maxPrice)
    {
        Name = name;
        Price = UserUtils.GenerateRandomNumber(minPrice, maxPrice);
        Condition = ConditionWorking;
    }

    public Part(string name, int price)
    {
        Name = name;
        Price = price;
        Condition = ConditionWorking;
    }

    public Part(string name, string condition = ConditionWorking)
    {
        Name = name;
        Condition = condition;
    }

    public string Name { get; protected set; }
    public int Price { get; protected set; }
    public string Condition { get; protected set; }

    public void Show()
    {
        if (Price == 0)
            Console.Write($"{Name}".PadRight(20) + $"Состояние: {Condition}\n");
        else
            Console.Write($"{Name}".PadRight(20) + $"Цена: {Price}$".PadRight(10) + $"Состояние: {Condition}\n");
    }

    public Part Clone(string name, string condition = ConditionWorking) =>
        new Part(name, condition);

    public Part Clone(string name, int price) =>
        new Part(name, price);
}