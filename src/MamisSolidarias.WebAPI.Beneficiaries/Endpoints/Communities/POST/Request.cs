using FastEndpoints;
using FluentValidation;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.POST;

public class Request
{
    /// <summary>
    /// Communities to create
    /// </summary>
    public IEnumerable<CommunityRequest> Communities { get; set; } = new List<CommunityRequest>();
}

/// <summary>
/// Definition of a community
/// </summary>
/// <param name="Name">Name of the community</param>
/// <param name="Address">Address of the community</param>
/// <param name="Description">Description of the community. It is optional</param>
/// <param name="CommunityCode">Identifier for the community. It is optional</param>
public record CommunityRequest(string Name,string Address, string? Description, string? CommunityCode);

internal class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleForEach(t => t.Communities)
            .SetValidator(new CommunityValidator());

    }
}

internal class CommunityValidator : Validator<CommunityRequest>
{
    public CommunityValidator()
    {
        RuleFor(t => t.Address)
            .MaximumLength(500).WithMessage("La direccion no puede tener mas de 500 caracteres")
            .MinimumLength(5).WithMessage("La direccion no puede tener menos de 5 caracteres");

        RuleFor(t => t.Description)
            .MaximumLength(500).WithMessage("La descripcion no puede tener mas de 500 caracteres");
        
        RuleFor(t=> t.Name)
            .MaximumLength(500).WithMessage("El nombre no puede tener mas de 500 caracteres")
            .MinimumLength(5).WithMessage("El nombre no puede tener menos de 5 caracteres");

        RuleFor(t => t.CommunityCode)
            .MaximumLength(5).WithMessage("El codigo de la comunidad no puede tener mas de 5 caracteres")
            .Must(t=> t is null || t.Trim() != string.Empty)
            .WithMessage("El codigo de la comunidad no puede ser un string vacio");

    }
}
