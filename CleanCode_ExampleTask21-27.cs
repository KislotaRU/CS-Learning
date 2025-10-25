using System;
using System.Data;
using System.IO;
using System.Reflection;

namespace CS_JUNIOR
{
    internal class CleanCode_ExampleTask21_27
    {
        private enum PassportStatus
        {
            NotFound,
            Granted,
            Denied,
        }

        private void HandleClick(object sender, EventArgs e)
        {
            string passportID = " 34 77 333525";

            if (IsCorrectData(passportID) == false)
                throw new ArgumentException(nameof(passportID));

            result = SearchInDatabase(passportID);
        }

        private string SearchInDatabase(string passportID)
        {
            if (string.IsNullOrEmpty(passportID))
                throw new ArgumentException(nameof(passportID));

            string normalizedID = NormalizeData(passportID);
            string passportHash = CalculateHashBy(normalizedID);
            string result = string.Empty;
            PassportStatus passportStatus;

            try
            {
                passportStatus = GetPassportStatusFromDatabase(passportHash);
                result = CreateMessageBy(passportID, passportStatus);
            }
            catch (SQLiteException ex)
            {
                if (ex.ErrorCode != 1)
                    return result;

                MessageBox.Show("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
            }

            return result;
        }

        private PassportStatus GetPassportStatusFromDatabase(string passportHash)
        {
            if (string.IsNullOrEmpty(passportHash))
                throw new ArgumentException(nameof(passportHash));

            string connectionString = CreateConnectionString();
            string command = CreateCommand(passportHash);
            SQLiteConnection sqliteConnection = new SQLiteConnection(connectionString);

            sqliteConnection.Open();

            PassportStatus passportStatus = ExecuteCommand(sqliteConnection, command);

            sqliteConnection.Close();

            return passportStatus;
        }

        private bool IsCorrectData(string passportID)
        {
            if (string.IsNullOrWhiteSpace(passportID))
                throw new ArgumentException(nameof(passportID));

            int minLengthID = 10;
            string normalizedID;

            normalizedID = NormalizeData(passportID);

            return normalizedID.Length >= minLengthID;
        }

        private PassportStatus ExecuteCommand(SQLiteConnection sqliteConnection, string command)
        {
            if (sqliteConnection == null)
                throw new ArgumentNullException(nameof(sqliteConnection));

            if (string.IsNullOrEmpty(command))
                throw new ArgumentException(nameof(command));

            var sqliteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(command, sqliteConnection));
            DataTable dataTable = new DataTable();

            sqliteDataAdapter.Fill(dataTable);

            return GetPassportStatus(dataTable);
        }

        private PassportStatus GetPassportStatus(DataTable dataTable)
        {
            if (dataTable == null)
                throw new ArgumentNullException(nameof(dataTable));

            bool hasAccess;

            if (dataTable.Rows.Count == 0)
                return PassportStatus.NotFound;

            hasAccess = Convert.ToBoolean(dataTable.Rows[0].ItemArray[1]);

            return hasAccess ? PassportStatus.Granted : PassportStatus.Denied;
        }

        private string NormalizeData(string passportID)
        {
            if (string.IsNullOrEmpty(passportID))
                throw new ArgumentException(nameof(passportID));

            string whiteSpace = " ";
            string normalizedID = passportID.Trim().Replace(whiteSpace, string.Empty);

            return normalizedID;
        }

        private string CalculateHashBy(string normalizedID)
        {
            if (string.IsNullOrEmpty(normalizedID))
                throw new ArgumentException(nameof(normalizedID));

            return Form.ComputeSha256Hash(normalizedID);
        }

        private string CreateMessageBy(string passportID, PassportStatus passportStatus)
        {
            if (string.IsNullOrEmpty(passportID))
                throw new ArgumentException(nameof(passportID));

            string result = string.Empty;

            if (passportStatus == PassportStatus.Granted)
                result = $"По паспорту «{passportID}» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН";
            else if (passportStatus == PassportStatus.Denied)
                result = $"По паспорту «{passportID}» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ";
            else if (passportStatus == PassportStatus.NotFound)
                result = $"Паспорт «{passportID}» в списке участников дистанционного голосования НЕ НАЙДЕН";

            if (string.IsNullOrEmpty(result))
                throw new ArgumentOutOfRangeException(nameof(passportStatus));

            return result;
        }

        private string CreateConnectionString()
        {
            string databasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            return string.Format($"Data Source={databasePath}\\db.sqlite");
        }

        private string CreateCommand(string passportHash)
        {
            if (string.IsNullOrEmpty(passportHash))
                throw new ArgumentException(nameof(passportHash));

            return string.Format($"select * from passports where num='{passportHash}' limit 1;");
        }
    }
}