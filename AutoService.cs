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

    private int _balance = 0;
    private readonly Storage _storage;

    public AutoService()
    {
        _balance = 1000;
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

        _storage.Show();
    }

    private void ShowBalance()
    {
        Console.Write($"\nКол-во денег на счету Автосервиса: {_balance}$\n");
    }

    private void ServeCustomer()
    {

    }

    private void OrderPart()
    {
        int countToPay;

        ShowStorage();
        ShowBalance();

        if (_balance > 0)
        {
            if (_storage.TryOrderPart(out Shelf shelf, out int partCount))
            {
                countToPay = shelf.PartPrice * partCount;

                Console.Write($"Сумма заказа составляет: {countToPay}$\n");

                if (TryToPay(countToPay))
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

    private bool TryToPay(int countToPay)
    {
        if (_balance >= countToPay)
        {
            CountToPay = countToPay;
            return true;
        }
        else
        {
            CountToPay = 0;
            return false;
        }
    }

    private void ToPay()
    {
        _balance -= CountToPay;
    }
}

class Storage
{
    private readonly List<Shelf> _shelfs;

    public Storage()
    {
        _shelfs = new List<Shelf>()
        {
            new Shelf(new Part("Трансмиссия", 80), 0),
            new Shelf(new Part("Колесо", 25), 0),
            new Shelf(new Part("Лобовое стекло", 40), 0),
            new Shelf(new Part("Аккумулятор", 15), 0),
            new Shelf(new Part("Свеча зажигания", 10), 0),
            new Shelf(new Part("Моторное масло", 20), 0),
            new Shelf(new Part("Выхлопной коллектор", 30), 0),
            new Shelf(new Part("Бампер", 35), 0)
        };
    }

    public void Show()
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
}

class Shelf
{
    private readonly int _maxCountPart = 10;
    private readonly Part _part;

    private int _partCount;

    public Shelf(Part part, int itemCount)
    {
        _part = part;
        _partCount = itemCount;
    }

    public string PartName { get { return _part.Name; } }
    public int PartPrice { get { return _part.Price; } }

    public void Show()
    {
        Console.Write($"{_partCount}/{_maxCountPart} Цена: {PartPrice}$ {PartName}\n");
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

class Part
{
    public Part(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public string Name { get; private set; }
    public int Price { get; private set; }
}

class Customer
{
    private readonly Wallet _wallet;

    public Customer()
    {
        _wallet = new Wallet();
    }
}

class Wallet
{

}

class Car
{

}