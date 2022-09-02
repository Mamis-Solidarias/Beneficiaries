using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Beneficiaries.Id.POST;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

public partial class BeneficiariesClient
{
    public Task ReactivateBeneficiary(Request request, CancellationToken token)
        => CreateRequest(HttpMethod.Post, "beneficiaries", $"{request.Id}")
            .ExecuteAsync(token);
}