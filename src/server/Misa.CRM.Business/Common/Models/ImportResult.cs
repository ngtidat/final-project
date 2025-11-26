namespace Misa.CRM.Business.Common.Models;

public class ImportResult
{
    public int Total { get; set; }
    public int Success { get; set; }
    public int Failed { get; set; }
    public List<ImportErrorRow> Errors { get; set; } = new();
}

public class ImportErrorRow
{
    public int RowIndex { get; set; }
    public object? RowData { get; set; }
    public string Error { get; set; } = "";
}
