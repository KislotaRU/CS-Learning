﻿using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Store store = new Store();

            Console.ForegroundColor = ConsoleColor.White;

            store.Work();
        }
    }
}

static class UserUtils
{
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

class Store
{
    private readonly Customer _customer;
    private readonly Trader _trader;

    public Store()
    {
        int moneyCustomer = 300;

        _customer = new Customer(moneyCustomer);
        _trader = new Trader();
    }

    public void Work()
    {
        const string CommandShowInventory = "Посмотреть свой инвентарь";
        const string CommandBayItem = "Купить товар";
        const string CommandExit = "Выйти";

        string[] menu = new string[]
        {
            CommandShowInventory,
            CommandBayItem,
            CommandExit
        };

        string userInput;
        bool isRunning = true;

        while (isRunning)
        {
            Console.Write("\t\tМеню Магазина\n\n");

            Console.Write("Доступные команды:\n");
            PrintMenu(menu);

            Console.Write("\nОжидается ввод: ");
            userInput = GetCommandMenu(menu);

            Console.Clear();

            switch (userInput)
            {
                case CommandShowInventory:
                    _customer.ShowInventory();
                    break;

                case CommandBayItem:
                    AddItem();
                    break;

                case CommandExit:
                    isRunning = Exit();
                    continue;

                default:
                    Console.Write("Требуется ввести номер команды или саму команду.\n\n");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    private void AddItem()
    {
        _customer.ShowBalance();
        _trader.ShowInventory();

        if (_trader.TryGetItem(out Item item))
        {
            int moneyToPay = item.Price;

            if (_customer.CanToPay(moneyToPay))
            {
                Item purchasedItem = _trader.RemoveItem(item);

                _trader.TakeMoney(moneyToPay);
                _customer.AddItem(purchasedItem);

                Console.Write("Предмет успешно оплачен и добавлен в ваш инветарь.\n");
            }
            else
            {
                Console.Write("Не хватает денег для оплаты.\n");
            }
        }
        else
        {
            Console.Write("Не удалось получить предмет.\n");
        }
    }

    private bool Exit()
    {
        Console.Write("Вы завершили работу программы.\n\n");
        return false;
    }

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

class Human
{
    protected readonly Inventory Inventory;

    protected int Money;

    public Human(int money = 0)
    {
        Inventory = new Inventory();
        Money = money;
    }

    public virtual void ShowInventory() =>
        Inventory.Show();

    public void AddItem(Item item) =>
        Inventory.AddItem(item);

    public Item RemoveItem(Item item) =>
        Inventory.RemoveItem(item);
}

class Customer : Human 
{
    public Customer(int money = 0) : base(money)
    {
        Money = money;
    }

    public override void ShowInventory()
    {
        ShowBalance();
        Console.Write("Инвентарь покупателя: \n");
        base.ShowInventory();
    }

    public void ShowBalance() =>
        Console.Write($"Кол-во ваших денег: {Money}\n");

    public bool CanToPay(int moneyToPay) =>
        Money >= moneyToPay;

    public void Pay(int moneyToPay) =>
        Money -= moneyToPay > 0 ? moneyToPay : 0;
}

class Trader : Human
{
    private readonly List<Item> _items;

    public Trader()
    {
        _items = new List<Item>()
        {
            new Item("Молоко", 45),
            new Item("Мясо", 105),
            new Item("Чай", 80),
            new Item("Конфета", 10),
            new Item("Торт", 50),
            new Item("Гречка", 20)
        };

        SetInventory();
    }

    public override void ShowInventory()
    {
        Console.Write("Инвентарь торговца: \n");
        base.ShowInventory();
    }

    public void TakeMoney(int money) =>
        Money += money > 0 ? money : 0;

    public bool TryGetItem(out Item foundItem) =>
        Inventory.TryGetItem(out foundItem);

    private void SetInventory() =>
        _items.ForEach(item => Inventory.AddItem(item));
}

class Inventory
{
    private readonly List<Item> _items;

    public Inventory()
    {
        _items = new List<Item>();
    }

    public void Show()
    {
        int numberCell = 1;

        foreach (Item item in _items)
        {
            Console.Write($"\t{numberCell++}. ".PadRight(5));
            item.Show();
        }

        Console.WriteLine();
    }

    public void AddItem(Item item) =>
        _items.Add(item);

    public Item RemoveItem(Item item) =>
        _items.Remove(item) ? item : null;

    public bool TryGetItem(out Item foundItem)
    {
        int numberItem;

        foundItem = null;

        Console.Write("Введите номер предмета: ");
        numberItem = UserUtils.ReadInt();

        if (numberItem > 0 && numberItem <= _items.Count)
        {
            foundItem = _items[numberItem - 1];
            return true;
        }
        else
        {
            Console.Write("Под таким номером нет предмета.\n");
        }

        return false;
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
        Console.Write($"Предмет: {_name}".PadRight(20) + $"Цена: {Price}\n");
}