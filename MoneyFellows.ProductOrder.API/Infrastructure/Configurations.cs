using Asp.Versioning;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MoneyFellows.ProductOrder.Application.Products.Commands.CreateProductCommand;
using MoneyFellows.ProductOrder.Core.Interfaces;
using MoneyFellows.ProductOrder.Infrastructure;
using MoneyFellows.ProductOrder.Infrastructure.Data;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace MoneyFellows.ProductOrder.API.Infrastructure
{
    public static class Configurations
    {
        public static void AddConfigurations(this IServiceCollection services,
            ConfigureHostBuilder host,
            IConfiguration configuration)
        {

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            host.UseSerilog();

            // Add controllers
            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<CreateProductCommand>();
            });

            // Add API versioning
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            // Configure Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "API v1", Version = "v1" });
                options.SwaggerDoc("v2", new OpenApiInfo { Title = "API v2", Version = "v2" });

                // Configure Swagger to use the correct versioned routes
                options.DocInclusionPredicate((version, desc) =>
                {
                    var versionModel = desc.ActionDescriptor.EndpointMetadata.OfType<ApiVersionAttribute>().FirstOrDefault();
                    if (versionModel == null)
                    {
                        return true;
                    }
                    return versionModel.Versions.Any(v => $"v{v.ToString()}" == version);
                });

                options.OperationFilter<RemoveVersionFromParameter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();
            });

            // Adding DbContext Service
            services.AddDbContext<ProductOrderDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register MediatR with the assembly where the handlers are located
            services.AddMediatR(typeof(CreateProductCommand).Assembly);

            // Register FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();

            // Register AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Register repositories and services
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }

    public class RemoveVersionFromParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters?.FirstOrDefault(p => p.Name == "version");
            if (versionParameter != null)
            {
                operation.Parameters.Remove(versionParameter);
            }
        }
    }

    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();
            foreach (var (key, value) in swaggerDoc.Paths)
            {
                paths.Add(key.Replace("v{version}", swaggerDoc.Info.Version), value);
            }
            swaggerDoc.Paths = paths;
        }
    }
}
