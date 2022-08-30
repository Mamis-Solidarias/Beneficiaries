using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.WebAPI.Beneficiaries.Utils;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.DbAccess;

internal class FamiliesPost
{
    private const string InMemoryConnectionString = "DataSource=:memory:";

    private BeneficiariesDbContext _dbContext = null!;
    private DataFactory _dataFactory = null!;
    private Endpoints.Communities.Id.Families.POST.DbAccess _dbAccess = null!;

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

        _dataFactory = new DataFactory(_dbContext);
        _dbAccess = new(_dbContext);
    }
    

    [TearDown]
    public void Dispose()
    {
        _dataFactory.Dispose();
    }

    [Test]
    public async Task CreateFamilies_Succeeds()
    {
        // Arrange
        var families = _dataFactory.GetFamilies(5)
            .Select(t => t.Build())
            .ToArray();

        // Act
        await _dbAccess.CreateFamilies(families, default);
        
        // Assert
        _dbContext.Families.Count().Should().Be(5);
        foreach (var family in families)
        {
            _dbContext.Families.Contains(family).Should().BeTrue();
        }
    }

    [Test]
    public async Task CreateSingleFamily_Succeeds()
    {
        // Arrange
        Family family = _dataFactory.GetFamily();

        // Act
        await _dbAccess.CreateFamilies(new []{family}, default);
        
        // Assert
        _dbContext.Families.Contains(family).Should().BeTrue();
    }


}