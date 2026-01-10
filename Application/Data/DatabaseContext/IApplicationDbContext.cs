namespace Application.Data.DatabaseContext;

public interface IApplicationDbContext
{
    DbSet<Topic> Topics { get; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
