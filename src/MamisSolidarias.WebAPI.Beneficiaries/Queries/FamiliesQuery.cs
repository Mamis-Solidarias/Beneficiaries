using HotChocolate.AspNetCore.Authorization;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.AspNetCore.Mvc;

namespace MamisSolidarias.WebAPI.Beneficiaries.Queries;

[ExtendObjectType("Query")]
internal sealed class FamiliesQuery
{
    [Authorize(Policy = "CanRead")]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Family> GetFamilies([FromServices] BeneficiariesDbContext dbContext) =>
        dbContext.Families;
    
    [UseFirstOrDefault]
    [Authorize(Policy = "CanRead")]
    [UseProjection]
    public IQueryable<Family?> GetFamily([FromServices] BeneficiariesDbContext dbContext, string id) =>
        dbContext.Families.Where(t=> t.Id == id);
}
