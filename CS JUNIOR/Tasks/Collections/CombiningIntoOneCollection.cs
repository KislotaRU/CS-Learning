using System;
using System.Collections.Generic;

namespace CS_JUNIOR
{
    class CombiningIntoOneCollection
    {
        static void Main()
        {
            string[] damagePlayer = new string[] { "1", "2", "1", "4", "9", "7", "5", "6" };
            string[] damageEnemy = new string[] { "3", "2", "4", "2", "6", "8" };
            List<string> uniqueDamageUnits = new List<string>();

            RecordUniqueElements(uniqueDamageUnits, damagePlayer);
            RecordUniqueElements(uniqueDamageUnits, damageEnemy);

            PrintCollection(uniqueDamageUnits);
        }

        static void RecordUniqueElements(List<string> listUniqueElements, string[] arrayElements)
        {
            for (int i = 0; i < arrayElements.Length; i++)
            {
                if (listUniqueElements.Contains(arrayElements[i]) == false)
                {
                    listUniqueElements.Add(arrayElements[i]);
                }
            }
        }

        static void PrintCollection(List<string> list)
        {
            foreach (string item in list)
            {
                Console.Write($"{item} ");
            }
        }
    }
}