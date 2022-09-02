using System.Threading.Tasks;
using EntityFramework.Exceptions.Sqlite;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.WebAPI.Beneficiaries.Utils;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.DbAccess;

internal class BeneficiariesIdDelete
{
    private BeneficiariesDbContext _dbContext = null!;
    private Endpoints.Beneficiaries.Id.DELETE.DbAccess _dbAccess = null!;
    private DataFactory _dataFactory = null!;

    [SetUp]
    public void TestWithSqlite()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder<BeneficiariesDbContext>()
            .UseSqlite(connection)
            .UseExceptionProcessor()
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
    public async Task GetBeneficiaryById_Succeeds()
    {
        // Arrange
        const int id = 123;
        Beneficiary beneficiary = _dataFactory.GenerateBeneficiary()
            .WithId(id);
        
        // Act
        var result = await _dbAccess.GetBeneficiary(id, default);
        
        // Assert
        result.Should().NotBeNull();
        result?.Id.Should().Be(beneficiary.Id);
        result?.Dni.Should().Be(beneficiary.Dni);
    }

    [Test]
    public async Task GetBeneficiaryById_Fails()
    {
        // Arrange
        const int id = 123;
        
        // Act
        var result = await _dbAccess.GetBeneficiary(id, default);
        
        // Assert
        result.Should().BeNull();
    }
}