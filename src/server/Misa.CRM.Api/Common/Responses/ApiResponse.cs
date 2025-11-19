namespace Misa.CRM.Api.Common.Responses;

public class ApiResponse<T>
{
    public T? Data { get; set; }

    public MetaData? Meta { get; set; }

    public ApiError? Error { get; set; }

    public ApiResponse(T data, MetaData? meta = null)
    {
        Data = data;
        Meta = meta;
        Error = null;
    }

    public ApiResponse(ApiError error)
    {
        Error = error;
    }
}
