namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

partial class BeneficiariesClient
{
    /// <inheritdoc />
    public Task DeleteBeneficiary(int id, CancellationToken token)
        => CreateRequest(HttpMethod.Delete, "beneficiaries", $"{id}")
            .ExecuteAsync(token);
}