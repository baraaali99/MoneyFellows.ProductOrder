using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyFellows.ProductOrder.Application.Products.Queries;
using MoneyFellows.ProductOrder.Core.Interfaces;
using MoneyFellows.ProductOrder.Infrastructure;
using MoneyFellows.ProductOrder.Infrastructure.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSwaggerGen();

// Adding DbContext Service
builder.Services.AddDbContext<ProductOrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register MediatR with the assembly where the handlers are located
builder.Services.AddMediatR(typeof(GetProductsListQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(GetProductByIdQueryHandler).Assembly);

// Register FluentValidation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation();

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register repositories and services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Add API versioning
/*builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = new Microsoft.AspNetCore.Mvc.Versioning.UrlSegmentApiVersionReader();
});*/

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs.log",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));

// Add controllers
builder.Services.AddControllers();

// Configure the HTTP request pipeline.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
//app.UseApiVersioning();
app.MapControllers();
app.Run();