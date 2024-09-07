using System;

namespace CS_JUNIOR
{
    class Program
    {
        static string[] persons = new string[]
        {
                "Макарова Татьяна Долгих",
                "Иванов Иван Иванович",
                "Бикеев Александр Павлович",
                "Крюкова Ольга Петровна",
        };

        static string[] posts = new string[]
        {
                "Оператор",
                "Работник",
                "Начальник",
                "Редактор"
        };

        static void Main()
        {
            const string CommandAddDossier = "Добавить";
            const string CommandShowDossier = "Показать";
            const string CommandRemoveDossier = "Удалить";
            const string CommandSearchDossier = "Поиск";
            const string CommandExit = "Выйти";

            string userInput = null;

            while (userInput != CommandExit)
            {
                Console.Write("Программа \"Кадровый учёт\"\n\n");

                Console.Write("Доступные команды:\n" +
                             $"\t{CommandAddDossier}".PadRight(10) + "\tДобавляет новое досье.\n" +
                             $"\t{CommandShowDossier}".PadRight(10) + "\tПоказывает все досье.\n" +
                             $"\t{CommandRemoveDossier}".PadRight(10) + "\tУдаляет старое досье.\n" +
                             $"\t{CommandSearchDossier}".PadRight(10) + "\tПоиск досье по фамилии.\n" +
                             $"\t{CommandExit}".PadRight(10) + "\tЗавершает работу программы.\n\n");

                Console.Write("Ожидается ввод: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAddDossier:
                        AddDossier();
                        break;

                    case CommandShowDossier:
                        ShowDossier();
                        break;

                    case CommandRemoveDossier:
                        RemoveDossier();
                        break;

                    case CommandSearchDossier:
                        SearchDossier();
                        break;

                    case CommandExit:
                        Console.Write("Вы завершили работу программы.\n");
                        continue;

                    default:
                        Console.Write("Требуется ввести доступную команду.\n");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        static void AddDossier()
        {
            Console.Clear();

            string lastName;
            string name;
            string middleName;

            string fullName;

            string post;

            Console.Write("Введите Фамилию: ");
            lastName = Console.ReadLine();

            Console.Write("Введите Имя: ");
            name = Console.ReadLine();

            Console.Write("Введите Отчество: ");
            middleName = Console.ReadLine();

            Console.Write("Введите Должность: ");
            post = Console.ReadLine();

            ExpandArray(ref persons);
            fullName = lastName + " " + name + " " + middleName;
            persons[persons.Length - 1] = fullName;

            ExpandArray(ref posts);
            posts[posts.Length - 1] = post;
        }

        static void ShowDossier()
        {
            int numberDossier = 1;

            Console.Clear();

            Console.Write("Все досье:\n");

            for (int i = 0; i < persons.Length; i++)
            {
                Console.Write($"\t{numberDossier++}. {persons[i]} - {posts[i]}\n");
            }
        }

        static void RemoveDossier()
        {
            string userInput;

            Console.Clear();

            ShowDossier();

            Console.Write("\nВведите номер досье: ");
            userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int numberDossier))
            {
                if (numberDossier > 0 && numberDossier <= persons.Length)
                {
                    int indexDossier = numberDossier - 1;

                    ReduceArray(ref persons, indexDossier);
                    ReduceArray(ref posts, indexDossier);

                    Console.Write("\nДосье успешно удалено.\n");
                }
            }
            else
            {
                Console.Write("Требуется ввести номер досье.\n");
            }
        }

        static void SearchDossier()
        {
            string temporaryLastName;
            string lastName;

            int numberDossier = 1;

            Console.Clear();

            Console.Write("Введите фамилию: ");
            lastName = Console.ReadLine();

            Console.Write("\nРезультат запроса:\n");

            for (int i = 0; i < persons.Length; i++)
            {
                temporaryLastName = persons[i].Split(' ')[0];

                if (lastName.ToLower() == temporaryLastName.ToLower())
                {
                    Console.Write($"\t{numberDossier++}. {persons[i]} - {posts[i]}\n");
                }
            }
        }

        static void ExpandArray(ref string[] array)
        {
            string[] temporaryArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
                temporaryArray[i] = array[i];

            array = temporaryArray;
        }

        static void ReduceArray(ref string[] array, int index)
        {
            string[] temporaryArray = new string[array.Length - 1];

            for (int i = 0; i < temporaryArray.Length; i++)
            {
                if (i < index)
                    temporaryArray[i] = array[i];
                else
                    temporaryArray[i] = array[i + 1];
            }

            array = temporaryArray;
        }
    }
}