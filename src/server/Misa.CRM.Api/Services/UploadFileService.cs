using Misa.CRM.Business.Interfaces.Services;

namespace Misa.CRM.Api.Services;

public class UploadFileService : IUploadFileService
{
    private readonly IWebHostEnvironment _env;

    public UploadFileService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public string UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new FileNotFoundException("File không hợp lệ.");

        string uploadFolder = Path.Combine(_env.WebRootPath, "uploads/images");

        if (!Directory.Exists(uploadFolder))
            Directory.CreateDirectory(uploadFolder);

        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        string filePath = Path.Combine(uploadFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        // Trả về URL public
        return $"http://localhost:5078/uploads/images/{fileName}";
    }
}
