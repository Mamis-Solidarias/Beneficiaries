namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.GET;

public class Response
{
    /// <summary>
    /// Array of communities
    /// </summary>
    public IEnumerable<CommunityResponse> Communities { get; set; } = ArraySegment<CommunityResponse>.Empty;
}

/// <summary>
/// Response model for the Community
/// </summary>
/// <param name="Id"> Id of the community</param>
/// <param name="Name">Name of the community></param>
/// <param name="Description">Description of the community. Optional</param>
/// <param name="Address">Address of the community</param>
public record CommunityResponse(string Id, string Name, string? Description, string Address);