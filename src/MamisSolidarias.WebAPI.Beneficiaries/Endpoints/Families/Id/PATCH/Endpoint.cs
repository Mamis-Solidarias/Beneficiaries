using FastEndpoints;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.Id.PATCH;

internal class Endpoint : Endpoint<Request,Response>
{
    private readonly DbAccess _db;

    public Endpoint(BeneficiariesDbContext dbContext, DbAccess? dbAccess = null)
        => _db = dbAccess ?? new DbAccess(dbContext);

    public override void Configure()
    {
        Patch("families/{familyId}");
        Policies(Utils.Security.Policies.CanWrite);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var family = await _db.GetFamily(req.FamilyId,ct);

        if (family is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (!string.IsNullOrWhiteSpace(req.Address))
            family.Address = req.Address;
        
        if (req.Contacts is not null)
            family.Contacts = req.Contacts.Select(Map).ToList();

        if (!string.IsNullOrWhiteSpace(req.Details))
            family.Details = req.Details;

        if (!string.IsNullOrWhiteSpace(req.Name))
            family.Name = req.Name;

        await _db.SaveChanges(ct);

        await SendOkAsync(new Response(family.Id, family.Name, family.Address, family.Details,
            family.Contacts.Select(Map)),ct);
    }

    private static Contact Map(ContactRequest t)
        => new()
        {
            Type = Enum.Parse<ContactType>(t.Type),
            Content = t.Content.Trim(),
            Title = t.Title.Trim(),
            IsPreferred = t.IsPreferred,
        };
    
    private static ContactResponse Map(Contact t)
        => new ($"{t.Type}",t.Content,t.Title,t.IsPreferred);

}