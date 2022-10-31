using HotChocolate.AspNetCore.Authorization;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Beneficiaries.Queries;

[ExtendObjectType("Query")]
internal sealed class BeneficiariesQuery
{
    
    [Authorize(Policy = "CanRead")]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Beneficiary> GetBeneficiaries(BeneficiariesDbContext dbContext) =>
        dbContext.Beneficiaries;
    
    [UseFirstOrDefault]
    [Authorize(Policy = "CanRead")]
    [UseProjection]
    public IQueryable<Beneficiary?> GetBeneficiary(BeneficiariesDbContext dbContext, int id) =>
        dbContext.Beneficiaries.Where(t=> t.Id == id);


    [Authorize(Policy = "CanRead")]
    [UsePaging]
    [UseProjection]
    [UseSorting]
    public IQueryable<Beneficiary> GetFilteredBeneficiaries(BeneficiariesDbContext dbContext,FilterParams filter)
    {
        var query = dbContext.Beneficiaries.AsQueryable();

        if (filter.AgeStart is not null)
        {
            var date = DateOnly.FromDateTime(DateTime.Today.AddYears(-filter.AgeStart.Value));
            query = query.Where(t => t.Birthday <= date);
        }

        if (filter.AgeEnd is not null)
        {
            var date = DateOnly.FromDateTime(DateTime.Today.AddYears(-filter.AgeEnd.Value));
            query = query.Where(t => t.Birthday >= date);
        }

        if (filter.Gender is not null && Enum.TryParse<BeneficiaryGender>(filter.Gender,true, out var gender))
            query = query.Where(t => t.Gender == gender);
        
        if (filter.FirstName is not null)
            query = query.Where(t => t.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
        
        if (filter.LastName is not null)
            query = query.Where(t => t.LastName.ToLower().Contains(filter.LastName.ToLower()));
        
        if (filter.Type is not null && Enum.TryParse<BeneficiaryType>(filter.Type,true,out var type))
            query = query.Where(t => t.Type == type);
        
        if (filter.DniStarts is not null)
            query = query.Where(t => t.Dni.StartsWith(filter.DniStarts));
        
        if (filter.FamilyId is not null)
            query = query.Where(t => t.FamilyId == filter.FamilyId);
        
        if (filter.CommunityId is not null)
            query = query.Include(t=> t.Family)
                .Where(t => t.Family!.CommunityId == filter.CommunityId);

        if (filter.School is not null)
            query = query.Include(t => t.Education)
                .Where(t => t.Education != null &&
                            t.Education.School != null &&
                            t.Education.School.ToLower().Contains(filter.School.ToLower())
                );
        
        query = filter.IsActive is not null ? 
            query.Where(t => t.IsActive == filter.IsActive) : 
            query.Where(t => t.IsActive == true);

        return query;
    }

    public record FilterParams(
        int? AgeStart, int? AgeEnd, string? FirstName, string? LastName, string? Type, string? DniStarts,string? FamilyId,
        string? CommunityId, string? School, string? Gender, bool? IsActive
        );
}