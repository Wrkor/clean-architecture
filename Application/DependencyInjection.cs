namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services
    )
    {
        services.AddMapster();
        services.AddMediatR();

        return services;
    }

    private static IServiceCollection AddMapster(
        this IServiceCollection services
    )
    {
        var config = new TypeAdapterConfig();
        config.Scan(Assembly.GetExecutingAssembly());
        var mapper = new Mapper(config);
        services.AddSingleton<IMapper>(mapper);

        return services;
    }

    private static IServiceCollection AddMediatR(
        this IServiceCollection services
    )
    {
        services.AddMediatR(o =>
            o.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );

        return services;
    }
}
