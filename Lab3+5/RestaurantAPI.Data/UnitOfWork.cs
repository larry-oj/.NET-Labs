namespace RestaurantAPI.Data;

public class UnitOfWork : IMyUnitOfWork
{
    private readonly RestaurantDataContext _context;

    public IGenericRepository<Ingridient> Ingridients { get; private set; }
    public IGenericRepository<Meal> Meals { get; private set; }
    public IGenericRepository<MealIngridient> MealIngridients { get; private set; }
    public IGenericRepository<Data.Models.Order> Orders { get; private set; }
    public IGenericRepository<OrderItem> OrderItems { get; private set; }

    public UnitOfWork(RestaurantDataContext context, 
        IGenericRepository<Ingridient> ingridientRepo, 
        IGenericRepository<Meal> mealRepo,
        IGenericRepository<MealIngridient> mealIngridientRepo, 
        IGenericRepository<Data.Models.Order> orderRepo, 
        IGenericRepository<OrderItem> orderItemRepo)
    {
        _context = context;
        Ingridients = ingridientRepo;
        Meals = mealRepo;
        MealIngridients = mealIngridientRepo;
        Orders = orderRepo;
        OrderItems = orderItemRepo;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}