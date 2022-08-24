using System.Collections.Generic;
using System.Linq;
using Bogus;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.HttpClient.Beneficiaries.Utils;

internal static class DataFactory
{
    private static readonly Faker<Community> UserGenerator = new Faker<Community>()
        .RuleFor(t => t.Id, (_,w) => w.Name[..2])
        .RuleFor(t=> t.Address, f=> f.Address.Direction())
        .RuleFor(t => t.Name, f => f.Address.City());
    
    public static Community GetCommunity()
    {
        return UserGenerator.Generate();
    }

    public static IEnumerable<Community> GetCommunities(int n)
    {
        return Enumerable.Range(0, n).Select(_ => GetCommunity());
    }
}