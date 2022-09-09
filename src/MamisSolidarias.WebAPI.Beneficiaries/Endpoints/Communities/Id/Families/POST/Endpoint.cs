using FastEndpoints;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.POST;

internal class Endpoint : Endpoint<Request>
{
    private readonly DbAccess _db;

    public Endpoint(BeneficiariesDbContext dbContext, DbAccess? db = null)
        => _db = db ?? new DbAccess(dbContext);


    public override void Configure()
    {
        Post("communities/{id}/families");
        Policies(Utils.Security.Policies.CanWrite);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        try
        {
            var families = req.Families.Select(t=> Map(t,req.Id)).ToList();
            await _db.CreateFamilies(families, ct);
            await SendAsync(new{},201,ct);
        }
        catch (DbUpdateException)
        {
            AddError("Las familias ingresadas no son validas");
            await SendErrorsAsync(409,ct);
        }
    }

    private static Family Map(FamilyRequest f, string communityId)
        => new()
        {
            // To be overwritten by postgres using triggers
            Id = Guid.NewGuid().ToString(),
            FamilyNumber = f.FamilyNumber ?? 0,
            Address = f.Address,
            Details = f.Details,
            CommunityId = communityId,
            Name = f.Name,
            Contacts = f.Contacts.Select(t => new Contact
            {
                Content = t.Content,
                IsPreferred = t.IsPreferred,
                Title = t.Title,
                Type = Enum.Parse<ContactType>(t.Type)
            }).ToList()
        };
    
}