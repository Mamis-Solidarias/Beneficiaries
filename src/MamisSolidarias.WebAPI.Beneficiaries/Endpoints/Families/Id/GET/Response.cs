namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.Id.GET;

/// <summary>
/// Model for a family
/// </summary>
/// <param name="Id">Id of the family</param>
/// <param name="Name">Name of the family</param>
/// <param name="Address">Address of the family</param>
/// <param name="Details">Additional comments of the family</param>
/// <param name="Contacts">List of ways to contact the family</param>
public record Response(
    string Id,
    string Name,
    string Address,
    string? Details,
    IEnumerable<ContactResponse> Contacts
);


/// <summary>
/// Model explaining how to contact a family
/// </summary>
/// <param name="Type">How to contact the family. (facebook, phone, wpp, etc)</param>
/// <param name="Content">Text that contains the information to contact the family</param>
/// <param name="Title">Name of the contact</param>
/// <param name="IsPreferred">It defines if this is a preferred method of communication</param>
public record ContactResponse(
    string Type,
    string Content,
    string Title,
    bool IsPreferred
);