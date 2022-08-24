using System.Threading.Tasks;
using MamisSolidarias.Infrastructure.Beneficiaries;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.DbAccess;

internal class Community_Get
{
    private const string InMemoryConnectionString = "DataSource=:memory:";
    
    private BeneficiariesDbContext _dbContext = null!;
    private Endpoints.Communities.POST.DbAccess _dbAccess = null!;

    [SetUp]
    public void TestWithSqlite()
    {
        var connection = new SqliteConnection(InMemoryConnectionString);
        connection.Open();
        var options = new DbContextOptionsBuilder<BeneficiariesDbContext>()
            .UseSqlite(connection)
            .Options;
        
        _dbContext = new BeneficiariesDbContext(options);
        _dbContext.Database.EnsureCreated();

        _dbAccess = new Endpoints.Communities.POST.DbAccess(_dbContext);
    }

    [TearDown]
    public void Dispose()
    {
        _dbContext.Dispose();
    }

    [Test]
    public async Task WithManyValues_Succeeds()
    {
        // Arrange
        
    }
}