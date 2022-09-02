using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.Id.Beneficiaries.POST;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

public partial class BeneficiariesClient
{
    public Task<Response?> CreateBeneficiaries(Request request, CancellationToken token)
        => CreateRequest(HttpMethod.Post, "families", request.FamilyId, "beneficiaries")
            .WithContent(new
            {
                request.Beneficiaries
            })
            .ExecuteAsync<Response>(token);
}