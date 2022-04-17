namespace RestaurantAPI.Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    T GetById(int id);
    IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);
    IEnumerable<T> Find(Func<T, bool> expression, params Expression<Func<T, object>>[] includes);
    Task AddAsync(T item);
    Task AddRangeAsync(IEnumerable<T> item);
    Task UpdateAsync(T item);
    Task RemoveAsync(T item);
    Task RemoveRangeAsync(IEnumerable<T> item);
    Task SaveAsync();
}