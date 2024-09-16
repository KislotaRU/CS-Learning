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
    public static int ReadInt()
    {
        int number;

        string userInput = Console.ReadLine();

        while (int.TryParse(userInput, out number) == false)
        {
            Console.Write("Требуется ввести число: ");
            userInput = Console.ReadLine();
        }

        return number;
    }
}

class Database
{
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
        const string CommandShowPlayers = "Показать игроков";
        const string CommandAddPlayer = "Добавить игрока";
        const string CommandRemovePlayer = "Удалить игрока";
        const string CommandBanPlayer = "Забанить игрока";
        const string CommandUnbanPlayer = "Разбанить игрока";
        const string CommandExit = "Выйти";

        string[] menu = new string[]
        {
                CommandShowPlayers,
                CommandAddPlayer,
                CommandRemovePlayer,
                CommandBanPlayer,
                CommandUnbanPlayer,
                CommandExit
        };

        string userInput;
        bool isRunning = true;

        while (isRunning)
        {
            Console.Write("\t**** БАЗА ДАННЫХ ****\n\n");

            Console.Write("Доступные команды:\n");
            PrintMenu(menu);

            Console.Write("\nОжидается ввод: ");
            userInput = GetCommandMenu(menu);

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
                    isRunning = Exit();
                    continue;

                default:
                    Console.Write("Требуется ввести номер команды или саму команду.\n\n");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    private void Show()
    {
        Console.Write("Все игроки:\n");

        foreach (Player player in _players)
        {
            Console.Write("\t");
            player.ShowInfo();
        }

        Console.WriteLine();
    }

    private void AddPlayer()
    {
        string nickname;
        int level;

        Console.Write("\tДобавление игрока.\n");

        Console.Write("Введите никнейм: ");
        nickname = Console.ReadLine();

        Console.Write("Введите уровень: ");
        level = UserUtils.ReadInt();

        _players.Add(new Player(nickname, level));
        Console.Write("Игрок успешно добавлен.\n\n");
    }

    private void RemovePlayer()
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

    private void BanPlayer()
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

    private void UnbanPlayer()
    {
        Show();

        Console.Write("Разбан игрока.\n");

        if (TryGetPlayer(out Player foundPlayer))
        {
            if (foundPlayer.IsBanned)
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

    private bool Exit()
    {
        Console.Write("Вы завершили программу.\n\n");
        return false;
    }

    private bool TryGetPlayer(out Player foundPlayer)
    {
        int id;
        foundPlayer = null;

        Console.Write("Введите Id игрока: ");
        id = UserUtils.ReadInt();

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

        return false;
    }

    private void PrintMenu(string[] menu)
    {
        for (int i = 0; i < menu.Length; i++)
            Console.Write($"\t{i + 1}. {menu[i]}\n");
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

class Player
{
    private static int s_id = 0;

    private readonly string _nickname;
    private readonly int _level;

    public Player(string nickname, int level = 0, bool isBanned = false)
    {
        Id = ++s_id;
        _nickname = nickname;
        _level = level;
        IsBanned = isBanned;
    }

    public int Id { get; private set; }
    public bool IsBanned { get; private set; }

    public void ShowInfo() =>
        Console.Write($"Id: {Id}".PadRight(6) + $"Ник: {_nickname}".PadRight(20) + $"Уровень: {_level}".PadRight(13) + $"Бан: {(IsBanned ? "Да" : "Нет")}\n");

    public void Ban() =>
        IsBanned = true;

    public void Unban() =>
        IsBanned = false;
}