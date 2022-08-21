using MamisSolidarias.HttpClient.Beneficiaries.Models;
using MamisSolidarias.HttpClient.Beneficiaries.Services;
using MamisSolidarias.Utils.Http;
using Microsoft.AspNetCore.Http;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

public partial class BeneficiariesClient : IBeneficiariesClient
{
    private readonly HeaderService _headerService;
    private readonly IHttpClientFactory _httpClientFactory;
    
    public BeneficiariesClient(IHttpContextAccessor? contextAccessor,IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _headerService = new HeaderService(contextAccessor);
    }
    
    private ReadyRequest CreateRequest(HttpMethod httpMethod,params string[] urlParams)
    {
        var client = _httpClientFactory.CreateClient("Beneficiaries");
        var request = new HttpRequestMessage(httpMethod, string.Join('/', urlParams));
        
        var authHeader = _headerService.GetAuthorization();
        if (authHeader is not null)
            request.Headers.Add("Authorization",authHeader);
        
        return new ReadyRequest(client,request);
    }
}