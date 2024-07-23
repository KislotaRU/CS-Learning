using System;

namespace CS_JUNIOR
{
    class BossFight
    {
        static void Main()
        {
            const int MoveCountSpellBlizzardKrampus = 5;
            const int MoveCountSpellDeafeningRoar = 3;
            const int MoveCountSpellPolarBearTotem = 12;
            const int MoveCountSpellBlockOfIce = 1;
            const int MoveCountSpellDragonArmor = 1;
            const int BonusMoveSpellDeafeningRoar = 2;
            const string SpellBlizzardKrampus = "Метель Крампуса";
            const string SpellPolarBearTotem = "Тотем полярного медведя";
            const string SpellDeafeningRoar = "Оглушительный рёв";
            const string SpellSharpTalons = "Острые когти";
            const string SpellBlockOfIce = "Глыба льда";
            const string SpellIceSpikes = "Ледяные шипы";
            const string SpellDragonBite = "Укус дракона";
            const string SpellDragonArmor = "Броня дракона";

            Random random = new Random();
            int validSpellBlizzardKrampus = 0;
            int validSpellDeafeningRoar = 0;
            int validSpellPolarBearTotem = 0;
            int validSpellBlockOfIce = 0;
            int validSpellDragonArmor = 0;
            int numberOfSpellsPlayer = 6;
            int numberOfSpellsBoss = 2;
            int bossDamage = 35;
            int playerDamage = 25;
            float dodgeChance = 0.3f;
            float chanceToStun = 0.25f;
            float chanceRetaliatoryDamage = 0.70f;
            float retaliatoryDamage = 0.5f;
            float playerDamageRatio = 2.5f;
            float bossHealthRecovery = 0.2f;
            float bossHealth = 300;
            float playerHealth = 120;
            string userInput;
            string bossInput;
            bool isPolarBearTotem = false;
            bool isStunned = false;
            bool isProtectionPlayer = false;
            bool isProtectionBoss = false;
            bool isPlayerDodge = false;
            bool isRetaliatoryDamage = false;

            Console.WriteLine("Вы Маг Льда. Перед вами стоит Дракон, который не даёт вам покоя. Вам нужно его победить.\n");

            while (playerHealth > 0 && bossHealth > 0)
            {
                Console.WriteLine($"В арсенале у вас есть {numberOfSpellsPlayer} заклинаний:" +
                                  $"\n\t{SpellBlizzardKrampus} - шанс {dodgeChance} увернуться от удара. Действует {MoveCountSpellBlizzardKrampus} ходов.\n" +
                                  $"\n\t{SpellPolarBearTotem} - позволяет использовать {SpellSharpTalons}. Действует {MoveCountSpellPolarBearTotem} ходов.\n" +
                                  $"\n\t{SpellDeafeningRoar} - шанс {chanceToStun} оглушить противника на {MoveCountSpellDeafeningRoar} хода. Больше на {BonusMoveSpellDeafeningRoar} " +
                                  $"\n\tхода при действии {SpellBlizzardKrampus}.\n" +
                                  $"\n\t{SpellSharpTalons} - наносит противнику {playerDamage} урона. Урон больше в {playerDamageRatio} раза при" +
                                  $"\n\tдействии {SpellDeafeningRoar}.\n" +
                                  $"\n\t{SpellBlockOfIce} - абсолютная защита на {MoveCountSpellBlockOfIce} ход.\n" +
                                  $"\n\t{SpellIceSpikes} - шанс {chanceRetaliatoryDamage} нанести в ответ {retaliatoryDamage} от полученного урона.\n");

                Console.WriteLine($"\n\n\t\t\t\tТекущее здоровье игрока: {playerHealth}" +
                                  $"\n\t\t\t\tТекущее здоровье дракона: {bossHealth}\n");

                if (validSpellBlizzardKrampus > 0)
                    Console.WriteLine($"Действие {SpellBlizzardKrampus} закончится через {validSpellBlizzardKrampus}.");
                else
                    isPlayerDodge = false;

                if (validSpellPolarBearTotem > 0)
                    Console.WriteLine($"Действие {SpellPolarBearTotem} закончится через {validSpellPolarBearTotem}.");
                else
                    isPolarBearTotem = false;

                if (validSpellDeafeningRoar > 0)
                    Console.WriteLine($"Действие {SpellDeafeningRoar} закончится через {validSpellDeafeningRoar}.");
                else
                    isStunned = false;

                if (isRetaliatoryDamage == true)
                    Console.WriteLine($"Действуют {SpellIceSpikes}.");

                if (validSpellBlockOfIce > 0)
                    Console.WriteLine($"Действие {SpellBlockOfIce} закончится через {validSpellBlockOfIce}.");
                else
                    isProtectionPlayer = false;

                if (validSpellDragonArmor > 0)
                    Console.WriteLine($"У дракона действует {SpellDragonArmor}.");
                else
                    isProtectionBoss = false;

                Console.Write("\nВведите название заклинания: ");
                userInput = Console.ReadLine();
                Console.WriteLine();

                switch (userInput)
                {
                    case SpellBlizzardKrampus:
                        Console.Write($"Игрок: использовал {SpellBlizzardKrampus}. ");

                        if (dodgeChance >= random.NextDouble())
                        {
                            Console.WriteLine("(Упех)");
                            isPlayerDodge = true;
                            validSpellBlizzardKrampus = MoveCountSpellBlizzardKrampus;
                        }
                        else
                        {
                            Console.WriteLine("(Провал)");
                        }

                        break;
                    case SpellPolarBearTotem:
                        Console.Write($"Игрок: использовал {SpellPolarBearTotem}. ");

                        if (isPolarBearTotem == false)
                        {
                            Console.WriteLine("(Успех)");
                            isPolarBearTotem = true;
                            validSpellPolarBearTotem = MoveCountSpellPolarBearTotem;
                        }
                        else
                        {
                            Console.WriteLine("(Уже стоит)");
                        }

                        break;
                    case SpellDeafeningRoar:
                        Console.Write($"Игрок: использовал {SpellDeafeningRoar}. ");

                        if (isProtectionBoss == false)
                        {
                            if (chanceToStun >= random.NextDouble())
                            {
                                Console.WriteLine("(Успех)");
                                isStunned = true;

                                if (isPlayerDodge == true)
                                    validSpellDeafeningRoar = MoveCountSpellDeafeningRoar + BonusMoveSpellDeafeningRoar;
                                else
                                    validSpellDeafeningRoar = MoveCountSpellDeafeningRoar;
                            }
                            else
                            {
                                Console.WriteLine("(Провал)");
                            }
                        }
                        else
                        {
                            Console.WriteLine("(Дракон защитился)");
                        }

                        break;
                    case SpellSharpTalons:
                        Console.Write($"Игрок: использовал {SpellSharpTalons}. ");

                        if (isPolarBearTotem == true)
                        {
                            if (isProtectionBoss == false)
                            {
                                Console.WriteLine("(Успех)");

                                if (isStunned == true)
                                    bossHealth -= playerDamage * playerDamageRatio;
                                else
                                    bossHealth -= playerDamage;
                            }
                            else
                            {
                                Console.WriteLine("(Дракон защитился и восстановил часть здоровья)");
                                bossHealth += playerDamage * bossHealthRecovery;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"(Провал. Необходим {SpellPolarBearTotem})");
                        }

                        break;
                    case SpellBlockOfIce:
                        Console.Write($"Игрок: использовал {SpellBlockOfIce}. ");

                        if (isProtectionPlayer == false)
                        {
                            Console.WriteLine("(Успех)");
                            isProtectionPlayer = true;
                            validSpellBlockOfIce = MoveCountSpellBlockOfIce;
                        }
                        else
                        {
                            Console.WriteLine("(Игрок уже под защитой)");
                        }

                        break;
                    case SpellIceSpikes:
                        Console.Write($"Игрок: использовал {SpellIceSpikes}. ");

                        if (isRetaliatoryDamage == false)
                        {
                            Console.WriteLine("(Успех)");
                            isRetaliatoryDamage = true;
                        }
                        else
                        {
                            Console.WriteLine("(Уже действует)");
                        }

                        break;
                    default:
                        Console.WriteLine("\t\t\t\tНеизвестное заклинание.");
                        Console.WriteLine("\nДля следующего хода нажмите любую клавишу...");
                        Console.ReadLine();
                        Console.Clear();
                        continue;
                }

                validSpellDragonArmor--;

                if (random.Next(numberOfSpellsBoss) == 0)
                    bossInput = SpellDragonBite;
                else
                    bossInput = SpellDragonArmor;

                if (bossHealth > 0)
                {
                    if (isStunned == false)
                    {
                        switch (bossInput)
                        {
                            case SpellDragonBite:
                                Console.Write($"Дракон: использовал {SpellDragonBite}. ");

                                if (isPlayerDodge == false && isProtectionPlayer == false)
                                {
                                    if ((isRetaliatoryDamage == true) && (chanceRetaliatoryDamage >= random.NextDouble()))
                                    {
                                        playerHealth -= bossDamage;
                                        bossHealth -= bossDamage * retaliatoryDamage;
                                        isRetaliatoryDamage = false;
                                    }
                                    else
                                    {
                                        playerHealth -= bossDamage;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("(Игрок увернулся)");
                                }

                                break;
                            case SpellDragonArmor:
                                Console.WriteLine($"Дракон: использовал {SpellDragonArmor}.");

                                if (isProtectionBoss == false)
                                {
                                    isProtectionBoss = true;
                                    validSpellDragonArmor = MoveCountSpellDragonArmor;
                                }

                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Дракон под станом.");
                    }
                }

                validSpellBlizzardKrampus--;
                validSpellPolarBearTotem--;
                validSpellDeafeningRoar--;
                validSpellBlockOfIce--;

                Console.WriteLine("\nДля следующего хода нажмите любую клавишу...");
                Console.ReadLine();
                Console.Clear();
            }

            if (playerHealth < 0 && bossHealth < 0)
            {
                Console.WriteLine("\tНИЧЬЯ!\n");
            }
            else if (playerHealth < 0)
            {
                Console.WriteLine("\tДРАКОН ПОБЕДИЛ!\n");
            }
            else if (bossHealth < 0)
            {
                Console.WriteLine("\tИГРОК ПОБЕДИЛ!\n");
            }
        }
    }
}