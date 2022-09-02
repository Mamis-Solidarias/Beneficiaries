using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.Utils.Test;
using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.POST;
using MamisSolidarias.WebAPI.Beneficiaries.Utils;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints;

internal class FamiliesPost
{
    private Endpoint _endpoint = null!;
    private readonly Mock<Communities.Id.Families.POST.DbAccess> _mockDb = new();
    
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
        const string communityId = "XT";
        var families = DataFactory.GetFamilies(1)
            .Select(t => t.WithCommunityId(communityId).Build())
            .ToList();

        var request = new Request
        {
            Id = communityId,
            Families = families.Select(Map)
        };

        _mockDb.Setup(t =>
            t.CreateFamilies(
                It.Is<IEnumerable<Family>>(r=> r.SequenceEqual(families)), 
                It.IsAny<CancellationToken>()
                )
        ).Returns(Task.CompletedTask);
        
        // Act
        await _endpoint.HandleAsync(request, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(201);
    }
    
    [Test]
    public async Task WithRepeatedFamilies_Fails()
    {
        // Arrange
        var communityId = "XT";
        Family fam = DataFactory.GetFamily()
            .WithCommunityId(communityId);
        var families = new[] {fam, fam};

        var request = new Request
        {
            Id = communityId,
            Families = families.Select(Map)
        };

        _mockDb.Setup(t =>
            t.CreateFamilies(
                It.Is<IEnumerable<Family>>(r => 
                    // ReSharper disable PossibleMultipleEnumeration
                    r.DistinctBy(f=> new {f.FamilyNumber, f.CommunityId}).Count() != r.Count()),
                    // ReSharper enable PossibleMultipleEnumeration
                It.IsAny<CancellationToken>()
            )
        ).ThrowsAsync(new DbUpdateException());
        
        // Act
        await _endpoint.HandleAsync(request, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(409);
    }
    [Test]
    public async Task IfIdAlreadyExists_Fails()
    {
        // Arrange
        var communityId = "XT";
        Family fam = DataFactory.GetFamily().WithCommunityId(communityId);
        var families = new[] {fam};

        var request = new Request
        {
            Id = communityId,
            Families = families.Select(Map)
        };

        _mockDb.Setup(t =>
            t.CreateFamilies(
                It.Is<IEnumerable<Family>>(r => 
                    r.Any(e=> e.FamilyNumber == fam.FamilyNumber && e.CommunityId == fam.CommunityId)
                    ),
                It.IsAny<CancellationToken>()
            )
        ).ThrowsAsync(new DbUpdateException());
        
        // Act
        await _endpoint.HandleAsync(request, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(409);
    }

    private static FamilyRequest Map(Family t)
        => new (
            t.FamilyNumber,
            t.Name,
            t.Address,
            t.Details,
            t.Contacts.Select(r =>
                new ContactRequest(
                    r.Type.ToString(),
                    r.Content,
                    r.Title,
                    r.IsPreferred)
            ));


}
