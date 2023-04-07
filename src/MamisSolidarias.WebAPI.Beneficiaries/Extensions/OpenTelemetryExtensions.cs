using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace MamisSolidarias.WebAPI.Beneficiaries.Extensions;

internal static class OpenTelemetryExtensions
{
    private static ILogger? _logger;

    public static void SetUpOpenTelemetry(this IServiceCollection services, IConfiguration configuration,
        ILoggingBuilder logging, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger("OpenTelemetry");
        var options = configuration.GetSection("OpenTelemetry").Get<OpenTelemetryOptions>();

        if (options is null)
        {
            _logger.LogWarning("OpenTelemetry options not found");
            return;
        }

        var resourceBuilder = ResourceBuilder
            .CreateDefault()
            .AddService(options.Name, "MamisSolidarias", options.Version)
            .AddTelemetrySdk();


        services.AddOpenTelemetry().WithTracing(tracerProviderBuilder =>
            {
                tracerProviderBuilder
                    .SetResourceBuilder(resourceBuilder)
                    .AddNewRelicExporter(options.NewRelic)
                    .AddConsoleExporter(options.UseConsole)
                    .AddJaegerExporter(options.Jaeger)
                    .AddHttpClientInstrumentation(t => t.RecordException = true)
                    .AddAspNetCoreInstrumentation(t => t.RecordException = true)
                    .AddEntityFrameworkCoreInstrumentation(t => t.SetDbStatementForText = true)
                    .AddHotChocolateInstrumentation()
                    .AddNpgsql();
            })
            .WithMetrics(meterProviderBuilder =>
            {
                meterProviderBuilder
                    .SetResourceBuilder(resourceBuilder)
                    .AddNewRelicExporter(options.NewRelic)
                    .AddRuntimeInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();
            });

        // Configure the OpenTelemetry SDK for logs
        logging.ClearProviders();
        logging.AddConsole();

        logging.AddOpenTelemetry(logsProviderBuilder =>
        {
            logsProviderBuilder.IncludeFormattedMessage = true;
            logsProviderBuilder.ParseStateValues = true;
            logsProviderBuilder.IncludeScopes = true;
            logsProviderBuilder
                .SetResourceBuilder(resourceBuilder)
                .AddNewRelicExporter(options.NewRelic);
        });
    }

    private static TracerProviderBuilder AddNewRelicExporter(this TracerProviderBuilder builder,
        NewRelicOptions? newRelicOptions)
    {
        if (string.IsNullOrWhiteSpace(newRelicOptions?.Url) || string.IsNullOrWhiteSpace(newRelicOptions.ApiKey))
        {
            _logger?.LogWarning("NewRelic trace options not found");
            return builder;
        }

        return builder.AddOtlpExporter(t =>
        {
            t.Endpoint = new Uri(newRelicOptions.Url);
            t.Headers = $"api-key={newRelicOptions.ApiKey}";
        });
    }

    private static void AddNewRelicExporter(this OpenTelemetryLoggerOptions builder,
        NewRelicOptions? newRelicOptions)
    {
        if (string.IsNullOrWhiteSpace(newRelicOptions?.Url) || string.IsNullOrWhiteSpace(newRelicOptions.ApiKey))
        {
            _logger?.LogWarning("NewRelic log options not found");
            return;
        }

        builder.AddOtlpExporter(t =>
        {
            t.Endpoint = new Uri(newRelicOptions.Url);
            t.Headers = $"api-key={newRelicOptions.ApiKey}";
        });
    }

    private static MeterProviderBuilder AddNewRelicExporter(this MeterProviderBuilder builder,
        NewRelicOptions? newRelicOptions)
    {
        if (string.IsNullOrWhiteSpace(newRelicOptions?.Url) || string.IsNullOrWhiteSpace(newRelicOptions.ApiKey))
        {
            _logger?.LogWarning("NewRelic metric options not found");
            return builder;
        }

        return builder.AddOtlpExporter((t, m) =>
        {
            t.Endpoint = new Uri(newRelicOptions.Url);
            t.Headers = $"api-key={newRelicOptions.ApiKey}";
            m.TemporalityPreference = MetricReaderTemporalityPreference.Delta;
        });
    }

    private static TracerProviderBuilder AddJaegerExporter(this TracerProviderBuilder builder,
        JaegerOptions? jaegerOptions)
    {
        if (jaegerOptions is null || string.IsNullOrWhiteSpace(jaegerOptions.Url))
        {
            _logger?.LogWarning("Jaeger options not found");
            return builder;
        }

        return builder.AddJaegerExporter(t => t.AgentHost = jaegerOptions.Url);
    }

    private static TracerProviderBuilder AddConsoleExporter(this TracerProviderBuilder builder, bool useConsole)
    {
        if (useConsole)
            builder.AddConsoleExporter();
        return builder;
    }

    private sealed class NewRelicOptions
    {
        public string? ApiKey { get; } = null;
        public string? Url { get; } = null;
    }

    private sealed class JaegerOptions
    {
        public string? Url { get; } = string.Empty;
    }

    private sealed class OpenTelemetryOptions
    {
        public string Name { get; init; } = string.Empty;
        public string Version { get; init; } = string.Empty;
        public JaegerOptions? Jaeger { get; init; }
        public NewRelicOptions? NewRelic { get; init; }
        public bool UseConsole { get; init; }
    }
}