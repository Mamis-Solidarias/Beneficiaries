
namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.GET;

public class Response
{
    /// <summary>
    /// The requested families
    /// </summary>
    public IEnumerable<FamilyResponse> Families { get; set; } = new List<FamilyResponse>();

    /// <summary>
    /// Current page
    /// </summary>
    public int Page { get; set; }
    
    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages { get; set; }
}

/// <summary>
/// Model for a family
/// </summary>
/// <param name="Id">Id of the family</param>
/// <param name="Name">Name of the family</param>
/// <param name="Address">Address of the family</param>
/// <param name="Details">Additional comments of the family</param>
/// <param name="Contacts">List of ways to contact the family</param>
public record FamilyResponse(
    string Id,
    string Name,
    string Address,
    string? Details,
    IEnumerable<ContactResponse> Contacts
);


public enum ContactType
{
    Phone,
    Email,
    Whatsapp,
    Facebook,
    Instagram,
    Other
}

/// <summary>
/// Model explaining how to contact a family
/// </summary>
/// <param name="Type">How to contact the family. (facebook, phone, wpp, etc)</param>
/// <param name="Content">Text that contains the information to contact the family</param>
/// <param name="Title">Name of the contact</param>
/// <param name="IsPreferred">It defines if this is a preferred method of communication</param>
public record ContactResponse(
    ContactType Type,
    string Content,
    string Title,
    bool IsPreferred
    );