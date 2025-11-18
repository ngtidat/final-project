namespace Misa.CRM.Business.Dtos;

public abstract class BaseDto : BaseInfoDto
{
    /// <summary>
    /// Các cột có thể search, định nghĩa ở DTO con
    /// </summary>
    public abstract string[]? SearchableColumns { get; }
}
