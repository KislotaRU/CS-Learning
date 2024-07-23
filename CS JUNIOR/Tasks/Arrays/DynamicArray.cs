using System;

namespace CS_JUNIOR
{
    class DynamicArray
    {
        static void Main()
        {
            const string Sum = "sum";
            const string Exit = "exit";

            string userInput;
            int[] array = new int[0];
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
                        int sum = 0;

                        for (int i = 0; i < array.Length; i++)
                        {
                            sum += array[i];
                        }

                        Console.WriteLine($"\nСумма всех чисел в массиве = {sum}");
                        break;
                    case Exit:
                        isOpen = false;
                        break;
                    default:
                        if (int.TryParse(userInput, out int result) == true)
                        {
                            int[] tempArray = new int[array.Length + 1];

                            for (int i = 0; i < array.Length; i++)
                            {
                                tempArray[i] = array[i];
                            }

                            array = tempArray;
                            array[array.Length - 1] = result;

                            Console.WriteLine("Число записано в массив.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Неизвестная команда.");
                        }

                        break;
                }

                Console.WriteLine("\nПродолжить...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}