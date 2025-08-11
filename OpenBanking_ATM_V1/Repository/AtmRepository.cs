using MongoDB.Driver;
using OpenBanking_ATM_V1.Dtos;
using OpenBanking_ATM_V1.Models;
using AutoMapper;
using OpenBanking_ATM_V1.CustomException;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OpenBanking_ATM_V1.Repository
{
    public class AtmRepository : IAtmRepository
    {
        private readonly IMongoCollection<Atm> _atmCollection;
        private readonly IMapper _mapper;
        private readonly IMongoCollection<AtmAttributes> _atmAttributesCollection;

        public AtmRepository(IMapper mapper, string connectionString, string dbName)
        {
            _mapper = mapper;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(dbName);
            _atmCollection = database.GetCollection<Atm>("atms");
            _atmAttributesCollection = database.GetCollection<AtmAttributes>("atmAttributes");  // <-- add this line

        }

        public async Task<Atm> CreateAtm(string bankId, CreateAtmBody createAtmBody)
        {
            if (string.IsNullOrWhiteSpace(bankId))
                throw ObpExceptionATM.BankNotFound();

            var atm = new Atm
            {
                Id = Guid.NewGuid().ToString(),
                BankId = bankId,
                Name = createAtmBody.Name,
                Address = createAtmBody.Address,
                Location = createAtmBody.Location,
                Meta = createAtmBody.Meta,
                Monday = createAtmBody.Monday,
                Tuesday = createAtmBody.Tuesday,
                Wednesday = createAtmBody.Wednesday,
                Thursday = createAtmBody.Thursday,
                Friday = createAtmBody.Friday,
                Saturday = createAtmBody.Saturday,
                Sunday = createAtmBody.Sunday,
                IsAccessible = createAtmBody.IsAccessible,
                LocatedAt = createAtmBody.LocatedAt,
                MoreInfo = createAtmBody.MoreInfo,
                HasDepositCapability = createAtmBody.HasDepositCapability,
                MinimumWithdrawal = createAtmBody.MinimumWithdrawal,
                BranchIdentification = createAtmBody.BranchIdentification,
                SiteIdentification = createAtmBody.SiteIdentification,
                SiteName = createAtmBody.SiteName,
                CashWithdrawalNationalFee = createAtmBody.CashWithdrawalNationalFee,
                CashWithdrawalInternationalFee = createAtmBody.CashWithdrawalInternationalFee,
                BalanceInquiryFee = createAtmBody.BalanceInquiryFee,
                atm_type = createAtmBody.atm_type,
                phone = createAtmBody.phone,
                SupportedLanguages = _mapper.Map<List<Supported_languages>>(createAtmBody.SupportedLanguages),
                Services = _mapper.Map<List<Services>>(createAtmBody.Services),
                AccessibilityFeatures = _mapper.Map<List<Accessibility_features>>(createAtmBody.AccessibilityFeatures),
                SupportedCurrencies = _mapper.Map<List<Supported_currencies>>(createAtmBody.SupportedCurrencies),
                Notes = _mapper.Map<List<Notes>>(createAtmBody.Notes),
                LocationCategories = _mapper.Map<List<Location_categories>>(createAtmBody.LocationCategories)
            };

            await _atmCollection.InsertOneAsync(atm);

            return atm;
        }

        public async Task<Atm> GetAtmById(string atmId)
        {
            var filter = Builders<Atm>.Filter.Eq(a => a.Id, atmId);
            var atm = await _atmCollection.Find(filter).FirstOrDefaultAsync();

            if (atm == null)
                throw ObpExceptionATM.ATMNotFound();

            return atm;
        }




        public async Task<AtmAttributes> CreateAtmAttributes(string bankId, string atmId, AtmAttributesBody atmAttributesBody)
{
    if (string.IsNullOrWhiteSpace(bankId))
        throw ObpExceptionATM.BankNotFound();

    if (string.IsNullOrWhiteSpace(atmId))
        throw ObpExceptionATM.AtmNotFound();

    if (atmAttributesBody == null)
        throw new ArgumentNullException(nameof(atmAttributesBody));

    var atmAttribute = new AtmAttributes
    {
        BankId = bankId,
        AtmId = atmId,
        Name = atmAttributesBody.name,
        Type = atmAttributesBody.type,
        Value = atmAttributesBody.value,
        IsActive = atmAttributesBody.is_active
    };

    await _atmAttributesCollection.InsertOneAsync(atmAttribute);

    return atmAttribute;
}
    }
}
