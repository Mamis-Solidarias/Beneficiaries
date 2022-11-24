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
}