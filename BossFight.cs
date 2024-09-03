using System;

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

            const int FullValue = 100;
            const int HalfValue = 50;

            int playerMaxHealthPoints = 100;
            int playerMaxMagicPoints = 7;

            int playerHealthPoints = playerMaxHealthPoints;
            int playerMagicPoints = playerMaxMagicPoints;
            int playerBaseDamege = 15;

            int playerFireballDamege = 30;
            int playerExplosionDamege = 40;
            int playerEstusesCount = 2;
            int playerCostFireball = 2;
            int playerCostExplosion = 4;
            bool isUsedFireball = false;

            int bossHealthPoints = 210;
            int bossBaseDamege = 20;

            string userInput;

            Console.ForegroundColor = ConsoleColor.White;

            while (playerHealthPoints > 0 && bossHealthPoints > 0)
            {
                Console.Write("Перед вами Босс. Ваша задача одолеть его доступными действиями.\n\n");

                Console.Write($"Здоровье Игрока: {playerHealthPoints}\n" +
                              $"Здоровье Босса: {bossHealthPoints}\n\n");

                if (playerHealthPoints > 0)
                {
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
                            bossHealthPoints -= playerBaseDamege;
                            Console.Write($"Игрок нанёс урон по Боссу в размере {playerBaseDamege}\n");
                            break;

                        case CommandFireballAttack:
                            if (playerMagicPoints >= playerCostFireball)
                            {
                                isUsedFireball = true;
                                playerMagicPoints -= playerCostFireball;
                                bossHealthPoints -= playerFireballDamege;
                                Console.Write($"Игрок нанёс урон по Боссу в размере {playerFireballDamege}\n");
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
                                    bossHealthPoints -= playerExplosionDamege;
                                    Console.Write($"Игрок нанёс урон по Боссу в размере {playerExplosionDamege}\n");
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
                            float temporaryHalfValue;

                            if (playerEstusesCount > 0)
                            {
                                playerEstusesCount--;

                                temporaryHalfValue = HalfValue / FullValue;

                                playerHealthPoints += (int)(playerMaxHealthPoints * temporaryHalfValue);

                                if (playerHealthPoints > playerMaxHealthPoints)
                                    playerHealthPoints = playerMaxHealthPoints;

                                playerMagicPoints += (int)(playerMaxMagicPoints * temporaryHalfValue);

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

                        playerHealthPoints -= bossBaseDamege;
                        Console.Write($"Босс нанёс урон по Игроку в размере {bossBaseDamege}.\n");
                    }
                    else
                    {
                        bossHealthPoints = 0;
                        Console.Write("Игрок сделал фаталити Боссу. Босс не в состоянии продолжать бой.\n");
                    }
                }
                else
                {
                    playerHealthPoints = 0;
                    Console.Write("Босс сделал фаталити Игроку. Игрок не в состоянии продолжать бой.\n");
                }

                Console.ReadKey();
                Console.Clear();
            }

            if (playerHealthPoints > 0)
                Console.Write("Игрок одержал верх над боссом.\n");
            else if (bossHealthPoints > 0)
                Console.Write("Босс одержал верх над игроком.\n");
            else
                Console.Write("Ничья.\n");
        }
    }
}