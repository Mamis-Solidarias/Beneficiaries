using System;
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

internal class Families_Post
{
    private const string InMemoryConnectionString = "DataSource=:memory:";

    private BeneficiariesDbContext _dbContext = null!;
    private DataFactory _dataFactory = null!;
    private Endpoints.Families.POST.DbAccess _dbAccess = null!;

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

        _dbAccess = new Endpoints.Families.POST.DbAccess(_dbContext);
        _dataFactory = new DataFactory(_dbContext);
    }

    [TearDown]
    public void Dispose()
    {
        _dataFactory.Dispose();
    }
}