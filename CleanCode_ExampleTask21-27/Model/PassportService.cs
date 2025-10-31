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

        public StatusAccess GetStatusBy(Passport passport)
        {
            if (passport == null)
                throw new ArgumentNullException(nameof(passport));

            string hash = _hashCalculator.GetHash(passport.Id);

            return _passportDatabase.GetStatusPassportBy(hash);
        }
    }
}