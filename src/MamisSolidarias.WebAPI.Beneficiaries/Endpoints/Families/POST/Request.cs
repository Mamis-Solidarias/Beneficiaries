using FastEndpoints;
using FluentValidation;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.POST;

/// <summary>
/// Parameters to create a set of families
/// </summary>
public class Request
{
    
    /// <summary>
    /// Families to create
    /// </summary>
    public IEnumerable<FamilyRequest> Families { get; set; } = ArraySegment<FamilyRequest>.Empty;
}

internal class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleForEach(t => t.Families)
            .SetValidator(new FamilyRequestValidation());

        RuleFor(t => t.Families)
            .NotEmpty().WithMessage("Se debe intentar de crear al menos una familia");
    }
}


/// <summary>
/// Family Model
/// </summary>
/// <param name="FamilyNumber">Optional. Allows the possibility of implementing custom IDs</param>
/// <param name="Name">Name of the family</param>
/// <param name="Address">Address of the family</param>
/// <param name="CommunityId">FamilyNumber of the community they live in</param>
/// <param name="Details">Extra details about the family</param>
/// <param name="Contacts">A set of ways to contact the family</param>
public record FamilyRequest(
    int? FamilyNumber,
    string Name,
    string Address,
    string CommunityId,
    string? Details,
    IEnumerable<ContactRequest> Contacts
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
/// Contact Model
/// </summary>
/// <param name="Type">Type of the contact. It can be 'Phone', 'Email', 'Whatsapp', 'Facebook', 'Instagram', 'Other'</param>
/// <param name="Content">Content of the contact. For example, a phone number or a instagram username</param>
/// <param name="Title">The name of the contact</param>
/// <param name="IsPreferred">If this is a preferred method of reaching out to the family</param>
public record ContactRequest(ContactType Type, string Content, string Title, bool IsPreferred);



internal class FamilyRequestValidation : Validator<FamilyRequest>
{
    public FamilyRequestValidation()
    {
        RuleForEach(t => t.Contacts)
            .SetValidator(new ContactRequestValidator());

        RuleFor(t => t.Address)
            .NotEmpty().WithMessage("La familia tiene que tener una dirección donde vive")
            .MaximumLength(500).WithMessage("La dirección no puede tener más de 500 caracteres");

        RuleFor(t => t.Details)
            .MaximumLength(500).WithMessage("Los comentarios no pueden tener mas de 500 caracteres");

        RuleFor(t => t.FamilyNumber)
            .LessThan(99999 + 1).WithMessage("El identifiacdor no puede tener mas de 5 caracteres");

        RuleFor(t => t.Name)
            .NotEmpty().WithMessage("La familia debe tener un nombre")
            .MaximumLength(100).WithMessage("El nombre de familia no puede tener mas de 100 caracteres");

        RuleFor(t => t.CommunityId)
            .NotEmpty().WithMessage("La familia debe estar asignada a una comunidad")
            .MaximumLength(5).WithMessage("El identificador de comunidad debe tener como maximo 5 caracteres");
    }
}

internal class ContactRequestValidator : Validator<ContactRequest>
{
    public ContactRequestValidator()
    {
        RuleFor(t => t.Content)
            .NotEmpty().WithMessage("El contenido no puede estar vacio")
            .MaximumLength(100).WithMessage("El contenido no tener mas de 100 caracteres");

        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("La forma de contacto debe tener un nombre")
            .MaximumLength(100).WithMessage("El titulo debe tener como maximo 100 caracteres");
    }
}
