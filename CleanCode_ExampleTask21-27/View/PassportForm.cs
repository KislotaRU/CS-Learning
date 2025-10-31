using System;
using System.IO;
using System.Reflection;
using CS_JUNIOR.CleanCode_ExampleTask21_27.Model;
using CS_JUNIOR.CleanCode_ExampleTask21_27.Presentor;

namespace CS_JUNIOR.CleanCode_ExampleTask21_27.View
{
    internal class PassportForm
    {
        private readonly PassportPresenter _passportPresenter;

        public PassportForm()
        {
            string connectionString = CreateConnectionString();
            var passportDatabase = new PassportDatabase(connectionString);
            IHashCalculator hashCalculator = new HashCalculatorSha256();
            var passportService = new PassportService(passportDatabase, hashCalculator);
            _passportPresenter = new PassportPresenter(passportService);
        }

        public string Text { get; private set; }

        private void HandleClick(object sender, EventArgs e)
        {
            string result = _passportPresenter.GetStatusAccess(Text);

            MessageBox.Show(result);
        }

        private string CreateConnectionString()
        {
            string databasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return $"Data Source={databasePath}\\db.sqlite";
        }
    }
}