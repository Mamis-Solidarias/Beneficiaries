using Bogus;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Utils;

internal class HealthBuilder
{
    private static readonly Faker<Health> Generator = new Faker<Health>()
        .RuleFor(t => t.Id, f => f.UniqueIndex)
        .RuleFor(t => t.Observations, f => f.Lorem.Sentence())
        .RuleFor(t => t.HasCovidVaccine, f => f.Random.Bool())
        .RuleFor(t => t.HasMandatoryVaccines, f => f.Random.Bool())
        ;

    private readonly Health _health = Generator.Generate();

    public HealthBuilder() { }
    public HealthBuilder(Health health) => _health = health;

    public HealthBuilder WithId(int id)
    {
        _health.Id = id;
        return this;
    }

    public HealthBuilder WithObservations(string? obs)
    {
        _health.Observations = obs;
        return this;
    }

    public HealthBuilder HasCovidVaccine(bool vaccine)
    {
        _health.HasCovidVaccine = vaccine;
        return this;
    }

    public HealthBuilder HasMandatoryVaccines(bool vaccines)
    {
        _health.HasMandatoryVaccines = vaccines;
        return this;
    }

    public Health Build() => _health;

    public static implicit operator Health(HealthBuilder b) => b.Build();
    public static implicit operator HealthBuilder(Health b) => new(b);
}