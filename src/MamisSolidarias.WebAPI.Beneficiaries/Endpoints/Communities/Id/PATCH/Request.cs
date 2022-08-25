using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.PATCH;

public class Request
{
    /// <summary>
    /// Id of the community to update
    /// </summary>
    [FromRoute] public string Id { get; set; } = string.Empty;
    
    /// <summary>
    /// Optional. New address of the community
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// Optional. New description of the community
    /// </summary>
    public string? Description { get; set; }
}

internal class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(t => t.Address)
            .MaximumLength(500).WithMessage("La direccion no puede tener mas de 500 caracteres")
            .MinimumLength(5).WithMessage("La direccion no puede tener menos de 5 caracteres");

        RuleFor(t => t.Description)
            .MaximumLength(500).WithMessage("La descripcion no puede tener mas de 500 caracteres");
        
    }
}
