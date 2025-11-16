namespace Misa.CRM.Business.MisaAttributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class MisaRequiredAttribute : Attribute
{
    public string ErrorMessage { get; set; } = "This field is required";

    public MisaRequiredAttribute()
    {
    }

    public MisaRequiredAttribute(string errorMessage)
    {
        this.ErrorMessage = errorMessage;
    }
}
