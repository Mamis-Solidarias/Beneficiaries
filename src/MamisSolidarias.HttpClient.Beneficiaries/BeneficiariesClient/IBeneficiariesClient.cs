using MamisSolidarias.HttpClient.Beneficiaries.Models;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

/// <summary>
/// Interface for the Beneficiaries Client
/// </summary>
public interface IBeneficiariesClient
{
    /// <summary>
    /// It creates a set of communities
    /// </summary>
    /// <returns>List of the created ids</returns>
    Task<BeneficiariesClient.CreateCommunitiesResponse?> CreateCommunities(
        BeneficiariesClient.CreateCommunitiesRequest request,
        CancellationToken token
    );

    /// <summary>
    /// It updates a single community
    /// </summary>
    Task<BeneficiariesClient.UpdateCommunityResponse?> UpdateCommunity(
        int id,
        BeneficiariesClient.UpdateCommunityRequest request,
        CancellationToken token
    );

    /// <summary>
    /// It creates a set of families
    /// </summary>
    /// <param name="communityId"></param>
    /// <param name="request">Parameters</param>
    /// <param name="token">Cancellation token</param>
    /// <returns></returns>
    Task CreateFamilies(
        string communityId,
        BeneficiariesClient.CreateFamiliesRequest request,
        CancellationToken token
    );


    /// <summary>
    /// It updates a family's data
    /// </summary>
    Task<BeneficiariesClient.UpdateFamilyResponse?> UpdateFamily(
        string familyId,
        BeneficiariesClient.UpdateFamilyRequest request,
        CancellationToken token
    );


    /// <summary>
    /// It creates a set of beneficiaries in a given family
    /// </summary>
    Task<BeneficiariesClient.CreateBeneficiariesResponse?> CreateBeneficiaries(
        string familyId,
        BeneficiariesClient.CreateBeneficiariesRequest request,
        CancellationToken token
    );

    /// <summary>
    /// It soft deletes a beneficiary
    /// </summary>
    Task DeleteBeneficiary(int id, CancellationToken token);

    /// <summary>
    /// It reactivates a previously deleted beneficiary
    /// </summary>
    Task ReactivateBeneficiary(int id, CancellationToken token);

    /// <summary>
    /// It partially updates a beneficiary
    /// </summary>
    Task<Beneficiary?> UpdateBeneficiary(
        int id,
        BeneficiariesClient.UpdateBeneficiaryRequest request,
        CancellationToken token
    );
}