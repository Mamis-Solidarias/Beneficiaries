using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.Utils.Test;
using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.POST;
using MamisSolidarias.WebAPI.Beneficiaries.Utils;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints;

internal class Communities_Post
{
    private Endpoint _endpoint = null!;
    private DataFactory _dataFactory = new(null);
    private readonly Mock<Communities.POST.DbAccess> _mockDb = new();
    
    [SetUp]
    public void Setup()
    {
        _endpoint = EndpointFactory.CreateEndpoint<Endpoint>(null, _mockDb.Object)
            .Build();
        
    }

    [TearDown]
    public void Teardown()
    {
        _mockDb.Reset();
    }

    [Test]
    public async Task WithValidParameters_WithGeneratedIds_Succeeds()
    {
        // Arrange
        var communities = _dataFactory.GetCommunities(2)
            .Select(t=> t.Build())
            .ToArray();
        
        var request = new Request
        {
            Communities =communities
                .Select(t=> new CommunityRequest(t.Name,t.Address,t.Description,t.Id))
        };

        _mockDb.Setup(t => t.CreateCommunities(It.IsAny<IEnumerable<Community>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(communities);
        
        // Act
        await _endpoint.HandleAsync(request,default);
        var response = _endpoint.Response;
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(201);
        response.Communities.Should().HaveCount(2);
        response.Communities.Should().Equal(communities.Select(t=> t.Id));
    }

    [Test]
    public async Task WithValidParameters_WithoutIds_Succeeds()
    {
        // Arrange
        var communities = _dataFactory.GetCommunities(2)
            .Select(t=> t.Build())
            .ToArray();

        var request = new Request
        {
            Communities = communities
                .Select(t=> new CommunityRequest(t.Name,t.Address,t.Description,null))
        };

        _mockDb.Setup(t => t.CreateCommunities(It.IsAny<IEnumerable<Community>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(communities);
        
        // Act
        await _endpoint.HandleAsync(request,default);
        var response = _endpoint.Response;
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(201);
        response.Communities.Should().HaveCount(2);
        response.Communities.Should().Equal(communities.Select(t=> t.Id));
    }

    [Test]
    public async Task WithInvalidParameters_RepeatedIds_Fails()
    {
        // Arrange
        var communities = _dataFactory.GetCommunities(2)
            .Select(t=>t.Build())
            .ToArray();

        var request = new Request
        {
            Communities = communities
                .Select(t=> new CommunityRequest(t.Name,t.Address,t.Description,null))
        };

        _mockDb.Setup(t => t.CreateCommunities(It.IsAny<IEnumerable<Community>>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new DbUpdateException());
        
        // Act
        await _endpoint.HandleAsync(request,default);
        var response = _endpoint.Response;
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(409);
    }
}