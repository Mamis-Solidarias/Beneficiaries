using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityFramework.Exceptions.Common;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.Utils.Test;
using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.Id.Beneficiaries.POST;
using MamisSolidarias.WebAPI.Beneficiaries.Utils;
using Moq;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints;

internal class FamiliesIdBeneficiariesPost
{
    private Endpoint _endpoint = null!;
    private readonly Mock<Families.Id.Beneficiaries.POST.DbAccess> _mockDb = new();

    [SetUp]
    public void Setup()
    {
        _endpoint = EndpointFactory.CreateEndpoint<Endpoint>(null, _mockDb.Object);
    }

    [TearDown]
    public void Teardown()
    {
        _mockDb.Reset();
    }

    [Test]
    public async Task WithValidParameters_CreateMultiple_Succeeds()
    {
        // Arrange
        const string familyId = "TXT-123";
        var beneficiaries = DataFactory.GetBeneficiaries(3)
            .Select(t => t.WithId(0).Build())
            .ToList();

        var req = new Request
        {
            Beneficiaries = beneficiaries.Select(Map),
            FamilyId = familyId
        };

        _mockDb.Setup(t =>
            t.FamilyDoesNotExist(
                It.Is<string>(r => r == familyId),
                It.IsAny<CancellationToken>()
            )
        ).ReturnsAsync(false);
        
        // Act
        await _endpoint.HandleAsync(req, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        var response = _endpoint.Response;

        response.Beneficiaries.Should().HaveCount(beneficiaries.Count);
    }
    
    [Test]
    public async Task FamilyDoesNotExists_Fails()
    {
        // Arrange
        const string familyId = "TXT-123";
        var beneficiaries = DataFactory.GetBeneficiaries(3)
            .Select(t => t.WithId(0).Build())
            .ToList();

        var req = new Request
        {
            Beneficiaries = beneficiaries.Select(Map),
            FamilyId = familyId
        };

        _mockDb.Setup(t =>
            t.FamilyDoesNotExist(
                It.Is<string>(r => r == familyId),
                It.IsAny<CancellationToken>()
            )
        ).ReturnsAsync(true);
        
        // Act
        await _endpoint.HandleAsync(req, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(404);
    } 
    
    [Test]
    public async Task RepeatedDni_Fails()
    {
        // Arrange
        const string familyId = "TXT-123";
        var beneficiaries = DataFactory.GetBeneficiaries(3)
            .Select(t => t.WithDni("12332132").Build())
            .ToList();

        var req = new Request
        {
            Beneficiaries = beneficiaries.Select(Map),
            FamilyId = familyId
        };

        _mockDb.Setup(t =>
            t.FamilyDoesNotExist(
                It.Is<string>(r => r == familyId),
                It.IsAny<CancellationToken>()
            )
        ).ReturnsAsync(false);

        _mockDb.Setup(t =>
                t.AddBeneficiaries(It.IsAny<Beneficiary[]>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new UniqueConstraintException());

        // Act
        await _endpoint.HandleAsync(req, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(409);
    }


    private static BeneficiaryRequest Map(Beneficiary b)
        => new(
            b.FirstName, b.LastName, b.Type.ToString(),b.Gender.ToString(), b.Birthday,b.Dni,
            b.Comments, b.Likes, Map(b.Clothes), Map(b.Education), Map(b.Health),
            Map(b.Job)
        );

    private static JobRequest? Map(Job? job) =>
        job is null ? null : new JobRequest(job.Title);

    private static HealthRequest? Map(Health? health) =>
        health is null ? 
            null : 
            new HealthRequest(health.HasCovidVaccine, health.HasMandatoryVaccines, health.Observations);

    private static EducationRequest? Map(Education? education) =>
        education is null ? 
            null : 
            new EducationRequest(education.Year?.ToString(), education.School, education.TransportationMethod?.ToString());

    private static ClothesRequest? Map(Clothes? clothes) =>
        clothes is null ? 
            null : 
            new ClothesRequest(clothes.ShoeSize, clothes.ShirtSize, clothes.PantsSize);
}