using Microsoft.AspNetCore.Mvc;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.GET;

public class Request
{
    /// <summary>
    /// Id of the community
    /// </summary>
    [FromRoute] public string Id { get; set; } = String.Empty;
};