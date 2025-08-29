using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using OpenBanking_ATM_V1.CustomException;
using OpenBanking_ATM_V1.Dtos;
using OpenBanking_ATM_V1.Repository;
using AutoMapper;
using System;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using OpenBanking_ATM_V1.Shared.Services; // ✅ required
using OpenBanking_ATM_V1.Shared.Events; 

namespace OpenBanking_ATM_V1.Controllers
{
    [ApiController]
    [Route("obp/v5.0.0")]
    public class AtmController : ControllerBase
    {
        private readonly IAtmRepository _atmRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AtmController> _logger;
        private readonly RabbitMqPublisher _publisher;
        public AtmController(IAtmRepository atmRepository, IMapper mapper, ILogger<AtmController> logger , RabbitMqPublisher publisher)
        {
            _atmRepository = atmRepository;
            _mapper = mapper;
            _logger = logger;
            _publisher = publisher;
        }

        [HttpPost("banks/{bankId}/atms")]
       [ProducesResponseType( typeof(CreateAtmResponse),201)]
        [ProducesResponseType( typeof(string),400)]
        public async Task<IActionResult> CreateAtm(string bankId, [FromBody] CreateAtmBody createAtmBody)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(bankId))
                    throw ObpExceptionATM.BankNotFound();

                var atm = await _atmRepository.CreateAtm(bankId, createAtmBody);
                var responseDto = _mapper.Map<CreateAtmResponse>(atm);
                //------------------------------------------this is code related to the rabbit mq publisher 
                 _publisher.PublishAtmCreated(new AtmCreatedEvent
                {
                    atm_id = atm.Id,
                    BankId = atm.BankId,
                    Name = atm.Name
                });
                //------------------------------------------end 
                return CreatedAtAction(nameof(CreateAtm), new { bankId = atm.BankId, atmId = atm.Id }, responseDto);
            }
            catch (ObpExceptionATM ex)
            {
                _logger.LogWarning(ex.ToString());
                return BadRequest(ex.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating ATM");
                return StatusCode(500, ObpExceptionATM.UnknownError().ToString());
            }
        }
        [HttpPost("banks/{bankId}/atms/{atmId}/attributes")]
        [ProducesResponseType(typeof(AtmAttributesResponse), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> CreateAtmAttributes(string bankId, string atmId, [FromBody] AtmAttributesBody attributeBody)
        {
            try
            {
                var atmAttribute = await _atmRepository.CreateAtmAttributes(bankId, atmId, attributeBody);

                var responseDto = _mapper.Map<AtmAttributesResponse>(atmAttribute);

                // Optional: Publish event about new attribute
                _publisher.PublishAtmAttributeCreated(new AtmAttributeCreatedEvent
                {
                    BankId = bankId,
                    AtmId = atmId,
                    AttributeId = atmAttribute.Id,
                    Name = atmAttribute.Name,
                    Value = atmAttribute.Value,
                    IsActive = atmAttribute.IsActive
                });

                return CreatedAtAction(nameof(CreateAtmAttributes), new { bankId, atmId, attributeId = atmAttribute.Id }, responseDto);
            }
            catch (ObpExceptionATM ex)
            {
                _logger.LogWarning(ex.ToString());
                return BadRequest(ex.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating ATM attribute");
                return StatusCode(500, ObpExceptionATM.UnknownError().ToString());
            }
        }

        [HttpGet("banks/{bankId}/atms")]
        [ProducesResponseType(typeof(List<CreateAtmResponse>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> GetAtmsInBank(string bankId)
        {
            try
            {
                var atms = await _atmRepository.GetAtmsInBank(bankId);
                var responseDtos = _mapper.Map<List<CreateAtmResponse>>(atms);
                return Ok(responseDtos);
            }
            catch (ObpExceptionATM ex)
            {
                _logger.LogWarning(ex.ToString());
                return BadRequest(ex.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ATMs in bank");
                return StatusCode(500, ObpExceptionATM.UnknownError().ToString());
            }
        }

        [HttpGet("banks/{bankId}/atms/{atmId}")]
        [ProducesResponseType(typeof(CreateAtmResponse), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> GetAtmById(string bankId, string atmId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(bankId))
                    throw ObpExceptionATM.BankNotFound();

                var atm = await _atmRepository.GetAtmById(atmId);

                // Extra safety: make sure the ATM belongs to this bank
                if (atm.BankId != bankId)
                    throw ObpExceptionATM.ATMNotFound();

                var responseDto = _mapper.Map<CreateAtmResponse>(atm);
                return Ok(responseDto);
            }
            catch (ObpExceptionATM ex)
            {
                _logger.LogWarning(ex.ToString());
                return BadRequest(ex.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching ATM by ID");
                return StatusCode(500, ObpExceptionATM.UnknownError().ToString());
            }
        }






    }
}
