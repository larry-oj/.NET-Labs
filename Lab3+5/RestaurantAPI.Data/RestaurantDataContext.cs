namespace RestaurantAPI.Data;

public class RestaurantDataContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<Ingridient> Ingridients { get; set; }
    public DbSet<Meal> Meals { get; set; }
    public DbSet<MealIngridient> MealIngridients { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Data.Models.Order> Orders { get; set; }

    public RestaurantDataContext(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(_configuration.GetSection("Postgres:ConnectionString").Value);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingridient>()
            .HasIndex(e => e.Name)
            .IsUnique(true);

        modelBuilder.Entity<Meal>()
            .HasIndex(e => e.Name)
            .IsUnique(true);

        modelBuilder.UseSerialColumns();
    }
}