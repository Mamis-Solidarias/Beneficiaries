using System;
using System.Collections.Generic;
using System.Linq;
using MamisSolidarias.Infrastructure.Beneficiaries;

namespace MamisSolidarias.WebAPI.Beneficiaries.Utils;

internal class DataFactory : IDisposable
{
    private readonly BeneficiariesDbContext? _dbContext;
    public DataFactory(BeneficiariesDbContext? dbContext)
    {
        _dbContext = dbContext;
    }
    
    public CommunityBuilder GenerateCommunity() => new (_dbContext);
    public FamilyBuilder GenerateFamily() => new(_dbContext);
    public BeneficiaryBuilder GenerateBeneficiary() => new(_dbContext);
    
    public static CommunityBuilder GetCommunity() => new ();
    public static FamilyBuilder GetFamily() => new();
    
    public static ContactBuilder GetContact() => new();
    public static ClothesBuilder GetClothes() => new();
    public static EducationBuilder GetEducation() => new();
    public static HealthBuilder GetHealth() => new();
    public static JobBuilder GetJob() => new();
    public static BeneficiaryBuilder GetBeneficiary() => new((BeneficiariesDbContext?) null);

    
    public IEnumerable<CommunityBuilder> GenerateCommunities(int n)
    =>  Enumerable.Range(0, n).Select(_ => GenerateCommunity());
    
    public IEnumerable<FamilyBuilder> GenerateFamilies(int n)
    =>  Enumerable.Range(0, n).Select(_ => GenerateFamily());
    
    public IEnumerable<BeneficiaryBuilder> GenerateBeneficiaries(int n)
    =>  Enumerable.Range(0, n).Select(_ => GenerateBeneficiary());
    

    public static IEnumerable<CommunityBuilder> GetCommunities(int n) 
        => Enumerable.Range(0, n).Select(_ => GetCommunity());
    
    public static IEnumerable<FamilyBuilder> GetFamilies(int n) 
        => Enumerable.Range(0, n).Select(_ => GetFamily());
    
    public static IEnumerable<BeneficiaryBuilder> GetBeneficiaries(int n) 
        => Enumerable.Range(0, n).Select(_ => GetBeneficiary());

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}