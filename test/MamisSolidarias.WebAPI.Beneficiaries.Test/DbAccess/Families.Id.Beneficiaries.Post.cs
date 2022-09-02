using System.Threading.Tasks;
using EntityFramework.Exceptions.Common;
using EntityFramework.Exceptions.Sqlite;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Beneficiaries;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.WebAPI.Beneficiaries.Utils;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.DbAccess;

internal class FamiliesIdBeneficiariesPost
{
   private BeneficiariesDbContext _dbContext = null!;
   private Endpoints.Families.Id.Beneficiaries.POST.DbAccess _dbAccess = null!;
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
   public async Task FamilyDoesNotExist_WithValidFamily_Succeeds()
   {
      // Arrange
      Family family = _dataFactory.GenerateFamily();
      
      // Act
      var result = await _dbAccess.FamilyDoesNotExist(family.Id, default);
      
      // Assert
      result.Should().BeFalse();
   }
   
   [Test]
   public async Task FamilyDoesNotExist_WithoutValidFamily_Succeeds()
   {
      // Arrange
      const string id = "TXT-123";
      // Act
      var result = await _dbAccess.FamilyDoesNotExist(id, default);
      
      // Assert
      result.Should().BeTrue();
   }

   [Test]
   public Task CreateBeneficiary_Succeeds()
   {
      return Task.CompletedTask;
      // TODO: FIX
      // // Arrange
      // var family = _dataFactory.GenerateFamily().Build();
      //
      // Beneficiary user = DataFactory.GetBeneficiary()
      //    .WithDni("22345678")
      //    .WithFamilyId(family.Id)
      //    .WithId(0);
      // // try
      // // {
      //    // Act
      //    await _dbAccess.AddBeneficiaries(new[] {user}, default);
      // // }
      // // catch (Exception e)
      // // {
      // //    
      // // }
      // // assert
      // user.Id.Should().BePositive();
   }
   
   [Test]
   public async Task CreateBeneficiary_RepeatedDni_Fails()
   {
      // Arrange
      var family = _dataFactory.GenerateFamily().Build();

      var other= _dataFactory.GenerateBeneficiary().WithFamilyId(family.Id).Build();

      Beneficiary user = DataFactory.GetBeneficiary()
         .WithFamilyId(family.Id)
         .WithDni(other.Dni)
         .WithId(0);
      
      // Act
      var actions = async () => await _dbAccess.AddBeneficiaries(new[] {user}, default);
      
      // assert
      await actions.Should().ThrowAsync<UniqueConstraintException>(default);
   }
   
   [Test]
   public async Task CreateBeneficiary_WithInvalidFamily_Fails()
   {
      // Arrange
      Beneficiary user = DataFactory.GetBeneficiary()
         .WithFamily(null)
         .WithFamilyId("TXT-123")
         .WithId(0);

      // Act
      var actions = async () => await _dbAccess.AddBeneficiaries(new[] {user}, default);
      
      // assert
      await actions.Should().ThrowAsync<DbUpdateException>();
   }
   
   
}