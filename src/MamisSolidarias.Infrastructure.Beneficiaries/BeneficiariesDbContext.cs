using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.Infrastructure.Beneficiaries;

internal class BeneficiariesDbContext: DbContext
{
    public DbSet<Community> Communities { get; set; }
#pragma warning disable CS8618
    public BeneficiariesDbContext(DbContextOptions<BeneficiariesDbContext> options) : base(options)
#pragma warning restore CS8618
    {
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Community>(
            model =>
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

            }
        );



    }
    
}