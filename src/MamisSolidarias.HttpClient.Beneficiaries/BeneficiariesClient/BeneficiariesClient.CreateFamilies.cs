namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

partial class BeneficiariesClient
{
    /// <inheritdoc />
    public Task CreateFamilies(string communityId, CreateFamiliesRequest request, CancellationToken token)
    {
        return CreateRequest(HttpMethod.Post,"communities",communityId,"families")
            .WithContent(new
            {
                request.Families
            })
            .ExecuteAsync(token);
    }


    /// <param name="Families">Families to add</param>
    public sealed record CreateFamiliesRequest(IEnumerable<FamilyRequest> Families);
    
    /// <summary>
    /// Family Model
    /// </summary>
    /// <param name="FamilyNumber">Optional. Allows the possibility of implementing custom IDs</param>
    /// <param name="Name">Name of the family</param>
    /// <param name="Address">Address of the family</param>
    /// <param name="Details">Extra details about the family</param>
    /// <param name="Contacts">A set of ways to contact the family</param>
    public record FamilyRequest(
        int? FamilyNumber,
        string Name,
        string Address,
        string? Details,
        IEnumerable<ContactRequest> Contacts
    );
    
    /// <summary>
    /// Contact Model
    /// </summary>
    /// <param name="Type">Type of the contact. It can be 'Phone', 'Email', 'Whatsapp', 'Facebook', 'Instagram', 'Other'</param>
    /// <param name="Content">Content of the contact. For example, a phone number or a instagram username</param>
    /// <param name="Title">The name of the contact</param>
    /// <param name="IsPreferred">If this is a preferred method of reaching out to the family</param>
    public record ContactRequest(string Type, string Content, string Title, bool IsPreferred);

}