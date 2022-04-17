namespace RestaurantAPI.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly RestaurantDataContext _context;

    public GenericRepository(RestaurantDataContext context)
    {
        _context = context;
    }

    public T GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }
    public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes)
    {
        if (includes.Length > 0)
        {
            return Include(includes).ToList();
        }
        return _context.Set<T>().ToList();
    }
    public IEnumerable<T> Find(Func<T, bool> expression, params Expression<Func<T, object>>[] includes)
    {
        if (includes.Length > 0)
        {
            return Include(includes).Where(expression);
        }
        return _context.Set<T>().Where(expression);
    }
    public async Task AddAsync(T item)
    {
        _context.Set<T>().Add(item);
        await SaveAsync();
    }
    public async Task AddRangeAsync(IEnumerable<T> items)
    {
        _context.Set<T>().AddRange(items);
        await SaveAsync();
    }
    public async Task UpdateAsync(T item)
    {
        _context.Set<T>().Update(item);
        await SaveAsync();
    }
    public async Task RemoveAsync(T item)
    {
        _context.Set<T>().Remove(item);
        await SaveAsync();
    }
    public async Task RemoveRangeAsync(IEnumerable<T> items)
    {
        _context.Set<T>().RemoveRange(items);
        await SaveAsync();
    }
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
    private IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
    {
        DbSet<T> dbSet = _context.Set<T>();

        IQueryable<T> query = _context.Set<T>();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query ?? dbSet;
    }
}