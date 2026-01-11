namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString(
            "DbConnection"
        );

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(o =>
            o.UseSqlite(connectionString)
        );

        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}
