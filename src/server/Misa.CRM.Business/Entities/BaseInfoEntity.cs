using Misa.CRM.Business.MisaAttributes;

namespace Misa.CRM.Business.Entities;

public class BaseInfoEntity
{
    [MisaColumn("created_at")]
    public DateTime CreatedAt { get; set; }

    [MisaColumn("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [MisaColumn("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [MisaColumn("is_deleted")]
    public bool IsDeleted { get; set; }
}
