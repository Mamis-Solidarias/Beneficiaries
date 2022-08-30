using System;
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
using ContactType = MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Communities.Id.Families.GET.ContactType;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints;

internal class CommunitiesIdFamiliesGet
{
    private Endpoint _endpoint = null!;
    private readonly Mock<Communities.Id.Families.GET.DbAccess> _mockDbAccess = new();
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
        var pageSize = 10;
        var page = 2;
        Community community = _dataFactory.GetCommunity();
        var families = _dataFactory.GetFamilies(pageSize)
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
                new ContactResponse(Map(r.Type), r.Content, r.Title, r.IsPreferred))
        );
    
    private static ContactType  Map (MamisSolidarias.Infrastructure.Beneficiaries.Models.ContactType t)
        => t switch
        {
            Infrastructure.Beneficiaries.Models.ContactType.Email => ContactType.Email ,
            Infrastructure.Beneficiaries.Models.ContactType.Phone => ContactType.Phone,
            Infrastructure.Beneficiaries.Models.ContactType.Whatsapp => ContactType.Whatsapp,
            Infrastructure.Beneficiaries.Models.ContactType.Facebook => ContactType.Facebook,
            Infrastructure.Beneficiaries.Models.ContactType.Other => ContactType.Other,
            Infrastructure.Beneficiaries.Models.ContactType.Instagram => ContactType.Instagram ,
            _ => throw new ArgumentOutOfRangeException(nameof(t), t, "Invalid ContactType")
        };
}