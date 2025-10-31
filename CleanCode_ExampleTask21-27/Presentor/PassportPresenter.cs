using System;
using CS_JUNIOR.CleanCode_ExampleTask21_27.Model;
using CS_JUNIOR.CleanCode_ExampleTask21_27.View;

namespace CS_JUNIOR.CleanCode_ExampleTask21_27.Presentor
{
    internal class PassportPresenter
    {
        private readonly PassportService _passportService;
        private readonly IPassportForm _passportForm;

        public PassportPresenter(PassportService passportService, IPassportForm passportForm)
        {
            _passportService = passportService ?? throw new ArgumentNullException(nameof(passportService));
            _passportForm = passportForm ?? throw new ArgumentNullException(nameof(passportForm));
        }

        public void ReadStatusMessageBy(Passport passport)
        {
            if (passport == null)
                throw new ArgumentNullException(nameof(passport));

            try
            {
                var status = _passportService.GetStatusBy(passport);
                var message = CreateStatusMessage(passport.Id, status);

                _passportForm.ShowStatusMessage(message);
            }
            catch (SQLiteException)
            {
                _passportForm.ShowError("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
            }
            catch (Exception)
            {
                _passportForm.ShowError("Ошибка");
            }
        }

        private string CreateStatusMessage(string passportId, StatusAccess statusAccess)
        {
            if (string.IsNullOrEmpty(passportId))
                throw new ArgumentException(nameof(passportId));

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