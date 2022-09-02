using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.Utils.Test;
using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.GET;
using MamisSolidarias.WebAPI.Beneficiaries.Utils;
using Moq;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints;

internal class CommunitiesIdFamiliesGet
{
    private Endpoint _endpoint = null!;
    private readonly Mock<Communities.Id.Families.GET.DbAccess> _mockDbAccess = new();

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
        var pageSize = 10;
        var page = 2;
        Community community = DataFactory.GetCommunity();
        var families = DataFactory.GetFamilies(pageSize)
            .Select(t => t.WithCommunity(community).Build())
            .ToList();

        _mockDbAccess.Setup(t =>
                t.GetFamilies(
                    It.Is<string>(r => r == community.Id),
                    It.Is<int>(r => r == page),
                    It.Is<int>(r => r == pageSize),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(families);

        _mockDbAccess.Setup(t =>
                t.GetTotalEntries(
                    It.Is<string>(r => r == community.Id),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(100);

        var req = new Request
        {
            Id = community.Id!,
            Page = page,
            PageSize = pageSize
        };

        // Act
        await _endpoint.HandleAsync(req, default);

        // Assert
        _endpoint.HttpContext.Response.StatusCode.Should().Be(200);
        var response = _endpoint.Response;

        response.Page.Should().Be(page);
        response.TotalPages.Should().Be(10);
        response.Families.Should().BeEquivalentTo(families.Select(Map));
    }

    private static FamilyResponse Map(Family f)
        => new (f.Id, f.Name, f.Address, f.Details,
            f.Contacts.Select(r =>
                new ContactResponse(r.Type.ToString(), r.Content, r.Title, r.IsPreferred))
        );
    

}