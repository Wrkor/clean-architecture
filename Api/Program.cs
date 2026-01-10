var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    await app.InitializeDatabaseAsync();

app.UseApiServices();
app.Run();
