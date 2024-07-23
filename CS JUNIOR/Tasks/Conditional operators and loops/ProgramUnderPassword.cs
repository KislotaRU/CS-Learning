using System;

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