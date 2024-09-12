using System;
using System.Collections.Generic;

/*
 * У нас может быть множество должностей, без повторений. На одной должности может быть несколько сотрудников (их полное имя).
 * Вам надо реализовать:
 * 1. Добавление сотрудника (при отсутствии должности, она добавляется)
 * 2. Удаление сотрудника. (при отсутствии у должности каких либо сотрудников, должность также удаляется)
 * 3. Показ полной информации (показ всех должностей и сотрудников по этой должности)
 * Для решения задачи понадобится использовать две разные коллекции.
*/

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

            Dictionary<string, List<string>> posts = new Dictionary<string, List<string>>();

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
                userInput = GetCommandMenu(menu);

                Console.Clear();

                switch (userInput)
                {
                    case CommandAddEmployee:
                        AddEmployee(posts);
                        break;

                    case CommandRemoveEmployee:
                        RemoveEmployee(posts);
                        break;

                    case CommandShowEmployees:
                        ShowEmployees(posts);
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

        static void AddEmployee(Dictionary<string, List<string>> posts)
        {
            string fullName;
            string post;

            Console.Write("Введите ФИО: ");
            fullName = Console.ReadLine();

            Console.Write("Введите должность: ");
            post = Console.ReadLine();

            if (posts.ContainsKey(post) == false)
                posts.Add(post, new List<string>());

            posts[post].Add(fullName);
        }

        static void RemoveEmployee(Dictionary<string, List<string>> posts)
        {
            string post;
            string userInput;

            if (posts.Count > 0)
            {
                ShowEmployees(posts);

                Console.Write("\nВведите должность сотрудника: ");
                post = Console.ReadLine();

                if (posts.ContainsKey(post))
                {
                    List<string> employees = posts[post];

                    Console.Clear();
                    ShowEmployees(employees, post);

                    Console.Write("\nВведите номер сотрудника: ");
                    userInput = Console.ReadLine();

                    if (TryGetEmployee(employees, userInput, out string employee))
                    {
                        employees.Remove(employee);
                        Console.Write("Сотрудник успешно удалён.\n");

                        if (employees.Count == 0)
                            posts.Remove(post);
                    }
                    else
                    {
                        Console.Write("Такого сотрудника нет.\n");
                    }
                }
                else
                {
                    Console.Write("Такой должности нет.\n");
                }
            }
            else
            {
                Console.Write("Нет ни одного должности, на которой кто-нибудь бы работал.\n");
            }
        }

        static bool TryGetEmployee(List<string> employees, string userInput, out string employee)
        {
            employee = null;

            if (int.TryParse(userInput, out int numberEmployee))
            {
                if (numberEmployee > 0 && numberEmployee <= employees.Count)
                {
                    employee = employees[numberEmployee - 1];
                    return true;
                }
                else
                {
                    Console.Write("С таким номером нет сотрудника.\n");
                }
            }
            else
            {
                Console.Write("Требуется ввести номер сотрудника.\n");
            }

            return false;
        }

        static void ShowEmployees(Dictionary<string, List<string>> posts)
        {
            int numberEmloyee = 1;

            Console.Write("Все сотрудники: \n");

            foreach (string post in posts.Keys)
                for (int i = 0; i < posts[post].Count; i++)
                    Console.Write($"\t{numberEmloyee++}. {posts[post][i]} - {post}\n");
        }

        static void ShowEmployees(List<string> employees, string post)
        {
            int numberEmloyee = 1;

            Console.Write($"Все сотрудники на должности: {post}\n");

            foreach (string employee in employees)
                Console.Write($"\t{numberEmloyee++}. {employee} - {post}\n");
        }

        static void PrintMenu(string[] menu)
        {
            for (int i = 0; i < menu.Length; i++)
                Console.Write($"\t{i + 1}. {menu[i]}\n");
        }

        static string GetCommandMenu(string[] menu)
        {
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int number))
                if (number > 0 && number <= menu.Length)
                    return menu[number - 1];

            return userInput;
        }
    }
}