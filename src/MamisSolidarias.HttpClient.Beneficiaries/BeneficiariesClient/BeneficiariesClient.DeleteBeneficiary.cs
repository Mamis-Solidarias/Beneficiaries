using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Beneficiaries.Id.DELETE;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

public partial class BeneficiariesClient
{
    public Task DeleteBeneficiary(Request request, CancellationToken token)
        => CreateRequest(HttpMethod.Delete, "beneficiaries", $"{request.Id}")
            .ExecuteAsync(token);
}