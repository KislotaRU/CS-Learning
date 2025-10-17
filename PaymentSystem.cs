using System;
using System.Security.Cryptography;

namespace CS_JUNIOR
{
    //Выведите платёжные ссылки для трёх разных систем платежа: 
    //pay.system1.ru/order?amount=12000RUB&hash={MD5 хеш ID заказа}
    //order.system2.ru/pay?hash={MD5 хеш ID заказа + сумма заказа}
    //system3.com/pay?amount=12000&curency=RUB&hash={SHA-1 хеш сумма заказа + ID заказа + секретный ключ от системы}

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
        private readonly Order _order;
        private readonly string _link = "pay.system1.ru/order?amount=12000RUB&hash=";

        public PaymentSystemWithPay(Order order)
        {
            _order = order ?? throw new ArgumentNullException(nameof(order));
        }

        public string GetPayingLink(Order order)
        {
            string data = order.Id.ToString();

            return _link + MD5.Create(data);
        }
    }

    public class PaymentSystemWithOrder : IPaymentSystem
    {
        private readonly Order _order;
        private readonly string _link = "order.system2.ru/pay?hash=";

        public PaymentSystemWithOrder(Order order)
        {
            _order = order ?? throw new ArgumentNullException(nameof(order));
        }

        public string GetPayingLink(Order order)
        {
            string data = order.Id.ToString() + order.Amount.ToString();

            return _link + MD5.Create(data);
        }
    }

    public class PaySystemWithKey : IPaymentSystem
    {
        private readonly Order _order;
        private readonly string _link = "system3.com/pay?amount=12000&curency=RUB&hash=";
        private readonly string _key = "key";

        public PaySystemWithKey(Order order)
        {
            _order = order ?? throw new ArgumentNullException(nameof(order));
        }

        public string GetPayingLink(Order order)
        {
            string data = order.Amount.ToString() + order.Id.ToString() + _key;

            return _link + SHA1.Create(data);
        }
    }
}
