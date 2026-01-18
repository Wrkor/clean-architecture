namespace Application.Data.DatabaseContext;

public interface IApplicationDbContext
{
    DbSet<Comment> Comments { get; }
    DbSet<Topic> Topics { get; }
    DbSet<Relationship> Relationships { get; }
    DbSet<CustomIdentityUser> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
