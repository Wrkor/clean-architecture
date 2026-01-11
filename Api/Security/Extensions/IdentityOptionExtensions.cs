namespace Api.Security.Extensions;

public static class IdentityOptionExtensions
{
    public static IServiceCollection AddIdentityServices(
        this IServiceCollection services
    )
    {
        services
            .AddIdentityCore<CustomIdentityUser>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequiredLength = 1;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthentication();

        return services;
    }
}
