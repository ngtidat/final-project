namespace Misa.CRM.Business.MisaAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class MisaUniqueAttribute : Attribute
{
    public string ErrorMessage { get; }

    public MisaUniqueAttribute(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}
