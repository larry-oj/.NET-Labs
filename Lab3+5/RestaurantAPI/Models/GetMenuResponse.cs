namespace RestaurantAPI.Models;

public class GetMenuResponse
{
    [JsonPropertyName("menu")]
    public List<MenuItem> Menu { get; set; }
}