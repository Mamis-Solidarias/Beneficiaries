using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace MamisSolidarias.WebAPI.Beneficiaries.Extensions;

internal static class OpenTelemetry
{
    public static void SetUpOpenTelemetry(this IServiceCollection services, IConfiguration configuration, bool enableExporters)
    {
        services.AddOpenTelemetryTracing(tracerProviderBuilder =>
        {
            tracerProviderBuilder
                .AddSource(configuration["Service:Name"])
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName:configuration["Service:Name"],
                            serviceVersion: configuration["Service:Version"]))
                .AddHttpClientInstrumentation()
                .AddHotChocolateInstrumentation()
                .AddAspNetCoreInstrumentation(t => t.RecordException = true)
                .AddEntityFrameworkCoreInstrumentation(t => t.SetDbStatementForText = true);
            
            if (enableExporters)
            {
                tracerProviderBuilder
                    .AddConsoleExporter()
                    .AddJaegerExporter();
            }
        });        
    }
}