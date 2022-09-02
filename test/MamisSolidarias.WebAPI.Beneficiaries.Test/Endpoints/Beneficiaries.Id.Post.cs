using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.Utils.Test;
using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Beneficiaries.Id.POST;
using MamisSolidarias.WebAPI.Beneficiaries.Utils;
using Moq;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints;

internal class BeneficiariesIdPost
{
    private Endpoint _endpoint = null!;
    private readonly Mock<Beneficiaries.Id.POST.DbAccess> _mockDb = new();

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
    public async Task WithValidParameters_Succeeds()
    {
        // Arrange
        const int id = 123;
        Beneficiary beneficiary = DataFactory.GetBeneficiary()
            .WithId(id)
            .IsActive(false);

        _mockDb.Setup(t => t.GetBeneficiary(
            It.Is<int>(r => r == id),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(beneficiary);

        // Act
        await _endpoint.HandleAsync(new Request {Id = id}, default);

        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        beneficiary.IsActive.Should().BeTrue();
    }

    [Test]
    public async Task BeneficiaryDoesNotExists_Fails()
    {
        // Arrange
        const int id = 123;

        _mockDb.Setup(t => t.GetBeneficiary(
            It.Is<int>(r => r == id),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync((Beneficiary?) null);

        // Act
        await _endpoint.HandleAsync(new Request {Id = id}, default);

        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(404);
    }

    [Test]
    public async Task BeneficiaryIsAlreadyActive_Fails()
    {
        // Arrange
        const int id = 123;
        Beneficiary beneficiary = DataFactory.GetBeneficiary().WithId(id);

        _mockDb.Setup(t => t.GetBeneficiary(
            It.Is<int>(r => r == id),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(beneficiary);

        // Act
        await _endpoint.HandleAsync(new Request {Id = id}, default);

        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(400);
    }
}