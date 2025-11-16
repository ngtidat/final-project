using Misa.CRM.Business.MisaAttributes;

namespace Misa.CRM.Business.Entities.Common;

[MisaTable("product")]
[MisaColumnOverride("Id", "product_id")]
public class Product : BaseEntity
{
    [MisaColumn("product_code")]
    public string? ProductCode { get; set; }

    [MisaColumn("product_name")]
    public required string ProductName { get; set; }

    [MisaColumn("unit_price")]
    public decimal? UnitPrice { get; set; }

    [MisaColumn("unit")]
    public decimal? Unit{ get; set; }
}
