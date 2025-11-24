namespace Misa.CRM.Business.Dtos.Customer;

public class ChangeCustomerTypeDto
{
    public List<string> Ids { get; set; } = [];
    public Guid? CustomerTypeId { get; set; }
}
