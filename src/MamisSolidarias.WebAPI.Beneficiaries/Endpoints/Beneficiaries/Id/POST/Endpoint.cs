using FastEndpoints;
using MamisSolidarias.Infrastructure.Beneficiaries;


namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Beneficiaries.Id.POST;

internal class Endpoint : Endpoint<Request>
{
    private readonly DbAccess _db;

    public Endpoint(BeneficiariesDbContext dbContext, DbAccess? dbAccess = null)
    {
        _db = dbAccess ?? new DbAccess(dbContext);
    }

    public override void Configure()
    {
        Post("beneficiaries/{id}");
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

        if (beneficiary.IsActive is true)
        {
            AddError("El beneficiario no esta inactivo");
            await SendErrorsAsync(cancellation:ct);
            return;
        }

        beneficiary.IsActive = true;

        await _db.SaveChanges(ct);
        await SendOkAsync(ct);
    }


}