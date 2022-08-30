using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.Utils.Test;
using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.GET;
using MamisSolidarias.WebAPI.Beneficiaries.Utils;
using Moq;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints;

internal class CommunitiesIdGet
{
    private Endpoint _endpoint = null!;
    private readonly Mock<Communities.Id.GET.DbAccess> _mockDbAccess = new();
    private readonly DataFactory _dataFactory = new(null);

    [SetUp]
    public void SetUp()
    {
        _endpoint = EndpointFactory
            .CreateEndpoint<Endpoint>(null, _mockDbAccess.Object);
        
    }

    [TearDown]
    public void Teardown()
    {
        _mockDbAccess.Reset();
    }

    [Test]
    public async Task WithValidParameters_Succeeds()
    {
        // Arrange
        Community community = _dataFactory.GetCommunity();
        var request = new Request {Id = community.Id!};

        _mockDbAccess.Setup(t =>
                t.GetCommunityFromId(It.Is<string>(r => r == community.Id),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(community);
        
        // Act
        await _endpoint.HandleAsync(request, default);
        var response = _endpoint.Response;
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        response.Community.Should().NotBeNull();
        response.Community.Address.Should().Be(community.Address);
        response.Community.Description.Should().Be(community.Description);
        response.Community.Id.Should().Be(community.Id);
        response.Community.Name.Should().Be(community.Name);
    }

    [Test]
    public async Task CommunityDoesNotExists_Succeeds()
    {
        // Arrange
        var request = new Request {Id = "MIA"};

        _mockDbAccess.Setup(t =>
                t.GetCommunityFromId(It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync((Community?)null);
        
        // Act
        await _endpoint.HandleAsync(request, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(404);
    }
}