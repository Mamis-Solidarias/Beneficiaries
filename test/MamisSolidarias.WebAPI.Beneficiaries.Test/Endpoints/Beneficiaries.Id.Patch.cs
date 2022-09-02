using Moq;
using NUnit.Framework;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints;

internal class BeneficiariesIdPatch
{
    // private Endpoint _endpoint = null!;
    private readonly Mock<Beneficiaries.Id.PATCH.DbAccess> _mockDb = new();

    [SetUp]
    public void Setup()
    {
        // _endpoint = EndpointFactory.CreateEndpoint<Endpoint>(null, _mockDb.Object);
    }

    [TearDown]
    public void Teardown()
    {
        _mockDb.Reset();
    }

    // TODO: Implement tests when custom JsonConverter support is added
    // [Test]
    // public async Task WithAllParameters_Succeeds()
    // {
    //     // Arrange
    //     Beneficiary b = DataFactory.GetBeneficiary();
    //     Beneficiary newData = DataFactory.GetBeneficiary();
    //
    //     _mockDb.Setup(t => t.GetBeneficiary(
    //             It.Is<int>(r => r == b.Id),
    //             It.IsAny<CancellationToken>()
    //         ))
    //         .ReturnsAsync(b);
    //
    //     var request = new Request
    //     {
    //         Id = b.Id,
    //         Beneficiary = new BeneficiaryRequest
    //         {
    //             FirstName = newData.FirstName,
    //             LastName = newData.LastName,
    //             Type = newData.Type.ToString(),
    //             Gender = newData.Gender.ToString(),
    //             // Birthday = newData.Birthday,
    //             Dni = newData.Dni,
    //             Comments = newData.Comments,
    //             Likes = newData.Likes,
    //             Clothes = Map(newData.Clothes),
    //             Education = Map(newData.Education),
    //             Health = Map(newData.Health),
    //             Job = Map(newData.Job)
    //         }
    //     };
    //     
    //     // Act
    //     await _endpoint.HandleAsync(request, default);
    //     
    //     // Assert
    //     _endpoint.HttpContext.Response.StatusCode.Should().Be(200);
    //     var response = _endpoint.Response;
    //     response.Birthday.Should().Be(newData.Birthday);
    //     response.FirstName.Should().Be(newData.FirstName);
    //     response.LastName.Should().Be(newData.LastName);
    //     response.Type.Should().Be(newData.Type.ToString());
    //     response.Gender.Should().Be(newData.Gender.ToString());
    //     response.Dni.Should().Be(newData.Dni);
    //     response.Comments.Should().Be(newData.Comments);
    //     response.Likes.Should().Be(newData.Likes);
    //     response.Clothes.Should().Be(newData.Clothes);
    //     response.Education.Should().Be(newData.Education);
    //     response.Health.Should().Be(newData.Health);
    //     response.Job.Should().Be(newData.Job);
    //   
    // }
    //
    // private static JobRequest? Map(Job? j)
    //     => j is null ? null : new JobRequest(j.Title);
    //
    // private static HealthRequest? Map(Health? h)
    //     => h is null ? null : new HealthRequest(h.HasCovidVaccine, h.HasMandatoryVaccines, h.Observations);
    //
    // private static EducationRequest? Map(Education? e)
    //     => e is null ? null : new EducationRequest(e.Year, e.School, e.TransportationMethod?.ToString());
    //
    // private static ClothesRequest? Map(Clothes? c)
    //     => c is null ? null : new ClothesRequest(c.ShoeSize, c.ShirtSize, c.PantsSize);

    
}