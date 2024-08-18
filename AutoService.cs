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
    private const string CommandOrderPart = "Заказать деталь";
    private const string CommandServeCustomer = "Обслужить клиента";
    private const string CommandExit = "Завершить работу";

    private readonly Wallet _wallet;
    private readonly Storage _storage;
    private readonly Queue<Customer> _customers;

    public AutoService()
    {
        _wallet = new Wallet();
        _storage = new Storage();
    }

    public void Work()
    {

    }
}

class Storage
{
    private readonly List<Shelf> _shelfs;

    public Storage()
    {
        _shelfs = new List<Shelf>();
    }
}

class Shelf
{
    private Part _part;
    private int _itemCount;

    public Shelf(string name, int itemCount)
    {
        _part = new Part(name);
        _itemCount = itemCount;
    }

    public void Show()
    {
        Console.Write($"Запчасть: {_part.Name} | Количество: {_itemCount}\n");
    }

    public void AddItem(Part part, int )
    {
        if (_part == null)
        {
            _part = part;
        }
    }
}

class Part
{
    public Part(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
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