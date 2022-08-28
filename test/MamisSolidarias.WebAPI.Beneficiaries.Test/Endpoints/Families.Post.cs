using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using MamisSolidarias.Utils.Test;
using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.POST;
using MamisSolidarias.WebAPI.Beneficiaries.Utils;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ContactType = MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.POST.ContactType;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints;

internal class Families_Post
{
    private Endpoint _endpoint = null!;
    private DataFactory _dataFactory = new(null);
    private readonly Mock<Families.POST.DbAccess> _mockDb = new();
    
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
    public async Task WithValidParameters_Succeeds()
    {
        // Arrange
        var families = _dataFactory.GetFamilies(1)
            .Select(t => t.Build())
            .ToList();

        var request = new Request
        {
            Families = families.Select(t=>
                new FamilyRequest(
                    t.FamilyNumber,
                    t.Name,
                    t.Address,
                    t.CommunityId,
                    t.Details,
                    t.Contacts.Select(r=>
                        new ContactRequest(
                            MapContactType(r.Type),
                            r.Content,
                            r.Title,
                            r.IsPreferred)
                    ))
            )
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
        Family fam = _dataFactory.GetFamily();
        var families = new[] {fam, fam};

        var request = new Request
        {
            Families = families.Select(t=>
                new FamilyRequest(
                    t.FamilyNumber,
                    t.Name,
                    t.Address,
                    t.CommunityId,
                    t.Details,
                    t.Contacts.Select(r=>
                        new ContactRequest(
                            MapContactType(r.Type),
                            r.Content,
                            r.Title,
                            r.IsPreferred)
                    ))
            )
        };

        _mockDb.Setup(t =>
            t.CreateFamilies(
                It.Is<IEnumerable<Family>>(r => r.Count() >1 && r.DistinctBy(f=> new {f.FamilyNumber, f.CommunityId}).Count() == 1),
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
        Family fam = _dataFactory.GetFamily();
        var families = new[] {fam};

        var request = new Request
        {
            Families = families.Select(t=>
                new FamilyRequest(
                    t.FamilyNumber,
                    t.Name,
                    t.Address,
                    t.CommunityId,
                    t.Details,
                    t.Contacts.Select(r=>
                        new ContactRequest(
                            MapContactType(r.Type),
                            r.Content,
                            r.Title,
                            r.IsPreferred)
                    ))
            )
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
    
    private static ContactType  MapContactType (MamisSolidarias.Infrastructure.Beneficiaries.Models.ContactType t)
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
