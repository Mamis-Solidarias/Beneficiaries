using FastEndpoints;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.POST;
using Microsoft.EntityFrameworkCore;
using ContactType = MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.POST.ContactType;

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
                Type = MapContactType(t.Type)
            }).ToList()
        };

    private static MamisSolidarias.Infrastructure.Beneficiaries.Models.ContactType MapContactType (ContactType t)
        => t switch
        {
            ContactType.Email => Infrastructure.Beneficiaries.Models.ContactType.Email,
            ContactType.Phone => Infrastructure.Beneficiaries.Models.ContactType.Phone,
            ContactType.Whatsapp => Infrastructure.Beneficiaries.Models.ContactType.Whatsapp,
            ContactType.Facebook => Infrastructure.Beneficiaries.Models.ContactType.Facebook,
            ContactType.Other => Infrastructure.Beneficiaries.Models.ContactType.Other,
            ContactType.Instagram => Infrastructure.Beneficiaries.Models.ContactType.Instagram,
            _ => throw new ArgumentOutOfRangeException(nameof(t), t, "Invalid ContactType")
        };
}