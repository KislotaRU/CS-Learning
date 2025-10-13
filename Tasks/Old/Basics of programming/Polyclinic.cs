using System;

//Легенда:
//Вы заходите в поликлинику и видите огромную очередь из старушек, вам нужно рассчитать время ожидания в очереди.
//Формально:
//Пользователь вводит кол-во людей в очереди.
//Фиксированное время приема одного человека всегда равно 10 минутам.
//Пример ввода: Введите кол-во старушек: 14
//Пример вывода: "Вы должны отстоять в очереди 2 часа и 20 минут."

namespace CS_JUNIOR
{
    class Polyclinic
    {
        static void Main(string[] args)
        {
            int peopleQueue;
            int timeQueue;
            int timeQueueHour;
            int timeQueueMinutes;
            int timeService = 10;
            int minutesInHour = 60;

            Console.WriteLine("Введите кол-во людей перед Вами: ");
            peopleQueue = Convert.ToInt32(Console.ReadLine());

            timeQueue = peopleQueue * timeService;

            timeQueueHour = timeQueue / minutesInHour;
            timeQueueMinutes = timeQueue % minutesInHour;

            Console.WriteLine($"\nВы отстоите в очереди {timeQueueHour} часа и " +
                              $"{timeQueueMinutes} минут.");
        }
    }
}