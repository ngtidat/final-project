using Microsoft.AspNetCore.Http;

namespace Misa.CRM.Business.Interfaces.Services;

public interface IUploadFileService
{
    /// <summary>
    /// Tải và lưu ảnh trên server (wwwroot)
    /// </summary>
    /// <param name="file"></param>
    /// <returns>Đường dẫn của ảnh</returns>
    public string UploadFile(IFormFile file);
}
