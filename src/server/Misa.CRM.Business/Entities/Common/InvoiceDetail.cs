using Misa.CRM.Business.MisaAttributes;

namespace Misa.CRM.Business.Entities.Common;

[MisaTable("invoice_detail")]
public class InvoiceDetail : BaseInfoEntity
{
    [MisaColumn("invoice_id")]
    public required Guid InvoiceId { get; set; }

    public required Invoice Invoice { get; set; }

    [MisaColumn("product_id")]
    public required Guid ProductId { get; set; }

    public required Product Product { get; set; }
}
