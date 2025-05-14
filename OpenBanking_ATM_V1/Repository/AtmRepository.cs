using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OpenBanking_ATM_V1.CustomException;
using OpenBanking_ATM_V1.Data;
using OpenBanking_ATM_V1.Dtos;

public class ATMAttributesRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper; // AutoMapper injected for model to DTO mapping

    public ATMAttributesRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BankATM> GetBankAtm(string bankId, string atmId)
    {
        try
        {
            var bankExists = await _context.Atms.AnyAsync(b => b.Id == bankId);
            if (!bankExists)
            {
                throw ObpExceptionATM.BankNotFound();
            }

            var atm = await _context.Atms
                .Include(a => a.Address)
                .Include(a => a.Location)
                .Include(a => a.Meta)
                .Include(a => a.SupportedLanguages)
                .Include(a => a.Services)
                .Include(a => a.AccessibilityFeatures)
                .Include(a => a.SupportedCurrencies)
                .Include(a => a.Notes)
                .Include(a => a.LocationCategories)
                .FirstOrDefaultAsync(a => a.Id == atmId);

            if (atm == null)
            {
                throw ObpExceptionATM.ATMNotFound();
            }

            // Map the Atm model to BankATM DTO
            var bankAtmDto = _mapper.Map<BankATM>(atm);

            return bankAtmDto; // Return the mapped DTO
        }
        catch (ObpExceptionATM ex)
        {
            throw ex;
        }
        catch (Exception)
        {
            throw ObpExceptionATM.UnknownError();
        }
    }
}
