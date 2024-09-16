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

    public static int GetInt()
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

class Storage
{
    private readonly List<Book> _books;

    public Storage()
    {
        _books = new List<Book>()
        {
            new Book("Джоан Роулинг", "Гарри Поттер и узник Азкабана", 1999),
            new Book("Маргарет Митчелл", "Унесённые ветром", 1936),
            new Book("А.С. Пушкин", "Евгений Онегин", 1833),
            new Book("А.С. Пушкин", "Дубровкский", 1841),
            new Book("Н.В Гоголь", "Мёртвые души", 1842),
            new Book("Стивен Кинг", "Зелёная миля", 1996),
            new Book("Макс Фрай", "Болтливый мертвец", 1999),
        };
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
            Console.Write("\t\tМеню программы \"Хранилище книг\".\n\n");

            Console.Write("Доступные команды:\n");
            UserUtils.PrintMenu(menu);

            Console.Write("\nОжидается ввод: ");
            userInput = UserUtils.GetCommandMenu(menu);

            Console.Clear();

            switch (userInput)
            {
                case CommandShowBooks:
                    ShowBooks(_books);
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

            Console.ReadKey();
            Console.Clear();
        }
    }

    private void ShowBooks(List<Book> books)
    {
        Console.Write("Книги:\n");

        int numberBook = 1;

        foreach(Book book in books)
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
        int yearOfRelease;

        Console.Write("Введите автора: ");
        author = Console.ReadLine();

        Console.Write("Введите название книги: ");
        name = Console.ReadLine();

        Console.Write("Введите год выпуска: ");
        yearOfRelease = UserUtils.GetInt();

        _books.Add(new Book(author, name, yearOfRelease));
        Console.Write("Книга успешна добавлена.\n\n");
    }

    private void RemoveBook()
    {
        ShowBooks(_books);

        if (TryGetBook(out Book book))
        {
            _books.Remove(book);
            Console.Write("Книга успешна удалена.\n\n");
        }
        else
        {
            Console.Write("Не удалось удалить книгу.\n\n");
        }
    }

    private void SearchBook()
    {
        const string CommandSearchBookByAuthor = "Автор";
        const string CommandSearchBookByName = "Название";
        const string CommandSearchBookByYearOfRelease = "Год выпуска";

        string[] menu = new string[]
        {
            CommandSearchBookByAuthor,
            CommandSearchBookByName,
            CommandSearchBookByYearOfRelease,
        };

        string userInput;

        List<Book> foundBooks = new List<Book>();

        Console.Write("\t\tПараметры поиска.\n\n");

        Console.Write("Доступные параметры:\n");
        UserUtils.PrintMenu(menu);

        Console.Write("\nОжидается ввод:\n");
        userInput = UserUtils.GetCommandMenu(menu);

        Console.Clear();

        switch (userInput)
        {
            case CommandSearchBookByAuthor:
                SearchByAuthor(foundBooks);
                break;

            case CommandSearchBookByName:
                SearchByName(foundBooks);
                break;

            case CommandSearchBookByYearOfRelease:
                SearchByYearOfRelease(foundBooks);
                break;

            default:
                Console.Write("Требуется ввести номер параметра или сам параметр.\n");
                break;
        }

        if (foundBooks.Count > 0)
        {
            Console.Write("Найденные книги по запросу.\n");
            ShowBooks(foundBooks);
        }
        else
        {
            Console.Write("Не удалось найти ни одной книги.\n");
        }
    }

    private void SearchByAuthor(List<Book> foundBooks)
    {
        string author;

        Console.Write("Введите автора: ");
        author = Console.ReadLine();

        foreach (Book book in _books)
            if (book.Author == author)
                foundBooks.Add(book);
    }

    private void SearchByName(List<Book> foundBooks)
    {
        string name;

        Console.Write("Введите название: ");
        name = Console.ReadLine();

        foreach (Book book in _books)
            if (book.Name == name)
                foundBooks.Add(book);
    }

    private void SearchByYearOfRelease(List<Book> foundBooks)
    {
        int yearOfRelease;

        Console.Write("Введите год выпуска: ");
        yearOfRelease = UserUtils.GetInt();

        foreach (Book book in _books)
            if (book.YearOfRelease == yearOfRelease)
                foundBooks.Add(book);
    }

    private bool TryGetBook(out Book book)
    {
        int numberBook;

        book = null;

        Console.Write("Введите номер книги: ");
        numberBook = UserUtils.GetInt();

        if (numberBook > 0 && numberBook <= _books.Count)
        {
            book = _books[numberBook - 1];
            return true;
        }
        else
        {
            Console.Write("Под таким номером нет книги.\n");
        }

        return false;
    }
}

class Book
{
    public Book(string author, string name, int yearOfRelease)
    {
        Author = author;
        Name = name;
        YearOfRelease = yearOfRelease;
    }

    public string Author { get; private set; }
    public string Name { get; private set; }
    public int YearOfRelease { get; private set; }

    public void Show() =>
        Console.Write($"Автор: {Author}".PadRight(25) + $"Книга: \"{Name}\" {YearOfRelease} г.\n");
}