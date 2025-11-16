namespace Misa.CRM.Business.MisaAttributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class MisaColumnOverrideAttribute : Attribute
{
    public string PropertyName { get; }
    public string ColumnName { get; }

    public MisaColumnOverrideAttribute(string propertyName, string columnName)
    {
        PropertyName = propertyName;
        ColumnName = columnName;
    }
}

