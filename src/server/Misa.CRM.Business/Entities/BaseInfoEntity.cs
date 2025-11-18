using Misa.CRM.Business.Entities.Common;
using Misa.CRM.Business.MisaAttributes;

namespace Misa.CRM.Business.Entities;

public class BaseInfoEntity
{
    [MisaColumn("created_at")]
    public DateTime CreatedAt { get; set; }

    [MisaColumn("created_by_id")]
    public Guid? CreatedById { get; set; }

    [MisaColumn("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [MisaColumn("updated_by_id")]
    public Guid? UpdatedById { get; set; }

    [MisaColumn("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [MisaColumn("deleted_by_id")]
    public Guid? DeletedById { get; set; }

    [MisaColumn("is_deleted")]
    public bool IsDeleted { get; set; }
}
