using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace MamisSolidarias.Infrastructure.Beneficiaries.Models;

public class Community
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Id { get; set; }
    
    public ICollection<Family> Families { get; set; } = new List<Family>();
}

internal class CommunityModelBuilder : IEntityTypeConfiguration<Community>
{
    public void Configure(EntityTypeBuilder<Community> model)
    {
            model.HasKey(t => t.Id);
            
            model.Property(t => t.Id).HasValueGenerator<CommunityIdGenerator>();
            
            model.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(500);
            
            model.Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(500);
            
            model.Property(t => t.Description)
                .HasMaxLength(1000);

            model.HasMany(t => t.Families)
                .WithOne(t => t.Community)
                .HasForeignKey(t => t.CommunityId)
                .HasPrincipalKey(t => t.Id);
    }
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