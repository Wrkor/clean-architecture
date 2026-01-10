namespace Infrastructure.Data.DatabaseContext;

public class ApplicationDbContext(DbContextOptions options)
    : DbContext(options),
        IApplicationDbContext
{
    public DbSet<Topic> Topics => Set<Topic>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
