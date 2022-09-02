using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Beneficiaries.Id.DELETE;

public class Request
{
    /// <summary>
    /// Id of the Beneficiary to delete
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