namespace Misa.CRM.Business.MisaAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class MisaColumnAttribute : Attribute
{
    public string ColumnName { get; set; }

    public MisaColumnAttribute(string columnName)
    {
        ColumnName = columnName;
    }
}