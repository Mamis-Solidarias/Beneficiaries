using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MamisSolidarias.Infrastructure.Beneficiaries.Models;

public enum TransportationMethod
{
    Walking,
    Car,
    PublicTransport,
    Bike,
    Horse,
    Other
}

public enum SchoolYear
{
    PreSchool3,
    PreSchool4,
    PreSchool5,
    PrimarySchool1,
    PrimarySchool2,
    PrimarySchool3,
    MiddleSchool4,
    MiddleSchool5,
    MiddleSchool6,
    MiddleSchool7,
    HighSchool1,
    HighSchool2,
    HighSchool3,
    HighSchool4,
    HighSchool5,
    HighSchool6
}

public enum SchoolCycle
{
    PreSchool,
    PrimarySchool,
    MiddleSchool,
    HighSchool
}

public class Education
{
    public int Id { get; set; }
    public SchoolYear? Year { get; set; }
    public SchoolCycle? Cycle { get; set; }
    public string? School { get; set; }
    public TransportationMethod? TransportationMethod { get; set; }
}

internal class EducationModelBuilder : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(t => t.Year);
        builder.Property(t => t.Cycle);
        builder.Property(t => t.School)
            .HasMaxLength(100);
        builder.Property(t => t.TransportationMethod)
            .HasConversion<int>();
    }
}