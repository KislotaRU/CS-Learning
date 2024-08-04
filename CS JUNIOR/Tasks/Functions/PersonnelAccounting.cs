using System;

//Будет 2 массива: 1) фио 2) должность.
//Описать функцию заполнения массивов досье, функцию форматированного вывода, функцию поиска по фамилии и функцию удаления досье.
//Функция расширяет уже имеющийся массив на 1 и дописывает туда новое значение.
//Программа должна быть с меню, которое содержит пункты:
//1) добавить досье
//2) вывести все досье (в одну строку через “-” фио и должность с порядковым номером в начале)
//3) удалить досье (Массивы уменьшаются на один элемент. Нужны дополнительные проверки, чтобы не возникало ошибок)
//4) поиск по фамилии
//5) выход

namespace CS_JUNIOR
{
    class PersonnelAccounting
    {
        static void Main()
        {
            const string CommandAddDossier = "Добавить досье";
            const string CommandWithDrawAllDossiers = "Вывести все досье";
            const string CommandRemoveDossier = "Удалить досье";
            const string CommandSearchByLastName = "Поиск по фамилии";
            const string CommandExit = "Закрыть программу";

            string[] fullNames = new string[0];
            string[] workingPositions = new string[0];

            bool isWorking = true;
            string userInput;

            while (isWorking == true)
            {
                Console.WriteLine("\t\tВас приветствует программа <Кадровый учёт>!" +
                                  "\nНиже предсталено меню программы:" +
                                  $"\n\t{CommandAddDossier}" +
                                  $"\n\t{CommandWithDrawAllDossiers}" +
                                  $"\n\t{CommandRemoveDossier}" +
                                  $"\n\t{CommandSearchByLastName}" +
                                  $"\n\t{CommandExit}");

                Console.Write("\nВведите пунк меню: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAddDossier:
                        AddDossier(ref fullNames, ref workingPositions);
                        break;
                    case CommandWithDrawAllDossiers:
                        PrintAllDossiers(fullNames, workingPositions);
                        break;
                    case CommandRemoveDossier:
                        RemoveDossier(ref fullNames, ref workingPositions);
                        break;
                    case CommandSearchByLastName:
                        SearchDossierOnLastName(fullNames, workingPositions);
                        break;
                    case CommandExit:
                        Console.WriteLine("Программа завершается.");
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

        static void AddDossier(ref string[] fullNames, ref string[] workingPositions)
        {
            ExpandArray(ref fullNames);
            Console.Write("Введите ФИО: ");
            fullNames[fullNames.Length - 1] = Console.ReadLine();

            ExpandArray(ref workingPositions);
            Console.Write("Введите Должность: ");
            workingPositions[workingPositions.Length - 1] = Console.ReadLine();

            Console.WriteLine("Досье записано!");
        }

        static void PrintAllDossiers(string[] fullNames, string[] workingPositions)
        {
            int temporaryCountRecords = 0;

            for (int i = 0; i < fullNames.Length; i++)
            {
                temporaryCountRecords++;
                Console.Write($"\n\t{temporaryCountRecords}. {fullNames[i]} - {workingPositions[i]}");
            }

            Console.WriteLine("\n\nВсе досье выведены!");
        }

        static void RemoveDossier(ref string[] fullNames, ref string[] workingPositions)
        {
            int indexDelet;

            Console.Write("Введите индекс досье: ");
            indexDelet = Convert.ToInt32(Console.ReadLine());

            if ((fullNames.Length >= indexDelet) && (indexDelet > 0))
                ReduceArray(ref fullNames, indexDelet);

            if ((workingPositions.Length >= indexDelet) && (indexDelet > 0))
                ReduceArray(ref workingPositions, indexDelet);
                
            Console.WriteLine("Досье удаленно!");
        }

        static void SearchDossierOnLastName(string[] fullNames, string[] workingPositions)
        {
            string[] temporaryFullName;
            string searchLastName;
            int temporaryCountRecords = 0;
            bool isSearch = false;

            Console.Write("Введите фамилию: ");
            searchLastName = Console.ReadLine();

            for (int i = 0; i < fullNames.Length; i++)
            {
                temporaryCountRecords++;
                temporaryFullName = fullNames[i].Split(' ');

                if (temporaryFullName[0] == searchLastName)
                {
                    Console.Write($"\n\t{temporaryCountRecords}. {fullNames[i]} - {workingPositions[i]}");
                    isSearch = true;
                }
            }

            if (isSearch == true)
                Console.WriteLine($"\n\nВыведены все досье с этой фамилией - {searchLastName}");
            else
                Console.WriteLine($"\n\nНичего не найдено по запросу - {searchLastName}");
        }

        static void ExpandArray(ref string[] array)
        {
            string[] temporaryArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                temporaryArray[i] = array[i];
            }

            array = temporaryArray;
        }

        static void ReduceArray(ref string[] array, int indexDelet)
        {
            string[] temporaryArray = new string[array.Length - 1];

            for (int i = 0; i < temporaryArray.Length; i++)
            {
                if (i < indexDelet - 1)
                    temporaryArray[i] = array[i];
                else if (i >= indexDelet - 1)
                    temporaryArray[i] = array[i + 1];
            }

            array = temporaryArray;
        }
    }
}