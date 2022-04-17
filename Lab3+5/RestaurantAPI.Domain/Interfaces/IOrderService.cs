namespace RestaurantAPI.Domain.Interfaces;

public interface IOrderService
{
    List<Order> GetAll(bool? isActive = null);
    Order Get(int tableNum);
    Task ComposeOrderAsync(List<string> items, int tableNum);
    Task SetOrderAsCompleted(int talbeNum);
    Task DeleteLastAsync(int talbeNum);
}