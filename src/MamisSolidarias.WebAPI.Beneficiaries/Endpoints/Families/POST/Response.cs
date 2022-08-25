namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.POST;

/// <summary>
/// Response model
/// </summary>
/// <param name="Ids">Ids of the created families</param>
public record Response(IEnumerable<string> Ids);