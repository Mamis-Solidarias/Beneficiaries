using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MamisSolidarias.Infrastructure.Beneficiaries.Models;

public class Clothes
{
    public int Id { get; set; }

    public int? ShoeSize { get; set; }
    public string? ShirtSize { get; set; }
    public string? PantsSize { get; set; }
}

internal class ClothesModelBuilder : IEntityTypeConfiguration<Clothes>
{
    public void Configure(EntityTypeBuilder<Clothes> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        
        builder.Property(t => t.ShirtSize).HasMaxLength(50).IsRequired(false);
        builder.Property(t => t.ShoeSize).IsRequired(false);
        builder.Property(t => t.PantsSize).HasMaxLength(50).IsRequired(false);
    }
    
}

