namespace Application.Data.DatabaseContext;

public interface IApplicationDbContext
{
    DbSet<Topic> Topics { get; }
}
