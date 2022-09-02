using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Beneficiaries.Id.PATCH;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

public partial class BeneficiariesClient
{
    public Task<Response?> UpdateBeneficiary(Request request, CancellationToken token)
        => CreateRequest(HttpMethod.Patch, "beneficiaries", $"{request.Id}")
            .WithContent(new
            {
                request.Beneficiary
            })
            .ExecuteAsync<Response>(token);
}