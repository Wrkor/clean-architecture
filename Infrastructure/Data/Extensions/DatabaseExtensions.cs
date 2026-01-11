namespace Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.MigrateAsync();
        await SeedData(dbContext);
    }

    private static async Task SeedData(ApplicationDbContext dbContext)
    {
        await SeedTopicsAsync(dbContext);
    }

    private static async Task SeedTopicsAsync(ApplicationDbContext dbContext)
    {
        if (!await dbContext.Topics.AnyAsync())
        {
            dbContext.Topics.AddRange(InitialData.Topics);
            await dbContext.SaveChangesAsync();
        }
    }
}
