using HotChocolate.AspNetCore.Authorization;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Queries;

[ExtendObjectType("Query")]
internal sealed class CommunitiesQuery
{
    [Authorize(Policy = "CanRead")]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Community> GetCommunities(BeneficiariesDbContext dbContext) =>
        dbContext.Communities;

    [UseFirstOrDefault]
    [Authorize(Policy = "CanRead")]
    [UseProjection]
    public IQueryable<Community?> GetCommunity(BeneficiariesDbContext dbContext,string id) =>
        dbContext.Communities.Where(t=> t.Id == id);
}