
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using AzureWebApi.Mapping;
using Data;
using Logic;
using Logic.Interfaces.Handlers;
using Logic.Services;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;

namespace AzureWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.Configure<KeyVaultSettings>(
                builder.Configuration.GetSection("KeyVault"));

            

            // Configure the HTTP request pipeline.
            

            builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
            
            builder.Services.AddSingleton<SecretClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<KeyVaultSettings>>().Value;
                return new SecretClient(new Uri(settings.VaultUri), new DefaultAzureCredential());
            });

            builder.Services.AddDbContext<ProductsDbContext>((sp, options) =>
            {
                var configurationService = sp.GetRequiredService<IConfigurationService>();
                var connStr = configurationService.GetSqlConnectionString();
                options.UseSqlServer(connStr);
            });

            TypeAdapterConfig.GlobalSettings.Scan(typeof(MappingConfig).Assembly);
            builder.Services.AddSingleton(TypeAdapterConfig.GlobalSettings);
            builder.Services.AddScoped<IMapper, ServiceMapper>();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(AddReviewCommandHandler).Assembly);
                using var sp = builder.Services.BuildServiceProvider();
                var configService = sp.GetRequiredService<IConfigurationService>();
                cfg.LicenseKey = configService.GetMediatrKey();
            });

            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(AddReviewCommandHandler).Assembly));

            builder.Services.AddScoped<IProductsDbContext, ProductsDbContext>();

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
