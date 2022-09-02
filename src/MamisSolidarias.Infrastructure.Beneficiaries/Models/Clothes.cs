using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MamisSolidarias.Infrastructure.Beneficiaries.Models;

internal class Clothes
{
    public int Id { get; set; }

    public string? ShoeSize { get; set; }
    public string? ShirtSize { get; set; }
    public string? PantsSize { get; set; }
}

internal class ClothesModelBuidler : IEntityTypeConfiguration<Clothes>
{
    public void Configure(EntityTypeBuilder<Clothes> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        
        builder.Property(t => t.ShirtSize).HasMaxLength(50);
        builder.Property(t => t.ShoeSize).HasMaxLength(50);
        builder.Property(t => t.PantsSize).HasMaxLength(50);
    }
    
}

