namespace MamisSolidarias.WebAPI.Beneficiaries.Extensions;

internal static class EnumExtensions
{
    public static T? Parse<T>(this string? value, bool ignoreCase = true) where T : struct
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return Enum.TryParse<T>(value, ignoreCase, out var result) ? result : null;
    }
}