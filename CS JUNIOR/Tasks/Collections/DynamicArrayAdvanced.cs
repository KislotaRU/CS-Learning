using System;
using System.Collections.Generic;

//В массивах вы выполняли задание "Динамический массив"
//Используя всё изученное, напишите улучшенную версию динамического массива(не обязательно брать своё старое решение)
//Задание нужно, чтобы вы освоились с List и прощупали его преимущество.
//Проверка на ввод числа обязательна.
//Пользователь вводит числа, и программа их запоминает.
//Как только пользователь введёт команду sum, программа выведет сумму всех веденных чисел.
//Выход из программы должен происходить только в том случае, если пользователь введет команду exit.

namespace CS_JUNIOR
{
    class DynamicArrayAdvanced
    {
        static void Main()
        {
            const string Sum = "sum";
            const string Exit = "exit";

            List<int> numbers = new List<int>();

            string userInput;
            bool isOpen = true;

            while (isOpen == true)
            {
                Console.WriteLine("Программа записывает числа, которые вы введёте.");
                Console.WriteLine("Доступные команды: " +
                                  $"\n\t{Sum} - возвращает сумму всех чисел в массиве." +
                                  $"\n\t{Exit} - завершает работу программы.");

                Console.Write("\nВведите число или команду: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case Sum:
                        SumNumbers(numbers);
                        break;
                    case Exit:
                        isOpen = false;
                        break;
                    default:
                        AddNumber(numbers, userInput);
                        break;
                }

                Console.WriteLine("\nПродолжить...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void AddNumber(List<int> list, string number)
        {
            if (int.TryParse(number, out int result) == true)
            {
                list.Add(result);
                Console.WriteLine("Число записано в массив.");
            }
            else
            {
                Console.WriteLine("Неизвестная команда.");
            }
        }

        static void SumNumbers(List<int> list)
        {
            int sum = 0;

            foreach (int number in list)
            {
                sum += number;
            }

            Console.WriteLine($"\nСумма всех чисел в массиве = {sum}");
        }
    }
}