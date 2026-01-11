namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services
    )
    {
        services.AddMediatR(o =>
            o.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );

        return services;
    }
}
