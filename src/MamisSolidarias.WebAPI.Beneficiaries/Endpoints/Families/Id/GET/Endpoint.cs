using FastEndpoints;
using MamisSolidarias.Infrastructure.Beneficiaries;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.Id.GET;

internal class Endpoint : Endpoint<Request,Response>
{
    private readonly DbAccess _db;

    public Endpoint(BeneficiariesDbContext dbContext, DbAccess? db = null)
        => _db = db ?? new DbAccess(dbContext);

    public override void Configure()
    {
        Get(
            "families/{familyId}"
            );
        Policies(Utils.Security.Policies.CanRead);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var family = await _db.GetFamily(req.FamilyId, ct);

        if (family is null)
            await SendNotFoundAsync(ct);
        else
            await SendOkAsync(new Response(family.Id, family.Name, family.Address, family.Details,
                family.Contacts.Select(t =>
                    new ContactResponse(t.Type.ToString(), t.Content, t.Title, t.IsPreferred)
                )
            ), ct);
    }
}