using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.GET;

internal class DbAccess
{
    private readonly BeneficiariesDbContext? _db;

    public DbAccess() { }
    public DbAccess(BeneficiariesDbContext dbContext) => _db = dbContext;

    public virtual Task<List<Family>> GetFamilies(string communityId, int page, int pageSize, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_db);
        return _db.Families.AsNoTracking()
            .Include(t => t.Contacts)
            .Where(t => t.CommunityId == communityId)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public virtual Task<int> GetTotalEntries(string communityId, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_db);
        return _db.Families
            .CountAsync(t => t.CommunityId == communityId, ct);
    }

}