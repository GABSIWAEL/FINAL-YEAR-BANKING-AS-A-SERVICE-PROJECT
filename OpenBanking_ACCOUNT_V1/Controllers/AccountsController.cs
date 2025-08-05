using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenBanking_ACCOUNT_V1.Dtos;
using OpenBanking_ACCOUNT_V1.Models;
using OpenBanking_ACCOUNT_V1.Repository;
using OpenBanking_ACCOUNT_V1.CustomExceptions;
using System;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using OpenBanking_ACCOUNT_V1.Shared.Services; // ✅ required
using OpenBanking_ACCOUNT_V1.Shared.Events; 

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
        private readonly RabbitMqPublisher _publisher;

        // ✅ inject it here


        public AccountsController(
            IAccountRepository accountRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<AccountsController> logger , RabbitMqPublisher publisher // ✅ inject it here
            )
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _publisher = publisher; // ✅ assign it

        }

        [HttpGet("banks/{bankId}/accounts/{accountId}/{viewId}/account")]
        [SwaggerOperation(
            Summary = "Get full account details" ,
            Description = "Returns detailed information for a specific account using bankId, accountId, and viewId."
            )]
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
                if (account.views_available == null )
                {
                    _logger.LogWarning("View access denied.");
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

        // *****************************************************************************************************************************************
        [HttpGet("banks/{bankId}/accounts")]
        [SwaggerOperation(
            Summary = "Get accounts at bank",
            Description = "Fetches all accounts under a specific bank using the bankId."
        )]
        public async Task<IActionResult> GetAccounts(string bankId)
        {
            _logger.LogInformation("Start GetAccounts. BankId: {BankId}", bankId);

            if (string.IsNullOrWhiteSpace(bankId))
            {
                _logger.LogWarning("BankId parameter is null or empty.");
                throw ObpException.BankNotFound();
            }

            try
            {
                _logger.LogInformation("Fetching account details from repository...");
                var accounts = await _accountRepository.GetAccountsInBank(bankId);

                if (accounts == null || !accounts.Any())
                {
                    _logger.LogWarning("No accounts found for BankId: {BankId}", bankId);
                    throw ObpException.AccountNotFound();
                }

                _logger.LogInformation("Mapping accounts to DTO...");
                var dto = _mapper.Map<List<AccountsAtBank>>(accounts);

                _logger.LogInformation("Successfully fetched and returned accounts.");
                return Ok(new { accounts = dto });
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
                _logger.LogError(ex, "Unhandled exception ({ExceptionType}) in GetAccounts: {Message}", ex.GetType().Name, ex.Message);
                return StatusCode(500, new
                {
                    code = ObpException.UnknownError().ErrorCode,
                    message = ObpException.UnknownError().Message,
                    details = ex.Message
                });
            }
        }

        // *****************************************************************************************************************************************
        [HttpGet("banks/{bankId}/balances")]
        [SwaggerOperation(
            Summary = "Get balances by bank",
            Description = "Returns balances of all accounts associated with the specified bankId."
        )]
        public async Task<IActionResult> GetAccountBalancesByBankId(string bankId)
        {
            _logger.LogInformation("Start GetAccountBalancesByBankId. BankId: {BankId}", bankId);

            if (string.IsNullOrWhiteSpace(bankId))
            {
                _logger.LogWarning("BankId parameter is null or empty.");
                throw ObpException.BankNotFound();
            }

            try
            {
                _logger.LogInformation("Fetching account details from repository...");
                var accounts = await _accountRepository.GetAccountBalancesByBankId(bankId);

                if (accounts == null || !accounts.Any())
                {
                    _logger.LogWarning("No accounts found for BankId: {BankId}", bankId);
                    throw ObpException.AccountNotFound();
                }

                _logger.LogInformation("Mapping AccountBalancesByBankId to DTO...");
                var dto = _mapper.Map<List<AccountBalancesByBANK_ID>>(accounts);

                _logger.LogInformation("Successfully fetched and returned accounts.");
                return Ok(new { accounts = dto });
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
                _logger.LogError(ex, "Unhandled exception ({ExceptionType}) in GetAccounts: {Message}", ex.GetType().Name, ex.Message);
                return StatusCode(500, new
                {
                    code = ObpException.UnknownError().ErrorCode,
                    message = ObpException.UnknownError().Message,
                    details = ex.Message
                });
            }
        }

        // *****************************************************************************************************************************************


        [HttpGet("banks/{bankId}/agents/{agent_id}")]
        [SwaggerOperation(
            Summary = "Get full Agent details",
            Description = "Returns detailed information for a specific agent using bankId and agent_id"
        )]
        public async Task<IActionResult> GetAgentByBankIdAndAgentIdAsync(string bankId, string agent_id)
        {
            _logger.LogInformation("Start GetAgentByBankIdAndAgentIdAsync. BankId: {BankId}, AgentId: {AgentId}", bankId, agent_id);

            if (string.IsNullOrWhiteSpace(bankId))
            {
                _logger.LogWarning("BankId parameter is null or empty.");
                throw ObpException.BankNotFound(); // OBP-30001
            }

            if (string.IsNullOrWhiteSpace(agent_id))
            {
                _logger.LogWarning("AgentId parameter is null or empty.");
                throw ObpException.AgentNotFound(); // OBP-30201
            }

            try
            {
                _logger.LogInformation("Fetching agent from repository...");
                var agent = await _accountRepository.GetAgentByBankIdAndAgentIdAsync(bankId, agent_id);

                if (agent == null)
                {
                    _logger.LogWarning("Agent not found for BankId: {BankId}, AgentId: {AgentId}", bankId, agent_id);
                    throw ObpException.AgentNotFound(); // OBP-30201
                }

                _logger.LogInformation("Mapping Agent to DTO...");
                var dto = _mapper.Map<Agent>(agent); // Create and use AgentDto here

                _logger.LogInformation("Successfully fetched and returned agent.");
                return Ok(dto);
            }
            catch (ObpException ex)
            {
                _logger.LogWarning(ex, "OBP Exception ({ExceptionType}): {ErrorCode} - {Message}", ex.GetType().Name, ex.ErrorCode, ex.Message);
                return ex.ErrorCode switch
                {
                    "OBP-30001" => NotFound(new { code = ex.ErrorCode, message = ex.Message }), // Bank not found
                    "OBP-30201" => NotFound(new { code = ex.ErrorCode, message = ex.Message }), // Agent not found
                    "OBP-30325" => NotFound(new { code = ex.ErrorCode, message = ex.Message }), // Agent Account Link not found
                    _ => StatusCode(500, new { code = ObpException.UnknownError().ErrorCode, message = ObpException.UnknownError().Message })
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception ({ExceptionType}) in GetAgent: {Message}", ex.GetType().Name, ex.Message);
                return StatusCode(500, new
                {
                    code = ObpException.UnknownError().ErrorCode,
                    message = ObpException.UnknownError().Message,
                    details = ex.Message
                });
            }
        }

        // *****************************************************************************************************************************************

        /* [HttpGet("my/accounts")]
        [SwaggerOperation(
            Summary = "Get full Agent details",
            Description = "Returns detailed information for a specific agent using bankId and agent_id"
        )]
        */
        // *****************************************************************************************************************************************

        [HttpGet("banks/{bankId}/accounts-held")]
        [SwaggerOperation(
            Summary = "Get Accounts Held ",
            Description = "Returns detailed information for a specific account Held  using bankId "
        )]

        public async Task<IActionResult> GetAccountHeld (string bankId)
        {
            _logger.LogInformation("Start GetAccountHeldByBankId. BankId: {BankId}", bankId);

            if (string.IsNullOrWhiteSpace(bankId))
            {
                _logger.LogWarning("BankId parameter is null or empty.");
                throw ObpException.BankNotFound();
            }

            try
            {
                _logger.LogInformation("Fetching account details from repository...");
                var accounts = await _accountRepository.GetAccountsHeld(bankId);

                if (accounts == null || !accounts.Any())
                {
                    _logger.LogWarning("No accounts found for BankId: {BankId}", bankId);
                    throw ObpException.AccountNotFound();
                }

                _logger.LogInformation("Mapping AccountSheld to DTO...");
                var dto = _mapper.Map<List<AccountsHeld>>(accounts);

                _logger.LogInformation("Successfully fetched and returned accounts.");
                return Ok(new { accounts = dto });
            }
            catch (ObpException ex)
            {
                _logger.LogWarning(ex, "OBP Exception ({ExceptionType}): {ErrorCode} - {Message}", ex.GetType().Name, ex.ErrorCode, ex.Message);
                return ex.ErrorCode switch
                {
                    "OBP-20001" => Unauthorized(new { code = ex.ErrorCode, message = ex.Message }),
                    _ => StatusCode(500, new { code = ex.ErrorCode, message = ex.Message })
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception ({ExceptionType}) in GetAccounts: {Message}", ex.GetType().Name, ex.Message);
                return StatusCode(500, new
                {
                    code = ObpException.UnknownError().ErrorCode,
                    message = ObpException.UnknownError().Message,
                    details = ex.Message
                });
            }
        }
        // *****************************************************************************************************************************************


        [HttpGet("banks/{bankId}/agents")]
        [SwaggerOperation(
            Summary = "Get Agents At Bank  ",
            Description = "Returns detailed information for Agents At Bank  using bankId "
        )]

        public async Task<IActionResult> GetAgentsAtBank (string bankId)
        {
            _logger.LogInformation("Start GetAgentsAtBank. By BankId: {BankId}", bankId);

            if (string.IsNullOrWhiteSpace(bankId))
            {
                _logger.LogWarning("BankId parameter is null or empty.");
                throw ObpException.BankNotFound();
            }

            try
            {
                _logger.LogInformation("Fetching Agents  details from repository...");
                var agents = await _accountRepository.GetAgentsAtBank(bankId);


                _logger.LogInformation("Mapping GetAgentsAtBank to DTO...");
                var dto = _mapper.Map<List<Agents_at_Bank>>(agents);

                _logger.LogInformation("Successfully fetched and returned agents.");
                return Ok(new { agents = dto });
            }
            catch (ObpException ex)
            {
                _logger.LogWarning(ex, "OBP Exception ({ExceptionType}): {ErrorCode} - {Message}", ex.GetType().Name, ex.ErrorCode, ex.Message);
                return ex.ErrorCode switch
                {
                    "OBP-20001" => Unauthorized(new { code = ex.ErrorCode, message = ex.Message }),
                    _ => StatusCode(500, new { code = ex.ErrorCode, message = ex.Message })
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception ({ExceptionType}) in GetAgentsAtBank: {Message}", ex.GetType().Name, ex.Message);
                return StatusCode(500, new
                {
                    code = ObpException.UnknownError().ErrorCode,
                    message = ObpException.UnknownError().Message,
                    details = ex.Message
                });
            }
        }


        // *****************************************************************************************************************************************


        [HttpGet("management/banks/{bankId}/fast-firehose/accounts")]
        [SwaggerOperation(
            Summary = "Get Fast Firehose Accounts at Bank  ",
            Description = "Returns detailed information for Get Fast Firehose Accounts at Bank  using bankId "
        )]
        public async Task<IActionResult> GetFastFirehoseAccountsAtBank(string bankId)
        {
            _logger.LogInformation("Start GetFastFirehoseAccountsAtBank.by  BankId: {BankId}", bankId);

            if (string.IsNullOrWhiteSpace(bankId))
            {
                _logger.LogWarning("BankId parameter is null or empty.");
                throw ObpException.BankNotFound();
            }

            try
            {
                _logger.LogInformation("Fetching account details from repository...");
                var accounts = await _accountRepository.GetAccountsInBank(bankId);

                if (accounts == null || !accounts.Any())
                {
                    _logger.LogWarning("No accounts found for BankId: {BankId}", bankId);
                    throw ObpException.AccountNotFound();
                }

                _logger.LogInformation("Mapping accounts to DTO...");
                var dto = _mapper.Map<List<FastFirehoseAccountsAtBank>>(accounts);

                _logger.LogInformation("Successfully fetched and returned accounts.");
                return Ok(new { accounts = dto });
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
                _logger.LogError(ex, "Unhandled exception ({ExceptionType}) in GetAccounts: {Message}", ex.GetType().Name, ex.Message);
                return StatusCode(500, new
                {
                    code = ObpException.UnknownError().ErrorCode,
                    message = ObpException.UnknownError().Message,
                    details = ex.Message
                });
            }
        }

        [HttpGet("banks/{bankId}/agent/{agentId}")]
        [SwaggerOperation(
                Summary = "Get agent details",
                Description = "Returns detailed information for a specific agent using bankId and agentId."
        )]
        public async Task<IActionResult> GetAgent(string bankId, string agentId)
        {
                _logger.LogInformation("Start GetAgent. BankId: {BankId}, AgentId: {AgentId}", bankId, agentId);

                try
                {
                    _logger.LogInformation("Fetching agent from repository...");
                    var agent = await _accountRepository.GetAgent(bankId, agentId);

                    if (agent == null)
                    {
                        _logger.LogWarning("Agent not found for BankId: {BankId}, AgentId: {AgentId}", bankId, agentId);
                        _logger.LogWarning("Throwing OBP Exception of type: {ExceptionType} with code: {ErrorCode}", nameof(ObpException), "OBP-30019");
                        throw ObpException.AgentNotFound();
                    }

                    _logger.LogInformation("Mapping agent to DTO...");
                    var dto = _mapper.Map<Agent>(agent);

                    _logger.LogInformation("Successfully fetched and returned agent.");
                    return Ok(dto);
                }
                catch (ObpException ex)
                {
                    _logger.LogWarning(ex, "OBP Exception ({ExceptionType}): {ErrorCode} - {Message}", ex.GetType().Name, ex.ErrorCode, ex.Message);
                    return ex.ErrorCode switch
                    {
                        "OBP-30019" => NotFound(new { code = ex.ErrorCode, message = ex.Message }),
                        _ => StatusCode(500, new { code = ex.ErrorCode, message = ex.Message })
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled exception ({ExceptionType}) in GetAgent: {Message}", ex.GetType().Name, ex.Message);
                    return StatusCode(500, new
                    {
                        code = ObpException.UnknownError().ErrorCode,
                        message = ObpException.UnknownError().Message,
                        details = ex.Message
                    });
                }
        }

        [HttpPost("banks/{bankId}/accounts")]
        [SwaggerOperation(Summary = "Create a new account for a bank")]
        [ProducesResponseType(typeof(CreateAccountResponseDto), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> CreateAccount(string bankId, [FromBody] CreateAccountDto createAccountDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(bankId))
                    throw ObpException.BankNotFound();

                if (createAccountDto == null || string.IsNullOrWhiteSpace(createAccountDto.user_id))
                    throw ObpException.UserNotFound();

                var account = await _accountRepository.CreateAccountAsync(bankId, createAccountDto);

                var responseDto = _mapper.Map<CreateAccountResponseDto>(account);
                _publisher.PublishAccountCreated(new AccountCreatedEvent
                {
                    AccountId = account.id,
                    UserId = account.user_id,
                    Label = account.label
                });


                return CreatedAtAction(nameof(GetAccountById), new { bankId = bankId, accountId = account.id }, responseDto);
            }
            catch (ObpException ex)
            {
                _logger.LogWarning(ex.ToString());
                return BadRequest(ex.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating account");
                return StatusCode(500, ObpException.UnknownError().ToString());
            }
        }

        [HttpGet("banks/{bankId}/accounts/{accountId}")]
        [SwaggerOperation(Summary = "Get full account details by ID" ,Description = "Returns detailed information for a specific account  using bankId and accountId .")]
        [ProducesResponseType(typeof(CreateAccountResponseDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetAccountById(string bankId, string accountId)
        {
            var account = await _accountRepository.GetFullAccountByIdAsync(bankId, accountId);

            if (account == null)
                return NotFound(ObpException.AccountNotFound().ToString());

            var responseDto = _mapper.Map<CreateAccountResponseDto>(account);
            return Ok(responseDto);
        }


       [HttpPost("banks/{bankId}/accounts/{accountId}/products/{productCode}/attribute")]
[SwaggerOperation(Summary = "Create a New Account Attributes", Description = "Create a new account attributes based on the three elements: bankId, accountId, and productCode")]
public async Task<IActionResult> CreateAccountAttribute(
    string bankId,
    string accountId,
    string productCode,
    [FromBody] CreateAccountAttributeBodyDto dto)
{
    var attribute = await _accountRepository.CreateAccountAttribute(bankId, accountId, productCode, dto);
    var result = _mapper.Map<CreateAccountAttributeResponseDto>(attribute);

    _publisher.PublishAccountCreated(new AccountAttributeCreatedEvent
    {
        account_attribute_id = attribute.account_attribute_id,
        name = attribute.name,
        value = attribute.value,
        product_instance_code = attribute.product_instance_code
    });

    return Ok(result);
}



    }

}
