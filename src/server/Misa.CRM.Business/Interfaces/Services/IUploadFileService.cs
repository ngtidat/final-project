using Microsoft.AspNetCore.Http;

namespace Misa.CRM.Business.Interfaces.Services;

public interface IUploadFileService
{
    public string UploadFile(IFormFile file);
}
