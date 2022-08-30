using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.GET;

public class Request
{
    /// <summary>
    /// Id of the community
    /// </summary>
    [FromRoute]
    public string Id { get; set; } = string.Empty;
    
    /// <summary>
    /// Page number. It has to be 0 or higher
    /// </summary>
    [FromQuery] public int Page { get; set; }
    
    /// <summary>
    /// Size of the page. It has to be greater than or equal to 5.
    /// </summary>
    [FromQuery] public int PageSize { get; set; }
}

internal class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(t => t.Id)
            .NotEmpty();

        RuleFor(t => t.Page)
            .GreaterThanOrEqualTo(0);

        RuleFor(t => t.PageSize)
            .GreaterThanOrEqualTo(5);
    }
}