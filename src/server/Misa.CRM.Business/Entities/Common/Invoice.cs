using Misa.CRM.Business.MisaAttributes;

namespace Misa.CRM.Business.Entities.Common;

[MisaTable("invoice")]
[MisaColumnOverride("Id", "invoice_id")]
public class Invoice : BaseEntity
{
    [MisaColumn("customer_id")]
    public string? CustomerId { get; set; }

    public Customer? Customer { get; set; }

    [MisaColumn("shipping_address")]
    public string? ShippingAddress { get; set; }

    [MisaColumn("invoice_date")]
    public DateTime? InvoiceDate { get; set; }
}
