
namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

public interface IBeneficiariesClient
{
    /// <summary>
    /// It creates a set of communities
    /// </summary>
    /// <param name="request">Parameters</param>
    /// <param name="token">cancellation token</param>
    /// <returns>List of the created ids</returns>
    Task<MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.POST.Response?>
        CreateCommunities(MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.POST.Request request,
            CancellationToken token
        );
}