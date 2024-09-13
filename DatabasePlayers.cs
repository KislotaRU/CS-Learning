using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Database database = new Database();

            Console.ForegroundColor = ConsoleColor.White;

            database.Work();
        }
    }
}

static class UserUtils
{
    public static void PrintMenu(string[] menu)
    {
        for (int i = 0; i < menu.Length; i++)
            Console.Write($"\t{i + 1}. {menu[i]}\n");
    }

    public static string GetCommandMenu(string[] menu)
    {
        string userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int number))
            if (number > 0 && number <= menu.Length)
                return menu[number - 1];

        return userInput;
    }
}

class Database
{
    private const string CommandShowPlayers = "Показать игроков";
    private const string CommandAddPlayer = "Добавить игрока";
    private const string CommandRemovePlayer = "Удалить игрока";
    private const string CommandBanPlayer = "Забанить игрока";
    private const string CommandUnbanPlayer = "Разбанить игрока";
    private const string CommandExit = "Выйти";

    private readonly List<Player> _players;

    public Database()
    {
        _players = new List<Player>()
        { 
            new Player("Tears", 18),
            new Player("CraftyOftel", 46),
            new Player("Шарик"),
            new Player("LOLster", 3, true),
            new Player("Jester", 25),
            new Player("Storm", isBanned: true),
        };
    }

    public void Work()
    {
        string[] menu = new string[]
        {
                CommandShowPlayers,
                CommandAddPlayer,
                CommandRemovePlayer,
                CommandBanPlayer,
                CommandUnbanPlayer,
                CommandExit
        };

        string userInput = null;

        while (userInput != CommandExit)
        {
            Console.Write("\t**** БАЗА ДАННЫХ ****\n\n");

            Console.Write("Доступные команды:\n");
            UserUtils.PrintMenu(menu);

            Console.Write("\nОжидается ввод: ");
            userInput = UserUtils.GetCommandMenu(menu);

            Console.Clear();

            switch (userInput)
            {
                case CommandShowPlayers:
                    Show();
                    break;

                case CommandAddPlayer:
                    AddPlayer();
                    break;

                case CommandRemovePlayer:
                    RemovePlayer();
                    break;

                case CommandBanPlayer:
                    BanPlayer();
                    break;

                case CommandUnbanPlayer:
                    UnbanPlayer();
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

    public void Show()
    {
        Console.Write("Все игроки:\n");

        foreach (Player player in _players)
        {
            Console.Write("\t");
            player.ShowInfo();
        }

        Console.WriteLine();
    }

    public void AddPlayer()
    {
        Player player;

        string nickname;
        string userInput;

        Console.Write("\tДобавление игрока.\n");

        Console.Write("Введите никнейм: ");
        nickname = Console.ReadLine();

        Console.Write("Введите уровень: ");
        userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int level))
        {
            player = new Player(nickname, level);

            _players.Add(player);
        }
        else
        {
            Console.Write("Требуется ввести число.\n\n");
            return;
        }
    }

    public void RemovePlayer()
    {
        Show();

        Console.Write("Удаление игрока.\n");

        if (TryGetPlayer(out Player foundPlayer))
        {
            _players.Remove(foundPlayer);
            Console.Write("Игрок успешно удалён.\n\n");
        }
        else
        {
            Console.Write("Не удалось удалить игрока.\n\n");
        }
    }

    public void BanPlayer()
    {
        Show();

        Console.Write("Бан игрока.\n");

        if (TryGetPlayer(out Player foundPlayer))
        {
            if (foundPlayer.IsBanned == false)
            {
                foundPlayer.Ban();
                Console.Write("Игрок успешно забанен.\n\n");
            }
            else
            {
                Console.Write("Игрок уже имеет бан.\n\n");
            }
        }
        else
        {
            Console.Write("Не удалось забанить игрока.\n\n");
        }
    }

    public void UnbanPlayer()
    {
        Show();

        Console.Write("Разбан игрока.\n");

        if (TryGetPlayer(out Player foundPlayer))
        {
            if (foundPlayer.IsBanned == true)
            {
                foundPlayer.Unban();
                Console.Write("Игрок успешно разбанен.\n\n");
            }
            else
            {
                Console.Write("Игрок не имеет бана.\n\n");
            }
        }
        else
        {
            Console.Write("Не удалось разбанить игрока.\n\n");
        }
    }

    private bool TryGetPlayer(out Player foundPlayer)
    {
        string userInput;
        foundPlayer = null;

        Console.Write("Введите Id игрока: ");
        userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int id))
        {
            if (id > 0 && id <= _players.Count)
            {
                foreach (Player player in _players)
                {
                    if (player.Id == id)
                    {
                        foundPlayer = player;
                        return true;
                    }
                }
            }
            else
            {
                Console.Write("С таким Id не существует игрока.\n");
            }
        }
        else
        {
            Console.Write("Требуется ввести Id игрока.\n");
        }

        return false;
    }
}

class Player
{
    private static int s_id = 0;

    public Player(string nickname, int level = 0, bool isBanned = false)
    {
        Id = ++s_id;
        Nickname = nickname;
        Level = level;
        IsBanned = isBanned;
    }

    public int Id { get; private set; }
    public string Nickname { get; private set; }
    public int Level { get; private set; }
    public bool IsBanned { get; private set; }

    public void ShowInfo() =>
        Console.Write($"Id: {Id}".PadRight(6) + $"Ник: {Nickname}".PadRight(20) + $"Уровень: {Level}".PadRight(13) + $"Бан: {(IsBanned ? "Да" : "Нет")}\n");

    public void Ban() =>
        IsBanned = true;

    public void Unban() =>
        IsBanned = false;
}