using Request = MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.POST.Request;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

public partial class BeneficiariesClient
{
    public Task CreateFamilies(Request request, CancellationToken token)
    {
        return CreateRequest(HttpMethod.Post, "families")
            .WithContent(new
            {
                request.Families
            })
            .ExecuteAsync(token);
    }
}