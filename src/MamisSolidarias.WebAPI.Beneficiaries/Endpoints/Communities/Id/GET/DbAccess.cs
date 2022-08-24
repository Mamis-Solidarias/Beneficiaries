using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.GET;

internal class DbAccess
{
    private readonly BeneficiariesDbContext? _dbContext;

    public DbAccess() { }
    public DbAccess(BeneficiariesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual Task<Community?> GetCommunityFromId(string id, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(_dbContext);
        return _dbContext.Communities
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, token);
    }
}