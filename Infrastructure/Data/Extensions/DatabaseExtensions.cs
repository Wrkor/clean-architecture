namespace Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var manager = scope.ServiceProvider.GetRequiredService<
            UserManager<CustomIdentityUser>
        >();
        await dbContext.Database.MigrateAsync();
        await SeedData(dbContext, manager);
    }

    private static async Task SeedData(
        ApplicationDbContext dbContext,
        UserManager<CustomIdentityUser> manager
    )
    {
        await SeedTopicsAsync(dbContext);
        await SeedIdentityUsersAsync(dbContext, manager);
    }

    private static async Task SeedIdentityUsersAsync(
        ApplicationDbContext dbContext,
        UserManager<CustomIdentityUser> manager
    )
    {
        if (!await dbContext.Users.AnyAsync())
        {
            foreach (var user in InitialData.CustomIdentityUsers)
                await manager.CreateAsync(user, "111");
        }
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
