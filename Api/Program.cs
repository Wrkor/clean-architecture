var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseSqlite(builder.Configuration.GetConnectionString("DbConnection"))
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    await app.InitializeDatabaseAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
