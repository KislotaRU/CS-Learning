using System;
using System.Security.Cryptography;
using System.Text;

namespace CS_JUNIOR
{
    public class Program
    {
        static void Main()
        {
            Order order = new Order(1, 12000);
            string key = "key";

            IPaymentSystem paySystem1 = new PaymentSystemWithPay();
            IPaymentSystem paySystem2 = new PaymentSystemWithOrder();
            IPaymentSystem paySystem3 = new PaymentSystemWithKey(key);

            Console.WriteLine(paySystem1.GetPayingLink(order));
            Console.WriteLine(paySystem2.GetPayingLink(order));
            Console.WriteLine(paySystem3.GetPayingLink(order));
        }
    }

    public interface IPaymentSystem
    {
        string GetPayingLink(Order order);
    }

    public class Order
    {
        public readonly int Id;
        public readonly int Amount;

        public Order(int id, int amount)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            Id = id;
            Amount = amount;
        }
    }

    public class PaymentSystemWithPay : IPaymentSystem
    {
        private string _link = "pay.system1.ru/order?amount=12000RUB&hash=";

        public string GetPayingLink(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            string data = order.Id.ToString();
            string hash = GetHash(data);

            return _link + hash;
        }

        private string GetHash(string data)
        {
            if (String.IsNullOrWhiteSpace(data))
                throw new ArgumentException(nameof(data));

            MD5 md5 = MD5.Create();

            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(data));

            string hash = "";

            foreach (byte b in hashBytes)
            {
                hash += b.ToString();
            }

            return hash;
        }
    }

    public class PaymentSystemWithOrder : IPaymentSystem
    {
        private string _link = "order.system2.ru/pay?hash=";

        public string GetPayingLink(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            string data = order.Id.ToString() + order.Amount.ToString();
            string hash = GetHash(data);

            return _link + hash;
        }

        private string GetHash(string data)
        {
            if (String.IsNullOrWhiteSpace(data))
                throw new ArgumentException(nameof(data));

            MD5 md5 = MD5.Create();

            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(data));

            string hash = "";

            foreach (byte b in hashBytes)
            {
                hash += b.ToString();
            }

            return hash;
        }
    }

    public class PaymentSystemWithKey : IPaymentSystem
    {
        private readonly string _key;

        private string _link = "system3.com/pay?amount=12000&curency=RUB&hash=";

        public PaymentSystemWithKey(string key)
        {
            _key = String.IsNullOrWhiteSpace(key) ? throw new ArgumentException(nameof(key)) : key;
        }

        public string GetPayingLink(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            string data = order.Amount.ToString() + order.Id.ToString() + _key;
            string hash = GetHash(data);

            return _link + hash;
        }

        private string GetHash(string data)
        {
            if (String.IsNullOrWhiteSpace(data))
                throw new ArgumentException(nameof(data));

            SHA1 sha1 = SHA1.Create();

            byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(data));

            string hash = "";

            foreach (byte b in hashBytes)
            {
                hash += b.ToString();
            }

            return hash;
        }
    }
}
