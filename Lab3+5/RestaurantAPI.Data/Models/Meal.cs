namespace RestaurantAPI.Data.Models;

public class Meal
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public float NormalPrice { get; set; }
    public float LargePrice { get; set; }
}