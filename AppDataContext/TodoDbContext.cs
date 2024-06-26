namespace StoreApi.AppDataContext;

public class TodoDbContext(
    IConfiguration configuration
) : DbContext
{
    // DbSettings field to store the connection string
    private readonly IConfiguration _configuration = configuration;

    // DbSet property to represent the Todo table
    public DbSet<Todo> Todos { get; set; }

    // Configuring the database provider and connection string
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Db"));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    // Configuring the model for the Todo entity
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<Todo>()
            .ToTable("TodoApi")
            .HasKey(x => x.Id);
    }
}