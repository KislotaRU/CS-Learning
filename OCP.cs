using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    internal class Program
    {
        public static void Main()
        {
            var paymentSystemFactory = new PaymentSystemFactory();
            var orderForm = new OrderForm(paymentSystemFactory.SystemsId);
            var paymentHandler = new PaymentHandler();
            string systemId;
            IPaymentSystem paymentSystem;

            orderForm.ShowForm();
            systemId = orderForm.ReadForm();

            paymentSystem = paymentSystemFactory.Create(systemId);
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
        private readonly IEnumerable<string> _systemsId;

        public OrderForm(IEnumerable<string> systemsId)
        {
            if (systemsId == null)
                throw new ArgumentNullException(nameof(systemsId));

            foreach (var systemId in systemsId)
            {
                if (string.IsNullOrWhiteSpace(systemId))
                    throw new ArgumentException(nameof(systemId));
            }

            _systemsId = systemsId;
        }

        public void ShowForm()
        {
            ShowPaymentSystems();
        }

        public string ReadForm()
        {
            // Симуляция веб интерфейса.
            Console.WriteLine("Какое системой вы хотите совершить оплату?");
            return Console.ReadLine();
        }

        private void ShowPaymentSystems()
        {
            Console.WriteLine("Мы принимаем:");

            foreach (var systemId in _systemsId)
            {
                Console.Write($" {systemId},");
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

    public class PaymentSystemFactory
    {
        private readonly IReadOnlyDictionary<string, Func<IPaymentSystem>> _paymentSystems = new Dictionary<string, Func<IPaymentSystem>>()
        {
            { "QIWI", () => new PaymentSystemQIWI() },
            { "WebMoney", () => new PaymentSystemWebMoney() },
            { "Card", () => new PaymentSystemCard() },
        };

        public IEnumerable<string> SystemsId => _paymentSystems.Keys;

        public IPaymentSystem Create(string systemId)
        {
            if (string.IsNullOrEmpty(systemId))
                throw new ArgumentException(nameof(systemId));

            IPaymentSystem paymentSystem = null;

            if (_paymentSystems.TryGetValue(systemId, out var creatorPaymentSystem))
                paymentSystem = creatorPaymentSystem();

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