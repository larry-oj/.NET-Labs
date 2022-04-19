namespace RestaurantAPI.Models;

public class AddOrderRequest
{
    public class Pair
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("is_large")]
        public bool IsLarge { get; set; }
    }

    [JsonPropertyName("meals")]
    public List<Pair> Meals { get; set; }
    [JsonPropertyName("table_number")]
    public int TableNumber { get; set; }
}