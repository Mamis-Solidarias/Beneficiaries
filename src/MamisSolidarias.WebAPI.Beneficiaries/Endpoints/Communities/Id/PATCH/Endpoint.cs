using FastEndpoints;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.PATCH;

internal class Endpoint : Endpoint<Request,Response>
{
    public Endpoint(BeneficiariesDbContext dbContext, DbAccess? db = null) 
        => _db = db ?? new DbAccess(dbContext);
    
    private readonly DbAccess _db;
    
    public override void Configure()
    {
        Patch("communities/{id}");
        Policies(Utils.Security.Policies.CanWrite);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var community = await _db.GetCommunityById(req.Id, ct);
        if (community is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (!string.IsNullOrWhiteSpace(req.Description))
            community.Description = req.Description.Trim();

        if (!string.IsNullOrWhiteSpace(req.Address))
            community.Address = req.Address.Trim();
        
        await _db.SaveChanges(ct);

        await SendOkAsync(new Response(Map(community)), ct);
    }
    
    private static CommunityResponse Map(Community c)
        => new (c.Id!, c.Name, c.Description, c.Address);
}