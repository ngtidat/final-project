using Microsoft.AspNetCore.Http;
using Misa.CRM.Business.MisaAttributes;

namespace Misa.CRM.Business.Dtos.Customer;

public class CustomerCreateUpdateDto
{
    [MisaRequired(ErrorMessage = "Customer name is required")]
    [MisaMaxLength(128, "Fullname <= 128 characters")]
    public required string CustomerName { get; set; }

    [MisaColumn("customer_address")]
    [MisaMaxLength(255, "Address <= 255 characters")]
    public string? CustomerAddress { get; set; }

    [MisaPhone("Phone must be from 10 to 11 digits")]
    [MisaUnique("Phone number already exists")]
    public string? CustomerPhone { get; set; }

    [MisaEmail("Email is invalid format")]
    [MisaUnique("Email already exists")]
    public string? CustomerEmail { get; set; }

    public string? CustomerTaxCode { get; set; }

    public Guid? CustomerTypeId { get; set; }

    public string? CustomerIndustry { get; set; }

    public byte? Gender { get; set; }

    public string? OtherPhoneNumber { get; set; }

    public DateTime? LastPurchaseDate { get; set; }

    public string? PurchaseItems { get; set; }

    public string? PurchaseItemName { get; set; }

    public string? ShippingAddress { get; set; }

    public IFormFile? Avatar { get; set; }

    public string? CustomerAvatar { get; set; }
}
