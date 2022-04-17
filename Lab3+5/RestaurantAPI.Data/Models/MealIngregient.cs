namespace RestaurantAPI.Data.Models;

public class MealIngridient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [ForeignKey("MealId")]
    public virtual Meal Meal { get; set; }
    [ForeignKey("IngridientId")]
    public virtual Ingridient Ingridient { get; set; }
}