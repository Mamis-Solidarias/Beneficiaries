using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace MamisSolidarias.HttpClient.Beneficiaries.Utils;

internal class ConfigurationFactory
{
    internal static IConfiguration GetBeneficiariesConfiguration(
        string baseUrl = "https://test.com", int retries = 3, int timeout = 500
    )
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            {"BeneficiariesHttpClient:BaseUrl", baseUrl},
            {"BeneficiariesHttpClient:Retries", retries.ToString()},
            {"BeneficiariesHttpClient:Timeout", timeout.ToString()}
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
    }
}