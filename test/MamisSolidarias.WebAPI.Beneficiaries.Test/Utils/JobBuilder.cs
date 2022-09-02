using Bogus;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Utils;

internal class JobBuilder
{
    private static readonly Faker<Job> Generator = new Faker<Job>()
        .RuleFor(t => t.Id, f => f.UniqueIndex)
        .RuleFor(t => t.Title, f => f.Person.Company.Name)
        ;

    private readonly Job _job = Generator.Generate();

    public JobBuilder() { }
    public JobBuilder(Job j) => _job = j;

    public JobBuilder WithId(int id)
    {
        _job.Id = id;
        return this;
    }

    public JobBuilder WithTitle(string title)
    {
        _job.Title = title;
        return this;
    }

    public Job Build() => _job;

    public static implicit operator Job(JobBuilder b) => b.Build();
    public static implicit operator JobBuilder(Job b) => new(b);

}