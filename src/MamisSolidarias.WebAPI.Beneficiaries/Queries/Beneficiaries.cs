using HotChocolate.AspNetCore.Authorization;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.AspNetCore.Mvc;

namespace MamisSolidarias.WebAPI.Beneficiaries.Queries;

public class Beneficiaries
{
    // UsePaging > UseProjections > UseFiltering > UseSorting
    
    [Authorize(Policy = "CanRead")]
    [UsePaging]
    [UseProjection]
    [HotChocolate.Data.UseFiltering]
    [HotChocolate.Data.UseSorting]
    public IQueryable<Beneficiary> GetBeneficiaries([FromServices] BeneficiariesDbContext dbContext) =>
        dbContext.Beneficiaries;
}