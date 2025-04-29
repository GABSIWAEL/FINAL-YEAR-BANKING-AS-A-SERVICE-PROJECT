namespace OpenBanking_ATM_V1.CustomException
{
    public class ObpExceptionATM : Exception
    {
        public string ErrorCode { get; }

        private ObpExceptionATM(string errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public override string ToString()
        {
            return $"{ErrorCode}: {Message}";
        }

        // ==== Static Factory Methods for ATM Exceptions ====

        public static ObpExceptionATM UserNotLoggedIn()
            => new ObpExceptionATM("OBP-20001", "User not logged in. Authentication is required!");

        public static ObpExceptionATM BankNotFound()
            => new ObpExceptionATM("OBP-30001", "Bank not found. Please specify a valid value for BANK_ID.");

        public static ObpExceptionATM InvalidJsonFormat()
            => new ObpExceptionATM("OBP-10001", "Incorrect json format.");

        public static ObpExceptionATM UnknownError()
            => new ObpExceptionATM("OBP-50000", "Unknown Error.");

        public static ObpExceptionATM MissingRoles()
            => new ObpExceptionATM("OBP-20006", "User is missing one or more roles.");

        public static ObpExceptionATM ATMNotFound()
            => new ObpExceptionATM("OBP-30009", "ATM not found. Please specify a valid value for ATM_ID.");

        // You can add more custom ATM exceptions here if needed...
    }
}