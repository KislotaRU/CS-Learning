﻿using System;
using System.Collections.Generic;

/*
 * Вам надо создать магазин с продавцом  и покупателем.
 * Продавец имеет список своих товаров, которые может показать и продавать их. Продажа заключается в передаче покупателю товара и увеличение у себя денег.
 * Покупатель также имеет список товаров, что он купил, количество своих денег и всё это может показать.
 * Продавец может только продавать, а покупатель - только покупать.
 * В задаче понадобится использовать наследование.
*/

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
                    Trade();
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

    private void Trade()
    {
        _customer.ShowBalance();
        _trader.ShowInventory();

        if (_trader.TryGetItem(out Item item))
        {
            int moneyToPay = item.Price;

            if (_customer.CanToPay(moneyToPay))
            {
                _customer.Pay(moneyToPay);
                _trader.TakeMoney(moneyToPay);

                _trader.RemoveItem(item);
                _customer.AddItem(item);

                Console.Write("Предмет успешно оплачен и добавлен в ваш инвентарь.\n");
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
    protected readonly List<Item> Items;

    protected int Money;

    public Human(int money = 0)
    {
        Items = new List<Item>();
        Money = money;
    }

    public virtual void ShowInventory()
    {
        if (Items.Count > 0)
        {
            int numberItem = 1;

            foreach (Item item in Items)
            {
                Console.Write($"\t{numberItem}. ".PadRight(5));
                item.Show();

                numberItem++;
            }

            Console.WriteLine();
        }
        else
        {
            Console.Write("\tПусто.\n");
        }
    }
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
        Console.Write($"Кол-во ваших денег: {Money}\n\n");

    public void AddItem(Item item) =>
        Items.Add(item);

    public bool CanToPay(int moneyToPay) =>
        Money >= moneyToPay;

    public void Pay(int moneyToPay) =>
        Money -= moneyToPay > 0 ? moneyToPay : 0;
}

class Trader : Human
{
    public Trader()
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

        SetInventory(items);
    }

    public override void ShowInventory()
    {
        Console.Write("Инвентарь торговца: \n");
        base.ShowInventory();
    }

    public void RemoveItem(Item item) =>
        Items.Remove(item);

    public void TakeMoney(int money) =>
        Money += money > 0 ? money : 0;

    public bool TryGetItem(out Item foundItem)
    {
        int numberItem;

        foundItem = null;

        Console.Write("Введите номер предмета: ");
        numberItem = UserUtils.ReadInt();

        if (numberItem > 0 && numberItem <= Items.Count)
        {
            foundItem = Items[numberItem - 1];
            return true;
        }
        else
        {
            Console.Write("Под таким номером нет предмета.\n");
        }

        return false;
    }

    private void SetInventory(List<Item> items) =>
        items.ForEach(item => Items.Add(item));
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