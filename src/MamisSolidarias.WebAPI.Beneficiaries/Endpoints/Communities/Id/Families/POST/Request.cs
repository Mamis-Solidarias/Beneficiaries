using FastEndpoints;
using FluentValidation;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using PhoneNumbers;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.POST;

/// <summary>
/// Parameters to create a set of families
/// </summary>
public class Request
{

    /// <summary>
    /// Community Id
    /// </summary>
    public string Id { get; set; } = string.Empty;
    
    /// <summary>
    /// Families to create
    /// </summary>
    public IEnumerable<FamilyRequest> Families { get; set; } = new List<FamilyRequest>();
}

internal class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleForEach(t => t.Families)
            .SetValidator(new FamilyRequestValidation());

        RuleFor(t => t.Families)
            .NotEmpty().WithMessage("Se debe intentar de crear al menos una familia");
        
        RuleFor(t => t.Id)
            .NotEmpty().WithMessage("La familia debe estar asignada a una comunidad")
            .MaximumLength(5).WithMessage("El identificador de comunidad debe tener como maximo 5 caracteres");
    }
}


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
        
    }
}

internal class ContactRequestValidator : Validator<ContactRequest>
{
    public ContactRequestValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("La forma de contacto debe tener un nombre")
            .MaximumLength(100).WithMessage("El titulo debe tener como maximo 100 caracteres");

        RuleFor(t => t.Type)
            .IsEnumName(typeof(ContactType),false)
            .WithMessage("El tipo de contacto no es valido");
        
        RuleFor(t => t.Content)
            .NotEmpty().WithMessage("El contenido no puede estar vacio")
            .Must(t =>
            {
                var phoneNumberValidator = PhoneNumberUtil.GetInstance();
                var phoneNumber = phoneNumberValidator.Parse(t, "AR");
                return phoneNumberValidator.IsValidNumber(phoneNumber) && PhoneNumberType.MOBILE == phoneNumberValidator.GetNumberType(phoneNumber);
            }).WithMessage("El número de teléfono no es valido")
            .When(t=> t.Type.Equals(ContactType.Whatsapp.ToString(),StringComparison.InvariantCultureIgnoreCase))
            .Must(t =>
            {
                var phoneNumberValidator = PhoneNumberUtil.GetInstance();
                var phoneNumber = phoneNumberValidator.Parse(t, "AR");
                return phoneNumberValidator.IsValidNumber(phoneNumber) && PhoneNumberType.FIXED_LINE == phoneNumberValidator.GetNumberType(phoneNumber);
            }).WithMessage("El número de teléfono fijo no es valido")
            .When(t=>  t.Type.Equals(ContactType.Phone.ToString(),StringComparison.InvariantCultureIgnoreCase))
            .MaximumLength(100).WithMessage("El contenido no tener mas de 100 caracteres");
    }
}
