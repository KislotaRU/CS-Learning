using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CS_JUNIOR
{
    public class Program
    {
        static void Main()
        {
            Order order = new Order(1, 8904, "RUB");
            string key = "KEY";

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

    public interface IHashCalculator
    {
        string GetHash(string data);
    }

    public static class BuilderLink
    {
        public const string ParameterAmount = "amount";
        public const string ParameterCurrency = "currency";
        public const string ParameterHash = "hash";
        public const string SeporateParameters = "&";

        public static string Build(string baseLink, string endpointLink, IReadOnlyDictionary<string, string> parameters)
        {
            if (String.IsNullOrWhiteSpace(baseLink))
                throw new ArgumentException(nameof(baseLink));

            if (String.IsNullOrWhiteSpace(endpointLink))
                throw new ArgumentException(nameof(endpointLink));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            foreach (string parameter in parameters.Keys)
            {
                if (String.IsNullOrWhiteSpace(parameter))
                    throw new ArgumentException(nameof(parameter));
            }

            foreach (string parameter in parameters.Values)
            {
                if (String.IsNullOrWhiteSpace(parameter))
                    throw new ArgumentException(nameof(parameter));
            }

            string queryString = string.Join(SeporateParameters, parameters.Select(parameter => $"{parameter.Key}={parameter.Value}"));

            return $"{baseLink}/{endpointLink}?{queryString}";
        }
    }

    public class Order
    {
        public readonly int Id;
        public readonly int Amount;
        public readonly string Currency;

        public Order(int id, int amount, string currency)
        {
            Id = id > 0 ? id : throw new ArgumentOutOfRangeException(nameof(id));
            Amount = amount > 0 ? amount : throw new ArgumentOutOfRangeException(nameof(amount));
            Currency = String.IsNullOrWhiteSpace(currency) ? throw new ArgumentException(nameof(currency)) : currency;
        }
    }

    public class PaymentSystemWithPay : IPaymentSystem
    {
        private readonly IHashCalculator _hashCalculator;
        private readonly string _baseLink = "pay.system1.ru";
        private readonly string _endpointLink = "order";

        public PaymentSystemWithPay()
        {
            _hashCalculator = new HashCalculatorMD5();
        }

        public string GetPayingLink(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            string data = order.Id.ToString();
            string hash = _hashCalculator.GetHash(data);

            IReadOnlyDictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { BuilderLink.ParameterAmount, order.Amount.ToString() },
                { BuilderLink.ParameterHash, hash }
            };

            return BuilderLink.Build(_baseLink, _endpointLink, parameters);
        }
    }

    public class PaymentSystemWithOrder : IPaymentSystem
    {
        private readonly IHashCalculator _hashCalculator;
        private readonly string _baseLink = "order.system2.ru";
        private readonly string _endpointLink = "pay";

        public PaymentSystemWithOrder()
        {
            _hashCalculator = new HashCalculatorMD5();
        }

        public string GetPayingLink(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            string data = order.Id.ToString() + order.Amount.ToString();
            string hash = _hashCalculator.GetHash(data);

            IReadOnlyDictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { BuilderLink.ParameterHash, hash }
            };

            return BuilderLink.Build(_baseLink, _endpointLink, parameters);
        }
    }

    public class PaymentSystemWithKey : IPaymentSystem
    {
        private readonly IHashCalculator _hashCalculator;
        private readonly string _key;
        private readonly string _baseLink = "system3.com";
        private readonly string _endpointLink = "pay";

        public PaymentSystemWithKey(string key)
        {
            _key = String.IsNullOrWhiteSpace(key) ? throw new ArgumentException(nameof(key)) : key;
            _hashCalculator = new HashCalculatorSHA1();
        }

        public string GetPayingLink(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            string data = order.Amount.ToString() + order.Id.ToString() + _key;
            string hash = _hashCalculator.GetHash(data);

            IReadOnlyDictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { BuilderLink.ParameterAmount, order.Amount.ToString() },
                { BuilderLink.ParameterCurrency, order.Currency },
                { BuilderLink.ParameterHash, hash }
            };

            return BuilderLink.Build(_baseLink, _endpointLink, parameters);
        }
    }

    public class HashCalculatorMD5 : IHashCalculator
    {
        public string GetHash(string data)
        {
            if (String.IsNullOrWhiteSpace(data))
                throw new ArgumentException(nameof(data));

            MD5 md5 = MD5.Create();

            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(data));

            string hash = "";

            foreach (byte @byte in hashBytes)
            {
                hash += @byte.ToString();
            }

            return hash;
        }
    }

    public class HashCalculatorSHA1 : IHashCalculator
    {
        public string GetHash(string data)
        {
            if (String.IsNullOrWhiteSpace(data))
                throw new ArgumentException(nameof(data));

            SHA1 sha1 = SHA1.Create();

            byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(data));

            string hash = "";

            foreach (byte @byte in hashBytes)
            {
                hash += @byte.ToString();
            }

            return hash;
        }
    }
}
