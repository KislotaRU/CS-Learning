using System;
using System.Collections.Generic;
using System.Linq;

/*
 * У нас есть список всех игроков(минимум 10). У каждого игрока есть поля: имя, уровень, сила.
 * Требуется написать запрос для определения топ 3 игроков по уровню и топ 3 игроков по силе, после чего вывести каждый топ.
 * 2 запроса получится.
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            PlayerDatabase playerDatabase = new PlayerDatabase();

            Console.ForegroundColor = ConsoleColor.White;

            playerDatabase.Work();
        }
    }
}

public class PlayerDatabase
{
    private readonly List<Player> _players;

    public PlayerDatabase()
    {
        _players = new List<Player>()
        {
            new Player("Вафелька", 18, 78),
            new Player("Glitchy", 5, 34),
            new Player("Ушастик", 7, 38),
            new Player("Zany", 48, 243),
            new Player("Чудик", 1, 5),
            new Player("FizzBuzz", 99, 859),
            new Player("Titan", 18, 84),
            new Player("KnightX", 54, 350),
            new Player("Overlord", 18, 43),
            new Player("Storm", 19, 90),
            new Player("Shadow", 35, 445),
            new Player("Небесный", 65, 420),
            new Player("Страж", 34, 126),
            new Player("Raptor", 28, 100)
        };
    }

    public void Work()
    {
        const string CommandShow = "Показать всех";
        const string CommandShowTopPlayers = "Показать топ игроков";
        const string CommandExit = "Завершить работу";

        string[] menu = new string[]
        {
            CommandShow,
            CommandShowTopPlayers,
            CommandExit
        };

        string userInput;

        bool isWorking = true;

        while (isWorking)
        {
            Console.Write("\t\tМеню базы данных игроков\n\n");

            Console.Write("Доступные команды:\n");
            PrintMenu(menu);

            Console.Write("Ожидается ввод: ");
            userInput = GetCommandMenu(menu);

            Console.Clear();

            switch (userInput)
            {
                case CommandShow:
                    Show(_players);
                    break;

                case CommandShowTopPlayers:
                    ShowTopPlayers();
                    break;

                case CommandExit:
                    Console.Write("Вы завершии работу базы данных.\n\n");
                    isWorking = false;
                    continue;

                default:
                    Console.Write("Требуется ввести номер команды или саму команду.\n");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    private void Show(List<Player> players)
    {
        int numberPlayer = 1;

        if (players.Count > 0)
        {
            foreach (Player player in players)
            {
                Console.Write($"\t{numberPlayer}. ".PadRight(5));
                player.Show();

                numberPlayer++;
            }
        }
        else
        {
            Console.Write("Нет ни одной записи.\n");
        }

        Console.WriteLine();
    }

    private void ShowTopPlayers()
    {
        List<Player> topPlyaersByLevel;
        List<Player> topPlyaersByForce;
        int maxCountTopPlayers = 3;

        topPlyaersByLevel = _players.OrderByDescending(player => player.Level).Take(maxCountTopPlayers).ToList();
        topPlyaersByForce = _players.OrderByDescending(player => player.Force).Take(maxCountTopPlayers).ToList();

        Console.Write($"Топ {maxCountTopPlayers} игроков по уровню:\n");
        Show(topPlyaersByLevel);

        Console.Write($"Топ {maxCountTopPlayers} игроков по силе:\n");
        Show(topPlyaersByForce);
    }
     
    private void PrintMenu(string[] menu)
    {
        int numberCommand = 1;

        for (int i = 0; i < menu.Length; i++)
        {
            Console.Write($"\t{numberCommand}. {menu[i]}\n");
            numberCommand++;
        }

        Console.WriteLine();
    }

    private string GetCommandMenu(string[] menu)
    {
        string userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int number))
            if (number > 0 && number <= menu.Length)
                return menu[number - 1];

        return userInput;
    }
}

public class Player
{
    private readonly string _name;

    public Player(string name, int level, int force)
    {
        _name = name;
        Level = level;
        Force = force;
    }

    public int Level { get; }
    public int Force { get; }

    public void Show()
    {
        Console.Write($"{_name}".PadRight(15) +
                      $"| Уровень: {Level}".PadRight(13) +
                      $"| Сила: {Force}\n");
    }
}