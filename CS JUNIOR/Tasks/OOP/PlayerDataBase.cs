using System;
using System.Collections.Generic;
using System.IO;

//Реализовать базу данных игроков и методы для работы с ней.
//У игрока может быть уникальный номер, ник, уровень, флаг – забанен ли он(флаг - bool).
//Реализовать возможность добавления игрока, бана игрока по уникальный номеру, разбана игрока по уникальный номеру и удаление игрока.
//Создание самой БД не требуется, задание выполняется инструментами, 
//которые вы уже изучили в рамках курса. Но нужен класс, который содержит игроков и её можно назвать "База данных".

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Database database = new Database("Players");

            database.StartWork();
        }
    }

    class Player
    {
        private static int _ids;

        public Player(int level, string tag, string name, bool isBanned)
        {
            Id = ++_ids;
            Level = level;
            Tag = tag;
            Name = name;
            IsBanned = isBanned;
        }

        public int Id { get; private set; }
        public int Level { get; private set; }
        public string Tag { get; private set; }
        public string Name { get; private set; }
        public bool IsBanned { get; private set; }

        public void ShowStatus()
        {
            Console.Write($"ID - {Id}\t| Уровень - {Level}\t| Тег - [{Tag}] | Ник - {Name}\t| Заблокирован - {IsBanned}\n");
        }

        public void Ban()
        {
            IsBanned = true;
        }

        public void Unban()
        {
            IsBanned = false;
        }
    }

    class Database
    {
        private const string ConclusionDefault = "NULL";

        private List<Player> _players = new List<Player>();

        public Database(string nameFile = null)
        {
            ReadFile(nameFile);
        }

        public void StartWork()
        {
            const string CommandShowAllPlayers = "ShowAllPlayers";
            const string CommandShowBannedPlayers = "ShowBannedPlayers";
            const string CommandAddPlayer = "AddPlayer";
            const string CommandBanPlayer = "BanPlayer";
            const string CommandUnbanPlayer = "UnbanPlayer";
            const string CommandRemovePlayer = "RemovePlayer";
            const string CommandExit = "Exit";

            string userInput;
            bool isWork = true;

            while (isWork == true)
            {
                Console.WriteLine("\t\t\tЗапущена работа с базой данных игроков.");

                Console.WriteLine("Доступные команды:" +
                                  $"\n\t> {CommandShowAllPlayers} - Показать всех игроков." +
                                  $"\n\t> {CommandShowBannedPlayers} - Показать только заблокированных игроков." +
                                  $"\n\t> {CommandAddPlayer} - Добавить игрока." +
                                  $"\n\t> {CommandBanPlayer} - Заблокировать игрока." +
                                  $"\n\t> {CommandUnbanPlayer} - Разблокировать игрока." +
                                  $"\n\t> {CommandRemovePlayer} - Удалить игрока." +
                                  $"\n\t> {CommandExit} - Завершить работу с базой данных.");

                Console.Write("\nВведите команду для выполнения: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandShowAllPlayers:
                        ShowAllPlayers();
                        break;

                    case CommandShowBannedPlayers:
                        ShowBannedPlayers();
                        break;

                    case CommandAddPlayer:
                        AddPlayer();
                        break;

                    case CommandBanPlayer:
                        BanPlayer();
                        break;

                    case CommandUnbanPlayer:
                        UnbanPlayer();
                        break;

                    case CommandRemovePlayer:
                        RemovePlayer();
                        break;

                    case CommandExit:
                        Console.WriteLine("Завершение работы...");
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Такой команды нет!");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        public void AddPlayer(string[] playerData = null)
        {
            int level;
            string tag = ConclusionDefault;
            string name = ConclusionDefault;
            bool isBanned;

            if (playerData != null)
            {
                int i = 0;

                int.TryParse(playerData[i], out level);
                tag = playerData[++i];
                name = playerData[++i];
                bool.TryParse(playerData[++i], out isBanned);
            }
            else
            {
                string userInput;

                Console.WriteLine("\nДобавление игрока.");

                do
                {
                    Console.Write("Введите Уровень: ");
                    userInput = Console.ReadLine();
                } while (int.TryParse(userInput, out level) == false);

                Console.Write("Введите Тег: ");
                userInput = Console.ReadLine();
                tag = userInput ?? tag;

                Console.Write("Введите Ник: ");
                userInput = Console.ReadLine();
                name = userInput ?? name;

                do
                {
                    Console.Write("Введите статус блокировки true/false: ");
                    userInput = Console.ReadLine();
                } while (bool.TryParse(userInput, out isBanned) == false);

                Console.WriteLine("\nВсе данные введены.");
            }

            _players.Add(new Player(level, tag, name, isBanned));
        }

        public void ShowAllPlayers()
        {
            Console.WriteLine("\nПоказаны все существующие игроки:");

            for (int i = 0; i < _players.Count; i++)
            {
                _players[i].ShowStatus();
            }
        }

        public void BanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                player.Ban();
                Console.WriteLine($"Игрок \"{player.Name}\" заблокирован.");
            }
        }

        public void ShowBannedPlayers()
        {
            Console.WriteLine("\nПоказаны все заблокированные игроки:");

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].IsBanned == true)
                    _players[i].ShowStatus();
            }
        }

        public void UnbanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                player.Unban();
                Console.WriteLine($"Игрок \"{player.Name}\" разблокирован.");
            }
        }

        public void RemovePlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                _players.Remove(player);
                Console.WriteLine($"Игрок \"{player.Name}\" удалён.");
            }
        }

        private void ReadFile(string nameFile)
        {
            string[] newFile;

            if (File.Exists($"Database/{nameFile}.txt") == true)
                newFile = File.ReadAllLines($"Database/{nameFile}.txt");
            else
                return;

            for (int i = 0; i < newFile.Length; i++)
            {
                string[] playersData = newFile[i].Split(';');

                AddPlayer(playersData);
            }
        }

        private bool TryGetPlayer(out Player player)
        {
            string userInput;
            int id;

            player = null;

            Console.WriteLine("\nПоиск игрока.");

            do
            {
                Console.Write("Введите ID игрока: ");
                userInput = Console.ReadLine();
            } while (int.TryParse(userInput, out id) == false);

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].Id == id)
                {
                    player = _players[i];
                    return true;
                }
            }

            Console.WriteLine("Игрок не найден.");
            return false;
        }
    }
}