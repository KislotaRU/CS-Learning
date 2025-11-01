using System;
using System.Data;

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

        public StatusAccess GetStatusBy(Passport passport)
        {
            if (passport == null)
                throw new ArgumentNullException(nameof(passport));

            string hash = _hashCalculator.GetHash(passport.Id);
            var dataTable = _passportDatabase.GetPassportDataTableBy(hash);

            return GetPassportStatus(dataTable);
        }

        private StatusAccess GetPassportStatus(DataTable dataTable)
        {
            if (dataTable == null)
                throw new ArgumentNullException(nameof(dataTable));

            bool hasAccess;

            if (dataTable.Rows.Count == 0)
                return StatusAccess.NotFound;

            hasAccess = Convert.ToBoolean(dataTable.Rows[0].ItemArray[1]);

            return hasAccess ? StatusAccess.Granted : StatusAccess.Denied;
        }
    }
}