using FastEndpoints;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.WebAPI.Beneficiaries.Extensions;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Beneficiaries.Id.PATCH;

internal class Endpoint : Endpoint<Request, Response>
{
    private readonly DbAccess _db;

    public Endpoint(BeneficiariesDbContext dbContext, DbAccess? dbAccess = null)
    {
        _db = dbAccess ?? new DbAccess(dbContext);
    }

    public override void Configure()
    {
        Patch("beneficiaries/{id}");
        Policies(Utils.Security.Policies.CanWrite);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var b = await _db.GetBeneficiary(req.Id, ct);

        if (b is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (req.Beneficiary.Birthday is not null)
            b.Birthday = req.Beneficiary.Birthday.Value;

        if (req.Beneficiary.Clothes is not null)
            b.Clothes = Map(req.Beneficiary.Clothes);

        if (!string.IsNullOrWhiteSpace(req.Beneficiary.Comments))
            b.Comments = req.Beneficiary.Comments.Trim();

        if (!string.IsNullOrWhiteSpace(req.Beneficiary.Dni))
            b.Dni = req.Beneficiary.Dni.Trim();

        if (req.Beneficiary.Education is not null)
            b.Education = Map(req.Beneficiary.Education);

        if (!string.IsNullOrWhiteSpace(req.Beneficiary.Gender))
            b.Gender = Enum.Parse<BeneficiaryGender>(req.Beneficiary.Gender);

        if (req.Beneficiary.Health is not null)
            b.Health = Map(req.Beneficiary.Health);

        if (req.Beneficiary.Job is not null)
            b.Job = Map(req.Beneficiary.Job);

        if (!string.IsNullOrWhiteSpace(req.Beneficiary.Likes))
            b.Likes = req.Beneficiary.Likes;

        if (!string.IsNullOrWhiteSpace(req.Beneficiary.Type))
            b.Type = Enum.Parse<BeneficiaryType>(req.Beneficiary.Type);

        if (!string.IsNullOrWhiteSpace(req.Beneficiary.FirstName))
            b.FirstName = req.Beneficiary.FirstName;

        if (!string.IsNullOrWhiteSpace(req.Beneficiary.LastName))
            b.LastName = req.Beneficiary.LastName;

        await _db.SaveChanges(ct);
        await SendOkAsync(Map(b), ct);
    }

    private static Response Map(Beneficiary b)
        => new(b.Id, b.FamilyId, b.FirstName, b.LastName, b.Type.ToString(), b.Gender.ToString(),
            b.Birthday, b.Dni, b.Comments, b.Likes, Map(b.Clothes), Map(b.Education), Map(b.Health),
            Map(b.Job)
        );

    private static JobResponse? Map(Job? j)
        => j is null ? null : new JobResponse(j.Title);

    private static HealthResponse? Map(Health? h)
        => h is null ? null : new HealthResponse(h.HasCovidVaccine, h.HasMandatoryVaccines, h.Observations);

    private static EducationResponse? Map(Education? e)
        => e is null ? null : new EducationResponse(e.Year.ToString(),e.Cycle.ToString(), e.School, e.TransportationMethod?.ToString());

    private static ClothesResponse? Map(Clothes? c)
        => c is null ? null : new ClothesResponse(c.ShoeSize, c.ShirtSize, c.PantsSize);

    private static Job? Map(JobRequest? t)
    {
        if (t is null || string.IsNullOrWhiteSpace(t.Title))
            return null;
        
        return new Job { Title = t.Title.Trim() };
    }

    private static Health? Map(HealthRequest? t)
    {
        if (t is null || (string.IsNullOrWhiteSpace(t.Observations) && t.HasCovidVaccine is null && t.HasMandatoryVaccines is null))
            return null;
        
        return new Health
        {
            HasCovidVaccine = t.HasCovidVaccine,
            HasMandatoryVaccines = t.HasMandatoryVaccines,
            Observations = string.IsNullOrWhiteSpace(t.Observations) ? null : t.Observations
        };
    }

    private static Education? Map(EducationRequest? t)
    {
        if (t is null || (string.IsNullOrWhiteSpace(t.School) && string.IsNullOrWhiteSpace(t.Year) && string.IsNullOrWhiteSpace(t.TransportationMethod)))
            return null;
        
        return new Education
        {
            School = string.IsNullOrWhiteSpace(t.School) ? null : t.School.Trim(),
            Year = t.Year.Parse<SchoolYear>(),
            Cycle = t.Year.Parse<SchoolYear>().ToSchoolCycle(),
            TransportationMethod = t.TransportationMethod.Parse<TransportationMethod>()
        };
    }

    private static Clothes? Map(ClothesRequest? t)
    {
        if (t is null || (string.IsNullOrWhiteSpace(t.Shirt) && string.IsNullOrWhiteSpace(t.Pants) && string.IsNullOrWhiteSpace(t.Shoes)))
            return null;
        
        return new Clothes
        {
            PantsSize = string.IsNullOrWhiteSpace(t.Pants) ? null : t.Pants, 
            ShirtSize = string.IsNullOrWhiteSpace(t.Shirt) ? null : t.Shirt, 
            ShoeSize = string.IsNullOrWhiteSpace(t.Shoes)? null : t.Shoes
        };
    }
}