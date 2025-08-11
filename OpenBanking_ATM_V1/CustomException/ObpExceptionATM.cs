using System.Runtime.Serialization;

namespace OpenBanking_ATM_V1.CustomException
{
    public class ObpExceptionATM : Exception, ISerializable
    {
        public string ErrorCode { get; }
        public int StatusCode { get; } // Add this property to fix the error

        public ObpExceptionATM(string message, int statusCode, string errorCode = null) : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, StatusCode: {StatusCode}, ErrorCode: {ErrorCode}";
        }

        public static ObpExceptionATM UserNotLoggedIn()
        {
            return new ObpExceptionATM("User is not logged in.", 401, "UserNotLoggedIn");
        }

        public static ObpExceptionATM BankNotFound()
        {
            return new ObpExceptionATM("Bank not found.", 404, "BankNotFound");
        }
        public static ObpExceptionATM AtmNotFound()
        {
            return new ObpExceptionATM("ATM not found.", 404, "AtmNotFound");
        }

        public static ObpExceptionATM InvalidJsonFormat()
        {
            return new ObpExceptionATM("Invalid JSON format.", 400, "InvalidJsonFormat");
        }

        public static ObpExceptionATM UnknownError()
        {
            return new ObpExceptionATM("An unknown error occurred.", 500, "UnknownError");
        }

        public static ObpExceptionATM MissingRoles()
        {
            return new ObpExceptionATM("Missing required roles.", 403, "MissingRoles");
        }

        public static ObpExceptionATM ATMNotFound()
        {
            return new ObpExceptionATM("ATM not found.", 404, "ATMNotFound");
        }
    }
}