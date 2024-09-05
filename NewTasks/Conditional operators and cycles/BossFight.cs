using System;

/*
 * Перед вами Босс, у которого есть определенное количество жизней и атака. Атака может быть как всегда одной и той же,
 * так и определяться рандомом в начале раунда. У Босса обычная атака. Босс должен иметь возможность убить героя.
 * У героя есть 4 умения
 * 1. Обычная атака
 * 2. Огненный шар, который тратит ману
 * 3. Взрыв. Можно вызывать, только если был использован огненный шар. Для повторного применения надо повторно использовать огненный шар.
 * 4. Лечение. Восстанавливает здоровье и ману, но не больше их максимального значения. Можно использовать ограниченное число раз.
 * Если пользователь ошибся с вводом команды или не выполнилось условие, то герой пропускает ход и происходит атака Босса
 * Программа завершается только после смерти босса или смерти пользователя, а если у вас возможно одновременно убить друг друга, то надо сообщить о ничье. 
*/

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            const string CommandBaseAttack = "Базовая атака";
            const string CommandFireballAttack = "Огненный шар";
            const string CommandExplosionAttack = "Взрыв";
            const string CommandHealing = "Лечение";

            int playerMaxHealthPoints = 100;
            int playerMaxMagicPoints = 7;

            int playerHealthPoints = playerMaxHealthPoints;
            int playerMagicPoints = playerMaxMagicPoints;
            int playerBaseDamage = 15;

            int playerFireballDamage = 30;
            int playerExplosionDamage = 40;
            int playerEstusesCount = 2;
            int playerCostFireball = 2;
            int playerCostExplosion = 4;
            bool isUsedFireball = false;
            float playerRecoveryCoefficient = 0.5f;

            int bossHealthPoints = 210;
            int bossBaseDamage = 20;

            string userInput;

            Console.ForegroundColor = ConsoleColor.White;

            while (playerHealthPoints > 0 && bossHealthPoints > 0)
            {
                Console.Clear();

                Console.Write("Перед вами Босс. Ваша задача одолеть его доступными действиями.\n\n");

                Console.Write($"Здоровье Игрока: {playerHealthPoints}\n" +
                              $"Здоровье Босса: {bossHealthPoints}\n\n");

                Console.Write("Доступные действия:\n" +
                            $"\t{CommandBaseAttack} - Наносит стандартный урон.\n" +
                            $"\t{CommandFireballAttack} - Наносит урон огненным шаром.\n" +
                            $"\t{CommandExplosionAttack} - Позволяет создать взрыв ранее использованному огненному шару.\n" +
                            $"\t{CommandHealing} — Восстанавливает здоровье и ману.\n\n");

                Console.Write("Выберите действие: ");
                userInput = Console.ReadLine();

                Console.Write("\nАтакует Игрок.\n");

                switch (userInput)
                {
                    case CommandBaseAttack:
                        bossHealthPoints -= playerBaseDamage;
                        Console.Write($"Игрок нанёс урон по Боссу в размере {playerBaseDamage}\n");
                        break;

                    case CommandFireballAttack:
                        if (playerMagicPoints >= playerCostFireball)
                        {
                            isUsedFireball = true;
                            playerMagicPoints -= playerCostFireball;
                            bossHealthPoints -= playerFireballDamage;
                            Console.Write($"Игрок нанёс урон по Боссу в размере {playerFireballDamage}\n");
                        }
                        else
                        {
                            Console.Write($"Требуется кол-во очков маны в размере {playerCostFireball}.\n");
                        }

                        Console.Write($"Актуальное кол-во очков маны: {playerMagicPoints}/{playerMaxMagicPoints}.\n");
                        break;

                    case CommandExplosionAttack:
                        if (playerMagicPoints >= playerCostExplosion)
                        {
                            if (isUsedFireball)
                            {
                                isUsedFireball = false;
                                playerMagicPoints -= playerCostExplosion;
                                bossHealthPoints -= playerExplosionDamage;
                                Console.Write($"Игрок нанёс урон по Боссу в размере {playerExplosionDamage}\n");
                            }
                            else
                            {
                                Console.Write($"Требуется ранее использование действия \"{CommandFireballAttack}\"\n");
                            }
                        }
                        else
                        {
                            Console.Write($"Требуется кол-во очков маны в размере {playerCostExplosion}.\n");
                        }

                        Console.Write($"Актуальное кол-во очков маны: {playerMagicPoints}/{playerMaxMagicPoints}.\n");
                        break;

                    case CommandHealing:
                        if (playerEstusesCount > 0)
                        {
                            playerEstusesCount--;

                            playerHealthPoints += (int)(playerMaxHealthPoints * playerRecoveryCoefficient);

                            if (playerHealthPoints > playerMaxHealthPoints)
                                playerHealthPoints = playerMaxHealthPoints;

                            playerMagicPoints += (int)(playerMaxMagicPoints * playerRecoveryCoefficient);

                            if (playerMagicPoints > playerMaxMagicPoints)
                                playerMagicPoints = playerMaxMagicPoints;

                            Console.Write("Игрок успешно восстановил здоровье и ману.\n");
                        }
                        else
                        {
                            Console.Write("Эстус с восстановлением здоровья и маны закончился.\n");
                        }

                        Console.Write($"Актуальное кол-во эстусов: {playerEstusesCount}.\n");
                        break;

                    default:
                        Console.Write("Неизвестное действие. Вы пропускаете ход.\n");
                        break;
                }

                if (bossHealthPoints > 0)
                {
                    Console.Write("\nАтакует Босс.\n");

                    playerHealthPoints -= bossBaseDamage;
                    Console.Write($"Босс нанёс урон по Игроку в размере {bossBaseDamage}.\n");
                }

                Console.ReadKey();
            }

            if (playerHealthPoints > 0)
                Console.Write("Игрок одержал верх над боссом.\n\n");
            else
                Console.Write("Босс одержал верх над игроком.\n\n");
        }
    }
}