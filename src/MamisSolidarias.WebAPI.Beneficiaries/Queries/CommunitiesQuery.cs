using HotChocolate.AspNetCore.Authorization;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.AspNetCore.Mvc;

namespace MamisSolidarias.WebAPI.Beneficiaries.Queries;

[ExtendObjectType("Query")]
internal sealed class CommunitiesQuery
{
    [Authorize(Policy = "CanRead")]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Community> GetCommunities([FromServices] BeneficiariesDbContext dbContext) =>
        dbContext.Communities;

    [UseFirstOrDefault]
    [Authorize(Policy = "CanRead")]
    [UseProjection]
    public IQueryable<Community?> GetCommunity([FromServices] BeneficiariesDbContext dbContext,string id) =>
        dbContext.Communities.Where(t=> t.Id == id);
}