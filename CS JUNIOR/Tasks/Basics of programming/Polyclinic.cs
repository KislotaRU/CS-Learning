using System;

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