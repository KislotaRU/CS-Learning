using System;
using System.Collections.Generic;
using System.IO;

namespace CS_JUNIOR
{
    class DeckCards
    {
        static void Main()
        {
            Game game = new Game();
        }
    }

    class Player
    {
        private const int InitialQuantityCards = 0;

        private Pack _packCards;

        private int _maxCountCards = 6;

        public Player()
        {
            MaxCountCards = _maxCountCards;
            _packCards = new Pack(InitialQuantityCards);
        }

        public int MaxCountCards { get; private set; }

        public int GetCountCards()
        {
            return _packCards.CountCards;
        }

        public void ShowCards()
        {
            _packCards.ShowCards();
        }

        public void TakeCard(Card card)
        {
            _packCards.TakeCard(card);
        }
    }

    class Pack
    {
        private const string Suits = "Suits";
        private const string NameCards = "NameCards";

        private List<Card> _pack = new List<Card>();

        public Pack(int countCards)
        {
            CountCards = countCards;
        }

        public Pack()
        {
            Fill();
            Shuffle();
        }

        public int CountCards { get; private set; }

        public void ShowCards()
        {
            foreach (Card card in _pack)
            {
                card.ShowInfo();
            }
        }

        public void TakeCard(Card card)
        {
            _pack.Add(card);
            CountCards++;
        }

        public Card GetTopCard()
        {
            return _pack[_pack.Count - 1];
        }

        public void RemoveCard()
        {
            _pack.RemoveAt(_pack.Count - 1);
        }

        private void Fill()
        {
            string[] suits = ReadFile(Suits);
            string[] nameCards = ReadFile(NameCards);

            if (suits != null && nameCards != null)
            {
                for (int i = 0; i < suits.Length; i++)
                {
                    for (int j = 0; j < nameCards.Length; j++)
                    {
                        _pack.Add(new Card(suits[i], nameCards[j]));
                        CountCards++;
                    }
                }
            }
            else
            {
                Console.WriteLine("Не удалось заполнить колоду.");
            }
        }

        private void Shuffle()
        {
            Random random = new Random();

            int firstCard;
            int secondCard;
            Card temporaryCard;

            for (int i = 0; i < _pack.Count; i++)
            {
                for (int j = 0; j < _pack.Count; j++)
                {
                    firstCard = random.Next(0, _pack.Count);
                    secondCard = random.Next(0, _pack.Count);

                    temporaryCard = _pack[firstCard];
                    _pack[firstCard] = _pack[secondCard];
                    _pack[secondCard] = temporaryCard;
                }
            }
        }

        private string[] ReadFile(string nameFile = null)
        {
            if (File.Exists($"Database/{nameFile}.txt") == true)
                return File.ReadAllLines($"Database/{nameFile}.txt");
            else
                return null;
        }
    }

    class Game
    {
        private const string CommandPlay = "Play";
        private const string CommandExit = "Exit";

        private string _userInput;

        public Game()
        {
            StartWork();
        }

        public void StartWork()
        {
            bool isWork = true;

            while (isWork == true)
            {
                Console.WriteLine("Запущена карточная игра!");

                Console.WriteLine("\nДоступные команды:" +
                                  $"\n\t> {CommandPlay} - начать игру." +
                                  $"\n\t> {CommandExit} - выйти из игры.\n");

                Console.Write("Введите команду: ");
                _userInput = Console.ReadLine();

                switch (_userInput)
                {
                    case CommandPlay:
                        Play();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда.");
                        break;
                }

                Console.ReadLine();
                Console.Clear();
            }
        }

        private void Play()
        {
            Pack pack;
            Player player;

            Console.Clear();

            Console.WriteLine("Создание колоды карт...\n");
            pack = new Pack();

            Console.WriteLine("Создание игрока...\n");
            player = new Player();

            while (player.GetCountCards() < player.MaxCountCards)
            {
                Console.WriteLine($"У Вас {player.GetCountCards()} карт.\n" +
                                  "Вам надо именть не меньше 6 карт.\n" +
                                  "Возьмите карту из колоды.");

                Console.ReadLine();

                player.TakeCard(pack.GetTopCard());
                Console.Write("Вы взяли одну карту: ");
                pack.GetTopCard().ShowInfo();
                pack.RemoveCard();

                Console.ReadLine();
                Console.Clear();
            }

            Console.WriteLine("Колода карт игрока:");

            player.ShowCards();
        }
    }

    class Card
    {
        public Card(string suit, string name)
        {
            Suit = suit;
            Name = name;
        }

        public string Suit { get; private set; }
        public string Name { get; private set; }

        public void ShowInfo()
        {
            Console.Write($"Масть - [{Suit}]    \t| Название карты - {Name} \n");
        }
    }
}