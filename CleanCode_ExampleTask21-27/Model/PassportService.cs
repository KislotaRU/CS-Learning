using System;

namespace CS_JUNIOR.CleanCode_ExampleTask21_27.Model
{
    internal class PassportService
    {
        private readonly PassportDatabase _passportDatabase;
        private readonly IHashCalculator _hashCalculator;

        public PassportService(PassportDatabase passportDatabase, IHashCalculator hashCalculator)
        {
            _passportDatabase = passportDatabase ?? throw new ArgumentNullException(nameof(passportDatabase));
            _hashCalculator = hashCalculator ?? throw new ArgumentNullException(nameof(hashCalculator));
        }

        public string GetResulStatusBy(string passportId)
        {
            if (string.IsNullOrWhiteSpace(passportId))
                throw new ArgumentException(nameof(passportId));

            if (IsValidPassportId(passportId) == false)
                throw new ArgumentException(nameof(passportId));

            string normalizedId = NormalizePassportId(passportId);
            string hash = _hashCalculator.GetHash(normalizedId);
            var status = _passportDatabase.GetStatusPassportBy(hash);

            return CreateStatusMessage(passportId, status);
        }

        private bool IsValidPassportId(string passportId)
        {
            if (string.IsNullOrWhiteSpace(passportId))
                throw new ArgumentException(nameof(passportId));

            int minLengthId = 10;
            string normalized = NormalizePassportId(passportId);

            return normalized.Length >= minLengthId;
        }

        private string NormalizePassportId(string passportId)
        {
            if (string.IsNullOrWhiteSpace(passportId))
                throw new ArgumentException(nameof(passportId));

            string whiteSpace = " ";

            return passportId.Trim().Replace(whiteSpace, string.Empty);
        }

        private string CreateStatusMessage(string passportId, StatusAccess statusAccess)
        {
            string message = string.Empty;

            if (statusAccess == StatusAccess.Granted)
                message = $"По паспорту «{passportId}» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН";
            else if (statusAccess == StatusAccess.Denied)
                message = $"По паспорту «{passportId}» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ";
            else if (statusAccess == StatusAccess.NotFound)
                message = $"Паспорт «{passportId}» в списке участников дистанционного голосования НЕ НАЙДЕН";

            if (string.IsNullOrEmpty(message))
                throw new ArgumentOutOfRangeException(nameof(statusAccess));

            return message;
        }
    }
}