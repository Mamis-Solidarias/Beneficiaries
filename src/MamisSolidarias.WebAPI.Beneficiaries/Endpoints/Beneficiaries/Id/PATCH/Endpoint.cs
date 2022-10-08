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

        if (req.Beneficiary.Comments is not null)
            b.Comments = req.Beneficiary.Comments;

        if (req.Beneficiary.Dni is not null)
            b.Dni = req.Beneficiary.Dni;

        if (req.Beneficiary.Education is not null)
            b.Education = Map(req.Beneficiary.Education);

        if (req.Beneficiary.Gender is not null)
            b.Gender = Enum.Parse<BeneficiaryGender>(req.Beneficiary.Gender);

        if (req.Beneficiary.Health is not null)
            b.Health = Map(req.Beneficiary.Health);

        if (req.Beneficiary.Job is not null)
            b.Job = Map(req.Beneficiary.Job);

        if (req.Beneficiary.Likes is not null)
            b.Likes = req.Beneficiary.Likes;

        if (req.Beneficiary.Type is not null)
            b.Type = Enum.Parse<BeneficiaryType>(req.Beneficiary.Type);

        if (req.Beneficiary.FirstName is not null)
            b.FirstName = req.Beneficiary.FirstName;

        if (req.Beneficiary.LastName is not null)
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
        => t is null ? null : new Job {Title = t.Title};

    private static Health? Map(HealthRequest? t)
        => t is null
            ? null
            : new Health
            {
                HasCovidVaccine = t.HasCovidVaccine,
                HasMandatoryVaccines = t.HasMandatoryVaccines,
                Observations = t.Observations
            };

    private static Education? Map(EducationRequest? t)
        => t is null
            ? null
            : new Education
            {
                School = t.School,
                Year = t.Year.Parse<SchoolYear>(),
                Cycle = t.Year.Parse<SchoolYear>().ToSchoolCycle(),
                TransportationMethod = t.TransportationMethod.Parse<TransportationMethod>()
            };
    
    private static Clothes? Map(ClothesRequest? t)
        => t is null ? null : new Clothes {PantsSize = t.Pants, ShirtSize = t.Shirt, ShoeSize = t.Shoes};
    

}