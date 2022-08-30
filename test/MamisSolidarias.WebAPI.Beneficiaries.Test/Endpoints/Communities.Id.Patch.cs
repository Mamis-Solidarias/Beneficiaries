using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.Utils.Test;
using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.PATCH;
using MamisSolidarias.WebAPI.Beneficiaries.Utils;
using Moq;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints;

internal class CommunitiesIdPatch
{
    private Endpoint _endpoint = null!;
    private readonly DataFactory _dataFactory = new(null);
    private readonly Mock<Communities.Id.PATCH.DbAccess> _mockDb = new();
    
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
    public async Task UpdatingAllParameters_Succeeds()
    {
        // Arrange
        Community community = _dataFactory.GetCommunity();
        var newAddress = "Calle Falsa 123";
        var newDescription = "Nice place!";
        var request = new Request
        {
            Id = community.Id!,
            Address = newAddress,
            Description = newDescription
        };

        _mockDb.Setup(t => t.GetCommunityById(
                It.Is<string>(r => r == community.Id),
                It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(community);
        
        // Act
        await _endpoint.HandleAsync(request, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        _endpoint.Response.Community.Address.Should().Be(newAddress);
        _endpoint.Response.Community.Description.Should().Be(newDescription);
        _endpoint.Response.Community.Id.Should().Be(community.Id);
        _endpoint.Response.Community.Name.Should().Be(community.Name);
    }
    
    [Test]
    public async Task UpdatingOnlyAddress_Succeeds()
    {
        // Arrange
        Community community = _dataFactory.GetCommunity();
        var newAddress = "Calle Falsa 123";
        var request = new Request
        {
            Id = community.Id!,
            Address = newAddress,
        };

        _mockDb.Setup(t => t.GetCommunityById(
                It.Is<string>(r => r == community.Id),
                It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(community);
        
        // Act
        await _endpoint.HandleAsync(request, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        _endpoint.Response.Community.Address.Should().Be(newAddress);
        _endpoint.Response.Community.Description.Should().Be(community.Description);
        _endpoint.Response.Community.Id.Should().Be(community.Id);
        _endpoint.Response.Community.Name.Should().Be(community.Name);
    }
    
    [Test]
    public async Task UpdatingOnlyDescription_Succeeds()
    {
        // Arrange
        Community community = _dataFactory.GetCommunity();
        var newDescription = "Nice place!";
        var request = new Request
        {
            Id = community.Id!,
            Description = newDescription
        };

        _mockDb.Setup(t => t.GetCommunityById(
                It.Is<string>(r => r == community.Id),
                It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(community);
        
        // Act
        await _endpoint.HandleAsync(request, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        _endpoint.Response.Community.Address.Should().Be(community.Address);
        _endpoint.Response.Community.Description.Should().Be(newDescription);
        _endpoint.Response.Community.Id.Should().Be(community.Id);
        _endpoint.Response.Community.Name.Should().Be(community.Name);
    }
    
    [Test]
    public async Task CommunityDoesNotExists_Fails()
    {
        // Arrange
        var newAddress = "Calle Falsa 123";
        var newDescription = "Nice place!";
        var request = new Request
        {
            Id = "123",
            Address = newAddress,
            Description = newDescription
        };

        _mockDb.Setup(t => t.GetCommunityById(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())
            )
            .ReturnsAsync((Community?)null);
        
        // Act
        await _endpoint.HandleAsync(request, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(404);
    }
    
}