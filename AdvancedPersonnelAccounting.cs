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

            Dictionary<string, List<string>> employees = new Dictionary<string, List<string>>();
            List<string> posts = new List<string>();

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
                        ShowEmployees(employees, posts);
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

        static void AddEmployee(Dictionary<string, List<string>> employees, List<string> posts)
        {
            List<string> postsEmployee = new List<string>();
            
            string fullName;
            string post;

            Console.Write("Введите ФИО: ");
            fullName = Console.ReadLine();

            Console.Write("Введите должность: ");
            post = Console.ReadLine();

            if (employees.ContainsKey(fullName) == false)
            {
                if (posts.Contains(post) == false)
                    posts.Add(post);

                postsEmployee.Add(post);
                employees.Add(fullName, postsEmployee);
            }
            else
            {
                Console.Write("Такой сотрудник уже имеется.\n");
            }
        }

        static void RemoveEmployee(Dictionary<string, List<string>> employees, List<string> posts)
        {
            string userInput;

            if (employees.Count > 0)
            {
                ShowEmployees(employees, posts);

                Console.Write("\nВведите номер сотрудника: ");
                userInput = Console.ReadLine();

                if (TryGetEmployee(employees, userInput, out string employee))
                {
                    List<string> temporaryPosts = employees[employee];
                    string post = temporaryPosts[0];

                    employees.Remove(employee);
                    Console.Write("Сотрудник успешно удалён.\n");

                    if (TryRemovePost(employees, post))
                        posts.Remove(post);
                }
            }
            else
            {
                Console.Write("Нет ни одного сотрудника.\n");
            }
        }

        static bool TryGetEmployee(Dictionary<string, List<string>> employees, string userInput, out string employee)
        {
            employee = null;

            if (int.TryParse(userInput, out int numberEmployee))
            {
                if (numberEmployee > 0 && numberEmployee <= employees.Count)
                { 
                    foreach (string temporaryEmployee in employees.Keys)
                    {
                        numberEmployee--;

                        if (numberEmployee == 0)
                        {
                            employee = temporaryEmployee;
                            return true;
                        }
                    }
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

            return false;
        }

        static bool TryRemovePost(Dictionary<string, List<string>> employees, string post)
        {
            foreach(string employee in employees.Keys)
            {
                if (employees[employee][0] == post)
                    return false;
            }

            return true;
        }

        static void ShowEmployees(Dictionary<string, List<string>> employees, List<string> posts)
        {
            int numberEmloyee = 1;

            Console.Write("Все сотрудники: \n");

            foreach (string employee in employees.Keys)
                    Console.Write($"\t{numberEmloyee++}. {employee} - {employees[employee][0]}\n");

            Console.Write($"{posts.Count} должности\n");
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