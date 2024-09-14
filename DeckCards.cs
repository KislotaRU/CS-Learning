using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Croupier croupier = new Croupier();

            Console.ForegroundColor = ConsoleColor.White;

            croupier.Work();
        }
    }
}

static class UserUtils
{
    private readonly static Random random;

    static UserUtils()
    {
        random = new Random();
    }

    public static void ShuffleList<T>(List<T> list)
    {
        int firstIndex;
        int secondIndex;
        T temporaryElement;

        for (int i = 0; i < list.Count; i++)
        {
            firstIndex = i;
            secondIndex = random.Next(list.Count);

            temporaryElement = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temporaryElement;
        }
    }
}

class Player
{
    private readonly DeckCards _deckCards;

    private readonly int _maxCountCards = 6;

    public Player()
    {
        _deckCards = new DeckCards(_maxCountCards);
    }

    public void ShowCards()
    {
        Console.Write("Карты игрока:\n");

        _deckCards.Show();
    }

    public void AddCard(Card card) =>
            _deckCards.AddCard(card);

    public bool TryAddCards(int cardsTakenCount)
    {
        int temporaryCountCards = _deckCards.Count + cardsTakenCount;

        if (temporaryCountCards <= _maxCountCards)
            return true;
        else
            Console.Write("Вы пытаетсь взять больше карт, чем ваше максимальное кол-во карт.\n" +
                         $"Ваше текущее кол-во карт {_deckCards.Count}/{_maxCountCards}.\n");
        
        return false;
    }
}

class Croupier
{
    private readonly DeckCards _deckCards;
    private readonly Player _plyaer;

    public Croupier()
    {
        _deckCards = new DeckCards();
        _plyaer = new Player();
    }

    public void Work()
    {
        Console.Write("Крупье запрашивает кол-во карт, которое вам нужно выдать.\n\n");

        if (TryGetCards(out int cardsCount))
        {
            if (_plyaer.TryAddCards(cardsCount))
            {
                Card temporaryCard;

                for (int i = 0; i < cardsCount; i++)
                {
                    temporaryCard = GetCard();

                    _plyaer.AddCard(temporaryCard);
                }

                Console.Write("Карты успешно взяты из колоды.\n\n");
            }
            else
            {
                Console.Write("Не удалось взять указанное кол-во карт из колоды.\n\n");
            }
        }
        else
        {
            Console.Write("Не удалось получить указанное кол-во карт из колоды.\n\n");
        }

        _plyaer.ShowCards();
    }

    private bool TryGetCards(out int cardsCount)
    {
        string userInput;

        Console.Write("Введите кол-во: ");
        userInput = Console.ReadLine();

        if (int.TryParse(userInput, out cardsCount))
        {
            if (cardsCount > 0 && cardsCount <= _deckCards.Count)
                return true;
            else
                Console.Write("Такого кол-во карт нет в колоде.\n\n");
        }
        else
        {
            Console.Write("Требуется ввести кол-во карт.\n\n");
        }

        return false;
    }

    private Card GetCard() =>
        _deckCards.GetCard();
}

class DeckCards
{
    private readonly List<Card> _cards;

    public DeckCards()
    {
        _cards = new List<Card>();

        Create();
        Shuffle();
    }

    public DeckCards(int carsdCount)
    {
        _cards = new List<Card>();
    }

    public int Count {  get { return _cards.Count; } }

    public void Show()
    {
        int numberCard = 1;

        foreach (Card card in _cards)
        {
            Console.Write($"\t{numberCard++}. ".PadRight(5));
            card.Show();
        }

        Console.WriteLine();
    }

    public void AddCard(Card card) =>
        _cards.Add(card);

    public void RemoveCard(Card card) =>
        _cards.Remove(card);

    public Card GetCard()
    {
        Card temporaryCard = _cards[_cards.Count - 1];

        RemoveCard(temporaryCard);

        return temporaryCard;
    }

    private void Create()
    {
        int suitsCount = (int)Suits.Lenght;
        int nameCardsCount = (int)NameCards.Lenght;

        for (int i = 0; i < suitsCount; i++)
        {
            for (int j = 0; j < nameCardsCount; j++)
            {
                string suit = ((Suits)i).ToString();
                string nameCard = ((NameCards)j).ToString();

                _cards.Add(new Card(suit, nameCard));
            }
        }
    }

    private void Shuffle() =>
        UserUtils.ShuffleList(_cards);
}

class Card
{
    private readonly string _suit;
    private readonly string _name;

    public Card(string suit, string name)
    {
        _suit = suit;
        _name = name;
    }

    public void Show() =>
        Console.Write($"Название: {_name}".PadRight(16) + $"Масть: {_suit}.\n");
}

enum Suits
{
    Booby,
    Worms,
    Cross,
    Peaks,
    Lenght
}

enum NameCards
{
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace,
    Lenght
}