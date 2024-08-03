using System;
using System.Collections.Generic;
using System.IO;

//Создать хранилище книг.
//Каждая книга имеет название, автора и год выпуска (можно добавить еще параметры). 
//В хранилище можно добавить книгу, убрать книгу, показать все книги и показать 
//книги по указанному параметру (по названию, по автору, по году выпуска).
//Про указанный параметр, к примеру нужен конкретный автор, выбирается показ по авторам,
//запрашивается у пользователя автор и показываются все книги с этим автором.

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            StorageBooks storageBooks = new StorageBooks("Books");

            storageBooks.Work();
        }
    }

    class Book
    {
        private static int _id;

        public Book(string name, string author, int yearRelease)
        {
            Id = ++_id;
            Name = name;
            Author = author;
            YearRelease = yearRelease;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Author { get; private set; }
        public int YearRelease { get; private set; }

        public void ShowInfo()
        {
            Console.Write($"\nID: {Id}\t Год: {YearRelease}\t Автор: {Author}      \t Книга: \"{Name}\"");
        }
    }

    class StorageBooks
    {
        private const string ParameterName = "NameBook";
        private const string ParameterAuthor = "Author";
        private const string ParameterYearRelease = "YearRelease";

        private readonly List<Book> _library = new List<Book>();

        private string _userInput;

        public StorageBooks(string nameFile)
        {
            ReadFile(nameFile);
        }

        public void Work()
        {
            const string CommandAddBook = "Add";
            const string CommandRemoveBook = "Remove";
            const string CommandSearchBook = "Search";
            const string CommandShowBooks = "Show";
            const string CommandExit = "Exit";

            bool isWork = true;

            Console.WriteLine("Запущена работа с библиотекой.\n");

            while (isWork == true)
            {
                Console.WriteLine("\t\t\tМеню работы с библиотекой:" +
                                  $"\n\n\t> {CommandAddBook} - Добавить книгу." +
                                  $"\n\t> {CommandRemoveBook} - Выписать книгу." +
                                  $"\n\t> {CommandSearchBook} - Найти книгу." +
                                  $"\n\t> {CommandShowBooks} - Показать все книги." +
                                  $"\n\t> {CommandExit} - Завершить работу.\n");

                Console.Write("Ожидается ввод команды: ");
                _userInput = Console.ReadLine();

                switch (_userInput)
                {
                    case CommandAddBook:
                        AddBook();
                        break;

                    case CommandRemoveBook:
                        RemoveBook();
                        break;

                    case CommandSearchBook:
                        StartSearch();
                        break;

                    case CommandShowBooks:
                        ShowBooks();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("\nНеизветная команда! Попробуйте ещё раз.");
                        break;
                }

                Console.ReadKey(true);
                Console.Clear();
            }

            Console.WriteLine("Завершение работы...");
        }

        public void ShowBooks()
        {
            Console.WriteLine("\nВыведены все книги.");

            foreach (Book book in _library)
            {
                book.ShowInfo();
            }
        }

        public void AddBook(string[] bookData = null)
        {
            const int AmountBookData = 3;

            string name = "NULL";
            string author = "NULL";
            int yearRelease = -1;

            if (bookData != null)
            {
                int i = 0;

                if (bookData.Length == AmountBookData)
                {
                    int.TryParse(bookData[i], out yearRelease);
                    author = bookData[++i];
                    name = bookData[++i];
                }
            }
            else
            {
                Console.WriteLine("\nПроцесс добавление новой книги...\n");

                do
                {
                    Console.Write("Введите год выпуска: ");
                    _userInput = Console.ReadLine();
                }
                while (int.TryParse(_userInput, out yearRelease) == false);

                Console.Write("Введите Автора: ");
                author = Console.ReadLine();

                Console.Write("Введите название книги: ");
                name = Console.ReadLine();

                Console.WriteLine("\nПроцесс добавление новой книги завершён!");
            }

            _library.Add(new Book(name, author, yearRelease));
        }

        public void RemoveBook()
        {
            Console.WriteLine("Процесс удаления книги...");

            if (TryGetBook(out Book book) == true)
            {
                _library.Remove(book);
                Console.Write("\nДанная книга удалена:");
                book.ShowInfo();
            }

            Console.WriteLine("\nПроцесс удаления книги завершён!");
        }

        public void StartSearch()
        {
            const string CommandExit = "Exit";

            List<Book> foundBooks = new List<Book>();

            bool isSearch = true;

            Console.WriteLine("\nПроцесс поиска книги...");

            while (isSearch == true)
            {
                Console.WriteLine("\nПараметры поиска:" +
                                  $"\n\n\t> {ParameterName} - поиск по названию книги." +
                                  $"\n\t> {ParameterAuthor} - поиск по автору." +
                                  $"\n\t> {ParameterYearRelease} - поиск по дате выпуска." +
                                  $"\n\t> {CommandExit} - завершить поиск.\n");

                Console.Write("Выберите параметр поиска: ");
                _userInput = Console.ReadLine();

                switch (_userInput)
                {
                    case ParameterName:
                        Console.WriteLine("\nПоиск по названию книги.");
                        Search(foundBooks, _userInput);
                        break;

                    case ParameterAuthor:
                        Console.WriteLine("\nПоиск по Автору.");
                        Search(foundBooks, _userInput);
                        break;

                    case ParameterYearRelease:
                        Console.WriteLine("\nПоиск по дате выпуска.");
                        Search(foundBooks, _userInput);
                        break;

                    case CommandExit:
                        isSearch = false;
                        break;

                    default:
                        Console.WriteLine("\nНеизвестный параметр! Попробуйте ещё раз.");
                        break;
                }

                if (isSearch == true)
                {
                    Console.Write("Найденные книги:");

                    foreach (Book book in foundBooks)
                    {
                        book.ShowInfo();
                    }
                }

                foundBooks.Clear();
                Console.ReadLine();
                Console.Clear();
            }

            Console.WriteLine("\nПроцесс поиска книги завершён!");
        }

        private void Search(List<Book> foundBooks, string parameter = null)
        {
            Console.Write("Введите данные для поиска: ");
            _userInput = Console.ReadLine();

            if (parameter == ParameterName)
            {
                SearchNameBook(foundBooks);
            }
            else if (parameter == ParameterAuthor)
            {
                SearchAuthor(foundBooks);
            }
            else if (parameter == ParameterYearRelease)
            {
                SearchYearRelease(foundBooks);
            }
        }

        private void SearchNameBook(List<Book> foundBooks)
        {
            for (int i = 0; i < _library.Count; i++)
            {
                if (_library[i].Name.ToUpper() == _userInput.ToUpper())
                {
                    foundBooks.Add(_library[i]);
                }
            }
        }

        private void SearchAuthor(List<Book> foundBooks)
        {
            for (int i = 0; i < _library.Count; i++)
            {
                if (_library[i].Author.ToUpper() == _userInput.ToUpper())
                {
                    foundBooks.Add(_library[i]);
                }
            }
        }

        private void SearchYearRelease(List<Book> foundBooks)
        {
            int.TryParse(_userInput, out int yearRelease);

            for (int i = 0; i < _library.Count; i++)
            {
                if (_library[i].YearRelease == yearRelease)
                {
                    foundBooks.Add(_library[i]);
                }
            }
        }

        private bool TryGetBook(out Book book)
        {
            int id;

            book = null;

            do
            {
                Console.Write("Введите Id книги: ");
                _userInput = Console.ReadLine();
            }
            while (int.TryParse(_userInput, out id) == false);

            for (int i = 0; i < _library.Count; i++)
            {
                if (_library[i].Id == id)
                {
                    book = _library[i];
                    return true;
                }
            }

            Console.WriteLine("Книга не найдена.");
            return false;
        }

        private void ReadFile(string nameFile)
        {
            string[] books;

            if (File.Exists($"Database/{nameFile}.txt") == true)
                books = File.ReadAllLines($"Database/{nameFile}.txt");
            else
                return;

            for (int i = 0; i < books.Length; i++)
            {
                string[] bookData = books[i].Split('|');

                AddBook(bookData);
            }
        }
    }
}