namespace MamisSolidarias.HttpClient.Beneficiaries.Models;

/// <param name="Id">Id of the beneficiary</param>
/// <param name="FirstName">First name of the beneficiary</param>
/// <param name="LastName">Last name of the beneficiary</param>
/// <param name="Type">Type of the beneficiary</param>
/// <param name="Gender">Gender of the beneficiary</param>
/// <param name="Birthday">Birthday of the beneficiary (date only)</param>
/// <param name="Dni">DNI of the beneficiary. It can have dots in between the numbers or not</param>
/// <param name="Comments">Optional. Additional comments</param>
/// <param name="Likes">Optional. What the beneficiary likes</param>
/// <param name="Clothes">Optional. The size of the beneficiary's clothes</param>
/// <param name="Education">Optional. Where the beneficiary studies</param>
/// <param name="Health">Optional. Health information for the beneficiary</param>
/// <param name="Job">Optional. Job information for parents or tutors</param>
public record Beneficiary(
    int Id,string FirstName, string LastName, string Type, string Gender, DateOnly Birthday,
    string Dni, string? Comments, string? Likes, Clothes? Clothes,
    Education? Education, Health? Health, Job? Job
);