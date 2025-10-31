using System;
using CS_JUNIOR.CleanCode_ExampleTask21_27.Model;

namespace CS_JUNIOR.CleanCode_ExampleTask21_27.Presentor
{
    internal class PassportPresenter
    {
        private readonly PassportService _passportService;

        public PassportPresenter(PassportService passportService)
        {
            _passportService = passportService ?? throw new ArgumentNullException(nameof(passportService));
        }

        public string GetStatusAccess(string passportId)
        {
            if (string.IsNullOrWhiteSpace(passportId))
                throw new ArgumentException(nameof(passportId));

            try
            {
                return _passportService.GetResulStatusBy(passportId);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
                return string.Empty;
            }
        }
    }
}