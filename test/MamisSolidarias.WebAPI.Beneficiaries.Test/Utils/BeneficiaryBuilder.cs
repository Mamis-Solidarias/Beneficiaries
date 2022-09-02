using System;
using Bogus;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Utils;


internal class BeneficiaryBuilder
{
    private static readonly Faker<Beneficiary> Generator = new Faker<Beneficiary>()
        .RuleFor(t => t.Birthday, f => f.Date.PastDateOnly(20, new DateOnly(2010, 1, 1)))
        .RuleFor(t => t.Clothes, new ClothesBuilder().Build())
        .RuleFor(t => t.Comments, f => f.Lorem.Sentence())
        .RuleFor(t => t.Dni, f => f.PickRandomParam(
            f.Phone.PhoneNumber("########"),
            f.Phone.PhoneNumber("#######"),
            f.Phone.PhoneNumber("#########")
        ))
        .RuleFor(t => t.Education, new EducationBuilder().Build())
        .RuleFor(t => t.Family, new FamilyBuilder().Build())
        .RuleFor(t => t.Gender, f => f.PickRandom<BeneficiaryGender>())
        .RuleFor(t => t.Health, new HealthBuilder().Build())
        .RuleFor(t => t.Id, f => f.Random.Int(1,1000000))
        .RuleFor(t => t.Job, (_, b) =>
            DateTime.Now.Year - b.Birthday.Year > 18 ? new JobBuilder().Build() : null
        )
        .RuleFor(t => t.Likes, f => f.Lorem.Sentence())
        .RuleFor(t => t.Type, (_, b) =>
            (DateTime.Now.Year - b.Birthday.Year) > 18 ? BeneficiaryType.Adult : BeneficiaryType.Child
        )
        .RuleFor(t => t.FamilyId, (_, b) => b.Family!.Id)
        .RuleFor(t => t.FirstName, f => f.Person.FirstName)
        .RuleFor(t => t.LastName, f => f.Person.LastName)
        .RuleFor(t=> t.IsActive, true);

    private readonly Beneficiary _beneficiary = Generator.Generate();
    private readonly BeneficiariesDbContext? _dbContext;

    public BeneficiaryBuilder(BeneficiariesDbContext? dbContext) => _dbContext = dbContext;
    public BeneficiaryBuilder(Beneficiary b) => _beneficiary = b;

    public BeneficiaryBuilder WithLastName(string lastName)
    {
        _beneficiary.LastName = lastName;
        return this;
    }
    public BeneficiaryBuilder WithFirstName(string firstName)
    {
        _beneficiary.FirstName = firstName;
        return this;
    }
    public BeneficiaryBuilder WithType(BeneficiaryType type)
    {
        _beneficiary.Type = type;
        return this;
    }
    public BeneficiaryBuilder WithLikes(string? likes)
    {
        _beneficiary.Likes = likes;
        return this;
    }
    public BeneficiaryBuilder WithJob(Job job)
    {
        _beneficiary.Job = job;
        return this;
    }
    public BeneficiaryBuilder WithHealth(Health health)
    {
        _beneficiary.Health = health;
        return this;
    }
    public BeneficiaryBuilder WithGender(BeneficiaryGender gender)
    {
        _beneficiary.Gender = gender;
        return this;
    }
    public BeneficiaryBuilder WithFamilyId(string familyId)
    {
        _beneficiary.FamilyId = familyId;
        return this;
    }
    public BeneficiaryBuilder WithFamily(Family? family)
    {
        _beneficiary.Family = family;
        _beneficiary.FamilyId = family?.Id ?? string.Empty;
        return this;
    }

    public BeneficiaryBuilder WithEducation(Education education)
    {
        _beneficiary.Education = education;
        return this;
    }

    public BeneficiaryBuilder WithDni(string dni)
    {
        _beneficiary.Dni = dni;
        return this;
    }

    public BeneficiaryBuilder WithId(int id)
    {
        _beneficiary.Id = id;
        return this;
    }

    public BeneficiaryBuilder WithBirthday(DateOnly birthday)
    {
        _beneficiary.Birthday = birthday;
        return this;
    }

    public BeneficiaryBuilder WithClothes(Clothes clothes)
    {
        _beneficiary.Clothes = clothes;
        return this;
    }

    public BeneficiaryBuilder WithComments(string? comments)
    {
        _beneficiary.Comments = comments;
        return this;
    }

    public BeneficiaryBuilder IsActive(bool isActive)
    {
        _beneficiary.IsActive = isActive;
        return this;
    }

    public Beneficiary Build()
    {
        _dbContext?.Beneficiaries.Add(_beneficiary);
        _dbContext?.SaveChanges();
        _dbContext?.ChangeTracker.Clear();

        return _beneficiary;
    }

    public static implicit operator Beneficiary(BeneficiaryBuilder b) => b.Build();
    public static implicit operator BeneficiaryBuilder(Beneficiary b) => new(b);
}

