namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

partial class BeneficiariesClient
{
    public Task<CreateCommunitiesResponse?> CreateCommunities(CreateCommunitiesRequest request, CancellationToken token)
    {
        return CreateRequest(HttpMethod.Post, "communities")
            .WithContent(new
            {
                request.Communities
            })
            .ExecuteAsync<CreateCommunitiesResponse>(token);
    }
    
    /// <param name="Communities">Communities to create</param>
    public sealed record CreateCommunitiesRequest(IEnumerable<CommunityRequest> Communities);
    
    /// <param name="Communities">Set of the IDs of the created communities</param>
    public sealed record CreateCommunitiesResponse(IEnumerable<string> Communities);
    
    /// <summary>
    /// Definition of a community
    /// </summary>
    /// <param name="Name">Name of the community</param>
    /// <param name="Address">Address of the community</param>
    /// <param name="Description">Description of the community. It is optional</param>
    /// <param name="CommunityCode">Identifier for the community. It is optional</param>
    public record CommunityRequest(string Name,string Address, string? Description, string? CommunityCode);
}