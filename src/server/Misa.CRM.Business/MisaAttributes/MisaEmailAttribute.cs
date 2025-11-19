using System.Text.RegularExpressions;

namespace Misa.CRM.Business.MisaAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class MisaEmailAttribute : Attribute
{
    public string ErrorMessage { get; }

    public MisaEmailAttribute(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public bool IsValid(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return true;

        return Regex.IsMatch(email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.IgnoreCase);
    }
}
