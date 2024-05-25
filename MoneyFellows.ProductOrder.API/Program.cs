using Microsoft.EntityFrameworkCore;
using MoneyFellows.ProductOrder.API.Infrastructure;
using MoneyFellows.ProductOrder.API.Infrastructure.Middlewares;
using MoneyFellows.ProductOrder.Infrastructure.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddConfigurations(builder.Host, configuration);

// Configure the HTTP request pipeline.
var app = builder.Build();

await MigrateDatabase(builder);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "API v2");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.Run();

static async Task MigrateDatabase(WebApplicationBuilder webApplicationBuilder)
{
    await using var serviceProvider = webApplicationBuilder.Services.BuildServiceProvider();
    await using var dbContext = serviceProvider.GetRequiredService<ProductOrderDbContext>();

    try
    {
        await dbContext.Database.MigrateAsync();
        Log.Information("Database migrated successfully.");
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurred while migrating the database.");
    }
}
