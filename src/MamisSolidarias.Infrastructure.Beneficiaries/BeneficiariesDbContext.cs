using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.Infrastructure.Beneficiaries;

public class BeneficiariesDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public BeneficiariesDbContext(DbContextOptions<BeneficiariesDbContext> options) : base(options)
    {
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(
            model =>
            {
                model.HasKey(t => t.Id);
                model.Property(t => t.Id).ValueGeneratedOnAdd();
                model.Property(t => t.Name)
                    .IsRequired()
                    .HasMaxLength(500);

            }
        );



    }
    
}