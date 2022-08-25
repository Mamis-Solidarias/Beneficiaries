
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

    /// <summary>
    /// It retrieves all the communities generated
    /// </summary>
    /// <param name="token">Cancellation token</param>
    /// <returns>List of communities</returns>
    Task<MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.GET.Response?>
        GetCommunities(CancellationToken token
        );

    /// <summary>
    /// It retrieves a single community
    /// </summary>
    /// <param name="request">Parameters</param>
    /// <param name="token">Cancellation token</param>
    /// <returns></returns>
    Task<MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.GET.Response?>
        GetCommunity(MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.GET.Request request,
            CancellationToken token
        );

    Task<MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.PATCH.Response?>
        UpdateCommunity(
            MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.PATCH.Request request,
            CancellationToken token
        );
}