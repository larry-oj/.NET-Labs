namespace RestaurantAPI.Domain.Models;

public class MenuItem
{
    public string Name { get; set; }
    public List<string> Ingridients { get; set; }
    public float NormalPrice { get; set; }
    public float LargePrice { get; set; }
    public bool IsLarge { get; set; } = false;
}