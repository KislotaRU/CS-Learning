using System;
using System.Collections.Generic;

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
    public const int HundredPercent = 100;

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
        const string CommandUpdateData = "Обновить данные";
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
                    UpdateData();
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

    public void UpdateData()
    {
        CreateCustomer();
        Console.Write("Клиент встал в очередь.\n");
    }

    public void ServeCustomer()
    {
        if (_customers.Count > 0)
        {
            const string CommandAddItemInBasket = "Добавить предмет в корзину";
            const string CommandShowBasket = "Посмотреть корзину";
            const string CommandExecutePayBasket = "Перейти к оплате";
            const string CommandCancelBasket = "Выйти";

            string[] menu = new string[]
            {
                CommandAddItemInBasket,
                CommandShowBasket,
                CommandExecutePayBasket,
                CommandCancelBasket
            };

            string userInput;

            bool isServiced = true;

            Customer customer = _customers.Peek();

            while (isServiced)
            {
                Console.Write("\t\tМеню покупателя СуперМаркета\n\n");

                Console.Write("Доступные команды:\n");
                PrintMenu(menu);

                Console.Write("\nОжидается ввод: ");
                userInput = GetCommandMenu(menu);

                Console.Clear();

                switch (userInput)
                {
                    case CommandAddItemInBasket:
                        AddItemInBasket(customer);
                        break;

                    case CommandShowBasket:
                        ShowBasket(customer);
                        break;

                    case CommandExecutePayBasket:
                        ExecutePayBasket(customer);
                        break;

                    case CommandCancelBasket:
                        CancelBasket(customer);
                        isServiced = false;
                        continue;

                    default:
                        Console.Write("Требуется ввести номер команды или саму команду.\n\n");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }

            Console.Write("Клиент обслужен.\n");
            _customers.Dequeue();
            Console.ReadKey();
        }
        else
        {
            Console.Write("В очереди никого нет.\n" +
                          "Попробуйте обновить данные.\n");
        } 
    }

    private void AddItemInBasket(Customer customer)
    {
        int numberItem;
        int indexItem;
        int itemCount;

        ShowStorage();

        Console.Write("Выберите номер предмета: ");
        numberItem = UserUtils.ReadInt();

        indexItem = numberItem - 1;

        if (TryGetItem(indexItem, out Item foundItem))
        {
            Console.Write("Выберите кол-во предмета: ");
            itemCount = UserUtils.ReadInt();

            customer.TakeItem(foundItem, itemCount);
            Console.Write("Предмет успешно добавлен в корзину.\n");
        }
        else
        {
            Console.Write("Не удалось найти предмет.\n");
        }
    }

    private void ShowBasket(Customer customer) =>
        customer.ShowBasket();

    private void ExecutePayBasket(Customer customer)
    {
        if (customer.CellsCountInBasket > 0)
        {
            int indexItem;
            int moneyToPay;
            bool isPaid = true;

            while (isPaid)
            {
                moneyToPay = SumPurchases(customer);

                Console.Write("Требуется оплатить предметы в корзине.\n" +
                             $"К оплате: {moneyToPay}\n\n");

                if (TryTakeMoney(customer, moneyToPay))
                {
                    Console.Write("Оплата прошла успешна.\n");
                    isPaid = false;

                    customer.TransferBasketInBag();
                }
                else
                {
                    indexItem = UserUtils.GenerateRandomNumber(maxNumber: customer.CellsCountInBasket);
                    customer.TryGetItem(indexItem, out Item foundItem);

                    Console.Write("Недостаточно средст для оплаты.\n");

                    customer.PutItem(foundItem);
                    Console.Write("Клиент отложил один товар.\n");
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
        else
        {
            Console.Write("Вы ничего не взяли в корзину.\n");
        }
    }

    private int SumPurchases(Customer customer) =>
        customer.SumPurchases();

    private void CancelBasket(Customer customer)
    {
        customer.ClearBasket();
        Console.Write("Вы отложили покупки и вышли из очереди.\n\n");
    }

    private void CreateCustomer()
    {
        int minNumberMoney = 100;
        int maxNumberMoney = 201;

        int money = UserUtils.GenerateRandomNumber(minNumberMoney, maxNumberMoney);

        _customers.Enqueue(new Customer(money));
    }

    private void ShowStorage()
    {
        Console.Write("Ассортимент СуперМаркета.\n");
        _storage.Show();
    }

    private bool TryGetItem(int numberItem, out Item foundItem) =>
        _storage.TryGetItem(numberItem, out foundItem);

    private void TakeMoney(int money) =>
        _money += money > 0 ? money : 0;

    private bool TryTakeMoney(Customer customer, int moneyToPay)
    {
        if (customer.CanToPay(moneyToPay))
        {
            customer.Pay(moneyToPay);
            TakeMoney(moneyToPay);

            return true;
        }

        return false;
    }

    private void SetInventory(List<Item> items) =>
        items.ForEach(item => _storage.AddItem(item));

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

class Customer
{
    private readonly Inventory _bag;
    private readonly Inventory _basket;

    private int _money = 0;

    public Customer(int money)
    {
        _bag = new Inventory();
        _basket = new Inventory();

        _money = money;
    }

    public int CellsCountInBasket => _basket.CellCount;

    public void ShowBasket()
    {
        Console.Write("Корзина покупателя.\n");
        _basket.Show();
    }

    public void AddItem(Item item, int itemCount = 1) =>
        _bag.AddItem(item, itemCount);

    public void TakeItem(Item item, int itemCount = 1) =>
        _basket.AddItem(item, itemCount);

    public void PutItem(Item item, int itemCount = 1) =>
        _basket.RemoveItem(item, itemCount);

    public bool TryGetItem(int indexItem, out Item foundItem) =>
        _basket.TryGetItem(indexItem, out foundItem);

    public void ClearBasket() =>
        _basket.Clear();

    public int SumPurchases() =>
        _basket.CalculateCostItems();

    public void TransferBasketInBag()
    {
        List<Cell> temporaryCells;

        temporaryCells = _basket.Copy();

        foreach (Cell cell in temporaryCells)
            AddItem(cell.Item, cell.ItemCount);
    }

    public bool CanToPay(int moneyToPay) =>
        _money >= moneyToPay;

    public void Pay(int moneyToPay) =>
        _money -= moneyToPay > 0 ? moneyToPay : 0;
}

class Inventory
{
    private readonly List<Cell> _cells;

    public Inventory(List<Cell> cells)
    {
        _cells = cells;
    }

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

        if (itemCount > 0 && itemCount <= temporaryCell?.ItemCount)
            temporaryCell.DecreaseCount(itemCount);

        if (temporaryCell?.ItemCount <= 0)
            _cells.Remove(temporaryCell);
    }

    public void Clear() =>
        _cells.Clear();

    public int CalculateCostItems()
    {
        int costItems = 0;

        foreach (Cell cell in _cells)
            costItems += cell.Item.Price * cell.ItemCount;

        return costItems;
    }

    public List<Cell> Copy()
    {
        List<Cell> temporaryCells = new List<Cell>();
        
        foreach (Cell cell in _cells)
            temporaryCells.Add(cell);

        return temporaryCells;
    }

    public bool TryGetItem(int indexItem, out Item foundItem)
    {
        foundItem = null;

        if (indexItem >= 0 && indexItem < _cells.Count)
        {
            foundItem = _cells[indexItem].Item;
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
        ItemCount -= itemCount < 0 ? itemCount : 0;

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
            Console.Write($"Предмет: {_name}".PadRight(20) + $"Цена: {Price}\n");
}