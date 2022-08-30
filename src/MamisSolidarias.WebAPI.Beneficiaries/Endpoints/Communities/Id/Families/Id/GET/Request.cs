using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.Id.GET;

public class Request
{
    /// <summary>
    /// Id of the community
    /// </summary>
    [FromRoute] public string CommunityId { get; set; } = string.Empty;

    /// <summary>
    /// Id of the family
    /// </summary>
    [FromRoute] public string FamilyId { get; set; } = string.Empty;
}

internal class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(t => t.CommunityId)
            .NotEmpty().WithMessage("Se debe indicar la comunidad a la que pertenece");

        RuleFor(t => t.FamilyId)
            .NotEmpty().WithMessage("Se debe indicar una familia")
            .Must((r, t) => t.ToUpperInvariant().StartsWith(r.CommunityId.ToUpperInvariant()))
            .WithMessage("La familia debe ser de la misma comunidad");
    }
}