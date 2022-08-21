using MamisSolidarias.HttpClient.Beneficiaries.Models;
using MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace MamisSolidarias.HttpClient.Beneficiaries;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// It registers the BeneficiariesHttpClient using dependency injection
    /// </summary>
    /// <param name="builder"></param>
    public static void AddBeneficiariesHttpClient(this WebApplicationBuilder builder)
    {
        var configuration = new BeneficiariesConfiguration();
        builder.Configuration.GetSection("BeneficiariesHttpClient").Bind(configuration);
        ArgumentNullException.ThrowIfNull(configuration.BaseUrl);
        ArgumentNullException.ThrowIfNull(configuration.Timeout);
        ArgumentNullException.ThrowIfNull(configuration.Retries);

        builder.Services.AddSingleton<IBeneficiariesClient, BeneficiariesClient.BeneficiariesClient>();
        builder.Services.AddHttpClient("Beneficiaries", client =>
        {
            client.BaseAddress = new Uri(configuration.BaseUrl);
            client.Timeout = TimeSpan.FromMilliseconds(configuration.Timeout);
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");
        })
            .AddTransientHttpErrorPolicy(t =>
            t.WaitAndRetryAsync(configuration.Retries,
                retryAttempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, retryAttempt)))
        );
    }
}