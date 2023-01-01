using System.Text.RegularExpressions;
using FastEndpoints;
using FluentValidation;
using MamisSolidarias.Infrastructure.Beneficiaries.Models;
using Microsoft.AspNetCore.Mvc;

namespace MamisSolidarias.WebAPI.Beneficiaries.Endpoints.Beneficiaries.Id.PATCH;

public class Request
{
    /// <summary>
    /// If of the beneficiary to update
    /// </summary>
    [FromRoute] public int Id { get; set; }
    
    /// <summary>First name of the beneficiary</summary>
    public string? FirstName { get; init; }

    /// <summary>Last name of the beneficiary</summary>
    public string? LastName { get; init; }

    /// <summary>Type of the beneficiary</summary>
    public string? Type { get; init; }

    /// <summary>Gender of the beneficiary</summary>
    public string? Gender { get; init; }

    /// <summary>Birthday of the beneficiary (date only)</summary>
    public DateOnly? Birthday { get; init; }

    /// <summary>DNI of the beneficiary. It can have dots in between the numbers or not</summary>
    public string? Dni { get; init; }

    /// <summary>Additional comments</summary>
    public string? Comments { get; init; }

    /// <summary>What the beneficiary likes</summary>
    public string? Likes { get; init; }

    /// <summary>The size of the beneficiary's clothes</summary>
    public ClothesRequest? Clothes { get; init; }

    /// <summary>Where the beneficiary studies</summary>
    public EducationRequest? Education { get; init; }

    /// <summary>Health information for the beneficiary</summary>
    public HealthRequest? Health { get; init; }

    /// <summary>Job information for parents or tutors</summary>
    public JobRequest? Job { get; init; }
}

internal class RequestValidator : Validator<Request>
{
    private readonly Regex _dniPattern = new(@"^[1-9]\d{0,2}(\.?\d{3}){2}$",
        RegexOptions.Compiled | RegexOptions.Multiline);
    public RequestValidator()
    {
        
        RuleFor(t => t.Id)
            .NotEmpty().WithMessage("Se debe indicar el Id de un beneficiario")
            .GreaterThan(0).WithMessage("El Id debe ser valido");
        
        RuleFor(t => t.FirstName)
            .MaximumLength(100).WithMessage("El nombre debe tener menos de 100 caracteres")
            .When(t => t.FirstName is not null);

        RuleFor(t => t.LastName)
            .MaximumLength(100).WithMessage("El apellido debe tener menos de 100 caracteres")
            .When(t => t.LastName is not null);

        RuleFor(t => t.Type)
            .IsEnumName(typeof(BeneficiaryType)).WithMessage("El tipo de beneficiario no es valido")
            .When(t => t.Type is not null);

        RuleFor(t => t.Gender)
            .IsEnumName(typeof(BeneficiaryGender)).WithMessage("El genero del beneficiario no es valido")
            .When(t => t.Gender is not null);

        RuleFor(t => t.Birthday)
            .Must(t => t is null || t.Value.ToDateTime(TimeOnly.MinValue) < DateTime.Now)
            .When(t => t.Birthday is not null)
            .WithMessage("La fecha de nacimiento debe ser en el pasado")
            .When(t => t.Birthday is not null);

        RuleFor(t => t.Dni)
            .Matches(@"^\d{1,2}(\.\d{3}\.\d{3}|\d{6})$").WithMessage("El DNI no es valido")
            .MaximumLength(12).WithMessage("El DNI no puede tener mas de 12 caracteres")
            .When(t => t.Dni is not null)
            .Must(t => _dniPattern.IsMatch(t)).WithMessage("El DNI no es valido.")
            .When(t => t.Dni is not null);

        RuleFor(t => t.Comments)
            .MaximumLength(1000).WithMessage("Los comentarios no pueden tener mas de 1000 caracteres")
            .When(t => t.Comments is not null);
        
        RuleFor(t=> t.Likes)
            .MaximumLength(1000).WithMessage("Los gustos no pueden tener mas de 1000 caracteres")
            .When(t => t.Likes is not null);

        RuleFor(t => t.Clothes)
            .SetValidator(new ClothesRequestValidator()!)
            .When(t => t.Clothes is not null);

        RuleFor(t => t.Education)
            .SetValidator(new EducationRequestValidator()!)
            .When(t => t.Education is not null);

        RuleFor(t => t.Health)
            .SetValidator(new HealthRequestValidator()!)
            .When(t => t.Health is not null);

        RuleFor(t => t.Job)
            .SetValidator(new JobRequestValidator()!)
            .When(t => t.Job is not null);
       
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
            .GreaterThan(10).WithMessage("El tamaño de calzado debe ser mayor a 10")
            .LessThan(50).WithMessage("El tamaño de calzado debe ser menor a 50");
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
            .MaximumLength(50).WithMessage("El año escolar puede tener como maximo 50 caracteres");

        RuleFor(t => t.School)
            .MaximumLength(100).WithMessage("El nombre de la escuela debe tener menos de 100 caracteres");

        RuleFor(t => t.TransportationMethod)
            .IsEnumName(typeof(TransportationMethod)).WithMessage("El modo de transporte no es valido");
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
public record JobRequest(string? Title);
internal class JobRequestValidator : Validator<JobRequest>
{
    public JobRequestValidator()
    {
        RuleFor(t => t.Title)
            .MaximumLength(100).WithMessage("El titulo de trabajo debe tener menos de 100 caracteres");
    }
}
