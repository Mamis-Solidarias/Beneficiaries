using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.Id.PATCH;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

public partial class BeneficiariesClient
{
    public Task<Response?> UpdateFamily(Request request, CancellationToken token)
        => CreateRequest(HttpMethod.Patch,  "families", request.FamilyId)
            .WithContent(new
            {
                request.Address,
                request.Contacts,
                request.Details,
                request.Name
            })
            .ExecuteAsync<Response>(token);
}