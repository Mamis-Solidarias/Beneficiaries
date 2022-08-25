using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace MamisSolidarias.Infrastructure.Beneficiaries.Models;

internal class Family
{
    public string? Id { get; set; }
    
    internal int FamilyNumber { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public Community Community { get; set; }

    public string? Details { get; set; }
    
    public IEnumerable<Contact> Contacts { get; set; }
}

internal class FamilyIdGenerator : ValueGenerator<string>
{
    public override bool GeneratesTemporaryValues => false;
    public override string Next(EntityEntry entry)
    {
        if (entry.Entity is Family family)
        {
            return family.Id ?? string.Concat(family.Community.Id,"-",family.FamilyNumber);
        }

        throw new ArgumentException("Entity is not of type Community");
    }
}