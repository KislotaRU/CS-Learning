using System;

//Попрактикуйтесь в создании переменных, объявить 10 переменных разных типов.
//Напоминание: переменные именуются с маленькой буквы, если название состоит из нескольких слов, то комбинируем их следующим образом - названиеПеременной.
//Также имя всегда должно отражать суть того, что хранит переменная.
//Для сдачи ДЗ требуется сдать код, который вы можете загрузить на https://gist.github.com/ или https://pastebin.com/ Это не сайт https://github.com/
//где надо будет разбираться с работой git, а те сайты, на которые можно скопировать код.

namespace CS_JUNIOR
{
    class Variables
    {
        static void Main(string[] args)
        {
            string name = "Kislota_RU";
            char coinSymbol = '$';
            long moneyCount = 8;
            uint enemyCount = 3;
            int medicalKitCount = 0;
            byte medicalKitTreatment = 70;
            short cupcakePrice = 6;
            float healthPlayer = 100f;
            decimal timeReloadingWeapon = 7.5m;
            double damageTrap = 30;
            bool isAlivePlayer = true;
            bool isDamaged = false;
        }
    }
}