namespace RestaurantAPI.Data.Models;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public virtual ICollection<OrderItem> OrderItems { get; set; }
    [Required]
    public int TableNumber { get; set; }
    public bool Completed { get; set; }
}