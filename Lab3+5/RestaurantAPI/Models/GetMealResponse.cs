namespace RestaurantAPI.Models;

public class GetMealResponse
{
    [JsonPropertyName("menu_item")]
    public MenuItem MenuItem { get; set; }
}