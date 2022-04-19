namespace RestaurantAPI.Data.Models;

public class OrderItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; }
    [Required]
    [ForeignKey("MealId")]
    public virtual Meal PricedMeal { get; set; }
    [Required]
    public bool IsLarge { get; set; }
    [Required]
    [Range(1, 1000)]
    public int Quantity { get; set; }
}