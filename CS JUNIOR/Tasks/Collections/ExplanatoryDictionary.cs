using System;
using System.Collections.Generic;

//Создать программу, которая принимает от пользователя слово и выводит его значение. 
//Если такого слова нет, то следует вывести соответствующее сообщение.

namespace CS_JUNIOR
{
    class ExplanatoryDictionary
    {
        static void Main()
        {
            const string CommandSearchWords = "Поиск по словам";
            const string CommandExit = "Выход";

            string userInput;
            bool isExit = false;

            Dictionary<string, string> words = new Dictionary<string, string>
            {
                { "Апломб", "Это излишняя самоуверненность в поведении, в речи." },
                { "Опрометчиво", "Действовать, поступать слишком поспешно, необдуманно." },
                { "Бутафория", "Это ненастоящий предмет, созданный для пьес на сцене." },
                { "Ненавязчиво", "Без назойливых приставаний, обращений к кому-либо." },
                { "Слащавость", "Чрезмерная сентиментальность, приторная любезность в поведении." },
                { "Сентиментальность", "Поверхностные, несложные эмоции." },
            };

            while (isExit == false)
            {
                Console.WriteLine("Доступные команды:" +
                                 $"\n\t{CommandSearchWords} - найти значение слова." +
                                 $"\n\t{CommandExit} - завершить программу.\n");

                Console.Write("Введите команду для выполнения: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandSearchWords:
                        SearchWord(words);
                        break;

                    case CommandExit:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда. Попробуйте ещё раз.");
                        break;
                }

                ClearConsole();
            }
        }

        static void SearchWord(Dictionary<string, string> dictionary)
        {
            string searchWord;

            Console.Write("Введите слово для поиска: ");
            searchWord = Console.ReadLine();

            if (dictionary.ContainsKey(searchWord))
                Console.WriteLine(dictionary[searchWord]);
            else
                Console.WriteLine("Такого слова нет.");
        }

        static void ClearConsole()
        {
            Console.ReadLine();
            Console.Clear();
        }
    }
}