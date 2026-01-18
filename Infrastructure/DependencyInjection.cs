namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.AddDb(config);
        services.AddServices(config);
        services.AddIdentityServices(config);

        return services;
    }

    private static IServiceCollection AddDb(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        var connectionString = config.GetConnectionString("DbConnection");
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(o =>
            o.UseSqlite(connectionString)
        );

        return services;
    }

    private static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserAccessor, UserAccessor>();

        return services;
    }

    private static IServiceCollection AddIdentityServices(
        this IServiceCollection services,
        IConfiguration config
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

        var secretKey = config.GetValue<string>("Auth:SecretKey")!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                };
            });

        services.AddAuthorization(o =>
        {
            o.AddPolicy(
                "IsTopicAuthor",
                p =>
                {
                    p.Requirements.Add(new TopicDeletionRequirement());
                }
            );
        });

        services.AddTransient<
            IAuthorizationHandler,
            TopicDeletionRequirementHandler
        >();

        return services;
    }
}
