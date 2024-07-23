using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    class Store
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

        protected Inventory _inventory;

        public Human()
        {
            _inventory = new Inventory();

            Coins = DefaultCoins;
            Level = DefaultLevel;
        }

        public int Coins { get; protected set; }
        public int Level { get; protected set; }

        public virtual void ShowInventory()
        {
            _inventory.Show();
        }

        public virtual void ShowCoins()
        {
            Console.WriteLine(Coins);
        }

        public virtual void ShowLevel()
        {
            Console.WriteLine(Level);
        }

        public virtual void AddItem(Item item)
        {
            _inventory.AddItem(item);
        }

        public virtual void RemoveItem(Item item)
        {
            _inventory.RemoveItem(item);
        }

        public virtual void TakeCoins(int coins)
        {
            Coins += coins;
        }

        public virtual void TakeAwayCoins(int coins)
        {
            Coins -= coins;
        }

        public int ToPay(Trader trader)
        {
            Coins -= trader.CoinsToPay;

            return trader.CoinsToPay;
        }
    }

    class Player : Human
    {
        private const int CoinsPlayer = 500;
        private const int LevelPlayer = 10;

        public Player()
        {
            Coins = CoinsPlayer;
            Level = LevelPlayer;
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

        public override void AddItem(Item item)
        {
            base.AddItem(item);
            Console.Write("Предмет добавлен в инвентарь игрока.");
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

        public int CoinsToPay { get; private set; }
        public int LevelToPay { get; private set; }

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
            return _inventory.TryGetItem(out item);
        }

        public bool CheckSolvency(Player player, Item item)
        {
            CoinsToPay = item.Price;

            if (player.Coins >= CoinsToPay)
            {
                return true;
            }
            else
            {
                CoinsToPay = 0;
                return false;
            }
        }

        public bool CheckLevel(Player player, Item item)
        {
            LevelToPay = item.Level;

            if (player.Level >= LevelToPay)
            {
                return true;
            }
            else
            {
                LevelToPay = 0;
                return false;
            }
        }

        private void SetInventory()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                _inventory.AddItem(_items[i]);
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
        private const string CommandShowInventoryPlayer = "Player";
        private const string CommandShowInventoryTrader = "Trader";
        private const string CommandBuyItem = "Buy";
        private const string CommandLeave = "Leave";

        private Player _player = new Player();
        private Trader _trader = new Trader();

        public void Work()
        {
            string userInput;

            bool isWorking = true;

            while (isWorking == true)
            {
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
                if (_trader.CheckSolvency(_player, temporaryItem) == true)
                {
                    if (_trader.CheckLevel(_player, temporaryItem) == true)
                    {
                        _player.AddItem(temporaryItem);
                        _trader.RemoveItem(temporaryItem);

                        _trader.TakeCoins(_player.ToPay(_trader));
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
            Console.WriteLine("\t\t\t==============|Лавка Старца|==============");

            Console.Write("Возможные опции:" +
                          $"\n\t> {CommandShowInventoryPlayer} - Показать Ваш (Игрока) инвентарь." +
                          $"\n\t> {CommandShowInventoryTrader} - Показать инвентарь Продавца." +
                          $"\n\t> {CommandBuyItem} -  Купить предмет." +
                          $"\n\t> {CommandLeave} - Покинуть лавку.\n\n");

            _player.ShowLevel();
            _player.ShowCoins();
            _trader.ShowCoins();
        }
    }
}