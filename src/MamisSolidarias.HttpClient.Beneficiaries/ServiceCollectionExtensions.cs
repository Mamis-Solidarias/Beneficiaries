using MamisSolidarias.HttpClient.Beneficiaries.Models;
using MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;
using MamisSolidarias.HttpClient.Beneficiaries.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace MamisSolidarias.HttpClient.Beneficiaries;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// It registers the BeneficiariesHttpClient using dependency injection
    /// </summary>
    public static void AddBeneficiariesHttpClient(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var config = new BeneficiariesConfiguration();
        configuration.GetSection("BeneficiariesHttpClient").Bind(config);
        ArgumentNullException.ThrowIfNull(config.BaseUrl);
        ArgumentNullException.ThrowIfNull(config.Timeout);
        ArgumentNullException.ThrowIfNull(config.Retries);

        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddSingleton<IBeneficiariesClient, BeneficiariesClient.BeneficiariesClient>();
        serviceCollection.AddHttpClient("Beneficiaries", (services,client) =>
        {
            client.BaseAddress = new Uri(config.BaseUrl);
            client.Timeout = TimeSpan.FromMilliseconds(config.Timeout);
            
            var contextAccessor = services.GetService<IHttpContextAccessor>();
            if (contextAccessor is not null)
            {
                var authHeader = new HeaderService(contextAccessor).GetAuthorization();
                if (authHeader is not null)
                    client.DefaultRequestHeaders.Add("Authorization", authHeader);
            }
        })
            .AddTransientHttpErrorPolicy(t =>
            t.WaitAndRetryAsync(config.Retries,
                retryAttempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, retryAttempt)))
        );
    }
}