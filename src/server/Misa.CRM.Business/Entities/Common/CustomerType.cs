using Misa.CRM.Business.MisaAttributes;

namespace Misa.CRM.Business.Entities.Common;

[MisaTable("customer_type")]
// [MisaColumnOverride("Id", "customer_type_id")]
public class CustomerType : BaseInfoEntity
{
    [MisaColumn("customer_type_id")]
    public Guid CustomerTypeId { get; set; }

    [MisaColumn("customer_type_name")]
    [MisaRequired("Customer type name is required")]
    public required string CustomerTypeName { get; set; }
}
