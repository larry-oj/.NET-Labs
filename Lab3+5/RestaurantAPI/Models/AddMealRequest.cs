namespace RestaurantAPI.Models;

public class AddMealRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("ingredients")]
    public List<string> Ingredients { get; set; }
    [JsonPropertyName("normal_price")]
    public float NormalPrice { get; set; }
    [JsonPropertyName("large_price")]
    public float LargePrice { get; set; }
}