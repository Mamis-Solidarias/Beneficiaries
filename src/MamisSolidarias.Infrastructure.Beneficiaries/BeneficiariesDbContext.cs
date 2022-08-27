using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.EntityFrameworkCore;

namespace MamisSolidarias.Infrastructure.Beneficiaries;

internal class BeneficiariesDbContext : DbContext
{
    public DbSet<Community> Communities { get; set; }
    public DbSet<Family> Families { get; set; }

#pragma warning disable CS8618
    public BeneficiariesDbContext(DbContextOptions<BeneficiariesDbContext> options) : base(options) { }
#pragma warning restore CS8618
    


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new CommunityModelBuilder().Configure(modelBuilder.Entity<Community>());
        new FamilyModelBuilder().Configure(modelBuilder.Entity<Family>());
        new ContactModelBuilder().Configure(modelBuilder.Entity<Contact>());

    }
}



