namespace MamisSolidarias.WebAPI.Beneficiaries.Extensions;

internal static class EnumExtensions
{
    public static T? Parse<T>(this string? value, bool ignoreCase = true) where T : struct
    {
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        return Enum.Parse<T>(value, ignoreCase);
    }
}