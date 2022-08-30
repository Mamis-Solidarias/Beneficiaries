using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Utils;

internal class CommunityBuilder
{
    private static readonly Faker<Community> CommunityGenerator = new Faker<Community>()
            .RuleFor(t=> t.Address, f=> f.Address.FullAddress())
            .RuleFor(t => t.Name, f => f.Random.AlphaNumeric(10).ToUpperInvariant())
            .RuleFor(t => t.Id, f => f.Random.AlphaNumeric(4))
            .RuleFor(t=> t.Description,f=> f.Lorem.Lines(1))
        ;

    private BeneficiariesDbContext? _dbContext;
    public CommunityBuilder() => Community = CommunityGenerator.Generate();
    public CommunityBuilder(BeneficiariesDbContext? dbContext)
    {
        Community = CommunityGenerator.Generate();
        _dbContext = dbContext;
    }

    private Community Community { get; set; }


    public CommunityBuilder SaveToDb(BeneficiariesDbContext dbContext)
    {
        _dbContext = dbContext;
        return this;
    }
    public CommunityBuilder WithId(string? id)
    {
        Community.Id = id;
        return this;
    }
    public CommunityBuilder WithAddress(string address)
    {
        Community.Address = address;
        return this;
    }
    public CommunityBuilder WithName(string name)
    {
        Community.Name = name;
        return this;
    }
    public CommunityBuilder WithDescription(string? description)
    {
        Community.Description = description;
        return this;
    }

    public CommunityBuilder WithFamilies(IEnumerable<Family> families)
    {
        Community.Families = families.ToList();
        return this;
    }
    
    public Community Build()
    {
        _dbContext?.Add(Community);
        _dbContext?.SaveChanges();
        _dbContext?.ChangeTracker.Clear();

        return Community;
    }
    

    public static implicit operator CommunityBuilder(Community c) => new() {Community = c};

    public static implicit operator Community(CommunityBuilder c) => c.Build();
    
}