namespace Misa.CRM.Business.MisaAttributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class MisaPrimaryKeyAttribute : Attribute
{
    public string PrimaryKeyName { get; }

    public MisaPrimaryKeyAttribute(string primaryKeyName)
    {
        PrimaryKeyName = primaryKeyName;
    }
}
