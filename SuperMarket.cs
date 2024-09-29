using System;
using System.Collections.Generic;

/*
 * Написать программу администрирования супермаркетом.
 * Супермаркет содержит список товаров, которые он продает, очередь клиентов, которых надо обслужить и количество денег, которые заработаны.
 * Список товаров у супермаркета не уменьшаем, считаем их бесконечное количество.
 * Очередь клиентов можно задавать сразу, так и добавлять по необходимости.
 * Но при обслуживании одного клиента, он удаляется из очереди.
 * У клиента есть деньги, корзина и сумка. В корзине все товары, что не куплены, а в сумке все купленные.
 * При обслуживании клиента проверяется, может ли он оплатить товар, то есть сравнивается итоговая сумма покупки и количество денег.
 * Если оплатить клиент не может, то он случайный товар из корзины выкидывает до тех пор, пока его денег не хватит для оплаты.
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            SuperMarket superMarket = new SuperMarket();

            Console.ForegroundColor = ConsoleColor.White;

            superMarket.Work();
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

class SuperMarket
{
    private readonly Inventory _storage;
    private readonly Queue<Customer> _customers;

    private int _money = 0;

    public SuperMarket()
    {
        List<Item> items = new List<Item>()
        {
            new Item("Молоко", 45),
            new Item("Мясо", 105),
            new Item("Чай", 80),
            new Item("Конфета", 10),
            new Item("Торт", 50),
            new Item("Гречка", 20)
        };

        _storage = new Inventory();
        _customers = new Queue<Customer>();

        SetInventory(items);
    }

    public void Work()
    {
        const string CommandUpdateData = "Создать клиента";
        const string CommandServeCustomer = "Обслужить клиента";
        const string CommandExit = "Выйти";

        string[] menu = new string[]
        {
            CommandUpdateData,
            CommandServeCustomer,
            CommandExit
        };

        string userInput;
        bool isRunning = true;

        while (isRunning)
        {
            Console.Write("\t\tМеню управления СуперМаркета\n\n");

            Console.Write("Доступные команды:\n");
            PrintMenu(menu);

            Console.Write("\nОжидается ввод: ");
            userInput = GetCommandMenu(menu);

            Console.Clear();

            switch (userInput)
            {
                case CommandUpdateData:
                    CreateCustomer();
                    break;

                case CommandServeCustomer:
                    ServeCustomer();
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

    private void CreateCustomer()
    {
        int minNumberMoney = 100;
        int maxNumberMoney = 201;

        int money = UserUtils.GenerateRandomNumber(minNumberMoney, maxNumberMoney);

        _customers.Enqueue(new Customer(money));
        Console.Write("Клиент встал в очередь.\n");
    }

    public void ServeCustomer()
    {
        if (_customers.Count > 0)
        {
            Customer customer = _customers.Peek();

            int moneyToPay = customer.Work(_storage);

            TakeMoney(moneyToPay);
            Console.Write("Клиент обслужен.\n");
            
            _customers.Dequeue();
        }
        else
        {
            Console.Write("В очереди никого нет.\n" +
                          "Попробуйте обновить данные.\n");
        } 
    }

    private void TakeMoney(int money) =>
        _money += money > 0 ? money : 0;

    private void SetInventory(List<Item> items) =>
        items.ForEach(item => _storage.AddItem(item));

    private void PrintMenu(string[] menu)
    {
        int numberCommand = 1;

        for (int i = 0; i < menu.Length; i++)
        {
            Console.Write($"\t{numberCommand}. {menu[i]}\n");
            numberCommand++;
        }
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

class Customer
{
    private readonly Inventory _cart;

    private int _money = 0;
    private bool _isShopping = true;

    public Customer(int money)
    {
        _cart = new Inventory();

        _money = money;
    }

    public int CellsCountInCart => _cart.CellCount;

    public int Work(Inventory storage)
    {
        const string CommandAddItemInCart = "Добавить предмет в корзину";
        const string CommandRemoveItemInCart = "Убрать предмет из корзины";
        const string CommandShowCart = "Посмотреть корзину";
        const string CommandExecutePayCart = "Перейти к оплате";

        string[] menu = new string[]
        {
                CommandAddItemInCart,
                CommandRemoveItemInCart,
                CommandShowCart,
                CommandExecutePayCart
        };

        string userInput;

        int moneyToPay = 0;

        while (_isShopping)
        {
            Console.Write("\t\tМеню покупателя СуперМаркета\n\n");

            Console.Write("Доступные команды:\n");
            PrintMenu(menu);

            Console.Write("\nОжидается ввод: ");
            userInput = GetCommandMenu(menu);

            Console.Clear();

            switch (userInput)
            {
                case CommandAddItemInCart:
                    AddItemInCart(storage);
                    break;

                case CommandRemoveItemInCart:
                    RemoveItemInCart();
                    break;

                case CommandShowCart:
                    ShowCart();
                    break;

                case CommandExecutePayCart:
                    moneyToPay = ExecutePayCart();
                    break;

                default:
                    Console.Write("Требуется ввести номер команды или саму команду.\n\n");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }

        return moneyToPay;
    }

    private void AddItemInCart(Inventory storage)
    {
        int itemCount;

        storage.Show();

        if (storage.TryGetItem(out Item foundItem))
        {
            Console.Write("Выберите кол-во предмета: ");
            itemCount = UserUtils.ReadInt();

            _cart.AddItem(foundItem, itemCount);
            Console.Write("Предмет успешно добавлен в корзину.\n");
        }
        else
        {
            Console.Write("Не удалось найти предмет.\n");
        }
    }

    private int ExecutePayCart()
    {
        int moneyToPay = _cart.CalculateCostItems();

        Console.Write($"К оплате: {moneyToPay}$\n\n");

        Console.Write($"Кол-во ваших денег: {_money}$\n");

        if (CanToPay(moneyToPay))
        {
            Pay(moneyToPay);
            Console.Write("Оплата прошла успешна.\n");
            _isShopping = false;
        }
        else
        {
            Console.Write("Недостаточно средст для оплаты.\n");
            Console.Write("Необходимо убрать часть предметов.\n");
        }

        return moneyToPay;
    }

    private void RemoveItemInCart()
    {
        int itemCount;

        ShowCart();

        if (CellsCountInCart > 0)
        {
            if (_cart.TryGetItem(out Item foundItem))
            {
                Console.Write("Выберите кол-во предмета: ");
                itemCount = UserUtils.ReadInt();

                _cart.RemoveItem(foundItem, itemCount);
            }
            else
            {
                Console.Write("Не удалось убрать предмет.\n");
            }
        } 
    }

    private void ShowCart()
    {
        Console.Write("Корзина покупателя.\n");
        _cart.Show();
    }

    private bool CanToPay(int moneyToPay) =>
        _money >= moneyToPay;

    private void Pay(int moneyToPay) =>
        _money -= moneyToPay > 0 ? moneyToPay : 0;

    private void PrintMenu(string[] menu)
    {
        int numberCommand = 1;

        for (int i = 0; i < menu.Length; i++)
        {
            Console.Write($"\t{numberCommand}. {menu[i]}\n");
            numberCommand++;
        }
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

class Inventory
{
    private readonly List<Cell> _cells;

    public Inventory()
    {
        _cells = new List<Cell>();
    }

    public int CellCount => _cells.Count;

    public void Show()
    {
        int numberItem = 1;

        Console.Write("Предметы:\n");

        if (CellCount > 0)
        {
            foreach (Cell cell in _cells)
            {
                Console.Write($"\t{numberItem}. ".PadRight(4));
                cell.Show();

                numberItem++;
            }
        }
        else
        {
            Console.Write("\tПусто\n");
        }

        Console.WriteLine();
    }

    public void AddItem(Item item, int itemCount = 1)
    {
        Cell temporaryCell = null; 

        foreach (Cell cell in _cells)
        {
            if (cell.Item == item)
                temporaryCell = cell;    
        }

        if (temporaryCell == null)
        {
            temporaryCell = new Cell(item, itemCount);
            _cells.Add(temporaryCell);
        }
        else
        {
            temporaryCell.IncreaseCount(itemCount);
        }
    }

    public void RemoveItem(Item item, int itemCount = 1)
    {
        Cell temporaryCell = null;

        foreach (Cell cell in _cells)
        {
            if (cell.Item == item)
                temporaryCell = cell;
        }

        if (temporaryCell != null)
        {
            if (itemCount > 0 && itemCount <= temporaryCell.ItemCount)
                temporaryCell.DecreaseCount(itemCount);
            else
                Console.Write("Вы хотите убрать больше, чем это возможно.\n");

            if (temporaryCell.ItemCount <= 0)
                _cells.Remove(temporaryCell);
        }
    }

    public int CalculateCostItems()
    {
        int costItems = 0;

        foreach (Cell cell in _cells)
            costItems += cell.Item.Price * cell.ItemCount;

        return costItems;
    }

    public bool TryGetItem(out Item foundItem)
    {
        int numberItem;

        foundItem = null;

        Console.Write("Выберите номер предмета: ");
        numberItem = UserUtils.ReadInt();

        if (numberItem > 0 && numberItem <= _cells.Count)
        {
            foundItem = _cells[numberItem - 1].Item;
            return true;
        }
        else
        {
            Console.Write("Под таким номером нет предмета.\n");
            return false;
        }
    }
}

class Cell
{
    public Cell(Item item, int itemCount = 1)
    {
        Item = item;
        ItemCount = itemCount;
    }

    public Item Item { get; private set; }
    public int ItemCount { get; private set; }

    public void Show()
    {
        Console.Write($"Кол-во: x{ItemCount}".PadRight(12));
        Item.Show();
    }

    public void IncreaseCount(int itemCount) =>
        ItemCount += itemCount > 0 ? itemCount : 0;

    public void DecreaseCount(int itemCount)
    {
        ItemCount -= itemCount > 0 ? itemCount : 0;

        if (ItemCount < 0)
            ItemCount = 0;
    }
}

class Item
{
    private readonly string _name;

    public Item(string name, int price)
    {
        _name = name;
        Price = price;
    }

    public int Price { get; private set; }

    public void Show() =>
            Console.Write($"Предмет: {_name}".PadRight(20) + $"Цена: {Price}$\n");
}