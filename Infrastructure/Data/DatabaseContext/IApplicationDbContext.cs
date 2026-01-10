namespace Infrastructure.Data.DatabaseContext;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Topic> Topics => Set<Topic>();

    public ApplicationDbContext(DbContextOptions options)
        : base(options) { }
}
