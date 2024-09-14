using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Storage storage = new Storage();

            Console.ForegroundColor = ConsoleColor.White;

            storage.Work();
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

class Storage
{
    private List<Book> _books;

    public Storage()
    {
        _books = new List<Book>();
    }

    public void Work()
    {
        const string CommandShowBooks = "Показать книги";
        const string CommandAddBook = "Добавить книгу";
        const string CommandRemoveBook = "Удалить книгу";
        const string CommandSearchBook = "Найти книгу";
        const string CommandExit = "Выйти";

        string[] menu = new string[]
        {
            CommandShowBooks,
            CommandAddBook,
            CommandRemoveBook,
            CommandSearchBook,
            CommandExit
        };

        string userInput = null;

        while (userInput != CommandExit)
        {

            switch (userInput)
            {
                case CommandShowBooks:
                    ShowBooks();
                    break;

                case CommandAddBook:
                    AddBook();
                    break;

                case CommandRemoveBook:
                    RemoveBook();
                    break;

                case CommandSearchBook:
                    SearchBook();
                    break;

                case CommandExit:
                    Console.Write("Вы завершили программу.\n\n");
                    continue;

                default:
                    Console.Write("Требуется ввести номер команды или саму команду.\n\n");
                    break;
            }
        }
    }

    private void ShowBooks()
    {
        Console.Write("Все книги:\n");

        int numberBook = 1;

        foreach(Book book in _books)
        {
            Console.Write($"\t{numberBook++}. ".PadRight(5));
            book.Show();
        }

        Console.WriteLine();
    }

    private void AddBook()
    {
        string author;
        string name;
        string userInput;

        Console.Write("Введите автора: ");
        author = Console.ReadLine();

        Console.Write("Введите название книги: ");
        name = Console.ReadLine();

        Console.Write("Введите год выпуска: ");
        userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int yearOfRelease))
        {
            _books.Add(new Book(author, name, yearOfRelease));

            Console.Write("Книга успешна добавлена.\n\n");
        }
        else
        {
            Console.Write("Требуется ввести число.\n" +
                          "Не удалось добавить книгу.\n\n");
        }
    }

    private void RemoveBook()
    {

    }

    private void SearchBook()
    {

    }

    private bool TryGetBook()
    {
        return false;
    }
}

class Book
{
    private string _author;
    private string _name;
    private int _yearOfRelease;

    public Book(string author, string name, int yearOfRelease)
    {
        _author = author;
        _name = name;
        _yearOfRelease = yearOfRelease;
    }

    public void Show() =>
        Console.Write($"Автор: {_author} | Название: \"{_name}\" {_yearOfRelease} г.\n");
}