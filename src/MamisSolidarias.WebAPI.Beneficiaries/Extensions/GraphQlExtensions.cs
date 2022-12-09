using HotChocolate.Diagnostics;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Utils.Security;
using StackExchange.Redis;

namespace MamisSolidarias.WebAPI.Beneficiaries.Extensions;

internal static class GraphQlExtensions
{
    private sealed record GraphQlOptions(string GlobalSchemaName);

    public static void SetUpGraphQl(this IServiceCollection services, IConfiguration configuration,
        ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger("GraphQL");

        var builder = services.AddGraphQLServer()
            .AddBeneficiariesTypes()
            .AddQueryType(t => t.Name("Query"))
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
            .InitializeOnStartup();
        
        var options = configuration.GetSection("GraphQl").Get<GraphQlOptions>();

        if (options is null)
        {
            logger.LogWarning("GraphQl gateway options not found");
            return;
        }

        builder.PublishSchemaDefinition(t =>
        {
            t.SetName($"{Services.Beneficiaries}gql");
            t.PublishToRedis(options.GlobalSchemaName,
                sp => sp.GetRequiredService<ConnectionMultiplexer>()
            );
        });
    }
}