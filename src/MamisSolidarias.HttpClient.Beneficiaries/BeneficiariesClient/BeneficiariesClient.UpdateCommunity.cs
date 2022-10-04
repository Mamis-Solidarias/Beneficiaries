using MamisSolidarias.HttpClient.Beneficiaries.Models;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

partial class BeneficiariesClient
{
    /// <inheritdoc />
    public Task<UpdateCommunityResponse?> UpdateCommunity(int id, UpdateCommunityRequest request, CancellationToken token)
    {
        return CreateRequest(HttpMethod.Patch, $"communities/{id}")
            .WithContent(new
            {
                request.Address,
                request.Description
            })
            .ExecuteAsync<UpdateCommunityResponse>(token);
    }
    

    /// <param name="Address">Optional. New address of the community</param>
    /// <param name="Description">Optional. New description of the community</param>
    public sealed record UpdateCommunityRequest(string? Address, string? Description);
    
    /// <param name="Community">Requested community</param>
    public sealed record UpdateCommunityResponse(Community Community);
}