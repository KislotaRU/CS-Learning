using System;
using System.Data;
using System.IO;

namespace CS_JUNIOR.CleanCode_ExampleTask21_27.Model
{
    internal class PassportDatabase
    {
        private readonly string _connectionString;

        public PassportDatabase(string connectionString)
        {
            _connectionString = string.IsNullOrWhiteSpace(connectionString) ? throw new ArgumentException(nameof(connectionString)) : connectionString;
        }

        public DataTable GetPassportDataTableBy(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                throw new ArgumentException(nameof(hash));

            try
            {
                var connection = new SQLiteConnection(_connectionString);

                connection.Open();

                var command = CreateCommand(hash);
                var dataTable = ExecuteCommand(connection, command);

                connection.Close();

                return dataTable;
            }
            catch (SQLiteException ex)
            {
                throw new FileNotFoundException("Файл db.sqlite не найден. Положите файл в папку вместе с exe.", ex);
            }
        }

        private DataTable ExecuteCommand(SQLiteConnection sqliteConnection, string command)
        {
            if (sqliteConnection == null)
                throw new ArgumentNullException(nameof(sqliteConnection));

            if (string.IsNullOrEmpty(command))
                throw new ArgumentException(nameof(command));

            var sqliteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(command, sqliteConnection));
            DataTable dataTable = new DataTable();

            sqliteDataAdapter.Fill(dataTable);

            return dataTable;
        }

        private string CreateCommand(string hash)
        {
            if (string.IsNullOrEmpty(hash))
                throw new ArgumentException(nameof(hash));

            return string.Format($"select * from passports where num='{hash}' limit 1;");
        }
    }
}