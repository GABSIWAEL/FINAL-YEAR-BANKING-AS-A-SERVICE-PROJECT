using System;
using System.Collections.Generic;
namespace OpenBanking_ACCOUNT_V1.CustomExceptions
{


public class ObpException : Exception
{
    public string ErrorCode { get; }
    private ObpException(string errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }
    public override string ToString()
    {
        return $"{ErrorCode}: {Message}";
    }
    public static ObpException UserNotLoggedIn()
        => new ObpException("OBP-20001", "User not logged in. Authentication is required!");

    public static ObpException UserNotFound()
        => new ObpException("OBP-20057", "User not found by userId.");

    public static ObpException UnknownError()
        => new ObpException("OBP-50000", "Unknown Error.");

    public static ObpException MissingRoles()
        => new ObpException("OBP-20006", "User is missing one or more roles.");

    public static ObpException BankNotFound()
        => new ObpException("OBP-30001", "Bank not found. Please specify a valid value for BANK_ID.");

    public static ObpException AccountAccessNotFound()
        => new ObpException("OBP-30065", "Cannot find account access.");

    public static ObpException AccountNotFound()
        => new ObpException("OBP-30018", "Bank Account not found. Please specify valid values for BANK_ID and ACCOUNT_ID.");

    public static ObpException ViewAccessDenied()
        => new ObpException("OBP-20017", "Current user does not have access to the view. Please specify a valid value for VIEW_ID.");

    public static ObpException CustomerNotFound()
        => new ObpException("OBP-30002", "Customer not found. Please specify a valid value for CUSTOMER_NUMBER.");

    public static ObpException AgentNotFound()
        => new ObpException("OBP-30201", "Agent not found. Please specify a valid value for AGENT_ID.");

    public static ObpException AgentAccountLinkNotFound()
        => new ObpException("OBP-30325", "Agent Account Link not found.");

    public static ObpException AgentsNotFound()
        => new ObpException("OBP-30326", "Agents not found.");

    public static ObpException CheckBookNotReturned()
        => new ObpException("OBP-50211", "Connector did not return the set of check book.");
}
}