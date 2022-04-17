namespace RestaurantAPI.Models;

public class GetOrderResponse
{
    [JsonPropertyName("order")]
    public Domain.Models.Order Order { get; set; }
}