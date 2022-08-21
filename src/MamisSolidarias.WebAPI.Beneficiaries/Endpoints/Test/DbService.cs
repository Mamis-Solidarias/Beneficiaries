using MamisSolidarias.Infrastructure.Beneficiaries;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Test;

internal class DbService
{
    private readonly BeneficiariesDbContext? _dbContext;

    public DbService() { }
    public DbService(BeneficiariesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
}