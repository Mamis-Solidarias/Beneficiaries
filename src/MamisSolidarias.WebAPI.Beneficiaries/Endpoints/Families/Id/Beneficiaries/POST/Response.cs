namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.Id.Beneficiaries.POST;

public class Response
{
    /// <summary>
    /// A list with the created Ids
    /// </summary>
    public IEnumerable<int> Beneficiaries { get; set; } = new List<int>();
}