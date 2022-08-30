using System.Collections.Generic;
using System.Linq;
using Bogus;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Utils;

internal class FamilyBuilder
{
    private static readonly Faker<Family> FamilyGenerator = new Faker<Family>()
            .RuleFor(t=> t.Address, f=> f.Address.FullAddress())
            .RuleFor(t => t.Name, f => f.Address.City())
            .RuleFor(t=> t.Community, _ => new CommunityBuilder().Build())
            .RuleFor(t => t.InternalId, f => 1 + f.IndexFaker)
            .RuleFor(t=> t.CommunityId, (_,f) =>f.Community.Id)
            .RuleFor(t=> t.FamilyNumber, f=> 1+f.IndexFaker)
            .RuleFor(t=> t.Details,f=> f.Lorem.Lines(1))
            .RuleFor(t=> t.Id, (_,w)=> $"{w.CommunityId}-{w.FamilyNumber}")
            .RuleFor(t=>t.Contacts, (f) => Enumerable
                .Range(0,f.Random.Int(2,3))
                .Select(_=>new ContactBuilder().Build())
                .ToList()
            )
        ;

    private BeneficiariesDbContext? _dbContext;
    public FamilyBuilder() => Family = FamilyGenerator.Generate();
    public FamilyBuilder(BeneficiariesDbContext? dbContext)
    {
        Family = FamilyGenerator.Generate();
        _dbContext = dbContext;
    }

    private Family Family { get; set; }


    public FamilyBuilder DontSaveToDb()
    {
        _dbContext = null;
        return this;
    }
    public FamilyBuilder SaveToDb(BeneficiariesDbContext dbContext)
    {
        _dbContext = dbContext;
        return this;
    }
    public FamilyBuilder WithInternalId(int id)
    {
        Family.InternalId = id;
        return this;
    }

    public FamilyBuilder WithCommunity(Community? community)
    {
        Family.Community = community ?? new Community();
        Family.CommunityId = community?.Id ?? "";
        Family.Id = $"{Family.CommunityId}-{Family.FamilyNumber}";
        return this;
    }
    public FamilyBuilder WithCommunityId(string communityId)
    {
        Family.CommunityId = communityId;
        Family.Id = $"{Family.CommunityId}-{Family.FamilyNumber}";
        return this;
    }
    public FamilyBuilder WithFamilyNumber(int familyNumber)
    {
        Family.FamilyNumber = familyNumber;
        Family.Id = $"{Family.CommunityId}-{Family.FamilyNumber}";
        return this;
    }
    
    public FamilyBuilder WithAddress(string address)
    {
        Family.Address = address;
        return this;
    }
    public FamilyBuilder WithName(string name)
    {
        Family.Name = name;
        return this;
    }
    public FamilyBuilder WithDetails(string? details)
    {
        Family.Details = details;
        return this;
    }

    public FamilyBuilder WithContacts(IEnumerable<Contact> contacts)
    {
        Family.Contacts = contacts.ToList();
        return this;
    }
    
    public Family Build()
    {
        _dbContext?.Families.Add(Family);
        _dbContext?.SaveChanges();
        _dbContext?.ChangeTracker.Clear();

        return Family;
    }
    

    public static implicit operator FamilyBuilder(Family c) => new() {Family = c};

    public static implicit operator Family(FamilyBuilder c) => c.Build();
}