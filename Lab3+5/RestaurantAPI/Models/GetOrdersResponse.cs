namespace RestaurantAPI.Models;

public class GetOrdersResponse
{
    [JsonPropertyName("orders")]
    public List<Domain.Models.Order> Orders { get; set; }
}