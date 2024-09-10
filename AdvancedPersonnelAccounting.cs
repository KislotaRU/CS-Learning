using System;
using System.Collections.Generic;

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

            Dictionary<string, string> employees = new Dictionary<string, string>();
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

        static void AddEmployee(Dictionary<string, string> employees, Dictionary<string, int> posts)
        {
            string fullName;
            string post;

            Console.Write("Введите ФИО: ");
            fullName = Console.ReadLine();

            Console.Write("Введите должность: ");
            post = Console.ReadLine();

            if (TryGetPost(posts, post, out string foundPost))
                posts[foundPost]++;
            else
                posts.Add(post, 1);

            employees.Add(fullName, post);
        }

        static bool TryGetPost(Dictionary<string, int> posts, string userInput, out string foundPost) =>
            (foundPost = posts.ContainsKey(userInput) ? userInput : null) != null;

        static void RemoveEmployee(Dictionary<string, string> employees, Dictionary<string, int> posts)
        {
            string userInput;

            if (employees.Count > 0)
            {
                ShowEmployees(employees);

                Console.Write("\nВведите номер сотрудника: ");
                userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int numberEmployee))
                {
                    if (numberEmployee > 0 && numberEmployee <= employees.Count)
                    {
                        string employee = GetEmployee(employees, numberEmployee);

                        string post = employees[employee];

                        employees.Remove(employee);
                        posts[post]--;

                        if (posts[post] == 0)
                            posts.Remove(post);

                        Console.Write("Сотрудник успешно удалён.\n");
                    }
                    else
                    {
                        Console.Write("Такого сотрудника нет.\n");
                    }
                }
                else
                {
                    Console.Write("Требуется ввести номер сотрудника.\n");
                }
            }
            else
            {
                Console.Write("Нет ни одного сотрудника.\n");
            }
        }

        static string GetEmployee(Dictionary<string, string> employees, int numberEmployee)
        {
            int temporaryNumberEmployee = 0;

            foreach (string employee in employees.Keys)
            {
                temporaryNumberEmployee++;

                if (temporaryNumberEmployee == numberEmployee)
                    return employee;
            }

            return null;
        }

        static void ShowEmployees(Dictionary<string, string> employees)
        {
            int numberEmloyee = 1;

            Console.Write("Все сотрудники: \n");

            foreach (string employee in employees.Keys)
                Console.Write($"\t{numberEmloyee++}. {employee} - {employees[employee]}\n");
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