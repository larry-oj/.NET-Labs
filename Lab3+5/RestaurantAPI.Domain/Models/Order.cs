namespace RestaurantAPI.Domain.Models;

public class Order
{
    public int Id { get; set; }
    public List<MenuItem> OrderItems { get; set; }
    public int TableNumber { get; set; }
    public float Total 
    {
        get
        {
            var total = 0.0f;
            foreach (var item in OrderItems)
            {
                if (item.IsLarge)
                {
                    total += item.LargePrice;
                }
                else
                {
                    total += item.NormalPrice;
                }
            }
            return total;
        }
    }
    public bool Completed { get; set; } = false;
}