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

public class Education
{
    public int Id { get; set; }
    public string? Year { get; set; }
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
        
        builder.Property(t => t.Year)
            .HasMaxLength(50);
        builder.Property(t => t.School)
            .HasMaxLength(100);
        builder.Property(t => t.TransportationMethod)
            .HasConversion<int>();
    }
}