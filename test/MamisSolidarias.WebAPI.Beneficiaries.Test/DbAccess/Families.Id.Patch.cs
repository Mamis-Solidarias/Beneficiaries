using System.Threading.Tasks;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.WebAPI.Beneficiaries.Utils;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.DbAccess;

internal class FamiliesIdPatch
{
    private BeneficiariesDbContext _dbContext = null!;
    private Endpoints.Families.Id.PATCH.DbAccess _dbAccess = null!;
    private DataFactory _dataFactory = null!;

    [SetUp]
    public void TestWithSqlite()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder<BeneficiariesDbContext>()
            .UseSqlite(connection)
            .Options;
        
        _dbContext = new BeneficiariesDbContext(options);
        _dbContext.Database.EnsureCreated();

        _dbAccess = new(_dbContext);
        _dataFactory = new(_dbContext);
    }

    [TearDown]
    public void Dispose()
    {
        _dbContext.Dispose();
    }

    [Test]
    public async Task GetFamily_Exists_Succeeds()
    {
        // Arrange
        Family family = _dataFactory.GenerateFamily();
        
        // Act
        var response = await _dbAccess.GetFamily(family.Id, default);
        
        // Assert
        response.Should().NotBeNull();
        response!.Address.Should().Be(family.Address);
        response.Details.Should().Be(family.Details);
        response.Id.Should().Be(family.Id);
        response.Name.Should().Be(family.Name);
        response.CommunityId.Should().Be(family.CommunityId);
        response.FamilyNumber.Should().Be(family.FamilyNumber);
        response.InternalId.Should().Be(family.InternalId);
    }
    

    [Test]
    public async Task GetFamily_DoesNotExists_Fails()
    {
        // Arrange
        var familyId = "TXT-123";
        
        // Act
        var response = await _dbAccess.GetFamily(familyId ,default);
        
        // Assert
        response.Should().BeNull();
    }
}