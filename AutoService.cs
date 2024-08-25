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

    public static void ReadInputMenu(string[] arrayMenu, out string userInput)
    {
        userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int index))
            if (index > 0 && index <= arrayMenu.Length)
                userInput = arrayMenu[index - 1];
    }
}

class AutoService
{
    private const string CommandShowStorage = "Посмотреть свой склад";
    private const string CommandServeCustomer = "Обслужить машину";
    private const string CommandOrderPart = "Заказать запчасть";
    private const string CommandExit = "Завершить работу";

    private int _coins = 1000;
    private readonly Storage _storage;

    public AutoService()
    {
        _storage = new Storage();
    }

    public int CountToPay { get; private set; }

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
            Console.Write("\t\tАвтосервис \"100 по 100\"\n");

            ShowBalance();

            Console.Write("\nДоступные команды:\n");
            UserUtils.PrintMenu(menu);

            Console.Write("\nОжидается ввод: ");
            UserUtils.ReadInputMenu(menu, out userInput);

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
                    Exit();
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
        Console.Write($"\nКол-во денег на счету Автосервиса: {_coins}$\n");

    private void ServeCustomer()
    {
        Customer customer = new Customer();

        customer.ShowCar();

        Console.Write("Выберити, какую запчасть хотите заменить:");
    }

    private void OrderPart()
    {
        int coinsToPay;

        ShowStorage();
        ShowBalance();

        if (_coins > 0)
        {
            if (_storage.TryOrderPart(out Shelf shelf, out int partCount))
            {
                coinsToPay = shelf.PartPrice * partCount;

                Console.Write($"Сумма заказа составляет: {coinsToPay}$\n");

                if (TryToPay(coinsToPay))
                {
                    ToPay();

                    _storage.AddPart(shelf, partCount);
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
        else
        {
            Console.Write("У вас нет денег для заказа новых запчастей.\n");
        }
    }

    private void Exit()
    {
        Console.Write("Вы завершии работу Автосервиса.\n");
    }

    private bool TryToPay(int coinsToPay)
    {
        if (_coins >= coinsToPay)
        {
            CountToPay = coinsToPay;
            return true;
        }
        else
        {
            CountToPay = 0;
            return false;
        }
    }

    private void ToPay() =>
        _coins -= CountToPay;
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

        shelf = null;
        partCount = 0;

        Console.Write("Введите номер необходимой запчасти, которую хотите заказать: ");
        userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int indexShelf))
        {
            if (indexShelf > 0 && indexShelf <= _shelfs.Count)
            {
                shelf = _shelfs[--indexShelf];

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
                Console.Write("С таким номером нет запчасти.\n");
            }
        }
        else
        {
            Console.Write("Требуется ввести номер запчасти.\n");
        }

        return false;
    }

    public void AddPart(Shelf shelf, int partCount)
    {
        shelf.IncrementCount(partCount);
    }

    public void RemovePart(Shelf shelf, int partCount)
    {

    }

    private bool TryGetShelf(Part part, out Shelf foundShelf)
    {
        foreach (Shelf shelf in _shelfs)
        {
            if (shelf.PartName == part.Name)
            {
                foundShelf = shelf;
                return true;
            }
        }

        foundShelf = null;
        return false;
    }

    private void CreateShelfs()
    {
        List<Part> _possibleParts = new List<Part>()
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

        foreach (Part part in _possibleParts)
            _shelfs.Add(new Shelf(part));
    }
}

class Shelf
{
    private readonly int _maxCountPart = 10;
    private readonly Part _part;

    private int _partCount;

    public Shelf(Part part, int partCount = 0)
    {
        _part = part;
        _partCount = partCount;
    }

    public string PartName { get { return _part.Name; } }
    public int PartPrice { get { return _part.Price; } }

    public void Show()
    {
        Console.Write($"{_partCount}/{_maxCountPart} \n");

        _part.Show();
    }

    public void IncrementCount(int partCount)
    {
        _partCount += partCount;

        if (_partCount > _maxCountPart)
            _partCount = _maxCountPart;
    }

    public void DecrementCount(int partCount)
    {
        _partCount -= partCount;

        if (_partCount < 0)
            _partCount = 0;
    }

    public bool TryIncrementCount(out int partCount)
    {
        string userInput;
        int temporaryPartCount;

        if (_partCount != _maxCountPart)
        {
            Console.Write("Введите кол-во этой запчасти, которое хотите заказать: ");
            userInput = Console.ReadLine();

            if (int.TryParse(userInput, out partCount))
            {
                temporaryPartCount = _partCount + partCount;

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
}

class Customer
{
    private readonly Car car;

    public Customer()
    {
        car = new Car();
    }

    public void ShowCar()
    {
        Console.Write("Машина клиента:\n");

        car.Show();
    }

    public bool TryFixCar(Part newPart) =>
        car.TryFixBrokenPart(newPart);
}

class Car
{
    private readonly List<Part> _parts;

    private readonly int _maxCountBrokenParts = 4;

    public Car()
    {
        _parts = new List<Part>();

        CreateParts();
    }

    public void Show()
    {
        int numberPart = 0;

        if (_parts.Count != 0)
        {
            foreach (Part part in _parts)
                Console.Write($"\t{++numberPart}. {part.Name}\n");
        }
        else
        {
            Console.Write("\tМашина полностью исправна.\n");
        }
    }

    public bool TryFixBrokenPart(Part partAutoService)
    {
        foreach (Part partCar in _parts)
        {
            if (partCar.Name == partAutoService.Name)
                return true;
        }

        return false;
    }

    private void CreateParts()
    {
        List<Part> _possibleCarParts = new List<Part>()
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

        foreach (Part part in _possibleCarParts)
            _parts.Add(part);

        int countBrokenParts = UserUtils.GenerateRandomNumber(_maxCountBrokenParts);

        for (int i = 0; i < countBrokenParts; i++)
        {
            int indexPart = UserUtils.GenerateRandomNumber(_parts.Count);

            _parts.Remove(_possibleCarParts[indexPart]);
            _parts.Add(_possibleCarParts[indexPart].Clone(Part.ConditionBroken));
        }

        _parts.Reverse();
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

    public void Show(string condition = ConditionWorking)
    {
        Console.Write($"Цена: {Price}$ {Name} Состояние: {Condition}\n");
    }
}

class Transmission : Part
{
    private readonly string _name = "Трансмиссия";
    private readonly int _maxPrice = 80;

    public Transmission(string condition = ConditionWorking)
    {
        Name = _name;
        Price = UserUtils.GenerateRandomNumber(_maxPrice);
        Condition = condition;
    }

    public override Part Clone(string condition = ConditionWorking) =>
        new Transmission(condition);
}

class Wheel : Part
{
    private readonly string _name = "Колесо";
    private readonly int _maxPrice = 25;

    public Wheel(string condition = ConditionWorking)
    {
        Name = _name;
        Price = UserUtils.GenerateRandomNumber(_maxPrice);
        Condition = condition;
    }

    public override Part Clone(string condition = ConditionWorking) =>
        new Wheel(condition);
}

class Windshield : Part
{
    private readonly string _name = "Лобовое стекло";
    private readonly int _maxPrice = 40;

    public Windshield(string condition = ConditionWorking)
    {
        Name = _name;
        Price = UserUtils.GenerateRandomNumber(_maxPrice);
        Condition = condition;
    }

    public override Part Clone(string condition = ConditionWorking) =>
        new Windshield(condition);
}

class Battery : Part
{
    private readonly string _name = "Аккумулятор";
    private readonly int _maxPrice = 15;

    public Battery(string condition = ConditionWorking)
    {
        Name = _name;
        Price = UserUtils.GenerateRandomNumber(_maxPrice);
        Condition = condition;
    }

    public override Part Clone(string condition = ConditionWorking) =>
        new Battery(condition);
}

class SparkPlug : Part
{
    private readonly string _name = "Свеча зажигания";
    private readonly int _maxPrice = 10;

    public SparkPlug(string condition = ConditionWorking)
    {
        Name = _name;
        Price = UserUtils.GenerateRandomNumber(_maxPrice);
        Condition = condition;
    }

    public override Part Clone(string condition = ConditionWorking) =>
        new SparkPlug(condition);
}

class EngineOil : Part
{
    private readonly string _name = "Моторное масло";
    private readonly int _maxPrice = 20;

    public EngineOil(string condition = ConditionWorking)
    {
        Name = _name;
        Price = UserUtils.GenerateRandomNumber(_maxPrice);
        Condition = condition;
    }

    public override Part Clone(string condition = ConditionWorking) =>
        new EngineOil(condition);
}

class ExhaustManifold : Part
{
    private readonly string _name = "Выхлопной коллектор";
    private readonly int _maxPrice = 30;

    public ExhaustManifold(string condition = ConditionWorking)
    {
        Name = _name;
        Price = UserUtils.GenerateRandomNumber(_maxPrice);
        Condition = condition;
    }

    public override Part Clone(string condition = ConditionWorking) =>
        new ExhaustManifold(condition);
}

class Bumper : Part
{
    private readonly string _name = "Бампер";
    private readonly int _maxPrice = 35;

    public Bumper(string condition = ConditionWorking)
    {
        Name = _name;
        Price = UserUtils.GenerateRandomNumber(_maxPrice);
        Condition = condition;
    }

    public override Part Clone(string condition = ConditionWorking) =>
        new Bumper(condition);
}