using System;
using System.Collections.Generic;
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

internal class CommunitiesIdFamiliesGet
{
    private const string InMemoryConnectionString = "DataSource=:memory:";
    
    private BeneficiariesDbContext _dbContext = null!;
    private Endpoints.Communities.Id.Families.GET.DbAccess _dbAccess = null!;
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

        _dbAccess = new(_dbContext);
        _dataFactory = new(_dbContext);
    }

    [TearDown]
    public void Dispose()
    {
        _dbContext.Dispose();
    }

    [Test]
    public async Task GetTotalEntries_Succeeds()
    {
        // Arrange
        _dataFactory.GenerateCommunity()
            .WithId("XT")
            .WithFamilies(_dataFactory.GetFamilies(15)
                .Select(t => t.WithCommunityId("XT").Build()))
            .Build();
        
        _dataFactory.GenerateCommunity()
            .WithId("MMM")
            .WithFamilies(_dataFactory.GetFamilies(5)
                .Select(t => t.WithCommunityId("MMM").Build()))
            .Build();
        
        // Act
        var result = await _dbAccess.GetTotalEntries("XT", default);
        
        // Assert
        result.Should().Be(15);
    }

    [Test]
    public async Task GetTotalEntries_CommunityDoesNotExist()
    {
        // Arrange

        _dataFactory.GenerateCommunity()
            .WithId("MMM")
            .WithFamilies(_dataFactory.GetFamilies(5)
                .Select(t => t.WithCommunityId("MMM").Build()))
            .Build();

        // Act
        var result = await _dbAccess.GetTotalEntries("XT", default);
        
        // Assert
        result.Should().Be(0);
    }

    // Cannot be tested due to Sqlite limitations (linq query uses Include())
    
    // [Test]
    // public async Task GetFamilies_Succeeds()
    // {
    //     // Arrange
    //     _dataFactory.GenerateCommunity()
    //         .WithId("XT")
    //         .WithFamilies(_dataFactory.GetFamilies(15)
    //             .Select(t => t.WithCommunityId("XT").Build()))
    //         .Build();
    //
    //     
    //     _dataFactory.GenerateCommunity()
    //         .WithId("MMM")
    //         .WithFamilies(_dataFactory.GetFamilies(5)
    //             .Select(t => t.WithCommunityId("MMM").Build()))
    //         .Build();
    //
    //     var page = 0;
    //     var pageSize = 5;
    //     
    //     // Act
    //     var result = await _dbAccess.GetFamilies("XT", page,pageSize,default);
    //     
    //     // Assert
    //     result.Should().HaveCount(5);
    //     // result.Should().ContainEquivalentOf(xt.Families.Take(5));
    // }
    
}