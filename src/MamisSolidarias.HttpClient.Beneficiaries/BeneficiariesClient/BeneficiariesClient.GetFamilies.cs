using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.GET;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

public partial class BeneficiariesClient
{
    public Task<Response?> GetFamilies(Request request, CancellationToken token)
        => CreateRequest(HttpMethod.Get, "communities", request.Id, "families")
            .WithQuery(
                ("Page", $"{request.Page}"),
                ("PageSize",$"{request.PageSize}")
                )
            .ExecuteAsync<Response>(token);
}