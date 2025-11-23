using Microsoft.AspNetCore.Http;
using Misa.CRM.Business.Dtos.CustomerType;
using Misa.CRM.Business.MisaAttributes;

namespace Misa.CRM.Business.Dtos.Customer;

public class CustomerDto : BaseDto
{
    [MisaRequired]
    public required string CustomerId { get; set; }

    public CustomerTypeDto? CustomerType { get; set; }
    
    public required string CustomerName { get; set; }

    public string? CustomerTaxCode { get; set; }

    public string? ShippingAddress { get; set; }

    public byte Gender { get; set; }

    public string? CustomerPhone { get; set; }

    public string? CustomerEmail { get; set; }

    public string? CustomerAddress { get; set; }

    public string? CustomerIndustry { get; set; }

    public string? OtherPhoneNumber { get; set; }

    public DateTime? LastPurchaseDate { get; set; }

    public string? PurchaseItems { get; set; }

    public string? PurchaseItemName { get; set; }  

    public IFormFile? Avatar { get; set; }

    public string? CustomerAvatar { get; set; }
}
