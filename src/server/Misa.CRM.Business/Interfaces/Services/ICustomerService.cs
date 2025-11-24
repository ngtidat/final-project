using Microsoft.AspNetCore.Http;
using Misa.CRM.Business.Common.Models;
using Misa.CRM.Business.Dtos.Customer;
using Misa.CRM.Business.Entities.Common;

namespace Misa.CRM.Business.Interfaces.Services;

public interface ICustomerService : IBaseService<Customer, CustomerDto, CustomerCreateUpdateDto>
{
    /// <summary>
    /// Lấy danh sách khách hàng có loại khách hàng
    /// </summary>
    /// <returns>Danh sách khách hàng có thông tin loại khách hàng</returns>
    public IEnumerable<CustomerDto> GetCustomersWithType();

    /// <summary>
    /// Tìm kiếm và phân trang
    /// </summary>
    /// <param name="search">Chuỗi tìm kiếm</param>
    /// <param name="pageIndex">Trang hiện tại</param>
    /// <param name="pageSize">Số bản ghi một trang</param>
    /// <param name="sortColumn">Cột được sắp xếp</param>
    /// <param name="sortDirection">Hướng sắp xếp</param>
    /// <returns></returns>
    public PaginatedResult<CustomerDto> Paginate(string? search, int pageIndex, int pageSize, string? sortColumn, int sortDirection);

    /// <summary>
    /// Nhập dữ liệu từ file csv
    /// </summary>
    /// <param name="file">File dữ liệu</param>
    /// <returns>
    /// Kết quả import
    /// Số bản ghi thành công
    /// Số bản ghi thất bại
    /// Thông tin danh sách thất bại
    /// </returns>
    public ImportResult Import(IFormFile file);

    /// <summary>
    /// Tạo và lấy mã KH mới nhất
    /// </summary>
    /// <returns>Mã KH mới nhất</returns>
    public string GetNewCustomerId();

    /// <summary>
    /// Kiểm tra email đã tồn tại?
    /// </summary>
    /// <param name="email"></param>
    /// <returns>1-tồn tại hoặc 0-chưa tồn tại</returns>
    public int CheckEmailUnique(string email);

    /// <summary>
    /// Kiểm tra phone đã tồn tại?
    /// </summary>
    /// <param name="phone"></param>
    /// <returns>1-tồn tại hoặc 0-chưa tồn tại</returns>
    public int CheckPhoneUnique(string phone);

    /// <summary>
    /// Thay đổi loại khách hàng
    /// </summary>
    /// <param name="ids">Danh sách khách hàng thay đổi</param>
    /// <param name="id">Mã loại khách hàng</param>
    /// <returns>Số bản ghi thay đổi</returns>
    public int ChangeCustomerType(List<string> ids, Guid? customerTypeId);
}
