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
        services.AddCors();
        services.AddSwagger();

        return services;
    }

    public static IServiceCollection AddCors(this IServiceCollection services)
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

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseCors();
        app.UseDetailsReponse();
        app.UseExceptionHandler(_ => { });
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        if (app.Environment.IsDevelopment())
            app.UseSwagger();

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

    public static WebApplication UseDetailsReponse(this WebApplication app)
    {
        app.UseStatusCodePages(async ctx =>
        {
            if (ctx.HttpContext.Response.StatusCode == 403)
            {
                var details = new ProblemDetails()
                {
                    Title = "Forbidden",
                    Detail = "У вас недостаточно прав доступа",
                    Status = StatusCodes.Status403Forbidden,
                    Instance = ctx.HttpContext.Request.Path,
                };
                details.Extensions.Add(
                    "traceId",
                    ctx.HttpContext.TraceIdentifier
                );
                await ctx.HttpContext.Response.WriteAsJsonAsync(details);
            }
        });

        return app;
    }
}
