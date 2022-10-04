using MamisSolidarias.HttpClient.Beneficiaries.Models;

namespace MamisSolidarias.HttpClient.Beneficiaries.BeneficiariesClient;

partial class BeneficiariesClient
{
    public Task<Beneficiary?> UpdateBeneficiary(int id,UpdateBeneficiaryRequest request, CancellationToken token)
        => CreateRequest(HttpMethod.Patch, "beneficiaries", $"{id}")
            .WithContent(new
            {
                request.Beneficiary
            })
            .ExecuteAsync<Beneficiary>(token);


    public sealed record UpdateBeneficiaryRequest(UpdateBeneficiaryRequestModel Beneficiary);

    /// <param name="FirstName">First name of the beneficiary</param>
    /// <param name="LastName">Last name of the beneficiary</param>
    /// <param name="Type">Type of the beneficiary</param>
    /// <param name="Gender">Gender of the beneficiary</param>
    /// <param name="Birthday">Birthday of the beneficiary (yyyy-mm-dd)</param>
    /// <param name="Dni">DNI of the beneficiary. It can have dots in between the numbers or not</param>
    /// <param name="Comments">Additional comments</param>
    /// <param name="Likes">What the beneficiary likes</param>
    /// <param name="Clothes">The size of the beneficiary's clothes</param>
    /// <param name="Education">Where the beneficiary studies</param>
    /// <param name="Health">Health information for the beneficiary</param>
    /// <param name="Job">Job information for parents or tutors</param>
    public sealed record UpdateBeneficiaryRequestModel
    (
        string? FirstName, string? LastName, string? Type, string? Gender, DateOnly? Birthday, string? Dni,
        string? Comments, string? Likes, ClothesRequest? Clothes, EducationRequest? Education,
        HealthRequest? Health, JobRequest? Job
    );
    
    /// <param name="Shoes">Shoe size</param>
    /// <param name="Shirt">Shirt size</param>
    /// <param name="Pants">Pants size</param>
    public record ClothesRequest(string? Shoes, string? Shirt, string? Pants);

    /// <param name="Year">In which year the student is in</param>
    /// <param name="School">Which school does the student goes to</param>
    /// <param name="TransportationMethod">How does the student get to school</param>
    public record EducationRequest(string? Year, string? School, string? TransportationMethod);

    /// <param name="HasCovidVaccine">Does the beneficiary have the covid vaccine</param>
    /// <param name="HasMandatoryVaccines">Does the beneficiary have all the vaccines</param>
    /// <param name="Observations">Additional health information</param>
    public record HealthRequest(bool? HasCovidVaccine, bool? HasMandatoryVaccines, string? Observations);

    /// <param name="Title">Job title</param>
    public record JobRequest(string Title);
}