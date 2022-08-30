using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.Id.GET;

internal class DbAccess
{
    private readonly BeneficiariesDbContext? _dbContext;
    public DbAccess() { }
    public DbAccess(BeneficiariesDbContext? dbContext) => _dbContext = dbContext;
    
    public virtual Task<Family?> GetFamily(string id,CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_dbContext);
        return _dbContext.Families
            .Include(t=> t.Contacts)
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }
}