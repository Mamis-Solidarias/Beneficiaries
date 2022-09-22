using HotChocolate.Diagnostics;
using MamisSolidarias.Infrastructure.Beneficiaries;

namespace MamisSolidarias.WebAPI.Beneficiaries.Extensions;

internal static class GraphQl
{
    public static void SetUpGraphQl(this IServiceCollection services)
    {
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
            .RegisterDbContext<BeneficiariesDbContext>();
    }
}