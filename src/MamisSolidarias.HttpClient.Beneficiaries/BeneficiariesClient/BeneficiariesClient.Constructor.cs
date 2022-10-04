using MamisSolidarias.Utils.Http;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

public sealed partial class BeneficiariesClient : IBeneficiariesClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public BeneficiariesClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory; }
    
    private ReadyRequest CreateRequest(HttpMethod httpMethod,params string[] urlParams)
    {
        var client = _httpClientFactory.CreateClient("Beneficiaries");
        var request = new HttpRequestMessage(httpMethod, string.Join('/', urlParams));

        return new ReadyRequest(client,request);
    }
}