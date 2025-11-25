using Microsoft.AspNetCore.Http;

using Misa.CRM.Business.Common.Models;
using Misa.CRM.Business.Entities.Common;

namespace Misa.CRM.Business.Interfaces.Repositories;

public interface ICustomerRepository: IBaseRepository<Customer>
{
    /// <summary>
    /// Danh sách KH có thông tin loại KH
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Customer> GetCustomersWithTypeAsync();

    /// <summary>
    /// Tìm kiếm và phân trang
    /// </summary>
    /// <param name="search">Chuỗi tìm kiếm</param>
    /// <param name="pageIndex">Trang hiện tại</param>
    /// <param name="pageSize">Số bản ghi 1 trang</param>
    /// <param name="sortColumn">Trường sắp xếp</param>
    /// <param name="sortDirection">Cột sắp xếp</param>
    /// <returns>
    /// Kết quả phân trang:
    /// Trang hiện tại
    /// Số bản ghi một trang
    /// Tổng số bản ghi
    /// Danh sách bản ghi
    /// </returns> <summary>
    public PaginatedResult<Customer> SearchAndPaginate(string? search, int pageIndex, int pageSize, string? sortColumn, int sortDirection, Guid? customerTypeId);

    /// <summary>
    /// Nhập dữ liệu từ file csv vào DB
    /// </summary>
    /// <param name="customers">Danh sách khách hàng</param>
    /// <returns>Kết quả import</returns>
    public ImportResult Import(List<Customer> customers);

    /// <summary>
    /// Tạo và lấy mã KH mới nhất
    /// </summary>
    /// <returns>Mã KH mới nhất</returns>
    public string GetNewCustomerId();

    /// <summary>
    /// Kiểm tra email đã tồn tại chưa
    /// </summary>
    /// <param name="email"></param>
    /// <returns>1-tồn tại và 0-chưa tồn tại</returns>
    public int CheckEmailUnique(string email);

    /// <summary>
    /// Kiểm tra phone đã tồn tại chưa
    /// </summary>
    /// <param name="phone"></param>
    /// <returns>1-tồn tại và 0-chưa tồn tại</returns>
    public int CheckPhoneUnique(string phone);

    /// <summary>
    /// Thay đổi loại khách hàng
    /// </summary>
    /// <param name="ids">Danh sách khách hàng thay đổi</param>
    /// <param name="id">Mã loại khách hàng</param>
    /// <returns>Số bản ghi thay đổi</returns>
    public int ChangeCustomerType(List<string> ids, Guid? customerTypeId);
}
