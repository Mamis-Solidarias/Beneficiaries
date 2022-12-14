using FastEndpoints;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.GET;

internal class Endpoint : Endpoint<Request,Response>
{
    private readonly DbAccess _db;

    public Endpoint(BeneficiariesDbContext dbContext, DbAccess? db = null)
    {
        _db = db ?? new DbAccess(dbContext);
    }

    public override void Configure()
    {
        Get("communities/{id}/families");
        Policies(Utils.Security.Policies.CanRead);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var families = await _db.GetFamilies(req.Id, req.Page, req.PageSize, ct);
        var totalEntries = await _db.GetTotalEntries(req.Id, ct);
       
        await SendOkAsync(new Response
        {
            Families = families.Select(Map),
            Page = req.Page,
            TotalPages = totalEntries / req.PageSize + (totalEntries % req.PageSize is 0 ? 0 : 1)
        }, ct);
    }

    private static FamilyResponse Map(Family f)
        => new (f.Id, f.Name, f.Address, f.Details,
            f.Contacts.Select(r =>
                new ContactResponse(r.Type.ToString(), r.Content, r.Title, r.IsPreferred))
        );
}