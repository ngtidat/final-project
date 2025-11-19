using System.Text.RegularExpressions;

namespace Misa.CRM.Business.MisaAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class MisaPhoneAttribute : Attribute
{
    public string ErrorMessage { get; }

    public MisaPhoneAttribute(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public bool IsValid(string? phone)
    {
        if (string.IsNullOrWhiteSpace(phone)) return true;

        return Regex.IsMatch(phone, @"^\d{10,11}$");
    }
}
