using HotChocolate.Diagnostics;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Utils.Security;
using StackExchange.Redis;

namespace MamisSolidarias.WebAPI.Beneficiaries.Extensions;

internal static class GraphQl
{
    public static void SetUpGraphQl(this IServiceCollection services, IConfiguration configuration)
    {
        Console.WriteLine($"{configuration["Redis:Host"]}:{configuration["Redis:Port"]}");
        services.AddSingleton(ConnectionMultiplexer.Connect($"{configuration["Redis:Host"]}:{configuration["Redis:Port"]}"));
        
        services.AddGraphQLServer()
            .AddBeneficiariesTypes()
            .AddQueryType(t=> t.Name("Query"))
            .AddInstrumentation(t =>
            {
                t.Scopes = ActivityScopes.All;
                t.IncludeDocument = true;
                t.RequestDetails = RequestDetails.All; 
                t.IncludeDataLoaderKeys = true;
            })
            .AddAuthorization()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .RegisterDbContext<BeneficiariesDbContext>()
            .InitializeOnStartup()
            .PublishSchemaDefinition(t =>
            {
                t.SetName($"{Services.Beneficiaries}gql");
                t.PublishToRedis(configuration["GraphQl:GlobalSchemaName"],
                    sp => sp.GetRequiredService<ConnectionMultiplexer>()
                );
            });
    }
}