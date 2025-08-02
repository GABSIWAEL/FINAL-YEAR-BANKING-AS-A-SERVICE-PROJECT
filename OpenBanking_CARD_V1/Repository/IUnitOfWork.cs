namespace OpenBanking_CARD_V1.Repository
{
    public interface IUnitOfWork
    {
        Task<bool> CompleteAsync();
    }
}
