using Misa.CRM.Business.MisaAttributes;

namespace Misa.CRM.Business.Entities.Common;

[MisaTable("customer")]
public class Customer : BaseEntity
{
    [MisaPrimaryKey("customer_id")]
    [MisaColumn("customer_id")]
    public required string CustomerId { get; set; }

    [MisaColumn("customer_name")]
    [MisaRequired(ErrorMessage = "Customer name is required")]
    [MisaMaxLength(128, "Fullname <= 128 characters")]
    public required string CustomerName { get; set; }

    [MisaColumn("customer_address")]
    [MisaMaxLength(255, "Address <= 255 characters")]
    public string? CustomerAddress { get; set; }

    [MisaColumn("customer_phone")]
    [MisaPhone("Phone must be from 10 to 11 digits")]
    [MisaUnique("Phone number already exists")]
    public string? CustomerPhone { get; set; }

    [MisaColumn("customer_email")]
    [MisaEmail("Email is invalid format")]
    [MisaUnique("Email already exists")]
    public string? CustomerEmail { get; set; }

    [MisaColumn("customer_tax_code")]
    public string? CustomerTaxCode { get; set; }

    [MisaColumn("customer_type_id")]
    public Guid? CustomerTypeId { get; set; }

    public CustomerType? CustomerType { get; set; }

    [MisaColumn("customer_industry")]
    public string? CustomerIndustry { get; set; }

    [MisaColumn("gender")]
    public byte? Gender { get; set; }

    [MisaColumn("other_phone_number")]
    public string? OtherPhoneNumber { get; set; }

    [MisaColumn("last_purchase_date")]
    public DateTime? LastPurchaseDate { get; set; }

    [MisaColumn("purchase_items")]
    public string? PurchaseItems { get; set; }

    [MisaColumn("purchase_item_name")]
    public string? PurchaseItemName { get; set; }

    [MisaColumn("shipping_address")]
    public string? ShippingAddress { get; set; }
}

