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
                .AddSource(configuration["OpenTelemetry:Service:Name"])
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName:configuration["OpenTelemetry:Service:Name"],
                            serviceVersion: configuration["OpenTelemetry:Service:Version"]))
                .AddHttpClientInstrumentation(t =>
                {
                    t.RecordException = true;
                    t.SetHttpFlavor = true;
                })
                .AddHotChocolateInstrumentation()
                .AddAspNetCoreInstrumentation(t => t.RecordException = true)
                .AddEntityFrameworkCoreInstrumentation(t => t.SetDbStatementForText = true);
            
            if (enableExporters)
            {
                tracerProviderBuilder
                    .AddConsoleExporter()
                    .AddJaegerExporter(t =>
                    {
                        var jaegerHost = configuration["OpenTelemetry:Jaeger:Host"];
                        if (jaegerHost is not null)
                            t.Endpoint = new Uri($"{jaegerHost}/api/traces");
                    });
            }
        });        
    }
}