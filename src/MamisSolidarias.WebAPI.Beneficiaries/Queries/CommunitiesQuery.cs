using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Queries;

[ExtendObjectType("Query")]
internal sealed class CommunitiesQuery
{
    [Authorize(Policy = "CanRead")]
    [UsePaging(IncludeTotalCount = true, MaxPageSize = 500)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Community> GetCommunities(BeneficiariesDbContext dbContext) =>
        dbContext.Communities
            .OrderBy(t=> t.Id);

    [UseFirstOrDefault]
    [Authorize(Policy = "CanRead")]
    [UseProjection]
    public IQueryable<Community?> GetCommunity(BeneficiariesDbContext dbContext,string id) =>
        dbContext.Communities.Where(t=> t.Id == id);
}