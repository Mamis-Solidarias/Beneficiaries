using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.GET;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

public partial class BeneficiariesClient
{
    public Task<Response?> GetCommunities(CancellationToken token)
    {
        return CreateRequest(HttpMethod.Get, "communities")
            .ExecuteAsync<Response>(token);
    }
}