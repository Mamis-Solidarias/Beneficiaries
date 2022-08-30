using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.Id.GET;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

public partial class BeneficiariesClient
{
    public Task<Response?> GetFamily(Request request, CancellationToken token)
        => CreateRequest(
                HttpMethod.Get, "families", request.FamilyId
                )
            .ExecuteAsync<Response>(token);
}