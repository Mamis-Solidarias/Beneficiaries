using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MamisSolidarias.Infrastructure.Beneficiaries.Models;

internal class Beneficiary
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public BeneficiaryType Type { get; set; }
    public BeneficiaryGender Gender { get; set; }
    public DateOnly Birthday { get; set; }
    public string Dni { get; set; } = string.Empty;
    public string? Comments { get; set; }
    public string? Likes { get; set; }
    
    public string FamilyId { get; set; } = string.Empty;
    public Family? Family { get; set; }
    public Clothes? Clothes { get; set; }
    public Education? Education { get; set; }
    public  Health? Health { get; set; }
    public Job? Job { get; set; }
}

internal enum BeneficiaryType
{
    Child,
    Adult
}

internal enum BeneficiaryGender
{
    Male,
    Female,
    Other
}


internal class BeneficiaryModelBuilder : IEntityTypeConfiguration<Beneficiary>
{
    public void Configure(EntityTypeBuilder<Beneficiary> b)
    {
        b.HasKey(t => t.Id);
        b.Property(t => t.Id)
            .ValueGeneratedOnAdd().IsRequired();

        b.Property(t => t.FirstName).HasMaxLength(100).IsRequired();
        b.Property(t => t.LastName).HasMaxLength(100).IsRequired();
        b.Property(t => t.Type).HasConversion<int>().IsRequired();
        b.Property(t => t.Gender).HasConversion<int>().IsRequired();
        b.Property(t => t.Birthday).IsRequired();
        b.Property(t => t.Dni).HasMaxLength(15).IsRequired();

        b.HasIndex(t => t.Dni).IsUnique();
        
        b.Property(t => t.Comments).HasMaxLength(1000);
        b.Property(t => t.Likes).HasMaxLength(1000);

        b.HasOne(t => t.Family)
            .WithMany(t => t.Beneficiaries)
            .HasForeignKey(t => t.FamilyId)
            .HasPrincipalKey(t => t.Id)
            .IsRequired();

        b.HasOne(t => t.Clothes);

        b.HasOne(t => t.Education);

        b.HasOne(t => t.Health);

        b.HasOne(t => t.Job);

    }
}

