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
    public CommunityBuilder GetCommunity() => new ();
    public FamilyBuilder GetFamily() => new();
    public ContactBuilder GetContact() => new();

    
    public IEnumerable<CommunityBuilder> GenerateCommunities(int n)
    =>  Enumerable.Range(0, n).Select(_ => GenerateCommunity());
    
    public IEnumerable<FamilyBuilder> GenerateFamilies(int n)
    =>  Enumerable.Range(0, n).Select(_ => GenerateFamily());
    

    public IEnumerable<CommunityBuilder> GetCommunities(int n) 
        => Enumerable.Range(0, n).Select(_ => GetCommunity());
    
    public IEnumerable<FamilyBuilder> GetFamilies(int n) 
        => Enumerable.Range(0, n).Select(_ => GetFamily());
    
    public IEnumerable<ContactBuilder> GetContacts(int n) 
        => Enumerable.Range(0, n).Select(_ => GetContact());

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}