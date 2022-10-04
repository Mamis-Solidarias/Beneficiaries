namespace MamisSolidarias.HttpClient.Beneficiaries.Models;

/// <param name="Year">In which year the student is in</param>
/// <param name="School">Which school does the student goes to</param>
/// <param name="TransportationMethod">How does the student get to school</param>
public sealed record Education(string? Year, string? School, string? TransportationMethod);