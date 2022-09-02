using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Beneficiaries.Id.DELETE;

internal class DbAccess
{
    private readonly BeneficiariesDbContext? _dbContext;

    public DbAccess()
    {
    }

    public DbAccess(BeneficiariesDbContext? dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual Task<Beneficiary?> GetBeneficiary(int id, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_dbContext);
        return _dbContext.Beneficiaries
            .FirstOrDefaultAsync(t => t.IsActive == true && t.Id == id, ct);
    }

    public virtual Task SaveChanges(CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_dbContext);
        return _dbContext.SaveChangesAsync(ct);
    }
}