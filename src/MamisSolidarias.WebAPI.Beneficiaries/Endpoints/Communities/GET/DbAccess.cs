using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.GET;

internal class DbAccess
{
    private readonly BeneficiariesDbContext? _dbContext;

    public DbAccess() { }

    public DbAccess(BeneficiariesDbContext? dbContext)
    {
        _dbContext = dbContext;
    }


    /// <summary>
    /// It returns all the created communities
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns>List of communities</returns>
    public virtual Task<List<Community>> GetCommunities(CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_dbContext);
        return _dbContext.Communities
            .AsNoTracking()
            .ToListAsync(ct);
    }
}