using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using MamisSolidarias.Utils.Security;
using MamisSolidarias.WebAPI.Beneficiaries.Extensions;

namespace MamisSolidarias.WebAPI.Beneficiaries.StartUp;

internal static class ServiceRegistrar
{
    private static ILoggerFactory CreateLoggerFactory(IConfiguration configuration) =>
        LoggerFactory.Create(loggingBuilder => loggingBuilder
            .AddConfiguration(configuration)
            .AddConsole()
        );


    public static void Register(WebApplicationBuilder builder)
    {
        using var loggerFactory = CreateLoggerFactory(builder.Configuration);
        
        builder.Services.SetUpOpenTelemetry(builder.Configuration,builder.Logging, loggerFactory);
        builder.Services.AddDataProtection(builder.Configuration,loggerFactory);
        builder.Services.AddFastEndpoints(t=> t.SourceGeneratorDiscoveredTypes = DiscoveredTypes.All);
        builder.Services.AddAuthenticationJWTBearer(
            builder.Configuration["Jwt:Key"] ?? throw new ArgumentException("Jwt:Key"),
            builder.Configuration["Jwt:Issuer"]
        );
        builder.Services.AddAuthorization(t => t.ConfigurePolicies(Services.Beneficiaries));
        
        builder.Services.SetUpEntityFramework(builder.Configuration,builder.Environment,loggerFactory);
        builder.Services.AddRedis(builder.Configuration,loggerFactory);
        builder.Services.SetUpGraphQl(builder.Configuration,loggerFactory);
        builder.Services.AddSwaggerDoc(t=> t.Title = "Beneficiaries");
        builder.Services.AddMassTransit();

    }
}