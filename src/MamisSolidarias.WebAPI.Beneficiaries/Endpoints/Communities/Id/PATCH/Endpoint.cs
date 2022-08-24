using FastEndpoints;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.Utils.Security;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.PATCH;

internal class Endpoint : Endpoint<Request,Response>
{
    public Endpoint(BeneficiariesDbContext dbContext, DbAccess? db = null) 
        => _db = db ?? new DbAccess(dbContext);
    
    private readonly DbAccess _db;
    
    public override void Configure()
    {
        Patch("communities/{id}");
        Policies(Services.Beneficiaries.WritePermission());
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var community = await _db.GetCommunityById(req.Id, ct);

        if (community is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (req.Description is not null)
            community.Description = req.Description;

        if (req.Address is not null)
            community.Address = req.Address;

        await _db.SaveChanges(ct);

        await SendOkAsync(new Response(Map(community)), ct);
    }
    
    private static CommunityResponse Map(Community c)
        => new (c.Id!, c.Name, c.Description, c.Address);
}