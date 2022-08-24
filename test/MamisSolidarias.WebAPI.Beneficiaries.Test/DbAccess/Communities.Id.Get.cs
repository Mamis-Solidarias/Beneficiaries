using System.Threading.Tasks;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.WebAPI.Beneficiaries.Utils;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.DbAccess;

internal class CommunitiesIdGet
{
    private Endpoints.Communities.Id.GET.DbAccess _dbAccess = null!;
    private const string InMemoryConnectionString = "DataSource=:memory:";
    private BeneficiariesDbContext _dbContext = null!;
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

        _dbAccess = new Endpoints.Communities.Id.GET.DbAccess(_dbContext);
        _dataFactory = new DataFactory(_dbContext);
    }

    [TearDown]
    public void Dispose()
    {
        _dbContext.Dispose();
    }

    [Test]
    public async Task WithExistingCommunity_Succeeds()
    {
        // Arrange
        var id = "MIS";
        Community community = _dataFactory.GenerateCommunity()
            .WithId(id);
        
        // Act
        var value = await _dbAccess.GetCommunityFromId(id, default);
        
        // Assert
        value.Should().NotBeNull();
        value!.Address.Should().Be(community.Address);
        value.Description.Should().Be(community.Description);
        value.Name.Should().Be(community.Name);
        value.Id.Should().Be(id);
    }
    
    [Test]
    public async Task WithoutExistingCommunity_Succeeds()
    {
        // Arrange
        // Act
        var value = await _dbAccess.GetCommunityFromId("123", default);
        
        // Assert
        value.Should().BeNull();
    }



}