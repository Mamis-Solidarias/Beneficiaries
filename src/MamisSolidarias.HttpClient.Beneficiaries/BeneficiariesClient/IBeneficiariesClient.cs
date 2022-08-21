using MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Test;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

public interface IBeneficiariesClient
{
    Task<Response?> GetTestAsync(Request requestParameters, CancellationToken token = default);
}