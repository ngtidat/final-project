namespace Misa.CRM.Business.Dtos;

public class BaseInfoDto
{
    public DateTime CreatedAt { get; set; }

    public Guid? CreatedById { get; set; }

    public Guid? UpdatedById { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedById { get; set; }

    public bool IsDeleted { get; set; }
}
