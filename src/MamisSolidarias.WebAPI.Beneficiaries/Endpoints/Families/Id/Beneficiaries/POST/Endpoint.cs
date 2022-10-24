using EntityFramework.Exceptions.Common;
using FastEndpoints;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.WebAPI.Beneficiaries.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.Id.Beneficiaries.POST;

internal class Endpoint : Endpoint<Request,Response>
{
    private readonly DbAccess _db;

    public Endpoint(BeneficiariesDbContext dbContext, DbAccess? db = null)
        => _db = db ?? new DbAccess(dbContext);
    
    public override void Configure()
    {
        Post("families/{FamilyId}/beneficiaries");
        Policies(Utils.Security.Policies.CanWrite);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        try
        {
            if (await _db.FamilyDoesNotExist(req.FamilyId, ct))
            {
                await SendNotFoundAsync(ct);
                return;
            }

            var people = req.Beneficiaries
                .Select(t => new Beneficiary
                {
                    FirstName = t.FirstName.Trim(),
                    LastName = t.LastName.Trim(),
                    Type = Enum.Parse<BeneficiaryType>(t.Type),
                    Gender = Enum.Parse<BeneficiaryGender>(t.Gender),
                    Birthday = t.Birthday,
                    Dni = t.Dni.Replace(".","").Trim(),
                    Comments = string.IsNullOrWhiteSpace(t.Comments) ? null : t.Comments.Trim(),
                    Likes = string.IsNullOrWhiteSpace(t.Likes) ? null : t.Likes.Trim(),
                    FamilyId = req.FamilyId,
                    Clothes = Map(t.Clothes),
                    Education = Map(t.Education),
                    Health = Map(t.Health),
                    Job = Map(t.Job)
                }).ToArray();

            await _db.AddBeneficiaries(people, ct);

            await SendOkAsync(new Response {Beneficiaries = people.Select(t => t.Id)}, ct);

        }
        catch (UniqueConstraintException)
        {
            AddError("Ya existe otra persona con el mismo dni");
            await SendErrorsAsync(409, ct);
        }
        catch (DbUpdateException)
        {
            await SendErrorsAsync(409, ct);
        }
    }

    private static Job? Map(JobRequest? j)
    {
        if (j is null || string.IsNullOrWhiteSpace(j.Title))
            return null;
        
        return new Job {Title = j.Title.Trim()};
    }

    private static Health? Map(HealthRequest? h)
    {
        if (h is null || (h.HasCovidVaccine is null && h.HasMandatoryVaccines is null && string.IsNullOrWhiteSpace(h.Observations)))
            return null;

        return new Health
        {
            HasCovidVaccine = h.HasCovidVaccine,
            HasMandatoryVaccines = h.HasMandatoryVaccines,
            Observations = string.IsNullOrWhiteSpace(h.Observations) ? null : h.Observations.Trim()
        };
    }
    private static Education? Map(EducationRequest? e)
    {
        if (e is null || (string.IsNullOrWhiteSpace(e.School) && string.IsNullOrWhiteSpace(e.Year) && string.IsNullOrWhiteSpace(e.TransportationMethod)))
            return null;

        return new Education
        {
            Year = e.Year.Parse<SchoolYear>(),
            Cycle = e.Year.Parse<SchoolYear>().ToSchoolCycle(),
            School = string.IsNullOrWhiteSpace(e.School) ? null : e.School,
            TransportationMethod = e.TransportationMethod.Parse<TransportationMethod>()
        };
    }

    private static Clothes? Map(ClothesRequest? c)
    {
        if (c is null || (string.IsNullOrWhiteSpace(c.Pants) && string.IsNullOrWhiteSpace(c.Shirt) && string.IsNullOrWhiteSpace(c.Shoes)))
            return null;
        
        return new Clothes
        {
            ShoeSize =string.IsNullOrWhiteSpace(c.Shoes) ? null : c.Shoes.Trim(),
            ShirtSize = string.IsNullOrWhiteSpace(c.Shirt) ? null : c.Shirt.Trim(),
            PantsSize = string.IsNullOrWhiteSpace(c.Pants) ? null : c.Pants.Trim()
        };
    }
}