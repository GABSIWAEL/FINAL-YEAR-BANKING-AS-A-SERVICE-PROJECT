namespace OpenBanking_BRANCH_V1.CustomException
{
    public class ObpExceptionBRANCH : Exception
    {
        public string ErrorCode { get; }

        private ObpExceptionBRANCH(string errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public override string ToString()
        {
            return $"{ErrorCode}: {Message}";
        }

        // ==== Static Factory Methods for BRANCH Exceptions ====

        public static ObpExceptionBRANCH BranchNotFound()
            => new ObpExceptionBRANCH("OBP-300010", "Branch not found. Please specify a valid value for BRANCH_ID. Or License may not be set. meta.license.id and meta.license.name can not be empty.");

        public static ObpExceptionBRANCH UnknownError()
            => new ObpExceptionBRANCH("OBP-50000", "Unknown Error.");

        public static ObpExceptionBRANCH BankNotFound()
            => new ObpExceptionBRANCH("OBP-30001", "Bank not found. Please specify a valid value for BANK_ID.");

        public static ObpExceptionBRANCH NoBranchesAvailable()
            => new ObpExceptionBRANCH("OBP-32001", "No branches available. License may not be set.");

        // Add more exceptions here if necessary...
    }
}