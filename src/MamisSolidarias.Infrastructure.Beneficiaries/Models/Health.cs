using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MamisSolidarias.Infrastructure.Beneficiaries.Models;


internal class Health
{
    public int Id { get; set; }
    public bool? HasCovidVaccine { get; set; }
    public bool? HasMandatoryVaccines { get; set; }
    public string? Observations { get; set; }
}

internal class HealthModelBuilder : IEntityTypeConfiguration<Health>
{
    public void Configure(EntityTypeBuilder<Health> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Observations).HasMaxLength(1000);
    }
}