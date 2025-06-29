using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenBanking_ACCOUNT_V1.Dtos;
using OpenBanking_ACCOUNT_V1.Models;
using OpenBanking_ACCOUNT_V1.Repository;
using OpenBanking_ACCOUNT_V1.CustomExceptions;
using System;
using System.Threading.Tasks;
using AutoMapper;

namespace OpenBanking_ACCOUNT_V1.Controllers
{
    [ApiController]
    [Route("obp/v5.0.0")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(
            IAccountRepository accountRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<AccountsController> logger)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("banks/{bankId}/accounts/{accountId}/{viewId}/account")]
        public async Task<IActionResult> GetAccount(string bankId, string accountId, string viewId)
        {
            _logger.LogInformation("Start GetAccount. BankId: {BankId}, AccountId: {AccountId}, ViewId: {ViewId}", bankId, accountId, viewId);

            try
            {
                _logger.LogInformation("Fetching account details from repository...");
                var account = await _accountRepository.GetAccountWithDetailsAsync(bankId, accountId);

                if (account == null)
                {
                    _logger.LogWarning("Account not found for BankId: {BankId}, AccountId: {AccountId}", bankId, accountId);
                    _logger.LogWarning("Throwing OBP Exception of type: {ExceptionType} with code: {ErrorCode}", nameof(ObpException), "OBP-30018");
                    throw ObpException.AccountNotFound();
                }

                _logger.LogInformation("Validating view access...");
                if (account.views_available == null || account.views_available.id != viewId)
                {
                    _logger.LogWarning("View access denied. Provided ViewId: {ViewId}, Expected: {ExpectedViewId}", viewId, account.views_available?.id);
                    _logger.LogWarning("Throwing OBP Exception of type: {ExceptionType} with code: {ErrorCode}", nameof(ObpException), "OBP-20017");
                    throw ObpException.ViewAccessDenied();
                }

                _logger.LogInformation("Mapping account to DTO...");
                var dto = _mapper.Map<AccountbyIdFull>(account);

                _logger.LogInformation("Successfully fetched and returned account.");
                return Ok(dto);
            }
            catch (ObpException ex)
            {
                _logger.LogWarning(ex, "OBP Exception ({ExceptionType}): {ErrorCode} - {Message}", ex.GetType().Name, ex.ErrorCode, ex.Message);
                return ex.ErrorCode switch
                {
                    "OBP-20001" => Unauthorized(new { code = ex.ErrorCode, message = ex.Message }),
                    "OBP-20017" => StatusCode(403, new { code = ex.ErrorCode, message = ex.Message }),
                    "OBP-30001" => NotFound(new { code = ex.ErrorCode, message = ex.Message }),
                    "OBP-30018" => NotFound(new { code = ex.ErrorCode, message = ex.Message }),
                    _ => StatusCode(500, new { code = ex.ErrorCode, message = ex.Message })
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception ({ExceptionType}) in GetAccount: {Message}", ex.GetType().Name, ex.Message);
                return StatusCode(500, new
                {
                    code = ObpException.UnknownError().ErrorCode,
                    message = ObpException.UnknownError().Message,
                    details = ex.Message
                });
            }
        }


    }
}
