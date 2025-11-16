using Misa.CRM.Business.MisaAttributes;

namespace Misa.CRM.Business.Entities.Common;

[MisaTable("user")]
[MisaColumnOverride("Id", "user_id")]
public class User : BaseEntity
{
    [MisaColumn("user_name")]
    [MisaRequired("User name is required")]
    public string Name { get; set; } = string.Empty;

    [MisaColumn("user_email")]
    public string Email { get; set; } = string.Empty;

    [MisaColumn("password")]
    public string Password { get; set; } = string.Empty;
}
