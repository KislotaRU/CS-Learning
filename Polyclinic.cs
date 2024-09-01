using System;

/*
 * Пользователь вводит кол-во людей в очереди.
 * Фиксированное время приема одного человека всегда равно 10 минутам.
 * Пример ввода: Введите кол-во пациентов: 14
 * Пример вывода: "Вы должны отстоять в очереди 2 часа и 20 минут."
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            int minutesInHour = 60;

            int appointmentDurationInMinutes = 10;

            int queueTimeInMinutes;
            int hoursCount;
            int minutesCount;

            int peopleCount;

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("\tПоликлиника Новикова\n");

            Console.Write($"Вам надо попасть на приём к врачу. Приём занимает {appointmentDurationInMinutes} минут.\n");

            Console.Write("Введите кол-во людей, стоящие перед вами в очереди: ");
            peopleCount = Convert.ToInt32(Console.ReadLine());

            queueTimeInMinutes = peopleCount * appointmentDurationInMinutes;

            hoursCount = queueTimeInMinutes / minutesInHour;
            minutesCount = queueTimeInMinutes % minutesInHour;

            Console.Write($"\nВаша очередь подойдёт через кол-во часов {hoursCount} и минут {minutesCount}\n");
        }
    }
}
