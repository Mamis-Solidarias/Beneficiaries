using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Npgsql;

namespace MamisSolidarias.Infrastructure.Beneficiaries.Models;

internal class Community
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string? Description { get; set; }
    public string? Id { get; set; }
}


internal class CommunityIdGenerator : ValueGenerator<string>{
    
    public override string Next(EntityEntry entry)
    {
        if (entry.Entity is Community community)
        {
            return community.Id ?? string.Concat(community.Name.ToUpperInvariant().Take(2));
        }

        throw new ArgumentException("Entity is not of type Community");
    }

    public override bool GeneratesTemporaryValues => false;
}