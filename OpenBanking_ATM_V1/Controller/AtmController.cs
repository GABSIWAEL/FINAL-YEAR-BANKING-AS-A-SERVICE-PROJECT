using Microsoft.AspNetCore.Mvc;
using OpenBanking_ATM_V1.CustomException;


namespace OpenBanking_ATM_V1.Controller
{
    public class AtmController : ControllerBase
    {
        private readonly ATMAttributesRepository _atmRepository;

        public AtmController(ATMAttributesRepository atmRepository)
        {
            _atmRepository = atmRepository;
        }

        // GET: /obp/v5.1.0/banks/{bankId}/atms/{atmId}
        [HttpGet]
        public async Task<IActionResult> GetBankAtm(string bankId, string atmId)
        {
            try
            {
                // Fetch ATM data using the repository method
                var bankAtm = await _atmRepository.GetBankAtm(bankId, atmId);

                // Return the ATM DTO as a successful response
                return Ok(bankAtm);
            }
            catch (ObpExceptionATM ex)
            {
                // Return the specific error from the custom exception handler
                return StatusCode(ex.StatusCode, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Return a generic error if an unexpected exception occurs
                return StatusCode(500, new { message = "An unknown error occurred", details = ex.Message });
            }
        }
    }
}