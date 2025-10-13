using System;

//Создайте переменную типа string, в которой хранится пароль для доступа к тайному сообщению.
//Пользователь вводит пароль, далее происходит проверка пароля на правильность, и если пароль неверный,
//то попросите его ввести пароль ещё раз. Если пароль подошёл, выведите секретное сообщение.
//Если пользователь неверно ввел пароль 3 раза, программа завершается.

namespace CS_JUNIOR
{
    class ProgramUnderPassword
    {
        static void Main(string[] args)
        {
            string password = "*=E6F)qm>Y-+FAXToZP4)31EM";
            string userInput;
            string secretMessang = "Это невозможно... Но что ж, здесь координаты клада.\n";
            int countAttempts = 3;
            int attemptsLeft;

            for (int i = countAttempts; i > 0; i--)
            {
                attemptsLeft = i - 1;
                Console.Write("Введите пароль: ");
                userInput = Console.ReadLine();

                if (userInput == password)
                {
                    Console.WriteLine(secretMessang);
                    break;
                }
                else
                {
                    Console.Write("Пароль не верный.\n" +
                                 $"Осталось попыток: {attemptsLeft}.\n\n");
                }
            }
        }
    }
}