namespace Misa.CRM.Business.Common.Models;

public class PaginatedResult<T> where T : class
{
    public IEnumerable<T> Items { get; set; } = [];

    public int TotalRecords { get; set; }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public PaginatedResult(int pageIndex, int pageSize, int totalRecords, T[] items)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(pageIndex);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageSize);
        ArgumentOutOfRangeException.ThrowIfNegative(totalRecords);

        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        Items = items ?? [];
    }
}
