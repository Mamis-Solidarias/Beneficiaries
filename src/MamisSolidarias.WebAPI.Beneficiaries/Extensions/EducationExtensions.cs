using MamisSolidarias.Infrastructure.Beneficiaries.Models;

namespace MamisSolidarias.WebAPI.Beneficiaries.Extensions;

internal static class EducationExtensions
{
    public static SchoolCycle? ToSchoolCycle(this SchoolYear? year) =>
        year switch
        {
            SchoolYear.PreSchool3 => SchoolCycle.PreSchool,
            SchoolYear.PreSchool4 => SchoolCycle.PreSchool,
            SchoolYear.PreSchool5 => SchoolCycle.PreSchool,
            SchoolYear.PrimarySchool1 => SchoolCycle.PrimarySchool,
            SchoolYear.PrimarySchool2 => SchoolCycle.PrimarySchool,
            SchoolYear.PrimarySchool3 => SchoolCycle.PrimarySchool,
            SchoolYear.MiddleSchool4 => SchoolCycle.MiddleSchool,
            SchoolYear.MiddleSchool5 => SchoolCycle.MiddleSchool,
            SchoolYear.MiddleSchool6 => SchoolCycle.MiddleSchool,
            SchoolYear.MiddleSchool7 => SchoolCycle.MiddleSchool,
            SchoolYear.HighSchool1 => SchoolCycle.HighSchool,
            SchoolYear.HighSchool2 => SchoolCycle.HighSchool,
            SchoolYear.HighSchool3 => SchoolCycle.HighSchool,
            SchoolYear.HighSchool4 => SchoolCycle.HighSchool,
            SchoolYear.HighSchool5 => SchoolCycle.HighSchool,
            SchoolYear.HighSchool6 => SchoolCycle.HighSchool,
            null => null,
            _ => throw new ArgumentOutOfRangeException(nameof(year), year, null)
        };
}