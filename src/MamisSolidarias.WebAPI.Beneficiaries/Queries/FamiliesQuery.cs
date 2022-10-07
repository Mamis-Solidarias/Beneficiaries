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

    [Authorize(Policy = "CanRead")]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Family?> GetFilteredFamilies([FromServices] BeneficiariesDbContext dbContext, FamiliesFilter filter)
    {
        var query = dbContext.Families.AsQueryable();

        if (filter.CommunityId is not null)
            query = query.Where(t => t.CommunityId == filter.CommunityId);
        
        if (filter.Name is not null)
            query = query.Where(t => t.Name.ToLower().Contains(filter.Name.ToLower()));

        return query;
    }

    public record FamiliesFilter(string? CommunityId, string? Name);
}


