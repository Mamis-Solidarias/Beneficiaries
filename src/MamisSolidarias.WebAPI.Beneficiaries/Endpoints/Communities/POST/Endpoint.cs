using FastEndpoints;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.POST;

internal class Endpoint : Endpoint<Request,Response>
{
    private readonly DbAccess _db;

    public Endpoint(BeneficiariesDbContext dbContext, DbAccess? db = null)
    {
        _db = db ?? new DbAccess(dbContext);
    }

    public override void Configure()
    {
        Post("communities");
        Policies(Utils.Security.Policies.CanWrite);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        try
        {
            var mappedCommunities = req.Communities.Select(Map);
            var communities = await _db.CreateCommunities(mappedCommunities, ct);

            await SendAsync(new Response
            {
                Communities = communities.Select(t => t.Id).OfType<string>()
            }, 201, ct);
        }
        catch (DbUpdateException)
        {
            AddError("Las comunidades ingresadas no son validas");
            await SendErrorsAsync(409, ct);
        }
    }

    private static Community Map(CommunityRequest req)
        => new()
        {
            Description = string.IsNullOrWhiteSpace(req.Description) ? null : req.Description.Trim(),
            Address = req.Address.Trim(),
            Name = req.Name.Trim(),
            Id = string.IsNullOrWhiteSpace(req.CommunityCode) ? null : req.CommunityCode.Trim()
        };
}