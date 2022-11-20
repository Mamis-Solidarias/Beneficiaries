using MamisSolidarias.Infrastructure.Beneficiaries;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Beneficiaries.Extensions;

internal static class EntityFrameworkExtensions
{
    public static void SetUpEntityFramework(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {

        var connectionString = env.EnvironmentName.ToLower() switch
        {
            "production" => configuration.GetConnectionString("Production"),
            _ => configuration.GetConnectionString("Development")
        };
        
        services.AddDbContext<BeneficiariesDbContext>(
            t => 
                t.UseNpgsql(connectionString, r=> r.MigrationsAssembly("MamisSolidarias.WebAPI.Beneficiaries"))
                    .EnableSensitiveDataLogging(!env.IsProduction())
                    .EnableDetailedErrors(!env.IsProduction())
        );
    }
    
    public static void RunMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BeneficiariesDbContext>();
        db.Database.Migrate();
    }
}