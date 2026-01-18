namespace Infrastructure.Data.DatabaseContext;

public class ApplicationDbContext(DbContextOptions options)
    : IdentityDbContext<CustomIdentityUser>(options),
        IApplicationDbContext
{
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<Relationship> Relationships => Set<Relationship>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly()
        );
    }
}
