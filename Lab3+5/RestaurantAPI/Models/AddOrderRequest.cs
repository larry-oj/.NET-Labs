namespace RestaurantAPI.Models;

public class AddOrderRequest
{
    [JsonPropertyName("meals")]
    public List<string> Meals { get; set; }
    [JsonPropertyName("table_number")]
    public int TableNumber { get; set; }
}