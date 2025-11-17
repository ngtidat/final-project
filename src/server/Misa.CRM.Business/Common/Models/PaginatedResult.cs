namespace Misa.CRM.Business.Common.Models;

public class PaginatedResult<T> where T : class 
{
    public IEnumerable<T> Items { get; set; } = [];

    public int TotalRecords { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}
