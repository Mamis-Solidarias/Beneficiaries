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

internal class CommunitiesPost
{
    private const string InMemoryConnectionString = "DataSource=:memory:";

    private BeneficiariesDbContext _dbContext = null!;
    private DataFactory _dataFactory = null!;
    private Endpoints.Communities.POST.DbAccess _dbAccess = null!;

    [SetUp]
    public void TestWithSqlite()
    {
        var connection = new SqliteConnection(InMemoryConnectionString);
        connection.Open();
        var options = new DbContextOptionsBuilder<BeneficiariesDbContext>()
            .UseSqlite(connection)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .Options;
        
        _dbContext = new BeneficiariesDbContext(options);
        _dbContext.Database.EnsureCreated();

        _dbAccess = new Endpoints.Communities.POST.DbAccess(_dbContext);
        _dataFactory = new DataFactory(_dbContext);
    }

    [TearDown]
    public void Dispose()
    {
        _dataFactory.Dispose();
    }

    [Test]
    public async Task WithGeneratedIDs_Succeeds()
    {
        // Arrange
        var communities = DataFactory.GetCommunities(3)
            .Select(t=> t.Build())
            .ToArray();
        
        // Act
        await _dbAccess.CreateCommunities(communities, default);
        
        // Assert
        _dbContext.Communities.Should().HaveCount(3);
        foreach (var community in communities)
        {
            var res = await _dbContext.Communities
                .FirstOrDefaultAsync(t => 
                        t.Id == community.Id &&
                        t.Address == community.Address &&
                        t.Description == community.Description &&
                        t.Name == community.Name
                    );
            res.Should().NotBeNull();
        }
    }

    [Test]
    public async Task WithAutoGeneratedIDs_Succeeds()
    {
        // Arrange
        var communities = DataFactory.GetCommunities(3)
            .Select(t=> t.WithId(null).Build())
            .ToArray();
        
        // Act
        await _dbAccess.CreateCommunities(communities, default);
        
        // Assert
        _dbContext.Communities.Should().HaveCount(3);
        foreach (var community in communities)
        {
            var res = await _dbContext.Communities
                .FirstOrDefaultAsync(t => 
                        t.Id == community.Name.ToUpperInvariant().Substring(0,2) &&
                        t.Address == community.Address &&
                        t.Description == community.Description &&
                        t.Name == community.Name
                    );
            res.Should().NotBeNull();
        }
    }
    
    [Test]
    public async Task WithRepeatedIds_Fails()
    {
        // Arrange
        Community entity = _dataFactory.GenerateCommunity();
        
        Community community = DataFactory.GetCommunity();
        community.Id = entity.Id;

        // Act
        var action = async () => await _dbAccess.CreateCommunities(new []{community}, default);
        
        // Assert
        await action.Should().ThrowExactlyAsync<DbUpdateException>();
        _dbContext.Communities.Should().HaveCount(1);
    }
    
    [Test]
    public async Task AddsMultiple_WithRepeatedIds_Fails()
    {
        // Arrange
        Community entity = _dataFactory.GenerateCommunity();
        
        var communities = DataFactory.GetCommunities(5)
            .Select(t=> t.Build())
            .ToArray();
        
        communities.Last().Id = entity.Id;

        // Act
        var action = async () => await _dbAccess.CreateCommunities(communities, default);
        
        // Assert
        await action.Should().ThrowExactlyAsync<DbUpdateException>();
        _dbContext.Communities.Should().HaveCount(1);
    }
}















