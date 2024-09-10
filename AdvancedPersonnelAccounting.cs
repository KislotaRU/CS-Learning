using System;
using System.Collections.Generic;
using System.Linq;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            const string CommandAddEmployee = "Добавить нового сотрудника";
            const string CommandRemoveEmployee = "Удалить нового сотрудника";
            const string CommandShowEmployees = "Показать всех сотрудников";
            const string CommandExit = "Завершить работу программы";

            List<string> employees = new List<string>();
            Dictionary<string, int> posts = new Dictionary<string, int>();

            string[] menu = new string[]
            {
                CommandAddEmployee,
                CommandRemoveEmployee,
                CommandShowEmployees,
                CommandExit
            };

            string userInput = null;

            Console.ForegroundColor = ConsoleColor.White;

            while (userInput != CommandExit)
            {
                Console.Write("\tПрограмма \"Кадровый учёт\".\n\n");

                Console.Write("Доступные команды:\n");
                PrintMenu(menu);

                Console.Write("\nОжидается ввод: ");
                userInput = GetParagraphMenu(menu);

                Console.Clear();

                switch (userInput)
                {
                    case CommandAddEmployee:
                        AddEmployee(employees, posts);
                        break;

                    case CommandRemoveEmployee:
                        RemoveEmployee(employees, posts);
                        break;

                    case CommandShowEmployees:
                        ShowEmployees(employees);
                        break;

                    case CommandExit:
                        Console.Write("Вы завершили работу программы.\n\n");
                        continue;

                    default:
                        Console.Write("Требуется ввести номер команды или саму команду.\n\n");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        static void AddEmployee(List<string> employees, Dictionary<string, int> posts)
        {
            string separator = " - ";
            string fullName;
            string post;

            int postCount = 0;

            Console.Write("Введите ФИО: ");
            fullName = Console.ReadLine();

            Console.Write("Введите должность: ");
            post = Console.ReadLine();

            if (TryGetPost(posts, post, out string foundPost))
            {
                fullName += separator + foundPost;
                posts[foundPost]++;
            }
            else
            {
                fullName += separator + post;
                posts.Add(post, postCount++);
            }

            employees.Add(fullName);
        }

        static bool TryGetPost(Dictionary<string, int> posts, string userInput, out string foundPost)
        {
            foundPost = null;

            foreach (string post in posts.Keys)
            {
                if (userInput == post)
                {
                    foundPost = post;
                    return true;
                }
            }

            return false;
        }

        static void RemoveEmployee(List<string> employees, Dictionary<string, int> posts)
        {
            string userInput;

            ShowEmployees(employees);

            Console.Write("\nВведите номер сотрудника: ");
            userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int numberEmployee))
            {
                int index = numberEmployee - 1;

                string post = employees[index].Split().Last();

                employees.RemoveAt(index);
                posts[post]--;

                if (posts[post] == 0)
                    posts.Remove(post);
            }
            else
            {
                Console.Write("Такого сотрудника нет.\n");
            }
        }

        static void ShowEmployees(List<string> employees)
        {
            int numberEmloyee = 1;

            Console.Write("Все сотрудники: \n");

            foreach (string employee in employees)
                Console.Write($"\t{numberEmloyee++}. {employee}\n");
        }

        static void PrintMenu(string[] menu)
        {
            for (int i = 0; i < menu.Length; i++)
                Console.Write($"\t{i + 1}. {menu[i]}\n");
        }

        static string GetParagraphMenu(string[] menu)
        {
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int number))
                if (number > 0 && number <= menu.Length)
                    return menu[number - 1];

            return userInput;
        }
    }
}