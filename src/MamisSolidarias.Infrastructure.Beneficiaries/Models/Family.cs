using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MamisSolidarias.Infrastructure.Beneficiaries.Models;

public class Family
{

    internal int InternalId { get; set; }
    public string Id { get; set; } = string.Empty;
    public int FamilyNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string CommunityId { get; set; } = string.Empty;
    public Community Community { get; set; } = null!;
    public string? Details { get; set; }
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    public ICollection<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();
}

internal class FamilyModelBuilder : IEntityTypeConfiguration<Family>
{
    public void Configure(EntityTypeBuilder<Family> model)
    {
        model.Property(t => t.FamilyNumber)
            .IsRequired();

        model.HasIndex(t => t.FamilyNumber);

        model.HasKey(t=> t.InternalId);
        model.Property(t => t.InternalId)
            .ValueGeneratedOnAdd();

        model.HasIndex(t => t.Id)
            .IsUnique();
        
        model.Property(t => t.Id)
            .IsRequired();
        
        
        model.Property(t => t.Address)
            .IsRequired()
            .HasMaxLength(500);

        model.Property(t => t.Details)
            .HasMaxLength(500);

        model.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);


        model.HasMany(t => t.Contacts);

        model.HasOne(t => t.Community)
            .WithMany(t => t.Families)
            .HasForeignKey(t => t.CommunityId)
            .HasPrincipalKey(t => t.Id);

        model.HasMany(t => t.Beneficiaries)
            .WithOne(t => t.Family)
            .HasForeignKey(t => t.FamilyId)
            .HasPrincipalKey(t => t.Id);
    }
}


