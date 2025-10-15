using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    public class Program
    {
        static void Main()
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);

            shop.Show(); //Вывод всех товаров на складе с их остатком

            Cart cart = shop.Cart();
            cart.Add(iPhone12, 4);
            //cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

            cart.Show(); //Вывод всех товаров в корзине

            Console.WriteLine(cart.Order().Paylink);

            cart.Add(iPhone12, 9); //Ошибка, после заказа со склада убираются заказанные товары
        }
    }

    public interface IWarehouse
    {
        void ReduceCount(Cell cell);
        int GetCountGood(Good good);
    }

    public class Shop
    {
        private readonly Warehouse _warehouse;

        public Shop(Warehouse warehouse)
        {
            _warehouse = warehouse ?? throw new ArgumentNullException(nameof(warehouse));
        }

        public Cart Cart() =>
            new Cart(_warehouse);

        public void Show()
        {
            Console.WriteLine("Ассортимент магазина");
            _warehouse.Show();
        }
    }

    public class Warehouse : IWarehouse
    {
        private readonly List<Cell> _cells;

        public Warehouse()
        {
            _cells = new List<Cell>();
        }

        public void Show()
        {
            char separator = '-';
            int countSymbol = 15;

            Console.WriteLine("Склад:");

            foreach (Cell cell in _cells)
                cell.Show();

            Console.WriteLine($"{new string(separator, countSymbol)}\n");
        }

        public void Delive(Good good, int count)
        {
            if (good == null)
                throw new ArgumentNullException(nameof(good));

            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Cell newCell = new Cell(good, count);
            int indexCell = _cells.FindIndex(cell => cell.Good == good);

            if (indexCell < 0)
                _cells.Add(newCell);
            else
                _cells[indexCell].Merge(newCell);
        }

        public void ReduceCount(Cell cell)
        {
            int index;

            if (cell == null)
                throw new ArgumentNullException(nameof(cell));

            index = _cells.FindIndex(temporaryCell => temporaryCell.Good == cell.Good);

            if (index < 0)
                throw new IndexOutOfRangeException(nameof(index));

            _cells[index].Unmerge(cell);

            if (_cells[index].Count == 0)
                _cells.RemoveAt(index);
        }

        public int GetCountGood(Good good)
        {
            int index;

            if (good == null)
                throw new ArgumentNullException(nameof(good));

            index = _cells.FindIndex(temporaryCell => temporaryCell.Good == good);

            if (index < 0)
                throw new IndexOutOfRangeException(nameof(index));

            return _cells[index].Count;
        }
    }

    public class Cart
    {
        private readonly IWarehouse _warehouse;
        private readonly List<Cell> _cells;

        public Cart(IWarehouse warehouse)
        {
            _warehouse = warehouse ?? throw new ArgumentNullException(nameof(warehouse));
            _cells = new List<Cell>();
        }

        public void Show()
        {
            char separator = '-';
            int countSymbol = 15;

            Console.WriteLine("Корзина:");

            foreach (Cell cell in _cells)
                cell.Show();

            Console.WriteLine($"{new string(separator, countSymbol)}\n");
        }

        public void Add(Good good, int count)
        {
            Cell newCell;
            int index;
            int availableCount;
            int totalCount;

            if (good == null)
                throw new ArgumentNullException(nameof(good));

            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            index = _cells.FindIndex(cell => cell.Good == good);
            availableCount = _warehouse.GetCountGood(good);
            totalCount = index < 0 ? count : count + _cells[index].Count;

            if (availableCount < totalCount)
                throw new InvalidOperationException($"Доступно {availableCount}, запрашивается {totalCount}");

            newCell = new Cell(good, count);

            if (index < 0)
                _cells.Add(newCell);
            else
                _cells[index].Merge(newCell);
        }

        public Order Order()
        {
            foreach (Cell cell in _cells)
            {
                _warehouse.ReduceCount(cell);
                cell.Unmerge(cell);
            }

            _cells.Clear();

            return new Order();
        }
    }

    public class Cell
    {
        public Cell(Good good, int count)
        {
            Good = good ?? throw new ArgumentNullException(nameof(good));
            Count = count > 0 ? count : throw new ArgumentOutOfRangeException(nameof(count));
        }

        public Good Good { get; private set; }
        public int Count { get; private set; }

        public void Show() =>
            Console.WriteLine($"Товар: {Good.Name}; Кол-во: {Count}.");

        public void Merge(Cell newCell)
        {
            if (newCell == null)
                throw new ArgumentNullException(nameof(newCell));

            if (newCell.Good != Good)
                throw new InvalidOperationException(nameof(Good));

            Count = Math.Max(Count + newCell.Count, 0);
        }

        public void Unmerge(Cell newCell)
        {
            if (newCell == null)
                throw new ArgumentNullException(nameof(newCell));

            if (newCell.Good != Good)
                throw new InvalidOperationException(nameof(Good));

            Count = Math.Max(Count - newCell.Count, 0);
        }
    }

    public class Good
    {
        public Good(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }
    }

    public class Order
    {
        public Order()
        {
            Paylink = "*Ссылка для оплаты*";
        }

        public string Paylink { get; }
    }
}