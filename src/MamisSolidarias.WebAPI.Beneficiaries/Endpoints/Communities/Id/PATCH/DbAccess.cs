using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.PATCH;

internal class DbAccess
{
    private readonly BeneficiariesDbContext? _dbContext;
    public DbAccess() { }
    public DbAccess(BeneficiariesDbContext dbContext) => _dbContext = dbContext;

    
    public virtual Task<Community?> GetCommunityById(string id, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_dbContext);
        return _dbContext.Communities.FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public virtual Task SaveChanges(CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_dbContext);
        return _dbContext.SaveChangesAsync(ct);
    }
}