using System;

namespace CS_JUNIOR.CleanCode_ExampleTask21_27.Model
{
    internal class Passport
    {
        private const int MinLength = 10;

        public Passport(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException(nameof(id));

            if (IsValidId(id) == false)
                throw new ArgumentException(nameof(id));

            Id = id;
        }

        public string Id { get; }

        private bool IsValidId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException(nameof(id));

            string normalizedId = Normalize(id);

            return normalizedId.Length >= MinLength;
        }

        private string Normalize(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentException(nameof(data));

            string whiteSpace = " ";

            return data.Trim().Replace(whiteSpace, string.Empty);
        }
    }
}