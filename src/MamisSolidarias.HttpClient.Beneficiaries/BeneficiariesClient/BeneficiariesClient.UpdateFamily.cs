namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

partial class BeneficiariesClient
{
    /// <inheritdoc />
    public Task<UpdateFamilyResponse?> UpdateFamily(string familyId,UpdateFamilyRequest request, CancellationToken token)
        => CreateRequest(HttpMethod.Patch,  "families", familyId)
            .WithContent(new
            {
                request.Address,
                request.Contacts,
                request.Details,
                request.Name
            })
            .ExecuteAsync<UpdateFamilyResponse>(token);
    

    /// <param name="Name">Name of the family</param>
    /// <param name="Address">Address of the family</param>
    /// <param name="Details">Additional comments of the family</param>
    /// <param name="Contacts">List of ways to contact the family</param>
    public sealed record UpdateFamilyRequest(string? Name, string? Address, string? Details, IEnumerable<Contact>? Contacts);
    
    /// <param name="Type">Type of contact (facebook, whatsapp, instagram, phone, other)</param>
    /// <param name="Content">Content of the contact</param>
    /// <param name="Title">Title of the contact</param>
    /// <param name="IsPreferred">Is it a preferred method of communication</param>
    public record Contact(string Type, string Content, string Title, bool IsPreferred);
    
    /// <summary>
    /// Model for a family
    /// </summary>
    /// <param name="Id">Id of the family</param>
    /// <param name="Name">Name of the family</param>
    /// <param name="Address">Address of the family</param>
    /// <param name="Details">Additional comments of the family</param>
    /// <param name="Contacts">List of ways to contact the family</param>
    public sealed record UpdateFamilyResponse(string Id, string Name, string Address, string Details, IEnumerable<Contact> Contacts);

}