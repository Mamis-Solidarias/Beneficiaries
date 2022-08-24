using FastEndpoints;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.Utils.Security;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.GET;

internal class Endpoint : Endpoint<Request,Response>
{
    private readonly DbAccess _db;

    public Endpoint(BeneficiariesDbContext dbContext,DbAccess? dbAccess=null)
    {
        _db = dbAccess ?? new DbAccess(dbContext);
    }

    public override void Configure()
    {
        Get("communities/{id}");
        Policies(Services.Beneficiaries.ReadPermission());
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var community = await _db.GetCommunityFromId(req.Id, ct);

        if (community is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        await SendOkAsync(new Response(Map(community)), ct);
    }

    private static CommunityResponse Map(Community c)
        => new (c.Id!, c.Name, c.Description, c.Address);
}