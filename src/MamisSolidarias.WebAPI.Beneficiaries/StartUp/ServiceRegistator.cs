using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Utils.Security;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace MamisSolidarias.WebAPI.Beneficiaries.StartUp;

internal static class ServiceRegistrator
{
    public static void Register(WebApplicationBuilder builder)
    {

        var connectionString = builder.Environment.EnvironmentName.ToLower() switch
        {
            "production" => builder.Configuration.GetConnectionString("Production"),
            _ => builder.Configuration.GetConnectionString("Development")
        };
        
        builder.Services.AddOpenTelemetryTracing(tracerProviderBuilder =>
        {
            tracerProviderBuilder
                .AddConsoleExporter()
                .AddJaegerExporter()
                .AddSource(builder.Configuration["Service:Name"])
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName: builder.Configuration["Service:Name"], serviceVersion: builder.Configuration["Service:Version"]))
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation(t=> t.RecordException = true)
                .AddEntityFrameworkCoreInstrumentation(t=> t.SetDbStatementForText = true);
        });        
        
        builder.Services.AddFastEndpoints();
        builder.Services.AddAuthenticationJWTBearer(builder.Configuration["JWT:Key"]);
        builder.Services.AddAuthorization(t =>
        {
           t.ConfigurePolicies(Services.Beneficiaries);
        });
        
        builder.Services.AddDbContext<BeneficiariesDbContext>(
            t => 
                t.UseNpgsql(connectionString, r=> r.MigrationsAssembly("MamisSolidarias.WebAPI.Beneficiaries"))
                    .EnableSensitiveDataLogging(!builder.Environment.IsProduction())
                    
        );

        if (!builder.Environment.IsProduction())
            builder.Services.AddSwaggerDoc();
    }
}
