namespace OpenBanking_ACCOUNT_V1.Repository
{
    public interface IUnitOfWork
    {
        Task<bool> CompleteAsync();
    }
}
