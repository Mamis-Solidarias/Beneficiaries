using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.Utils.Test;
using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.Id.GET;
using MamisSolidarias.WebAPI.Beneficiaries.Utils;
using Moq;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints;

internal class CommunitiesIdFamiliesIdGet
{
    private Endpoint _endpoint = null!;
    private readonly Mock<Communities.Id.Families.Id.GET.DbAccess> _mockDbAccess = new();
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
        Family family = _dataFactory.GetFamily();
        var request = new Request
        {
            CommunityId = family.CommunityId,
            FamilyId = family.Id
        };

        _mockDbAccess.Setup(t =>
                t.GetFamily(It.Is<string>(r => r == family.Id),
                    It.IsAny<CancellationToken>()
                ))
            .ReturnsAsync(family);
        
        // Act
        await _endpoint.HandleAsync(request, default);
        var response = _endpoint.Response;
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        response.Address.Should().Be(family.Address);
        response.Details.Should().Be(family.Details);
        response.Name.Should().Be(family.Name);
        response.Id.Should().Be(family.Id);
        response.Contacts.Should().BeEquivalentTo(family.Contacts.Select(Map));
        response.Id.Should().Be(family.Id);
    }

    private ContactResponse Map(Contact t)
        => new ContactResponse(t.Type.ToString(), t.Content, t.Title, t.IsPreferred);

    [Test]
    public async Task CommunityDoesNotExists_Fails()
    {
        // Arrange
        var request = new Request
        {
            FamilyId = "TXT-123",
            CommunityId = "TXT"
        };

        _mockDbAccess.Setup(t =>
                t.GetFamily(It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync((Family?) null);
        
        // Act
        await _endpoint.HandleAsync(request, default);
        
        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(404);
    }
}