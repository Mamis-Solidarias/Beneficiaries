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
                    Comments = t.Comments?.Trim(),
                    Likes = t.Likes?.Trim(),
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
        return j is null ? null : new Job {Title = j.Title};
    }

    private static Health? Map(HealthRequest? h)
    {
        if (h is null)
            return null;

        return new Health
        {
            HasCovidVaccine = h.HasCovidVaccine,
            HasMandatoryVaccines = h.HasMandatoryVaccines,
            Observations = h.Observations
        };
    }
    private static Education? Map(EducationRequest? e)
    {
        if (e is null)
            return null;

        return new Education
        {
            Year = e.Year.Parse<SchoolYear>(),
            Cycle = e.Year.Parse<SchoolYear>().ToSchoolCycle(),
            School = e.School,
            TransportationMethod = e.TransportationMethod.Parse<TransportationMethod>()
        };
    }

    private static Clothes? Map(ClothesRequest? c)
    {
        if (c is null)
            return null;
        
        return new Clothes
        {
            ShoeSize = c.Shoes,
            ShirtSize = c.Shirt,
            PantsSize = c.Pants
        };
    }
}