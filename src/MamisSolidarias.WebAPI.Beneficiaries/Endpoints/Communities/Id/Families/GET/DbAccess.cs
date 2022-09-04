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
            .CountAsync(t => t.CommunityId == communityId,ct);
        
    }
    
    // retrieve all families from the database
    public virtual Task<List<Family>> GetFamilies(CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_db);
        return _db.Families.AsNoTracking()
            .Include(t => t.Contacts)
            .ToListAsync(ct);
    }
    
    // retrieve all families from the database without contacts
    public virtual Task<List<Family>> GetFamiliesWithoutContacts(CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_db);
        return _db.Families.AsNoTracking()
            .ToListAsync(ct);
    }
    
    // update a single family and retrieve all communities
    public virtual async Task<List<Community>> UpdateFamily(Family family, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_db);
        _db.Families.Update(family);
        await _db.SaveChangesAsync(ct);
        return await _db.Communities.AsNoTracking().ToListAsync(ct);
    }
    
    // update a single family and retrieve all communities tracking them
    public virtual async Task<List<Community>> UpdateFamilyTracking(Family family, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_db);
        _db.Families.Update(family);
        await _db.SaveChangesAsync(ct);
        return await _db.Communities.ToListAsync(ct);
    }
}