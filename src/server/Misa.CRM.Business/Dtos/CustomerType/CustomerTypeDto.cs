namespace Misa.CRM.Business.Dtos.CustomerType;

public class CustomerTypeDto : BaseDto
{
    public required Guid CustomerTypeId { get; set; }
    public required string CustomerTypeName { get; set; }
    public override string[] SearchableColumns => [];
}
