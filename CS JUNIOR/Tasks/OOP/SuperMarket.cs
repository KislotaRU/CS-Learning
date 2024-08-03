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
    class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Supermarket supermarket = new Supermarket();

            supermarket.Run();
        }
    }

    class Inventory
    {
        private const int LengthFooter = 55;
        private const char FooterSymbol = '-';

        private readonly List<Cell> _cells;

        public Inventory()
        {
            _cells = new List<Cell>();
        }

        public Inventory(Dictionary<Item, int> assortment)
        {
            _cells = new List<Cell>();

            ReadCells(assortment);
        }

        public int CellsCount { get { return _cells.Count; } }

        public void AddCell(Cell cell)
        {
            if (cell != null)
            {
                if (TryGetCell(out Cell temporaryCell, cell.IdItem) == true)
                {
                    if (temporaryCell.TrySetCountItem(cell.TemporaryCountItem) == true)
                        temporaryCell.IncrementCount();
                }
                else
                {
                    temporaryCell = new Cell(cell);

                    _cells.Add(temporaryCell);
                }

                Console.Write("Команда выполнена успешна.\n");
            }
            else
            {
                Console.Write($"Не удалось выполнить команду.\n");
            }
        }

        public Cell GetCell()
        {
            if (TryGetCell(out Cell cell) == true)
            {
                if (cell.TrySetCountItem() == true)
                {
                    cell.DecrementCount();

                    if (cell.CountItem == 0)
                    {
                        _cells.Remove(cell);
                    }

                    return cell;
                }
            }

            return null;
        }

        public void Show()
        {
            Console.Write($"\tId".PadRight(5) + $"\tНазвание".PadRight(20) + $"Цена".PadRight(10) + $"Кол-во" + "\n");

            foreach (Cell cell in _cells)
                cell.Show();

            Console.WriteLine();
            Console.WriteLine(new string(FooterSymbol, LengthFooter));
        }

        private bool TryGetCell(out Cell cell, int id = 0)
        {
            string _userInput;

            cell = null;

            if (id == 0)
            {
                do
                {
                    Console.Write("Введите Id продукта: ");
                    _userInput = Console.ReadLine();
                }
                while (int.TryParse(_userInput, out id) == false);
            }

            foreach (Cell temporaryCell in _cells)
            {
                if (temporaryCell.IdItem == id)
                {
                    cell = temporaryCell;
                    return true;
                }
            }

            return false;
        }

        private void ReadCells(Dictionary<Item, int> assortment)
        {
            foreach (Item item in assortment.Keys)
                _cells.Add(new Cell(item, assortment[item]));
        }
    }

    class Cell
    {
        private readonly Item _item;

        public Cell(Item item, int countItem)
        {
            _item = item;
            CountItem = countItem;
        }

        public Cell(Cell cell)
        {
            _item = new Item(cell.IdItem, cell._item.Name, cell.PriceItem);
            CountItem = cell.TemporaryCountItem;
        }

        public int CountItem { get; private set; }
        public int PriceItem { get { return _item.Price; } }
        public int IdItem { get { return _item.Id; } }
        public int TemporaryCountItem { get; private set; }

        public void Show()
        {
            _item.Show(CountItem);
        }

        public void DecrementCount()
        {
            CountItem -= TemporaryCountItem;
        }

        public void IncrementCount()
        {
            CountItem += TemporaryCountItem;
            TemporaryCountItem = 0;
        }

        public bool TrySetCountItem(int temporaryCountItem = 0)
        {
            string _userInput;

            if (CountItem > 0)
            {
                if (temporaryCountItem == 0)
                {
                    do
                    {
                        Console.Write("Введите кол-во данного товара, которое хотите взять: ");
                        _userInput = Console.ReadLine();
                    }
                    while (int.TryParse(_userInput, out temporaryCountItem) == false);
                }

                if (temporaryCountItem <= CountItem)
                {
                    TemporaryCountItem = temporaryCountItem;

                    return true;
                }
                else
                {
                    Console.Write("Вы хотите выбрать несуществующие кол-во товара.\n");
                }
            }
            else
            {
                Console.Write("Кол-во этого товара закончилось.\n");
            }

            return false;
        }
    }

    class Item
    {
        private static int s_id = 1;

        public Item(string name, int price)
        {
            Name = name;
            Price = price;

            Id = s_id++;
        }

        public Item(int id, string name, int price)
        {
            Name = name;
            Price = price;
            Id = id;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Price { get; private set; }

        public void Show(int count = 0)
        {
            Console.Write($"\t{Id}".PadRight(5) + $"\t{Name}".PadRight(20) + $"{Price}".PadRight(10) + $"{count}" + "\n");
        }
    }

    class Customer
    {
        public const string CommandSelectProduct = "Выбрать товар";
        public const string CommandRemoveProduct = "Убрать товар";
        public const string CommandShowBasket = "Показать корзину";
        public const string CommandFinishSelection = "Подойти к кассе";

        private const int LengthFooter = 40;
        private const char FooterSymbol = '-';

        private readonly Inventory _inventory;

        private readonly string[] _customerMenu = new string[]
        {
            CommandSelectProduct,
            CommandRemoveProduct,
            CommandShowBasket,
            CommandFinishSelection
        };

        public Customer()
        {
            Coins = GetRandomCountCoins();
            _inventory = new Inventory();
        }

        public string UserInput { get; private set; }
        public int Coins { get; private set; }
        public int CoinsToPay { get; private set; }
        public bool IsBusyInventory { get { return _inventory.CellsCount > 0; } }
        public bool IsFinishPurchase { get; private set; }

        public void PurchaseProducts()
        {
            IsFinishPurchase = false;

            PrintMenu(_customerMenu);

            ReadInput(_customerMenu);

            switch (UserInput)
            {
                case CommandSelectProduct:
                    break;

                case CommandRemoveProduct:
                    break;

                case CommandShowBasket:
                    ShowInventory();
                    break;

                case CommandFinishSelection:
                    FinishSelection();
                    break;

                default:
                    Console.Write("Неизвестная команда!");
                    Console.ReadKey();
                    break;
            }
        }

        public void ShowInventory()
        {
            Console.Clear();

            Console.Write("Корзина клиента:\n\n");

            if (IsBusyInventory == true)
                _inventory.Show();
            else
                Console.Write("В корзине пусто.\n");
        }

        public void ResetUserInput()
        {
            UserInput = null;
        }

        public bool TryPay(int totalAmountCoins)
        {
            if (Coins >= totalAmountCoins)
            {
                CoinsToPay = totalAmountCoins;
                return true;
            }
            else
            {
                CoinsToPay = 0;
                return false;
            }
        }

        public int ToPay()
        {
            Coins -= CoinsToPay;
            return CoinsToPay;
        }

        public void AddCell(Cell cell)
        {
            _inventory.AddCell(cell);
        }

        public Cell GetCell()
        {
            return _inventory.GetCell();
        }

        private void FinishSelection()
        {
            IsFinishPurchase = true;

            if (IsBusyInventory == true)
                Console.Write("Клиент закончил с выбором продуктов.\n");
            else
                Console.Write("Клиент ничего не выбрал.\n");
        }

        private int GetRandomCountCoins()
        {
            Random _random = new Random();

            int _maxRandomNumber = 150;
            int _minRandomNumber = 70;

            return _random.Next(_minRandomNumber, _maxRandomNumber);
        }

        private void ReadInput(string[] arrayMenu)
        {
            UserInput = Console.ReadLine();

            if (int.TryParse(UserInput, out int result))
                if ((result > 0) && (result <= arrayMenu.Length))
                    UserInput = arrayMenu[result - 1];
        }

        private void PrintMenu(string[] arrayMenu)
        {
            Console.Write("\t\tДоступные команды:\n\n");

            for (int i = 0; i < arrayMenu.Length; i++)
                Console.Write($"\t{i + 1}. {arrayMenu[i]}\n");

            Console.WriteLine();
            Console.WriteLine(new string(FooterSymbol, LengthFooter));
            Console.Write("Ожидается ввод: ");
        }
    }

    class CustomersLine
    {
        private const int MaxCountSlots = 3;

        private readonly Queue<Customer> _slots;

        public CustomersLine()
        {
            _slots = new Queue<Customer>();
        }

        public int Count { get { return _slots.Count; } }
        public bool IsBusy { get { return _slots?.Count > 0; } }
        public bool IsFull { get { return Count >= MaxCountSlots; } }

        public void AddCustomer(Customer customer)
        {
            if (IsFull == false)
                _slots.Enqueue(customer);
        }

        public Customer GetCustomer()
        {
            if (IsBusy == true)
                return _slots.Dequeue();

            return null;
        }
    }

    class Cashier
    {
        private readonly Inventory _inventory;

        public Cashier(Dictionary<Item, int> assortment)
        {
            _inventory = new Inventory(assortment);
        }

        public int Coins { get; private set; }
        public int CoinsToPay { get; private set; }
        public int CountServedCustomers { get; private set; }

        public void ShowInventory()
        {
            Console.Write("Актуальный ассортимент супермаркета.\n\n");
            _inventory.Show();
        }

        public void ShowStatus()
        {
            Console.Write($"Актуальная выручка кассира: {Coins}\n" +
                          $"Кол-во обслуженных клиентов: {CountServedCustomers}\n");
        }

        public void ServeCustomer(Customer customer)
        {
            do
            {
                customer.ShowInventory();

                Console.Write($"Итоговая сумма к оплате: {CoinsToPay}\n\n");

                if (customer.TryPay(CoinsToPay))
                {
                    TakeCoins(customer.ToPay());
                    Console.Write("Клиент оплатил чек.\n");

                    CountServedCustomers++;
                    CoinsToPay = 0;
                }
                else
                {
                    Console.Write("Недостаточно денег для оплаты.\n" +
                                  "Клиент должен выложить из корзины какой-то товар.\n");

                    Console.ReadKey();

                    _inventory.AddCell(customer.GetCell());

                    if (CoinsToPay <= 0)
                        Console.Write("\nКлиент выложил все товары из своей карзины.\n" +
                                      "Клиент ушёл.\n");
                }

                Console.ReadKey();
                Console.Clear();
            }
            while (CoinsToPay > 0);
        }

        public void AddCell(Cell cell)
        {
            _inventory.AddCell(cell);

            if (cell != null)
                CoinsToPay -= cell.TemporaryCountItem * cell.PriceItem;
        }

        public Cell GetCell()
        {
            Cell temporaryCell = _inventory.GetCell();

            if (temporaryCell != null)
                CoinsToPay += temporaryCell.TemporaryCountItem * temporaryCell.PriceItem;

            return temporaryCell;
        }

        private void TakeCoins(int takeCoins)
        {
            if (takeCoins > 0)
                Coins += takeCoins;
        }
    }

    class Supermarket
    {
        private const string CommandServeQueue = "Обслужить очередь";
        private const string CommandAddCustomer = "Добавить клиента";
        private const string CommandShowStatus = "Показать статус";
        private const string CommandCloseProgram = "Завершить программу";

        private const int LengthFooter = 40;
        private const char FooterSymbol = '-';

        private readonly Dictionary<Item, int> _assortment;
        private readonly Cashier _cashier;
        private readonly CustomersLine _customerLine;

        private readonly string[] _mainMenu = new string[]
        {
            CommandServeQueue,
            CommandAddCustomer,
            CommandShowStatus,
            CommandCloseProgram
        };

        private int _numberCustomer = 1;

        private bool _isWorking = true;

        public Supermarket()
        {
            _assortment = new Dictionary<Item, int>()
            {
                { new Item("Яблоки", 21), 5 },
                { new Item("Бананы", 13), 4 },
                { new Item("Йогурт", 38), 5 },
                { new Item("Сметана", 13), 9 },
                { new Item("Майонез", 19), 9 },
                { new Item("Шоколад", 74), 2 },
                { new Item("Молоко", 45), 6 },
                { new Item("Печенье", 47), 7 },
                { new Item("Зубная паста", 13), 10 },
                { new Item("Хлопья", 24), 9 },
                { new Item("Сок", 90), 3 },
                { new Item("Конфеты", 10), 10 },
            };

            _cashier = new Cashier(_assortment);
            _customerLine = new CustomersLine();
        }

        public string UserInput { get; private set; }

        public void Run()
        {
            Console.Write("\t\tДобро пожаловать в программу супермаркета \"Los Pollos Hermanos!\"\n\n" +
                          "\tПрограмма загружена и готова к работе.\n" +
                          "\tДля навигации в программе ипользуйте цифры.\n\n" +
                          "Для продолжения нажмите любую кнопку...\n");

            Console.ReadKey();
            Console.Clear();

            while (_isWorking == true)
            {
                PrintMenu(_mainMenu);

                ReadInput(_mainMenu);

                switch (UserInput)
                {
                    case CommandServeQueue:
                        ServeQueue();
                        break;

                    case CommandAddCustomer:
                        AddCustomer();
                        break;

                    case CommandShowStatus:
                        ShowStatus();
                        break;

                    case CommandCloseProgram:
                        CloseProgram();
                        break;

                    default:
                        Console.Write("Неизвестная команда!");
                        break;
                }

                if (UserInput != CommandCloseProgram)
                {
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private void ShowStatus()
        {
            Console.Clear();

            _cashier.ShowInventory();
            _cashier.ShowStatus();
        }

        private void AddCustomer()
        {
            Customer temporaryCustomer;
            Cell temporaryCell;

            if (_customerLine.IsFull == true)
            {
                Console.Write("Очередь полностью заполнена!\n");
            }
            else
            {
                temporaryCustomer = new Customer();

                do
                {
                    Console.Clear();

                    Console.Write("\t\tИнтерфейс добавления клиента.\n\n");

                    _cashier.ShowInventory();

                    if (temporaryCustomer.UserInput == Customer.CommandSelectProduct)
                    {
                        temporaryCell = _cashier.GetCell();

                        temporaryCustomer.AddCell(temporaryCell);

                        temporaryCustomer.ResetUserInput();
                        Console.ReadKey();
                    }
                    else if (temporaryCustomer.UserInput == Customer.CommandRemoveProduct)
                    {
                        temporaryCustomer.ShowInventory();

                        if (temporaryCustomer.IsBusyInventory == true)
                        {
                            temporaryCell = temporaryCustomer.GetCell();

                            _cashier.AddCell(temporaryCell);
                        }

                        temporaryCustomer.ResetUserInput();
                        Console.ReadKey();
                    }
                    else
                    {
                        temporaryCustomer.PurchaseProducts();

                        if (temporaryCustomer.UserInput == Customer.CommandShowBasket)
                            Console.ReadKey();
                    }
                }
                while (temporaryCustomer.IsFinishPurchase == false);

                if (temporaryCustomer.IsBusyInventory == true)
                    _customerLine.AddCustomer(temporaryCustomer);
            }
        }

        private void ServeQueue()
        {
            while (_customerLine.IsBusy == true)
            {
                Console.Clear();

                Console.Write("\t\tИнтерфейс обслуживания очереди.\n\n");
                Console.Write($"Кол-во клиентов в очереди: {_customerLine.Count}\n");
                Console.Write($"Клиент #{_numberCustomer}.\n");
                Console.ReadKey();

                _cashier.ServeCustomer(_customerLine.GetCustomer());
                _numberCustomer++;
            }

            Console.Write("Очередь пуста!\n" +
                          "Добавьте новых клиентов.\n");
        }

        private void CloseProgram()
        {
            _isWorking = false;
            Console.Write("Вы завершили работу программы.\n");
        }

        private void ReadInput(string[] arrayMenu)
        {
            UserInput = Console.ReadLine();

            if (int.TryParse(UserInput, out int result))
                if ((result > 0) && (result <= arrayMenu.Length))
                    UserInput = arrayMenu[result - 1];
        }

        private void PrintMenu(string[] arrayMenu)
        {
            Console.Write("\tДоступные команды:\n\n");

            for (int i = 0; i < arrayMenu.Length; i++)
                Console.Write($"\t{i + 1}. {arrayMenu[i]}\n");

            Console.WriteLine();
            Console.WriteLine(new string(FooterSymbol, LengthFooter));
            Console.Write("Ожидается ввод: ");
        }
    }
}