using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.Infrastructure.Beneficiaries;
#pragma warning disable CS8618
internal class BeneficiariesDbContext : DbContext
{
    public DbSet<Community> Communities { get; set; }
    public DbSet<Family> Families { get; set; }
    
    public DbSet<Beneficiary> Beneficiaries { get; set; }
    
    public BeneficiariesDbContext(DbContextOptions<BeneficiariesDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new CommunityModelBuilder().Configure(modelBuilder.Entity<Community>());
        new FamilyModelBuilder().Configure(modelBuilder.Entity<Family>());
        new ContactModelBuilder().Configure(modelBuilder.Entity<Contact>());
        
        new EducationModelBuilder().Configure(modelBuilder.Entity<Education>());
        new ClothesModelBuidler().Configure(modelBuilder.Entity<Clothes>());
        new JobModelBuilder().Configure(modelBuilder.Entity<Job>());
        new HealthModelBuilder().Configure(modelBuilder.Entity<Health>());
        new BeneficiaryModelBuilder().Configure(modelBuilder.Entity<Beneficiary>());

    }
}

#pragma warning restore CS8618



