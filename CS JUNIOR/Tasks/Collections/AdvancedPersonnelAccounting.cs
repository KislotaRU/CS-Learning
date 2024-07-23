using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    class AdvancedPersonnelAccounting
    {
        static void Main()
        {
            const string CommandAddDossier = "Добавить досье";
            const string CommandWithDrawAllDossiers = "Вывести все досье";
            const string CommandRemoveDossier = "Удалить досье";
            const string CommandExit = "Закрыть программу";

            List<string> fullNames = new List<string>();
            List<string> workingPositions = new List<string>();

            bool isWorking = true;
            string userInput;

            while (isWorking == true)
            {
                Console.WriteLine("\t\tВас приветствует программа <Кадровый учёт>!" +
                                  "\nНиже предсталено меню программы:" +
                                  $"\n\t{CommandAddDossier}" +
                                  $"\n\t{CommandWithDrawAllDossiers}" +
                                  $"\n\t{CommandRemoveDossier}" +
                                  $"\n\t{CommandExit}");

                Console.Write("\nВведите пунк меню: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAddDossier:
                        AddDossier(fullNames, workingPositions);
                        break;
                    case CommandWithDrawAllDossiers:
                        PrintAllDossiers(fullNames, workingPositions);
                        break;
                    case CommandRemoveDossier:
                        RemoveDossier(fullNames, workingPositions);
                        break;
                    case CommandExit:
                        Console.WriteLine("Завершение программы...");
                        isWorking = false;
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда. Попробуйте ещё.");
                        break;
                }

                Console.ReadLine();
                Console.Clear();
            }
        }

        static void AddDossier(List<string> fullNames, List<string> workingPositions)
        {
            Console.Write("Введите ФИО: ");
            fullNames.Add(Console.ReadLine());

            Console.Write("Введите Должность: ");
            workingPositions.Add(Console.ReadLine());

            Console.WriteLine("Досье записано!");
        }

        static void PrintAllDossiers(List<string> fullNames, List<string> workingPositions)
        {
            int temporaryCountRecords = 0;

            for (int i = 0; i < fullNames.Count; i++)
            {
                temporaryCountRecords++;
                Console.Write($"\n\t{temporaryCountRecords}. {fullNames[i]} - {workingPositions[i]}");
            }

            Console.WriteLine("\n\nВсе досье выведены!");
        }

        static void RemoveDossier(List<string> fullNames, List<string> workingPositions)
        {
            Console.Write("Введите индекс досье: ");

            if (int.TryParse(Console.ReadLine(), out int indexDelet) == true)
            {
                if (indexDelet <= fullNames.Count && indexDelet > 0)
                {
                    fullNames.RemoveAt(indexDelet - 1);
                    workingPositions.RemoveAt(indexDelet - 1);
                    Console.WriteLine("Досье удаленно!");
                }
                else
                {
                    Console.WriteLine("Такого индекса нет.");
                }
            }
            else
            {
                Console.WriteLine("Индекс состоит только из цифр!");
            }
        }
    }
}