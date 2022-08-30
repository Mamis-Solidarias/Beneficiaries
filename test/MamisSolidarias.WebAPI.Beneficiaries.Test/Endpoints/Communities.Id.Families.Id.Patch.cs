using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.Utils.Test;
using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.Id.PATCH;
using MamisSolidarias.WebAPI.Beneficiaries.Utils;
using Moq;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints;

internal class CommunitiesIdFamiliesIdPatch
{
    private Endpoint _endpoint = null!;
    private readonly DataFactory _dataFactory = new(null);
    private readonly Mock<Communities.Id.Families.Id.PATCH.DbAccess> _mockDb = new();
    
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
    public async Task WithAllParameters_Succeeds()
    {
        // Arrange
        Family family = _dataFactory.GetFamily();
        
        _mockDb.Setup(t => t.GetFamily(
            It.Is<string>(r => r == family.Id),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(family);

        var request = new Request
        {
            FamilyId = family.Id,
            Name = "New name",
            CommunityId = family.CommunityId,
            Address = "New address",
            Details = "New details",
            Contacts = new List<ContactRequest>(),
        };
        
        // Act
        await _endpoint.HandleAsync(request, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        var response = _endpoint.Response;

        response.Address.Should().Be(request.Address);
        response.Contacts.Should().BeEmpty();
        response.Details.Should().Be(request.Details);
        response.Name.Should().Be(request.Name);
    }
    
    [Test]
    public async Task WithOnlyAddress_Succeeds()
    {
        // Arrange
        Family family = _dataFactory.GetFamily();
        
        _mockDb.Setup(t => t.GetFamily(
            It.Is<string>(r => r == family.Id),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(family);

        var request = new Request
        {
            FamilyId = family.Id,
            CommunityId = family.CommunityId,
            Address = "New address",
        };
        
        // Act
        await _endpoint.HandleAsync(request, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        var response = _endpoint.Response;

        response.Address.Should().Be(request.Address);
        response.Contacts.Should().HaveSameCount(family.Contacts);
        response.Details.Should().Be(family.Details);
        response.Name.Should().Be(family.Name);
    }
    
    [Test]
    public async Task WithOnlyName_Succeeds()
    {
        // Arrange
        Family family = _dataFactory.GetFamily();
        
        _mockDb.Setup(t => t.GetFamily(
            It.Is<string>(r => r == family.Id),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(family);

        var request = new Request
        {
            FamilyId = family.Id,
            Name = "New name",
            CommunityId = family.CommunityId,
        };
        
        // Act
        await _endpoint.HandleAsync(request, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        var response = _endpoint.Response;

        response.Address.Should().Be(family.Address);
        response.Contacts.Should().HaveSameCount(family.Contacts);
        response.Details.Should().Be(family.Details);
        response.Name.Should().Be(request.Name);
    }
    
    [Test]
    public async Task WithOnlyDetails_Succeeds()
    {
        // Arrange
        Family family = _dataFactory.GetFamily();
        
        _mockDb.Setup(t => t.GetFamily(
            It.Is<string>(r => r == family.Id),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(family);

        var request = new Request
        {
            FamilyId = family.Id,
            CommunityId = family.CommunityId,
            Details = "New details",
        };
        
        // Act
        await _endpoint.HandleAsync(request, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        var response = _endpoint.Response;

        response.Address.Should().Be(family.Address);
        response.Contacts.Should().HaveSameCount(family.Contacts);
        response.Details.Should().Be(request.Details);
        response.Name.Should().Be(family.Name);
    }
    
    [Test]
    public async Task WithOnlyContacts_Succeeds()
    {
        // Arrange
        Family family = _dataFactory.GetFamily();
        
        _mockDb.Setup(t => t.GetFamily(
            It.Is<string>(r => r == family.Id),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(family);

        var request = new Request
        {
            FamilyId = family.Id,
            CommunityId = family.CommunityId,
            Contacts = new []
            {
                new ContactRequest("Facebook","carlos","Facebook Profile",false),
                new ContactRequest("Whatsapp","+5491198233456","Whatsapp Madre",true)
            }
        };
        
        // Act
        await _endpoint.HandleAsync(request, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        var response = _endpoint.Response;

        response.Address.Should().Be(family.Address);
        response.Contacts.Should().BeEquivalentTo(request.Contacts);
        response.Details.Should().Be(family.Details);
        response.Name.Should().Be(family.Name);
    }

    [Test]
    public async Task FamilyDoesNotExist_Fails()
    {
        // Arrange
        const string familyId = "CT-123";
        _mockDb.Setup(t =>
            t.GetFamily(
                It.Is<string>(r => r == familyId),
                It.IsAny<CancellationToken>()
            )
        ).ReturnsAsync((Family?) null);

        var request = new Request
        {
            CommunityId = "CT",
            FamilyId = familyId,
            Name = "new name"
        };
        
        // Act
        await _endpoint.HandleAsync(request, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(404);
    }
}