using FastEndpoints;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.Utils.Security;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.GET;

internal class Endpoint : EndpointWithoutRequest<Response>
{
    private readonly DbAccess _db;

    public Endpoint(BeneficiariesDbContext dbContext,DbAccess? db = null)
    {
        _db = db ?? new DbAccess(dbContext);
    }

    public override void Configure()
    {
        Get("communities");
        Policies(Services.Beneficiaries.ReadPermission());
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var communities = await _db.GetCommunities(ct);

        await SendOkAsync(new Response
        {
            Communities = communities.Select(Map)
        }, ct);
    }

    private static CommunityResponse Map(Community community) =>
        new(community.Id!, community.Name, community.Description, community.Address);
}