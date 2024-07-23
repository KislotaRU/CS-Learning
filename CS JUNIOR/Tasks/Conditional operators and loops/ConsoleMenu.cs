using System;

namespace CS_JUNIOR
{
    class ConsoleMenu
    {
        static void Main(string[] args)
        {
            string userInput = "";
            const string CommandSetName = "SetName";
            const string CommandSetPassword = "SetPassword";
            const string CommandClearConsole = "ClearConsole";
            const string CommandChangeConsoleColor = "ChangeConsoleColor";
            const string CommandChangePassword = "ChangePassword";
            const string CommandWriteName = "WriteName";
            const string CommandEsc = "Esc";
            string userName = null;
            string userPassword = null;
            ConsoleColor consoleColorGreen = ConsoleColor.Green;
            ConsoleColor consoleColorRed = ConsoleColor.Red;
            Console.ForegroundColor = consoleColorGreen;

            Console.Write("Данная программа выполняет ряд доступных команд.");

            while (userInput != CommandEsc)
            {
                Console.WriteLine("\nСписок доступных команд: " +
                                  $"\n\t{CommandSetName} - установить имя." +
                                  $"\n\t{CommandSetPassword} - установить пароль." +
                                  $"\n\t{CommandClearConsole} - очистить консоль." +
                                  $"\n\t{CommandChangeConsoleColor} - изменить цвет консоли." +
                                  $"\n\t{CommandChangePassword} - изменить пароль." +
                                  $"\n\t{CommandWriteName} - вывести имя." +
                                  $"\n\t{CommandEsc} - завершить программу.");

                Console.Write("Команда: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandSetName:
                        Console.Write("Введите имя: ");
                        userName = Console.ReadLine();
                        break;
                    case CommandSetPassword:
                        if (userPassword == null)
                        {
                            Console.Write("Создайте пароль: ");
                            userPassword = Console.ReadLine();
                            Console.WriteLine("Пароль установлен.");
                        }
                        else
                        {
                            Console.WriteLine("У Вас уже установлен пароль.");
                        }

                        break;
                    case CommandClearConsole:
                        Console.Clear();
                        Console.WriteLine("\tКонсоль очищена...");
                        break;
                    case CommandChangeConsoleColor:
                        if (Console.ForegroundColor == consoleColorGreen)
                        {
                            Console.Write("Цвет консоли изменён на ");
                            Console.ForegroundColor = consoleColorRed;
                            Console.WriteLine("красный.");
                        }
                        else
                        {
                            Console.Write("Цвет консоли изменён на ");
                            Console.ForegroundColor = consoleColorGreen;
                            Console.WriteLine("зелёный.");
                        }

                        break;
                    case CommandChangePassword:
                        if (userPassword == null)
                        {
                            Console.WriteLine("У Вас не установлен пароль.");
                        }
                        else
                        {
                            Console.Write("Введите актуальный пароль для изменений: ");
                            userInput = Console.ReadLine();

                            if (userPassword == userInput)
                            {
                                Console.Write("Введите новый пароль: ");
                                userPassword = Console.ReadLine();
                                Console.WriteLine("Пароль изменён.");
                            }
                            else
                            {
                                Console.WriteLine("Пароль неверный. Попробуйте позже.");
                            }
                        }

                        break;
                    case CommandWriteName:
                        if (userPassword != null)
                        {
                            Console.Write("Для просмотра имени введите пароль: ");
                            userInput = Console.ReadLine();

                            if (userPassword == userInput)
                            {
                                Console.WriteLine($"Имя: {userName}");
                            }
                            else
                            {
                                Console.WriteLine("Пароль неверный. Попробуйте позже.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Пароль не установлен. Установите его и попробуйте снова.");
                        }

                        break;
                    case CommandEsc:
                        Console.WriteLine("Завершение программы...");
                        userInput = CommandEsc;
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда.");
                        break;
                }
            }
        }
    }
}