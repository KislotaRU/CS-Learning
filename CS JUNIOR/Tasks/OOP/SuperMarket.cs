using System;
using System.Collections.Generic;

//Написать программу администрирования супермаркетом.
//В супермаркете есть очередь клиентов.
//У каждого клиента в корзине есть товары, также у клиентов есть деньги.
//Клиент, когда подходит на кассу, получает итоговую сумму покупки и старается её оплатить.
//Если оплатить клиент не может, то он рандомный товар из корзины выкидывает до тех пор, пока его денег не хватит для оплаты.
//Клиентов можно делать ограниченное число на старте программы.
//Супермаркет содержит список товаров, из которых клиент выбирает товары для покупки.

namespace CS_JUNIOR
{
    class SuperMarket
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Supermarket supermarket = new Supermarket();

            supermarket.Start();
        }
    }

    abstract class Human
    {
        protected Inventory _inventory;

        protected const int LengthFooter = 40;
        protected const char FooterSymbol = '-';

        protected string _userInput = null;

        public Human(int coinsCount, Inventory inventory = null)
        {
            Coins = coinsCount;
            _inventory = inventory;
        }

        public int Coins { get; private set; }
        public int CoinsToPay { get; protected set; }

        public virtual void ShowBalance()
        {
            Console.Write($"Баланс: {Coins}");
        }

        public virtual void ShowInventory()
        {
            _inventory.Show();
        }

        public virtual void AddItem(Item item)
        {
            _inventory.AddItem(item);
        }

        public virtual Item RemoveItem()
        {
            return _inventory.TakeItem();
        }

        public virtual void TakeCoins(int takeCoins)
        {
            if (takeCoins > 0)
                Coins += takeCoins;
        }

        public virtual void TakeAwayCoins(int takeAwayCoins)
        {
            if (takeAwayCoins > 0)
                Coins -= takeAwayCoins;
        }

        public virtual int ToPay(Human human)
        {
            Coins -= human.CoinsToPay;
            return human.CoinsToPay;
        }

        internal void CheckCommandMenu(string[] arrayMenu, out string userInput)
        {
            userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int result))
                if ((result > 0) && (result <= arrayMenu.Length))
                    userInput = arrayMenu[result - 1];
        }

        internal void PrintMenu(string[] arrayMenu)
        {
            Console.Write("\t\tДоступные команды:\n\n");

            for (int i = 0; i < arrayMenu.Length; i++)
                Console.Write($"\t{i + 1}. {arrayMenu[i]}\n");

            Console.WriteLine();
            Console.WriteLine(new string(FooterSymbol, LengthFooter));
            Console.Write("Ожидается ввод: ");
        }
    }

    class Customer : Human
    {
        private const string CommandSelectProduct = "Выбрать товар";
        private const string CommandRemoveProduct = "Убрать товар";
        private const string CommandShowBasket = "Показать корзину";
        private const string CommandFinishSelection = "Подойти к кассе";

        private readonly string[] CustomerMenu = new string[]
        {
            CommandSelectProduct,
            CommandRemoveProduct,
            CommandShowBasket,
            CommandFinishSelection
        };

        private Random _random;

        private int _maxRandomNumber = 500;
        private int _minRandomNumber = 200;

        private bool _isFinishPurchase = false;
        private bool _isFinishSelectItem = true;

        public Customer(int coinsCount = 0, Inventory inventory = null) : base(coinsCount, inventory)
        {
            coinsCount = GetRandomCoinCount();
            _inventory = new Inventory();
        }

        public void StartPurchase()
        {
            _isFinishPurchase = false;

            if (_isFinishSelectItem == false)
                MenuPurchase();
            else
                MainMenu();
        }

        public override void ShowBalance()
        {
            Console.Write("Показать баланс клиента.\n");
            base.ShowBalance();
        }

        public override void ShowInventory()
        {
            Console.Write("\nКорзина клиента:\n");

            if (_inventory.ItemsCount > 0)
                base.ShowInventory();
            else
                Console.Write("В корзине пусто.\n");
        }

        public bool GetStatusPurchase()
        {
            return _isFinishPurchase;
        }

        public bool GetStatusSelectItem()
        {
            return _isFinishSelectItem;
        }

        private void SelectProduct()
        {
            _isFinishSelectItem = false;
        }

        private void RemoveProduct()
        {

        }

        private void FinishSelection()
        {
            _isFinishPurchase = true;
            Console.Write("Клиент закончил с выбором продуктов.");
        }

        private int GetRandomCoinCount()
        {
            _random = new Random();

            return _random.Next(_minRandomNumber, _maxRandomNumber);
        }

        private void MainMenu()
        {
            PrintMenu(CustomerMenu);

            CheckCommandMenu(CustomerMenu, out _userInput);

            switch (_userInput)
            {
                case CommandSelectProduct:
                    SelectProduct();
                    break;

                case CommandRemoveProduct:
                    RemoveProduct();
                    break;

                case CommandShowBasket:
                    ShowInventory();
                    break;

                case CommandFinishSelection:
                    FinishSelection();
                    break;

                default:
                    Console.Write("Неизвестная команда!");
                    break;
            }
        }

        private void MenuPurchase()
        {
            Console.WriteLine();
            Console.WriteLine(new string(FooterSymbol, LengthFooter));

            //_inventory.AddItem();
        }
    }

    class Cashier : Human
    {
        private const string CommandServeQueue = "Обслужить очередь";
        private const string CommandAddCustomer = "Добавить клиента";
        private const string CommandBreakWork = "Прервать работу";

        private readonly List<Item> _assortment = new List<Item>()
        {
            new Item("Яблоки", 21, 5),
            new Item("Бананы", 13, 4),
            new Item("Йогурт", 38, 5),
            new Item("Сметана", 13, 9),
            new Item("Майонез", 19, 9),
            new Item("Шоколад", 74, 2),
            new Item("Молоко", 45, 6),
            new Item("Печенье", 47, 7),
            new Item("Зубная паста", 13, 10),
            new Item("Хлопья", 24, 9),
            new Item("Сок", 90, 3),
            new Item("Конфеты", 10, 10),
        };

        private readonly string[] CashierMenu = new string[]
        {
            CommandServeQueue,
            CommandAddCustomer,
            CommandBreakWork
        };

        private CustomerLine _customerLine;
        private Customer _customer;

        private int _customerCount = 1;

        private bool _isOpenShop = true;
        private bool _isAddNewCustomer = false;
        private bool _isFinishSelectItem = true;

        public Cashier(int coinCount, Inventory inventory = null) : base(coinCount, inventory)
        {
            _inventory = new Inventory(_assortment);
            _customerLine = new CustomerLine();
        }

        public void Start()
        {
            _isOpenShop = true;

            while (_isOpenShop == true)
            {
                PrintMenu(CashierMenu);

                CheckCommandMenu(CashierMenu, out _userInput);

                switch (_userInput)
                {
                    case CommandServeQueue:
                        ServeQueue();
                        break;

                    case CommandAddCustomer:
                        AddCustomer();
                        break;

                    case CommandBreakWork:
                        BreakWork();
                        break;

                    default:
                        Console.Write("Неизвестная команда!");
                        break;
                }

                Console.ReadLine();
                Console.Clear();
            }
        }

        public override void ShowInventory()
        {
            Console.Write("Актуальный ассортимент супермаркета.\n\n");
            base.ShowInventory();
        }

        public bool CheckSolvency(Customer customer, int price)
        {
            if (customer.Coins >= price)
            {
                CoinsToPay = price;
                return true;
            }
            else
            {
                CoinsToPay = 0;
                return false;
            }
        }

        public void ServeQueue()
        {
            Console.Clear();

            Console.Write("\t\tИнтерфейс обслуживания очереди.\n\n");
            Console.Write($"Кол-во клиентов в очереди: {_customerLine.CustomerCount}\n");

            if (_customerLine.TryGetCustomer() == true)
            {
                Console.Write($"Клиент #{_customerCount++}.\n");

                _customer = _customerLine.RemoveCustomer();
                _customer.ShowInventory();

                //...
            }
            else
            {
                Console.Write("Очередь пуста!\n" +
                              "Добавьте клиентов.\n");
            }
        }

        public void AddCustomer()
        {
            Item temporaryItem;

            _customer = new Customer();

            do
            {
                Console.ReadLine();
                Console.Clear();

                ShowInventory();

                _customer.StartPurchase();

                _isAddNewCustomer = _customer.GetStatusPurchase();
                _isFinishSelectItem = _customer.GetStatusSelectItem();

                if (_isFinishSelectItem == false)
                {
                    temporaryItem = _inventory.TakeItem();
                    _customer.AddItem(temporaryItem);
                }


            }
            while (_isAddNewCustomer == false);

            _isAddNewCustomer = false;
        }

        public void BreakWork()
        {
            _isOpenShop = false;
            Console.Write("Вы прервали работу кассиром и вышли в главное меню программы.\n");
        }
    }

    class Inventory
    {
        private const int DefaultItemsCount = 0;

        private const int LengthFooter = 55;
        private const char FooterSymbol = '-';

        private List<Item> _items;

        private string _userInput = "";

        public Inventory(List<Item> items = null)
        {
            _items = items;
            ItemsCount = items?.Count ?? DefaultItemsCount;
        }

        public int ItemsCount { get; private set; }

        public void AddItem(Item item)
        {
            Item temporaryItem;

            if (item != null)
            {
                if (TryGetItem(out temporaryItem, (int)item?.Id) == true)
                {
                    if (temporaryItem.TrySetIncrementCount(item.Count) == true)
                    {
                        temporaryItem.IncrementCount();
                    }
                }
                else
                {
                    temporaryItem = new Item(item.Id, item.Name, item.Price);

                    if (temporaryItem.TrySetIncrementCount(item.Count) == true)
                    {
                        temporaryItem.IncrementCount();
                        _items.Add(temporaryItem);
                    }
                    else
                    {
                        temporaryItem = null;
                    }
                }

                if (temporaryItem == null)
                    Console.Write($"Не удалось добавить продукт.\n");
                else
                    Console.Write("Команда успешно выполнена.\n");
            }
        }

        public Item TakeItem()
        {
            if (TryGetItem(out Item item) == true)
            {
                if (item.TrySetDecrementCount() == true)
                {
                    item.DecrementCount();

                    if (item.Count == 0)
                        _items.Remove(item);

                    Console.Write("Команда успешно выполнена.\n");
                    return item;
                }
            }

            Console.Write($"Не удалось удалить продукт.\n");
            return null;
        }

        public void Show()
        {
            Console.Write($"\tID".PadRight(5) + $"\tНазвание".PadRight(20) + $"Цена".PadRight(10) + $"Кол-во" + "\n");

            foreach (Item item in _items)
                item.Show();

            Console.WriteLine();
            Console.WriteLine(new string(FooterSymbol, LengthFooter));
        }

        private bool TryGetItem(out Item item, int id = 0)
        {
            item = null;

            if (id == 0)
            {
                do
                {
                    Console.Write("Введите ID продукта: ");
                    _userInput = Console.ReadLine();
                }
                while (int.TryParse(_userInput, out id) == false);
            }

            foreach (Item temporaryItem in _items)
            {
                if (temporaryItem.Id == id)
                {
                    item = temporaryItem;
                    return true;
                }
            }

            if (item == null)
                Console.Write($"Такого продукта нет.");

            return false;
        }
    }

    class CustomerLine
    {
        private const int CustomerMaxCount = 3;
        private const int CustomerMinCount = 0;

        private Queue<Customer> _customers;

        public CustomerLine()
        {
            CustomerCount = CustomerMinCount;
        }

        public int CustomerCount { get; private set; }

        public void AddCustomer(Customer customer)
        {
            if (CustomerCount < CustomerMaxCount)
            {
                CustomerCount++;
                _customers.Enqueue(customer);
            }
        }

        public Customer RemoveCustomer()
        {
            if (CustomerCount > 0)
            {
                CustomerCount--;
                return _customers.Dequeue();
            }

            return null;
        }

        public bool TryGetCustomer()
        {
            if (_customers?.Count > 0)
                return true;
            else
                return false;
        }
    }

    class Item
    {
        private static int _id = 1;

        private int _decrementNumber = 0;
        private int _incrementNumber = 0;

        private bool _isDecrementCorrect = false;
        private bool _isIncrementCorrect = false;

        private string _userInput = "";

        public Item(string name, int price, int count)
        {
            Name = name;
            Price = price;
            Count = count;

            Id = _id++;
        }

        public Item(int id, string name, int price, int count = 0)
        {
            Name = name;
            Price = price;
            Id = id;
            Count = count;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Price { get; private set; }
        public int Count { get; private set; }

        public void Show()
        {
            Console.Write($"\t{Id}".PadRight(5) + $"\t{Name}".PadRight(20) + $"{Price}".PadRight(10) + $"{Count}" + "\n");
        }

        public void DecrementCount()
        {
            if (_isDecrementCorrect == true)
            {
                _isDecrementCorrect = false;
                Count -= _decrementNumber;
                _incrementNumber = _decrementNumber;
            }
        }

        public void IncrementCount()
        {
            if (_isIncrementCorrect == true)
            {
                _isIncrementCorrect = false;
                Count += _incrementNumber;
            }
        }

        public bool TrySetDecrementCount(int decrementNumber = 0)
        {
            if (Count > 0)
            {
                if (decrementNumber == 0)
                {
                    do
                    {
                        Console.Write("Введите кол-во данного товара, которое хотите убрать.");
                        _userInput = Console.ReadLine();
                    }
                    while (int.TryParse(_userInput, out decrementNumber));
                }

                if (decrementNumber <= Count)
                {
                    _decrementNumber = decrementNumber;
                    _isDecrementCorrect = true;

                    return true;
                }
                else
                {
                    Console.Write("Вы хотите убрать несуществующие кол-во товара.\n");
                }

            }
            else
            {
                Console.Write("Кол-во такого товара закончилось.\n");
            }

            return false;
        }

        public bool TrySetIncrementCount(int allQuantity, int incrementNumber = 0)
        {
            if (incrementNumber == 0)
            {
                do
                {
                    Console.Write("Введите кол-во данного товара, которое хотите взять.");
                    _userInput = Console.ReadLine();
                }
                while (int.TryParse(_userInput, out incrementNumber));
            }

            if (allQuantity >= incrementNumber)
            {
                _incrementNumber = incrementNumber;
                _isIncrementCorrect = true;

                return true;
            }
            else
            {
                Console.Write("Вы хотите взять несуществующие кол-во товара.\n");
            }

            return false;
        }
    }

    class Supermarket
    {
        private const string CommandOpenShop = "Открыть магазин";
        private const string CommandShowProducts = "Показать ассортимент";
        private const string CommandCloseProgram = "Завершить программу";
        private const string CommandBackMainMenu = "Вернуться в главное меню";

        private readonly string[] MainMenu = new string[]
        {
            CommandOpenShop,
            CommandShowProducts,
            CommandCloseProgram
        };

        private readonly string[] MenuViewingProducts = new string[]
        {
            CommandBackMainMenu
        };

        private Cashier _cashier;

        private int coinsCount = 0;

        private string _userInput;
        private bool _isWorking = true;
        private bool _isViewingProducts = true;

        public Supermarket()
        {
            _cashier = new Cashier(coinsCount);
        }

        public void ShowProducts()
        {
            _isViewingProducts = true;

            while (_isViewingProducts == true)
            {
                Console.Clear();

                _cashier.ShowInventory();

                _cashier.PrintMenu(MenuViewingProducts);

                _cashier.CheckCommandMenu(MenuViewingProducts, out _userInput);

                switch (_userInput)
                {
                    case CommandBackMainMenu:
                        _isViewingProducts = false;
                        break;

                    default:
                        PrintMessage();
                        break;
                }
            }

            Console.Clear();
        }

        public void Start()
        {
            PrintMessage();

            while (_isWorking == true)
            {
                _cashier.PrintMenu(MainMenu);

                _cashier.CheckCommandMenu(MainMenu, out _userInput);

                switch (_userInput)
                {
                    case CommandOpenShop:
                        OpenShop();
                        break;

                    case CommandShowProducts:
                        ShowProducts();
                        break;

                    case CommandCloseProgram:
                        CloseProgram();
                        break;

                    default:
                        PrintMessage();
                        break;
                }
            }
        }

        private void OpenShop()
        {
            PrintMessage();

            _cashier.Start();
        }

        private void CloseProgram()
        {
            _isWorking = false;
            Console.Write("Вы завершили работу программы.\n");
        }

        private void PrintMessage()
        {
            Console.Clear();

            if (_userInput == null)
            {
                Console.Write("\t\tДобро пожаловать в программу супермаркета \"Los Pollos Hermanos!\"\n\n" +
                              "\tПрограмма загружена и готова к работе.\n" +
                              "\tДля навигации в программе можно использовать цифры.\n\n" +
                              "Для продолжения нажмите любую кнопку...\n");
            }
            else if (_userInput == CommandOpenShop)
            {
                Console.Write("\t\tМагазин открыт.\n\n" +
                              "\tВы открыли магазин и начали появляться первые клиенты.\n" +
                              "\tДля администрирования очереди воспользуйтесь следующим меню.\n\n" +
                              "Для продолжения нажмите любую кнопку...\n");
            }
            else
            {
                Console.Write("Неизвестная команда!");
            }

            Console.ReadLine();
            Console.Clear();
        }
    }
}