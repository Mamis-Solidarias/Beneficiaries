using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.POST;

internal class DbAccess
{
    private readonly BeneficiariesDbContext? _dbContext;

    public DbAccess() {}
    public DbAccess(BeneficiariesDbContext? dbContext) => _dbContext = dbContext;

    public virtual Task CreateFamilies(IEnumerable<Family> families, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_dbContext);
        var enumerable = families as Family[] ?? families.ToArray();
        
        _dbContext.Families.AddRange(enumerable);
        return _dbContext.SaveChangesAsync(ct);
    }

    public virtual Task<List<Community>> GetCommunities(IEnumerable<string> select, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_dbContext);
        return _dbContext.Communities.Where(t => select.Contains(t.Id)).ToListAsync(ct);
    }
}