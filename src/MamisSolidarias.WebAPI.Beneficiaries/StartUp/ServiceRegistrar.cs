using FastEndpoints;
using FastEndpoints.Swagger;
using MamisSolidarias.WebAPI.Beneficiaries.Extensions;

namespace MamisSolidarias.WebAPI.Beneficiaries.StartUp;

internal static class ServiceRegistrar
{
    private static ILoggerFactory CreateLoggerFactory(IConfiguration configuration)
    {
        return LoggerFactory.Create(loggingBuilder => loggingBuilder
            .AddConfiguration(configuration)
            .AddConsole()
        );
    }


    public static void Register(WebApplicationBuilder builder)
    {
        using var loggerFactory = CreateLoggerFactory(builder.Configuration);

        builder.Services.SetUpOpenTelemetry(builder.Configuration, builder.Logging, loggerFactory);
        builder.Services.AddDataProtection(builder.Configuration, loggerFactory);
        builder.Services.AddFastEndpoints(t => t.SourceGeneratorDiscoveredTypes = DiscoveredTypes.All);
        builder.Services.AddAuth(builder.Configuration, loggerFactory);
        builder.Services.SetUpEntityFramework(builder.Configuration, builder.Environment, loggerFactory);
        builder.Services.AddRedis(builder.Configuration, loggerFactory);
        builder.Services.SetUpGraphQl(builder.Configuration, loggerFactory);
        builder.Services.AddSwaggerDoc(t => t.Title = "Beneficiaries");
        builder.Services.AddMassTransit();
    }
}