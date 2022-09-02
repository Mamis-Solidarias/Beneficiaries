using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Beneficiaries.Id.POST;

public class Request
{
    /// <summary>
    /// Beneficiary's ID
    /// </summary>
    [FromRoute] public int Id { get; set; }
}

internal class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(t => t.Id)
            .NotEmpty().WithMessage("Se debe indicar el Id de un beneficiario")
            .GreaterThan(0).WithMessage("El Id debe ser valido");
    }
}