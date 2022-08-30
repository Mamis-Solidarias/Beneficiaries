using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.Utils.Test;
using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.GET;
using MamisSolidarias.WebAPI.Beneficiaries.Utils;
using Moq;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints;

internal class CommunitiesGet
{
    private Endpoint _endpoint = null!;
    private readonly Mock<Communities.GET.DbAccess> _mockDbAccess = new();
    private readonly DataFactory _dataFactory = new(null);

    [SetUp]
    public void SetUp()
    {
        _endpoint = EndpointFactory
            .CreateEndpoint<Endpoint>(null, _mockDbAccess.Object)
            .Build();
        
    }

    [TearDown]
    public void Teardown()
    {
        _mockDbAccess.Reset();
    }

    [Test]
    public async Task WithManyCommunities_Succeeds()
    {
        // Arrange
        var communities = _dataFactory.GetCommunities(3).Select(t=> t.Build()).ToList();
        _mockDbAccess.Setup(t => t.GetCommunities(It.IsAny<CancellationToken>()))
            .ReturnsAsync(communities);
        
        // Act
        await _endpoint.HandleAsync(default);
        var response = _endpoint.Response;
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        response.Communities.Should().BeEquivalentTo(communities.Select(t=>
            new CommunityResponse(t.Id ?? string.Empty,t.Name,t.Description,t.Address)));
    }

    [Test]
    public async Task WithNoCommunities_Succeeds()
    {
        // Arrange
        var communities = new List<Community>();
        _mockDbAccess.Setup(t => t.GetCommunities(It.IsAny<CancellationToken>()))
            .ReturnsAsync(communities);
        
        // Act
        await _endpoint.HandleAsync(default);
        var response = _endpoint.Response;
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        response.Communities.Should().BeEquivalentTo(Enumerable.Empty<CommunityResponse>());
    }
}