using Misa.CRM.Business.MisaAttributes;

namespace Misa.CRM.Business.Entities.Common;

[MisaTable("customer")]
public class Customer : BaseInfoEntity
{
    [MisaColumn("customer_id")]
    [MisaRequired(ErrorMessage = "Customer ID is required")]
    public required string CustomerId { get; set; }

    [MisaColumn("customer_name")]
    [MisaRequired(ErrorMessage = "Customer name is required")]
    public required string CustomerName { get; set; }

    [MisaColumn("customer_address")]
    public string? CustomerAddress { get; set; }                        

    [MisaColumn("customer_phone")]
    public string? CustomerPhone { get; set; }           

    [MisaColumn("customer_email")]
    public string? CustomerEmail { get; set; }                          

    [MisaColumn("customer_tax_code")]
    public string? CustomerTaxCode { get; set; }

    [MisaColumn("customer_type_id")]                       
    public Guid? CustomerTypeId { get; set; } 

    public CustomerType? CustomerType { get; set; }                        

    [MisaColumn("customer_abbreviation")]
    public string? CustomerAbbreviation { get; set; }                   

    [MisaColumn("business_field")]
    public string? BusinessField { get; set; }

    [MisaColumn("customer_source")]                           
    public int? CustomerSource { get; set; }                            

    [MisaColumn("customer_industry")]
    public string? CustomerIndustry { get; set; }                        

    [MisaColumn("gender")]
    public byte? Gender { get; set; }                                    

    [MisaColumn("customer_zalo")]
    public string? CustomerZalo { get; set; }                            

    [MisaColumn("passport_number")]
    public string? PassportNumber { get; set; }                          

    [MisaColumn("customer_category")]
    public int? CustomerCategory { get; set; }                           

    [MisaColumn("customer_identity_number")]
    public string? CustomerIdentityNumber { get; set; }                  

    [MisaColumn("contact_channel")]
    public int? ContactChannel { get; set; }  

    [MisaColumn("budget_unit_code")]                            
    public string? BudgetUnitCode { get; set; }                          

    [MisaColumn("managing_unit")]
    public int? ManagingUnit { get; set; }                                

    [MisaColumn("other_phone_number")]
    public string? OtherPhoneNumber { get; set; }                        

    [MisaColumn("customer_preferred_name")]
    public string? CustomerPreferredName { get; set; }                    

    [MisaColumn("list_18")]
    public int? List18 { get; set; }   

    [MisaColumn("single_entry_settlement")]                                   
    public string? SingleEntrySettlement { get; set; }                   

    [MisaColumn("multi_line_entry")]
    public string? MultiLineEntry { get; set; }     

    [MisaColumn("last_purchase_date")]
    public DateTime? LastPurchaseDate { get; set; }

    [MisaColumn("purchase_items")]
    public string? PurchaseItems { get; set; }

    [MisaColumn("purchase_item_name")]
    public string? PurchaseItemName { get; set; } 

    [MisaColumn("shipping_address")]
    public string? ShippingAddress { get; set; }                      
}
