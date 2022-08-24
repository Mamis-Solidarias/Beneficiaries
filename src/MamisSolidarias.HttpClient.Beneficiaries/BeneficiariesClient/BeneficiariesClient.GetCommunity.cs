using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.GET;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

public partial class BeneficiariesClient
{
    public Task<Response?> GetCommunity(Request request, CancellationToken token)
    {
        return CreateRequest(HttpMethod.Get, $"communities/{request.Id}")
            .ExecuteAsync<Response>(token);
    }
}