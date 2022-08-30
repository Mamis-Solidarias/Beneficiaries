
using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.POST;

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

    /// <summary>
    /// It updates a single community
    /// </summary>
    /// <param name="request">Parameters</param>
    /// <param name="token">Cancellation token</param>
    /// <returns></returns>
    Task<MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.PATCH.Response?>
        UpdateCommunity(
            MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.PATCH.Request request,
            CancellationToken token
        );

    /// <summary>
    /// It creates a set of families
    /// </summary>
    /// <param name="request">Parameters</param>
    /// <param name="token">Cancellation token</param>
    /// <returns></returns>
    Task CreateFamilies(
        Request request,
        CancellationToken token
    );

    /// <summary>
    /// It retrieves all the families related to a single community
    /// </summary>
    /// <param name="request">Parameters</param>
    /// <param name="token">Cancellation token</param>
    /// <returns></returns>
    Task<MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.GET.Response?>
        GetFamilies(
            MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.GET.Request request,
            CancellationToken token
        );

    /// <summary>
    /// It updates a family's data
    /// </summary>
    /// <param name="request">Parameters</param>
    /// <param name="token">Cancellation token</param>
    /// <returns></returns>
    Task<MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.Id.PATCH.Response?>
        UpdateFamily(
            MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.Id.PATCH.Request request,
            CancellationToken token
        );

    /// <summary>
    /// It retrieves a single family
    /// </summary>
    /// <param name="request">Parameters</param>
    /// <param name="token">Cancellation token</param>
    /// <returns></returns>
    Task<MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.Id.GET.Response?>
        GetFamily(MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.Id.GET.Request request,
            CancellationToken token
        );

}