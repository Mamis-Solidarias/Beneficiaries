namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

public partial class BeneficiariesClient
{
    public Task ReactivateBeneficiary(int id, CancellationToken token)
        => CreateRequest(HttpMethod.Post, "beneficiaries", $"{id}")
            .ExecuteAsync(token);
}