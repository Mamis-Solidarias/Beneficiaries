using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.WebAPI.Beneficiaries.Utils;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.DbAccess;

internal class Communities_Get
{
    private const string InMemoryConnectionString = "DataSource=:memory:";
    
    private BeneficiariesDbContext _dbContext = null!;
    private Endpoints.Communities.GET.DbAccess _dbAccess = null!;
    private DataFactory _dataFactory = null!;

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

        _dbAccess = new Endpoints.Communities.GET.DbAccess(_dbContext);
        _dataFactory = new(_dbContext);
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
        var communities = _dataFactory.GenerateCommunities(5)
            .Select(t => t.Build())
            .ToArray();
        
        // Act
        var value = await _dbAccess.GetCommunities(default);
        
        // Assert
        value.Should().BeEquivalentTo(communities);
    }
    
    [Test]
    public async Task WithNoValues_Succeeds()
    {
        // Arrange

        // Act
        var value = await _dbAccess.GetCommunities(default);
        
        // Assert
        value.Should().BeEmpty();
    }
}