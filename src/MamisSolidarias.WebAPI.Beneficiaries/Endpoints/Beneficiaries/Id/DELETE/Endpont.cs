using FastEndpoints;
using MamisSolidarias.Infrastructure.Beneficiaries;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Beneficiaries.Id.DELETE;

internal class Endpoint : Endpoint<Request>
{
    private readonly DbAccess _db;

    public Endpoint(BeneficiariesDbContext dbContext, DbAccess? dbAccess = null)
    {
        _db = dbAccess ?? new DbAccess(dbContext);
    }

    public override void Configure()
    {
        Delete("beneficiaries/{id}");
        Policies(Utils.Security.Policies.CanWrite);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var beneficiary = await _db.GetBeneficiary(req.Id, ct);

        if (beneficiary is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        beneficiary.IsActive = false;

        await _db.SaveChanges(ct);

        await SendOkAsync(ct);
    }


}