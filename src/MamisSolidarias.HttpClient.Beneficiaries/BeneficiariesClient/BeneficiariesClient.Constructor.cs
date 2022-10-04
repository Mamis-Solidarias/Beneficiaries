using MamisSolidarias.Utils.Http;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

/// <inheritdoc />
public sealed partial class BeneficiariesClient : IBeneficiariesClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    /// <summary>
    /// It creates a new instance of <see cref="BeneficiariesClient"/>.
    /// </summary>
    /// <param name="httpClientFactory"></param>
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