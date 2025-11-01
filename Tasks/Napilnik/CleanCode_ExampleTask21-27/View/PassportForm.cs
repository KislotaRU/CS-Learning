using System;
using System.IO;
using System.Reflection;
using CS_JUNIOR.CleanCode_ExampleTask21_27.Model;
using CS_JUNIOR.CleanCode_ExampleTask21_27.Presentor;

namespace CS_JUNIOR.CleanCode_ExampleTask21_27.View
{
    internal class PassportForm : IPassportForm
    {
        private readonly PassportPresenter _passportPresenter;

        public PassportForm()
        {
            string connectionString = CreateConnectionString();
            var passportDatabase = new PassportDatabase(connectionString);
            IHashCalculator hashCalculator = new HashCalculatorSha256();
            var passportService = new PassportService(passportDatabase, hashCalculator);
            _passportPresenter = new PassportPresenter(passportService, this);
        }

        public void ShowStatusMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException(nameof(message));

            Console.WriteLine(message);
        }

        public void ShowError(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
                throw new ArgumentException(nameof(errorMessage));

            Console.WriteLine(errorMessage);
        }

        private void HandleClick(object sender, EventArgs e)
        {
            _passportPresenter.ReadStatusMessageBy();
        }

        private string CreateConnectionString()
        {
            string databasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return $"Data Source={databasePath}\\db.sqlite";
        }
    }
}