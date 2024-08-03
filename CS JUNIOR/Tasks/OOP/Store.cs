using System;
using System.Collections.Generic;

//Существует продавец, он имеет у себя список товаров, и при нужде, может вам его показать, 
//также продавец может продать вам товар. 
//После продажи товар переходит к вам, и вы можете также посмотреть свои вещи.
//Возможные классы – игрок, продавец, товар.
//Вы можете сделать так, как вы видите это.

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Shop shop = new Shop();

            shop.Work();
        }
    }

    abstract class Human
    {
        private const int DefaultCoins = 0;
        private const int DefaultLevel = 1;

        protected Inventory Inventory;

        public Human()
        {
            Inventory = new Inventory();

            Coins = DefaultCoins;
            Level = DefaultLevel;
        }

        public int Coins { get; protected set; }
        public int Level { get; protected set; }

        public virtual void ShowInventory()
        {
            Inventory.Show();
        }

        public virtual void ShowCoins()
        {
            Console.WriteLine(Coins);
        }

        public virtual void ShowLevel()
        {
            Console.WriteLine(Level);
        }
    }

    class Player : Human
    {
        public Player(int coins, int level)
        {
            Coins = coins;
            Level = level;
        }

        public override void ShowInventory()
        {
            Console.Write("Инвентарь игрока.\n");
            base.ShowInventory();
        }

        public override void ShowCoins()
        {
            Console.Write("Баланс игрока: ");
            base.ShowCoins();
        }

        public override void ShowLevel()
        {
            Console.Write("Уровень игрока: ");
            base.ShowLevel();
        }

        public bool CanPay(int price)
        {
            return Coins >= price;
        }

        public void Buy(Item item)
        {
            AddItem(item);
            Pay(item.Price);
        }

        private void AddItem(Item item)
        {
            Inventory.AddItem(item);
            Console.Write("Предмет добавлен в инвентарь игрока.");
        }

        private void Pay(int price)
        {
            Coins -= price;
        }
    }

    class Trader : Human
    {
        private List<Item> _items = new List<Item>() { new Item("Малое зелье маны.", 15, 1),
                                                       new Item("Мощное зелье здоровья.", 50, 3),
                                                       new Item("Змеиный лук.", 344, 5),
                                                       new Item("Железный меч.", 100, 1),
                                                       new Item("Медальон Жрицы Тьмы.", 888, 18),
                                                       new Item("Стрела Бога Зевса.", 60, 10),
                                                       new Item("Латунные доспехи.", 278, 5) };

        public Trader()
        {
            SetInventory();
        }

        public override void ShowInventory()
        {
            Console.Write("Инвентарь торговца.\n");
            base.ShowInventory();
        }

        public override void ShowCoins()
        {
            Console.Write("Баланс торговца: ");
            base.ShowCoins();
        }

        public bool TryGetItem(out Item item)
        {
            return Inventory.TryGetItem(out item);
        }

        public bool IsLevelConformity(int playerLevel, int itemLevel)
        {
            return playerLevel >= itemLevel;
        }

        public void Sell(Item item)
        {
            RemoveItem(item);
            TakeCoins(item.Price);
        }

        private void TakeCoins(int coins)
        {
            if (coins >= 0)
                Coins += coins;
        }

        private void RemoveItem(Item item)
        {
            Inventory.RemoveItem(item);
        }

        private void SetInventory()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                Inventory.AddItem(_items[i]);
            }
        }
    }

    class Inventory
    {
        private List<Item> _items = new List<Item>();

        public void Show()
        {
            if (_items.Count > 0)
            {
                foreach (Item item in _items)
                {
                    item.ShowInfo();
                }
            }
            else
            {
                Console.Write("Инвентарь пуст.");
            }
        }

        public bool TryGetItem(out Item item)
        {
            string userInput;

            int id;

            item = null;

            Console.Write("Введите Id предмета: ");

            do
            {
                userInput = Console.ReadLine();
            }
            while (int.TryParse(userInput, out id) == false);

            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Id == id)
                {
                    item = _items[i];

                    Console.WriteLine("Предмет в наличии.");

                    item.ShowInfo();

                    return true;
                }
            }

            Console.Write("Предмет не найден.");

            return false;
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
        }
    }

    class Item
    {
        private static int _id = 0;

        public Item(string name, int price, int level)
        {
            Id = ++_id;
            Name = name;
            Price = price;
            Level = level;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Price { get; private set; }
        public int Level { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{Id} - Id | Товар - {Name} | Цена - {Price} | Уровень - {Level}.");
        }
    }

    class Shop
    {
        private Player _player = new Player(500, 10);
        private Trader _trader = new Trader();

        public int CoinsToPay { get; private set; }

        public void Work()
        {
            const string CommandShowInventoryPlayer = "Player";
            const string CommandShowInventoryTrader = "Trader";
            const string CommandBuyItem = "Buy";
            const string CommandLeave = "Leave";

            string userInput;

            bool isWorking = true;

            while (isWorking == true)
            {
                Console.WriteLine("\t\t\t==============|Лавка Старца|==============");

                Console.Write("Возможные опции:" +
                              $"\n\t> {CommandShowInventoryPlayer} - Показать Ваш (Игрока) инвентарь." +
                              $"\n\t> {CommandShowInventoryTrader} - Показать инвентарь Продавца." +
                              $"\n\t> {CommandBuyItem} -  Купить предмет." +
                              $"\n\t> {CommandLeave} - Покинуть лавку.\n\n");

                ShowInfo();

                Console.Write("\nВведите команду: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandShowInventoryPlayer:
                        _player.ShowInventory();
                        break;

                    case CommandShowInventoryTrader:
                        _trader.ShowInventory();
                        break;

                    case CommandBuyItem:
                        Trade();
                        break;

                    case CommandLeave:
                        isWorking = false;
                        break;

                    default:
                        Console.Write("Ошибка!");
                        break;
                }

                if (isWorking == false)
                    break;

                Console.ReadLine();
                Console.Clear();
            }
        }

        private void Trade()
        {
            _trader.ShowInventory();
            Console.WriteLine();

            if (_trader.TryGetItem(out Item temporaryItem))
            {
                if (_player.CanPay(temporaryItem.Price) == true)
                {
                    if (_trader.IsLevelConformity(_player.Level, temporaryItem.Level) == true)
                    {
                        _player.Buy(temporaryItem);
                        _trader.Sell(temporaryItem);
                    }
                    else
                    {
                        Console.Write("Недостаточно высокий уровень.\nОтмена покупки.");
                    }
                }
                else
                {
                    Console.Write("Недостаточно средств.\nОтмена покупки.");
                }
            }
        }

        private void ShowInfo()
        {
            _player.ShowLevel();
            _player.ShowCoins();
            _trader.ShowCoins();
        }
    }
}