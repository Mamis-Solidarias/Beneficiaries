using MamisSolidarias.Infrastructure.Beneficiaries;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.POST;

internal class DbAccess
{
    private readonly BeneficiariesDbContext? _dbContext;

    public DbAccess() {}
    public DbAccess(BeneficiariesDbContext? dbContext) => _dbContext = dbContext;
    
}