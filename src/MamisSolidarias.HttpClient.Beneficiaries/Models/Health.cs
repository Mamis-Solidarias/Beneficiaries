namespace MamisSolidarias.HttpClient.Beneficiaries.Models;

/// <param name="HasCovidVaccine">Does the beneficiary have the covid vaccine</param>
/// <param name="HasMandatoryVaccines">Does the beneficiary have all the vaccines</param>
/// <param name="Observations">Additional health information</param>
public sealed record Health(bool? HasCovidVaccine, bool? HasMandatoryVaccines, string? Observations);