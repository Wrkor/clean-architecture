namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddControllers(o =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            var filter = new AuthorizeFilter(policy);
            o.Filters.Add(filter);
        });
        services.AddHttpContextAccessor();
        services.AddCors(config);
        services.AddSwagger();
        services.AddIdentityServices(config);

        return services;
    }

    public static IServiceCollection AddCors(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.AddCors(o =>
        {
            o.AddDefaultPolicy(p =>
            {
                p.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:3000");
            });
        });

        return services;
    }

    public static IServiceCollection AddSwagger(
        this IServiceCollection services
    )
    {
        services.AddOpenApi();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o =>
        {
            o.DocumentFilter<DescriptionFilter>();
        });

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

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            app.UseSwagger();

        app.UseCors();
        app.UseExceptionHandler(_ => { });
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }

    private static WebApplication UseSwagger(this WebApplication app)
    {
        app.MapOpenApi("/swagger/v1/swagger.json");
        app.UseSwagger(_ => { });
        app.UseSwaggerUI(o =>
        {
            o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            o.RoutePrefix = string.Empty;
        });

        return app;
    }
}
