using System;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            const string CommandAddDossier = "Добавить";
            const string CommandShowDossier = "Показать";
            const string CommandRemoveDossier = "Удалить";
            const string CommandSearchDossier = "Поиск";
            const string CommandExit = "Выйти";

            string[] persons = new string[]
            {
                "Макарова Татьяна Долгих",
                "Иванов Иван Иванович",
                "Бикеев Александр Павлович",
                "Крюкова Ольга Петровна",
            };

            string[] posts = new string[]
            {
                "Оператор",
                "Работник",
                "Начальник",
                "Редактор"
            };

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

                Console.Clear();

                switch (userInput)
                {
                    case CommandAddDossier:
                        AddDossier(ref persons, ref posts);
                        break;

                    case CommandShowDossier:
                        ShowDossiers(persons, posts);
                        break;

                    case CommandRemoveDossier:
                        RemoveDossier(ref persons, ref posts);
                        break;

                    case CommandSearchDossier:
                        SearchDossier(persons, posts);
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

        static void AddDossier(ref string[] persons, ref string[] posts)
        {
            string fullName;
            string post;

            Console.Write("Введите ФИО: ");
            fullName = Console.ReadLine();

            Console.Write("Введите Должность: ");
            post = Console.ReadLine();

            persons = ExpandArray(persons, fullName);

            posts = ExpandArray(posts, post);

            Console.Write("\nДосье добавлено.\n");
        }

        static void ShowDossiers(string[] persons, string[] posts)
        {
            int numberDossier = 1;

            Console.Write("Все досье:\n");

            for (int i = 0; i < persons.Length; i++)
                Console.Write($"\t{numberDossier++}. {persons[i]} - {posts[i]}\n");
        }

        static void RemoveDossier(ref string[] persons, ref string[] posts)
        {
            string userInput;

            ShowDossiers(persons, posts);

            Console.Write("\nВведите номер досье: ");
            userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int numberDossier))
            {
                if (numberDossier > 0 && numberDossier <= persons.Length)
                {
                    int indexDossier = numberDossier - 1;

                    persons = ReduceArray(persons, indexDossier);
                    posts = ReduceArray(posts, indexDossier);

                    Console.Write("\nДосье успешно удалено.\n");
                }
            }
            else
            {
                Console.Write("Требуется ввести номер досье.\n");
            }
        }

        static void SearchDossier(string[] persons, string[] posts)
        {
            string temporaryLastName;
            string lastName;

            int numberDossier = 1;
            bool isFoundDossier = false;

            Console.Write("Введите фамилию: ");
            lastName = Console.ReadLine();

            Console.Write("\nРезультат запроса:\n");

            for (int i = 0; i < persons.Length; i++)
            {
                temporaryLastName = persons[i].Split(' ')[0];

                if (lastName.ToLower() == temporaryLastName.ToLower())
                {
                    isFoundDossier = true;
                    Console.Write($"\t{numberDossier++}. {persons[i]} - {posts[i]}\n");
                }
            }

            if (isFoundDossier == false)
                Console.Write("\tНе найдено.\n");
        }

        static string[] ExpandArray(string[] array, string element)
        {
            string[] temporaryArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
                temporaryArray[i] = array[i];

            temporaryArray[temporaryArray.Length - 1] = element;
            
            return temporaryArray;
        }

        static string[] ReduceArray(string[] array, int index)
        {
            string[] temporaryArray = new string[array.Length - 1];

            for (int i = 0; i < index; i++)
                temporaryArray[i] = array[i];

            for (int i = index; i < array.Length - 1; i++)
                temporaryArray[i] = array[i + 1];

            return temporaryArray;
        }
    }
}