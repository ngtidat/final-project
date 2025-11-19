namespace Misa.CRM.Business.MisaAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class MisaMaxLengthAttribute : Attribute
{
    public int MaxLength { get; }
    public string ErrorMessage { get; }

    public MisaMaxLengthAttribute(int maxLength, string errorMessage)
    {
        MaxLength = maxLength;
        ErrorMessage = errorMessage;
    }
}


