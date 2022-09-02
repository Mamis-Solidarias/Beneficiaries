using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.Id.Beneficiaries.POST;

internal class DbAccess
{
    private readonly BeneficiariesDbContext? _dbContext;
    public DbAccess() { }
    public DbAccess(BeneficiariesDbContext? dbContext) => _dbContext = dbContext;

    public virtual async Task<bool> FamilyDoesNotExist(string familyId, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_dbContext);
        return ! await _dbContext.Families.AnyAsync(t => t.Id == familyId, ct);
    }

    public virtual async Task AddBeneficiaries(Beneficiary[] people, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_dbContext);
        await _dbContext.Beneficiaries.AddRangeAsync(people, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
}