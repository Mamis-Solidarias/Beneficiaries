using FastEndpoints;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.POST;

internal class Endpoint : Endpoint<Request>
{
    private readonly DbAccess _db;

    public Endpoint(BeneficiariesDbContext dbContext, DbAccess? db = null)
        => _db = db ?? new DbAccess(dbContext);


    public override void Configure()
    {
        Post("families");
        Policies(Utils.Security.Policies.CanWrite);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        try
        {
            var families = req.Families.Select(Map).ToList();
            await _db.CreateFamilies(families, ct);
            await SendOkAsync(ct);
        }
        catch (DbUpdateException)
        {
            AddError("Las familias ingresadas no son validas");
            await SendErrorsAsync(409,ct);
        }
    }

    private static Family Map(FamilyRequest f)
        => new()
        {
            FamilyNumber = f.FamilyNumber ?? 0,
            Address = f.Address,
            Details = f.Details,
            CommunityId = f.CommunityId,
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