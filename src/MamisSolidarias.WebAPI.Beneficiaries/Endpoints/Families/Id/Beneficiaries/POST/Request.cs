using FastEndpoints;
using FluentValidation;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.AspNetCore.Mvc;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Families.Id.Beneficiaries.POST;

public class Request
{
    /// <summary>
    /// If of the family
    /// </summary>
    [FromRoute] public string FamilyId { get; set; } = string.Empty;

    /// <summary>
    /// List of beneficiaries that belong to the family
    /// </summary>
    public IEnumerable<BeneficiaryRequest> Beneficiaries { get; set; } = new List<BeneficiaryRequest>();
}

internal class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleForEach(t => t.Beneficiaries)
            .SetValidator(new BeneficiaryRequestValidator());

        RuleFor(t => t.FamilyId)
            .NotEmpty().WithMessage("El Id de la familia no puede estar vacio");
    }
}

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
public record BeneficiaryRequest(
    string FirstName, string LastName, string Type, string Gender, DateOnly Birthday,
    string Dni, string? Comments, string? Likes, ClothesRequest? Clothes,
    EducationRequest? Education, HealthRequest? Health, JobRequest? Job
);

internal class BeneficiaryRequestValidator : Validator<BeneficiaryRequest>
{
    public BeneficiaryRequestValidator()
    {
        RuleFor(t => t.FirstName)
            .MaximumLength(100).WithMessage("El nombre debe tener menos de 100 caracteres")
            .NotEmpty().WithMessage("El nombre no puede estar vacio");
        
        RuleFor(t => t.LastName)
            .MaximumLength(100).WithMessage("El apellido debe tener menos de 100 caracteres")
            .NotEmpty().WithMessage("El apellido no puede estar vacio");

        RuleFor(t => t.Type)
            .IsEnumName(typeof(BeneficiaryType)).WithMessage("El tipo de beneficiario no es valido")
            .NotEmpty().WithMessage("El tipo de beneficiario no puede estar vacio");

        RuleFor(t => t.Gender)
            .IsEnumName(typeof(BeneficiaryGender)).WithMessage("El genero del beneficiario no es valido")
            .NotEmpty().WithMessage("El genero no puede estar vacio");

        RuleFor(t => t.Birthday)
            .Must(t => t.ToDateTime(TimeOnly.MinValue) < DateTime.Now).WithMessage("La fecha de nacimiento debe ser en el pasado")
            .NotEmpty().WithMessage("El cumpleaños no puede esar vacio");

        RuleFor(t => t.Dni)
            .Matches(@"^\d{1,2}(\.\d{3}\.\d{3}|\d{6})$").WithMessage("El DNI no es valido")
            .NotEmpty().WithMessage("El DNI no puede estar vacio")
            .MaximumLength(12).WithMessage("El DNI no puede tener mas de 12 caracteres");

        RuleFor(t => t.Comments)
            .MaximumLength(1000).WithMessage("Los comentarios no pueden tener mas de 1000 caracteres");
        
        RuleFor(t=> t.Likes)
            .MaximumLength(1000).WithMessage("Los gustos no pueden tener mas de 1000 caracteres");

        RuleFor(t => t.Clothes)
            .SetValidator(new ClothesRequestValidator()!);

        RuleFor(t => t.Education)
            .SetValidator(new EducationRequestValidator()!);

        RuleFor(t => t.Health)
            .SetValidator(new HealthRequestValidator()!);

        RuleFor(t => t.Job)
            .SetValidator(new JobRequestValidator()!);

    }
}


/// <param name="Shoes">Shoe size</param>
/// <param name="Shirt">Shirt size</param>
/// <param name="Pants">Pants size</param>
public record ClothesRequest(int? Shoes, string? Shirt, string? Pants);

internal class ClothesRequestValidator : Validator<ClothesRequest>
{
    public ClothesRequestValidator()
    {
        RuleFor(t => t.Pants)
            .MaximumLength(50).WithMessage("El tamaño de pantalones no puede tener mas de 50 caracteres");
        
        RuleFor(t => t.Shirt)
            .MaximumLength(50).WithMessage("El tamaño de remeras no puede tener mas de 50 caracteres");
        
        RuleFor(t => t.Shoes)
            .GreaterThan(10).WithMessage("El tamaño de calzado no puede ser menor a 10")
            .LessThan(50).WithMessage("El tamaño de calzado no puede ser mayor a 45");
    }
}

/// <param name="Year">In which year the student is in</param>
/// <param name="School">Which school does the student goes to</param>
/// <param name="TransportationMethod">How does the student get to school</param>
public record EducationRequest(string? Year, string? School, string? TransportationMethod);

internal class EducationRequestValidator : Validator<EducationRequest>
{
    public EducationRequestValidator()
    {
        RuleFor(t => t.Year)
            .IsEnumName(typeof(SchoolYear),false).WithMessage("El año escolar no es valido");

        RuleFor(t => t.School)
            .MaximumLength(100).WithMessage("El nombre de la escuela debe tener menos de 100 caracteres");

        RuleFor(t => t.TransportationMethod)
            .IsEnumName(typeof(TransportationMethod),false).WithMessage("El modo de transporte no es valido");
    }
}

/// <param name="HasCovidVaccine">Does the beneficiary have the covid vaccine</param>
/// <param name="HasMandatoryVaccines">Does the beneficiary have all the vaccines</param>
/// <param name="Observations">Additional health information</param>
public record HealthRequest(bool? HasCovidVaccine, bool? HasMandatoryVaccines, string? Observations);

internal class HealthRequestValidator : Validator<HealthRequest>
{
    public HealthRequestValidator()
    {
        RuleFor(t => t.Observations)
            .MaximumLength(1000).WithMessage("Las observaciones de salud no pueden tener mas de 1000 caracteres");
    }
}

/// <param name="Title">Job title</param>
public record JobRequest(string Title);
internal class JobRequestValidator : Validator<JobRequest>
{
    public JobRequestValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("El titulo del trabajo no debe estar vacio")
            .MaximumLength(100).WithMessage("El titulo de trabajo debe tener menos de 100 caracteres");
    }
}
