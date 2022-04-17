namespace RestaurantAPI.Services;

public class OrderService : IOrderService
{
    private readonly IMyUnitOfWork _uow;

    public OrderService(IMyUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public List<Domain.Models.Order> GetAll(bool? isActive = null)
    {
        var orders = _uow.Orders.GetAll(o => o.OrderItems);

        if (isActive != null)
        {
            orders = orders.Where(o => o.Completed == isActive);
        }

        var result = new List<Domain.Models.Order>();

        foreach (var order in orders)
        {
            var menuItems = new List<MenuItem>();
            foreach (var orderItem in order.OrderItems)
            {
                var meal = _uow.Meals.Find(m => m == orderItem.PricedMeal).FirstOrDefault();
                var menuItem = new MenuItem
                {
                    Name = meal.Name,
                    NormalPrice = meal.NormalPrice,
                    LargePrice = meal.LargePrice
                };
                menuItems.Add(menuItem);
            }
            var orderModel = new Domain.Models.Order
            {
                Id = order.Id,
                OrderItems = menuItems,
                TableNumber = order.TableNumber,
                Completed = order.Completed
            };
            result.Add(orderModel);
        }

        return result;
    }
    public Domain.Models.Order Get(int tableNum)
    {
        var order = _uow.Orders.Find(o => o.TableNumber == tableNum && o.Completed == false, o => o.OrderItems).FirstOrDefault();

        if (order == default(Data.Models.Order))
            throw new ArgumentException($"No active orders for table {tableNum} not found");

        var menuItems = new List<MenuItem>();
        foreach (var orderItem in order.OrderItems)
        {
            var meal = _uow.Meals.Find(m => m == orderItem.PricedMeal).FirstOrDefault();
            var menuItem = new MenuItem
            {
                Name = meal.Name,
                NormalPrice = meal.NormalPrice,
                LargePrice = meal.LargePrice
            };
            menuItems.Add(menuItem);
        }
        var orderModel = new Domain.Models.Order
        {
            Id = order.Id,
            OrderItems = menuItems,
            TableNumber = order.TableNumber,
            Completed = order.Completed
        };

        return orderModel;
    }
    public async Task ComposeOrderAsync(List<string> mealNames, int tableNum)
    {
        mealNames.ForEach(m => {
            var meal = _uow.Meals.Find(m1 => m1.Name == m).FirstOrDefault();
            if (meal == default(Data.Models.Meal))
                throw new ArgumentException($"Meal {m} does not exist");
        });

        var existingOrders = _uow.Orders.Find(o => o.TableNumber == tableNum && o.Completed == false);
        
        if (existingOrders.Any())
            throw new InvalidOperationException($"Active order for table {tableNum} already exists");

        var order = new Data.Models.Order
        {
            TableNumber = tableNum,
            Completed = false
        };

        await _uow.Orders.AddAsync(order);

        foreach (var mealName in mealNames)
        {
            var meal = _uow.Meals.Find(m => m.Name.ToLower() == mealName.ToLower()).FirstOrDefault();

            if (meal == default(Data.Models.Meal)) continue; // lmao xd

            var orderItem = new Data.Models.OrderItem
            {
                PricedMeal = meal,
                Order = order
            };
            await _uow.OrderItems.AddAsync(orderItem);
        }
    }
    public async Task SetOrderAsCompleted(int talbeNum)
    {
        var order = _uow.Orders.Find(o => o.TableNumber == talbeNum && o.Completed == false).FirstOrDefault();

        if (order == default(Data.Models.Order))
            throw new InvalidOperationException($"Active order for table {talbeNum} does not exist");

        order.Completed = true;
        await _uow.Orders.UpdateAsync(order);
    }
    public async Task DeleteLastAsync(int talbeNum)
    {
        var order = _uow.Orders.Find(o => o.TableNumber == talbeNum)
            .OrderBy(o => o.Id)
            .FirstOrDefault();

        if (order == default(Data.Models.Order))
            throw new InvalidOperationException($"No orders for table {talbeNum} exist");

        var orderItems = _uow.OrderItems.Find(oi => oi.Order == order);

        await _uow.OrderItems.RemoveRangeAsync(orderItems);
        await _uow.Orders.RemoveAsync(order);
    }
}