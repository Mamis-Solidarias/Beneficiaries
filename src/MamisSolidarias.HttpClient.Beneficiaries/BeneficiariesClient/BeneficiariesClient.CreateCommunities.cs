
using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.POST;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

public partial class BeneficiariesClient
{
    public Task<Response?> CreateCommunities(Request request, CancellationToken token)
    {
        return CreateRequest(HttpMethod.Post, "communities")
            .WithContent(new
            {
                request.Communities
            })
            .ExecuteAsync<Response>(token);
    }
}