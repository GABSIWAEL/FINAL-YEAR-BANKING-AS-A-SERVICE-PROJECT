namespace OpenBanking_CARD_V1.CustomException
{
    public class ObpExceptionCARD : Exception
    {
        public string ErrorCode { get; }

        private ObpExceptionCARD(string errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public override string ToString()
        {
            return $"{ErrorCode}: {Message}";
        }

        // ==== Static Factory Methods for CARD Exceptions ====

        public static ObpExceptionCARD UserNotLoggedIn()
            => new ObpExceptionCARD("OBP-20001", "User not logged in. Authentication is required!");

        public static ObpExceptionCARD BankNotFound()
            => new ObpExceptionCARD("OBP-30001", "Bank not found. Please specify a valid value for BANK_ID.");

        public static ObpExceptionCARD UnknownError()
            => new ObpExceptionCARD("OBP-50000", "Unknown Error.");

        public static ObpExceptionCARD MissingRoles()
            => new ObpExceptionCARD("OBP-20006", "User is missing one or more roles.");

        public static ObpExceptionCARD AccountNotFound()
            => new ObpExceptionCARD("OBP-30018", "Bank Account not found. Please specify valid values for BANK_ID and ACCOUNT_ID.");

        public static ObpExceptionCARD CardStatusNotReturned()
            => new ObpExceptionCARD("OBP-50212", "Connector did not return the set of status of credit card.");

        // Add more exceptions as needed...
    }
}