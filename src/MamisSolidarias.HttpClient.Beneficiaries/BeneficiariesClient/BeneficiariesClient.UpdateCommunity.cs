using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.PATCH;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

public partial class BeneficiariesClient
{
    public Task<Response?> UpdateCommunity(Request request, CancellationToken token)
    {
        return CreateRequest(HttpMethod.Patch, $"communities/{request.Id}")
            .WithContent(new
            {
                request.Address,
                request.Description
            })
            .ExecuteAsync<Response>(token);
    }
}