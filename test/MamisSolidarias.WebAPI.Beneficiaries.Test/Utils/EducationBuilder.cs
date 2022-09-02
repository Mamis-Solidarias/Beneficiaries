using Bogus;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Utils;

internal class EducationBuilder
{
    private static readonly Faker<Education> Generator = new Faker<Education>()
        .RuleFor(t => t.Id, f => f.UniqueIndex)
        .RuleFor(t => t.School, f => f.Address.City())
        .RuleFor(t => t.Year, f => f.Lorem.Word())
        .RuleFor(t => t.TransportationMethod, f => f.PickRandom<TransportationMethod>())
        ;
    private readonly Education _education = Generator.Generate();

    public EducationBuilder(Education education) => _education = education;
    public EducationBuilder() {}

    public EducationBuilder WithId(int id)
    {
        _education.Id = id;
        return this;
    }

    public EducationBuilder WithSchool(string? school)
    {
        _education.School = school;
        return this;
    }

    public EducationBuilder WithYear(string? year)
    {
        _education.Year = year;
        return this;
    }

    public EducationBuilder WithTransportationMethod(TransportationMethod? method)
    {
        _education.TransportationMethod = method;
        return this;
    }

    public Education Build() => _education;

    public static implicit operator Education(EducationBuilder b) => b.Build();
    public static implicit operator EducationBuilder(Education b) => new(b);
}