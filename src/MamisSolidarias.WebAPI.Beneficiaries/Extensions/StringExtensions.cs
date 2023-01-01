using System.Globalization;
using PhoneNumbers;

namespace MamisSolidarias.WebAPI.Beneficiaries.Extensions;

internal static class StringExtensions
{
    public static string ParsePhoneNumber(this string text)
    {
        var phoneUtils = PhoneNumberUtil.GetInstance();
        var phoneNumber = phoneUtils.Parse(text, "AR");
        return phoneUtils.GetNumberType(phoneNumber) is PhoneNumberType.MOBILE 
            ? $"{phoneNumber.CountryCode}{phoneNumber.NationalNumber}" 
            : phoneNumber.NationalNumber.ToString();
    }

    public static string Capitalize(this string text)
    {
        // capitalize first letter of each word
        var textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(text);
    }
}