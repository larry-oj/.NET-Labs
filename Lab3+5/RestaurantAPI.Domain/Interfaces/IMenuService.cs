namespace RestaurantAPI.Domain.Interfaces;

public interface IMenuService
{
    List<MenuItem> GetMenu();
    MenuItem GetMeal(string name);
    Task ComposeMealAsync(string name, List<string> ingridients, float normalPrice, float largePrice = 0.0f);
    Task DeleteMealAsync(string name);
}