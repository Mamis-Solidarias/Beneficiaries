using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.POST;

internal class DbAccess
{
    private readonly BeneficiariesDbContext? _dbContext;
    
    public DbAccess() {}

    public DbAccess(BeneficiariesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<IEnumerable<Community>> CreateCommunities(IEnumerable<Community> communities, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_dbContext);
        var enumerable = communities as Community[] ?? communities.ToArray();
        await _dbContext.Communities.AddRangeAsync(enumerable, ct);
        await _dbContext.SaveChangesAsync(ct);
        return enumerable;
    }
}