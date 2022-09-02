namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Beneficiaries.Id.PATCH;



/// <param name="FirstName">First name of the beneficiary</param>
/// <param name="LastName">Last name of the beneficiary</param>
/// <param name="Type">Type of the beneficiary</param>
/// <param name="Gender">Gender of the beneficiary</param>
/// <param name="Birthday">Birthday of the beneficiary (date only)</param>
/// <param name="Dni">DNI of the beneficiary. It can have dots in between the numbers or not</param>
/// <param name="Comments">Additional comments</param>
/// <param name="Likes">What the beneficiary likes</param>
/// <param name="Clothes">The size of the beneficiary's clothes</param>
/// <param name="Education">Where the beneficiary studies</param>
/// <param name="Health">Health information for the beneficiary</param>
/// <param name="Job">Job information for parents or tutors</param>
public record Response(
    int Id, string FamilyId,string FirstName, string LastName, string Type, string Gender, DateOnly Birthday,
    string Dni, string? Comments, string? Likes, ClothesResponse? Clothes,
    EducationResponse? Education, HealthResponse? Health, JobResponse? Job
);

/// <param name="Shoes">Shoe size</param>
/// <param name="Shirt">Shirt size</param>
/// <param name="Pants">Pants size</param>
public record ClothesResponse(string? Shoes, string? Shirt, string? Pants);

/// <param name="Year">In which year the student is in</param>
/// <param name="School">Which school does the student goes to</param>
/// <param name="TransportationMethod">How does the student get to school</param>
public record EducationResponse(string? Year, string? School, string? TransportationMethod);


/// <param name="HasCovidVaccine">Does the beneficiary have the covid vaccine</param>
/// <param name="HasMandatoryVaccines">Does the beneficiary have all the vaccines</param>
/// <param name="Observations">Additional health information</param>
public record HealthResponse(bool? HasCovidVaccine, bool? HasMandatoryVaccines, string? Observations);

/// <param name="Title">Job title</param>
public record JobResponse(string Title);
