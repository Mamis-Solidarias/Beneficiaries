namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

partial class BeneficiariesClient
{
    public Task DeleteBeneficiary(int id, CancellationToken token)
        => CreateRequest(HttpMethod.Delete, "beneficiaries", $"{id}")
            .ExecuteAsync(token);
}