// using System;
// using System.Net;
// using System.Net.Http;
// using System.Net.Http.Json;
// using System.Threading.Tasks;
// using FluentAssertions;
// using MamisSolidarias.HttpClient.Beneficiaries.Utils;
// using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Test;
// using Microsoft.Extensions.Configuration;
// using Moq;
// using NUnit.Framework;
// using RichardSzalay.MockHttp;
//
// namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;
//
// internal class BeneficiariesClientTestsGetTest
// {
//     private readonly MockHttpMessageHandler _httpHandlerMock = new();
//     private readonly Mock<IHttpClientFactory> _httpClientFactory = new();
//     private readonly IConfiguration _configuration = ConfigurationFactory.GetBeneficiariesConfiguration();
//     private BeneficiariesClient _client = null!;
//     
//     [SetUp]
//     public void CreateHttpRequest()
//     {
//         _httpClientFactory.Setup(t => t.CreateClient("Beneficiaries"))
//             .Returns(new System.Net.Http.HttpClient(_httpHandlerMock)
//             {
//                 BaseAddress = new Uri(_configuration["BeneficiariesHttpClient:BaseUrl"])
//             });
//         _client = new BeneficiariesClient(null,_httpClientFactory.Object);
//     }
//
//     [TearDown]
//     public void DisposeHttpRequest()
//     {
//         _httpHandlerMock.Clear();
//     }
//
//
//     [Test]
//     public async Task WithValidParameters_Succeeds()
//     {
//         // // arrange
//         // var user = DataFactory.GetUser();
//         // var request = new Request {Name = user.Name};
//         // var expectedResponse = new Response
//         // {
//         //     Name = user.Name,
//         //     Email = "me@mail.com",
//         //     Id = user.Id
//         // };
//         //
//         //  _httpHandlerMock.When(HttpMethod.Get, _configuration["BeneficiariesHttpClient:BaseUrl"] + $"/user/{user.Name}")
//         //     .Respond(HttpStatusCode.OK, JsonContent.Create(expectedResponse));
//         //
//         // // act
//         // var response = await _client.GetTestAsync(request);
//         //
//         // // assert
//         // response.Should().NotBeNull();
//         // response.Should().BeEquivalentTo(expectedResponse);
//     }
//
//     [Test]
//     public async Task WithInvalidUser_ThrowsNotFound()
//     {
//         // // arrange
//         // var user = DataFactory.GetUser();
//         // var request = new Request {Name = user.Name };
//         //
//         // _httpHandlerMock.When(HttpMethod.Get, _configuration["BeneficiariesHttpClient:BaseUrl"] + $"/user/{user.Name}")
//         //     .Respond(HttpStatusCode.NotFound);
//         //
//         // // act
//         // var action = async () => await _client.GetTestAsync(request); 
//         //
//         // // asser
//         // await action.Should().ThrowAsync<HttpRequestException>();
//     }
// }