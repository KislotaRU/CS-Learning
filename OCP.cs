using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    internal class Program
    {
        public static void Main()
        {
            var orderForm = new OrderForm();
            var paymentHandler = new PaymentHandler();
            var systemId = orderForm.ShowForm();

            IPaymentSystem paymentSystem = PaymentSystemFactory.Create(systemId);

            paymentHandler.ShowPaymentResult(paymentSystem);
        }
    }

    public interface IPaymentSystem
    {
        string SystemId { get; }

        void ShowPaymentResult();
    }

    public class OrderForm
    {
        public string ShowForm()
        {
            ShowPaymentSystems();

            // Симуляция веб интерфейса.
            Console.WriteLine("Какое системой вы хотите совершить оплату?");
            return Console.ReadLine();
        }

        private void ShowPaymentSystems()
        {
            Console.WriteLine("Мы принимаем: ");

            foreach (var key in PaymentSystemFactory.PaymentSystems.Keys)
            {
                Console.Write($"{key}, ");
            }
        }
    }

    public class PaymentHandler
    {
        public void ShowPaymentResult(IPaymentSystem paymentSystem)
        {
            if (paymentSystem == null)
                throw new ArgumentNullException(nameof(paymentSystem));

            Console.WriteLine($"Вы оплатили с помощью {paymentSystem.SystemId}");

            paymentSystem.ShowPaymentResult();

            Console.WriteLine("Оплата прошла успешно!");
        }
    }

    public static class PaymentSystemFactory
    {
        public static readonly IReadOnlyDictionary<string, IPaymentSystem> PaymentSystems = new Dictionary<string, IPaymentSystem>()
        {
            { "QIWI", new PaymentSystemQIWI() },
            { "WebMoney", new PaymentSystemWebMoney() },
            { "Card", new PaymentSystemCard() },
        };

        public static IPaymentSystem Create(string systemId)
        {
            if (string.IsNullOrEmpty(systemId))
                throw new ArgumentException(nameof(systemId));

            IPaymentSystem paymentSystem = null;

            foreach (var key in PaymentSystems.Keys)
            {
                if (key == systemId)
                {
                    paymentSystem = PaymentSystems[key];
                    break;
                }
            }

            if (paymentSystem == null)
                throw new ArgumentNullException(nameof(paymentSystem));

            return paymentSystem;
        }
    }

    public abstract class PaymentSystem : IPaymentSystem
    {
        public string SystemId { get; protected set; }

        public void ShowPaymentResult()
        {
            Console.WriteLine($"Проверка платежа через {SystemId}...");
        }
    }

    public class PaymentSystemQIWI : PaymentSystem
    {
        private readonly string _systemId = "QIWI";

        public PaymentSystemQIWI()
        {
            SystemId = _systemId;
        }
    }

    public class PaymentSystemWebMoney : PaymentSystem
    {
        private readonly string _systemId = "WebMoney";

        public PaymentSystemWebMoney()
        {
            SystemId = _systemId;
        }
    }

    public class PaymentSystemCard : PaymentSystem
    {
        private readonly string _systemId = "Card";

        public PaymentSystemCard()
        {
            SystemId = _systemId;
        }
    }
}