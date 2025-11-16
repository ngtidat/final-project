namespace Misa.CRM.Business.MisaAttributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public abstract class BaseValidationAttribute : Attribute
{
    public string ErrorMessage { get; set; } = "Invalid value";

    public abstract bool IsValid(object? value);
}
