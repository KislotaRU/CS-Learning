using System;
using System.Security.Cryptography;
using System.Text;

namespace CS_JUNIOR.CleanCode_ExampleTask21_27.Model
{
    internal class HashCalculatorSha256 : IHashCalculator
    {
        public string GetHash(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentException(nameof(data));

            SHA256 sha1 = SHA256.Create();

            byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(data));

            string hash = string.Empty;

            foreach (byte @byte in hashBytes)
            {
                hash += @byte.ToString();
            }

            return hash;
        }
    }
}