namespace Infrastructure.Data.DatabaseContext;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Topic> Topics => Set<Topic>();

    public ApplicationDbContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Topic>()
            .Property(p => p.Id)
            .HasConversion(id => id.Value, value => TopicId.Of(value));

        modelBuilder
            .Entity<Topic>()
            .OwnsOne(
                p => p.Location,
                el =>
                {
                    el.Property(p => p.City).HasColumnName("City");
                    el.Property(p => p.Street).HasColumnName("Street");
                }
            );
    }
}
