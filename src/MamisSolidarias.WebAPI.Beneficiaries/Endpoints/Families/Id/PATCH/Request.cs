using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.Id.PATCH;

public class Request
{
    /// <summary>
    /// Id of the family
    /// </summary>
    [FromRoute] public string FamilyId { get; set; } = string.Empty;
    
    /// <summary>
    /// New name of the family
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// New address of the family
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// New details of the family
    /// </summary>
    public string? Details { get; set; }
    
    /// <summary>
    /// New contacts for the family
    /// </summary>
    public IEnumerable<ContactRequest>? Contacts { get; set; }
}

internal class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleForEach(t => t.Contacts)
            .SetValidator(new ContactRequestValidator());
        
        RuleFor(t => t.Address)
            .MaximumLength(500).WithMessage("La dirección no puede tener más de 500 caracteres");

        RuleFor(t => t.Details)
            .MaximumLength(500).WithMessage("Los comentarios no pueden tener mas de 500 caracteres");

        RuleFor(t => t.Name)
            .MaximumLength(100).WithMessage("El nombre de familia no puede tener mas de 100 caracteres");

        RuleFor(t => t.FamilyId)
            .NotEmpty().WithMessage("Se debe indicar una familia");
    }
}


/// <param name="Type">Type of contact (facebook, whatsapp, instagram, phone, other)</param>
/// <param name="Content">Content of the contact</param>
/// <param name="Title">Title of the contact</param>
/// <param name="IsPreferred">Is it a preferred method of communication</param>
public record ContactRequest(string Type, string Content, string Title, bool IsPreferred);


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

        RuleFor(t => t.Type)
            .IsEnumName(typeof(MamisSolidarias.Infrastructure.Beneficiaries.Models.ContactType))
            .WithMessage("El tipo de contacto no es valido");
    }
}