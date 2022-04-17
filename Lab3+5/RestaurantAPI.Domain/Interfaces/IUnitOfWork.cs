namespace RestaurantAPI.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task CompleteAsync();
    }
}