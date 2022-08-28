using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.POST;

internal class DbAccess
{
    private readonly BeneficiariesDbContext? _dbContext;

    public DbAccess() {}
    public DbAccess(BeneficiariesDbContext? dbContext) => _dbContext = dbContext;

    public virtual async Task CreateFamilies(IEnumerable<Family> families, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_dbContext);
        
        foreach (var family in families)
        {
            await _dbContext.Families.AddAsync(family,ct);
        }
        
        await _dbContext.SaveChangesAsync(ct);
    }
}