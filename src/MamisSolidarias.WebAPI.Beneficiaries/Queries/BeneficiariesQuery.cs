using HotChocolate.AspNetCore.Authorization;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.AspNetCore.Mvc;

namespace MamisSolidarias.WebAPI.Beneficiaries.Queries;

[ExtendObjectType("Query")]
internal sealed class BeneficiariesQuery
{
    
    [Authorize(Policy = "CanRead")]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Beneficiary> GetBeneficiaries([FromServices] BeneficiariesDbContext dbContext) =>
        dbContext.Beneficiaries;
    
    [UseFirstOrDefault]
    [Authorize(Policy = "CanRead")]
    [UseProjection]
    public IQueryable<Beneficiary?> GetBeneficiary([FromServices] BeneficiariesDbContext dbContext, int id) =>
        dbContext.Beneficiaries.Where(t=> t.Id == id);



 
    
    

}