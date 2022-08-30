using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.PATCH;

internal class DbAccess
{
    private readonly BeneficiariesDbContext? _db;

    public DbAccess() { }
    public DbAccess(BeneficiariesDbContext? db) => _db = db;

    public virtual Task<Family?> GetFamily(string id,CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(_db);
        return _db.Families.FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public virtual Task SaveChanges(CancellationToken ct)
    {
       ArgumentNullException.ThrowIfNull(_db);
       return _db.SaveChangesAsync(ct);
    }
}