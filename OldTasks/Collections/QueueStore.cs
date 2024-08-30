using System;
using System.Collections.Generic;

//У вас есть множество целых чисел. Каждое целое число - это сумма покупки.
//Вам нужно обслуживать клиентов до тех пор, пока очередь не станет пуста.
//После каждого обслуженного клиента деньги нужно добавлять на наш счёт и выводить его в консоль.
//После обслуживания каждого клиента программа ожидает нажатия любой клавиши, 
//после чего затирает консоль и по новой выводит всю информацию, только уже со следующим клиентом.

namespace CS_JUNIOR
{
    class QueueStore
    {
        static void Main()
        {
            Random random = new Random();
            int minRange = 100;
            int maxRange = 5000;
            int countClients = 15;
            int countMoney = 0;

            Queue<int> moniesClients = new Queue<int>();

            for (int i = 0; i < countClients; i++)
            {
                moniesClients.Enqueue(random.Next(minRange, maxRange));
            }

            while (moniesClients.Count > 0)
            {
                ServeQueue(moniesClients, ref countMoney);

                ClearConsole();
            }

            Console.WriteLine($"Заработано денег за смену: {countMoney}");
        }

        static void ServeQueue(Queue<int> queue, ref int countMoney)
        {
            Console.WriteLine($"Кол-во всего клиентов: {queue.Count}\n");
            Console.WriteLine("Клиент обслуживается...");
            Console.WriteLine($"Сумма покупок клиента составила: {queue.Peek()}");
            countMoney += queue.Dequeue();
            Console.WriteLine("Деньги добавлены в кассу." +
                              $"\nАктуальное кол-во денег в кассе: {countMoney}");
        }

        static void ClearConsole()
        {
            Console.ReadLine();
            Console.Clear();
        } 
    }
}