namespace Misa.CRM.Business.MisaAttributes;

[AttributeUsage(AttributeTargets.Class)]
public class MisaTableAttribute : Attribute
{
    public string TableName { get; set; }

    public MisaTableAttribute(string tableName)
    {
        TableName = tableName;
    }
}
