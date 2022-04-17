namespace RestaurantAPI.Data.Interfaces
{
    public interface IMyUnitOfWork : IUnitOfWork
    {
        IGenericRepository<Ingridient> Ingridients { get; }
        IGenericRepository<Meal> Meals { get; }
        IGenericRepository<MealIngridient> MealIngridients { get; }
        IGenericRepository<Data.Models.Order> Orders { get; }
        IGenericRepository<OrderItem> OrderItems { get; }
    }
}