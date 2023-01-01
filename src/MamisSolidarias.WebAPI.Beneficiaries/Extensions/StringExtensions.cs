using System.Globalization;
using PhoneNumbers;

namespace MamisSolidarias.WebAPI.Beneficiaries.Extensions;

internal static class StringExtensions
{
    public static string ParsePhoneNumber(this string text)
    {
        var util = PhoneNumberUtil.GetInstance();
        var phoneNumber = util.Parse(text, "AR");
        return util.GetNumberType(phoneNumber) is PhoneNumberType.MOBILE 
            ? $"+{phoneNumber.CountryCode}{phoneNumber.NationalNumber}" 
            : $"+{phoneNumber.CountryCode}9{phoneNumber.NationalNumber}";
    }

    public static string Capitalize(this string text)
    {
        var textInfo = CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(text);
    }
}