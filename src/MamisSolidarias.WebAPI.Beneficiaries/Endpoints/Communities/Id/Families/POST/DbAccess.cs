using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.POST;

internal class DbAccess
{
    private readonly BeneficiariesDbContext? _dbContext;

    public DbAccess() {}
    public DbAccess(BeneficiariesDbContext? dbContext) => _dbContext = dbContext;

    public virtual async Task CreateFamilies(IEnumerable<Family> families, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_dbContext);
        
        await _dbContext.Families.AddRangeAsync(families,ct);
        
        await _dbContext.SaveChangesAsync(ct);
    }
}