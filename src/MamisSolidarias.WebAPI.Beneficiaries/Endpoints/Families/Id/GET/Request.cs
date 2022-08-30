using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.Id.GET;

public class Request
{
    /// <summary>
    /// Id of the family
    /// </summary>
    [FromRoute] public string FamilyId { get; set; } = string.Empty;
}

internal class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(t => t.FamilyId)
            .NotEmpty().WithMessage("Se debe indicar una familia");
    }
}