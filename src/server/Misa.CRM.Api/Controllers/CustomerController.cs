using Microsoft.AspNetCore.Mvc;
using Misa.CRM.Api.Common.Responses;
using Misa.CRM.Business.Common.Models;
using Misa.CRM.Business.Dtos.Customer;
using Misa.CRM.Business.Interfaces.Services;

namespace Misa.CRM.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    private readonly IUploadFileService _uploadFileService;

    public CustomerController(ICustomerService customerService, IUploadFileService uploadFileService)
    {
        _customerService = customerService;
        _uploadFileService = uploadFileService;
    }

    /// <summary>
    /// Lấy ra tất cả khách hàng không bị xóa
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        var customers = _customerService.GetAll();

        var response = new ApiResponse<IEnumerable<CustomerDto>>(
            data: customers,
            meta: new MetaData
            {
                Page = 1,
                PageSize = 100,
                Total = customers.Count()
            }
        );
        return Ok(response);
    }

    /// <summary>
    /// Lấy thông tin khách hàng theo mã KH
    /// </summary>
    /// <param name="id">Mã KH</param>
    /// <returns>Trả về các thông tin khách hàng cần thiết</returns> <summary>
    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var customer = _customerService.GetById(id);
        var response = new ApiResponse<CustomerDto>(
            data: customer
        );
        return Ok(response);
    }

    /// <summary>
    /// Tự động tạo mã KH mới nhất và trả về cho client
    /// </summary>
    /// <returns>Mã KH mới nhất</returns>
    [HttpGet("get-new-id")]
    public IActionResult GetNewCustomerId()
    {
        var newCustomerId = _customerService.GetNewCustomerId();
        var response = new ApiResponse<string>(
            data: newCustomerId
        );
        return Ok(response);
    }

    /// <summary>
    /// Lấy ra danh sách khách hàng theo thông tin tìm kiếm và phân trang
    /// </summary>
    /// <param name="pageIndex">Trang hiện tại</param>
    /// <param name="pageSize">Số lượng bản ghi của một trang</param>
    /// <param name="strSearch">Chuỗi tìm kiếm</param>
    /// <param name="sortColumn">Cột được sắp xếp</param>
    /// <param name="sortDirection">Hướng sắp xếp</param>
    /// <returns></returns>
    [HttpGet("search")]
    public IActionResult Search(int pageIndex = 1, int pageSize = 100, string? strSearch = null, string? sortColumn = "c.created_at", int sortDirection = 1, Guid? customerTypeId = null)
    {
        var result = _customerService.Paginate(strSearch, pageIndex, pageSize, sortColumn, sortDirection, customerTypeId);
        var response = new ApiResponse<PaginatedResult<CustomerDto>>(
            data: result
        );
        return Ok(response);
    }

    /// <summary>
    /// Thêm mới khách hàng
    /// </summary>
    /// <param name="customerCreateUpdateDto">Thông tin người dùng từ form nhập</param>
    /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
    [HttpPost("create")]
    public IActionResult Create([FromForm] CustomerCreateUpdateDto customerCreateUpdateDto)
    {
        string? avatarUrl = null;
        if (customerCreateUpdateDto.Avatar != null)
        {
            avatarUrl = _uploadFileService.UploadFile(customerCreateUpdateDto.Avatar);
        }

        customerCreateUpdateDto.CustomerAvatar = avatarUrl;

        var result = _customerService.Add(customerCreateUpdateDto);
        var response = new ApiResponse<int>(data: result);
        return Ok(response);
    }

    /// <summary>
    /// Cập nhật khách hàng
    /// </summary>
    /// <param name="customerDto">Thông tin từ form nhập</param>
    /// <returns>Số bản ghi ảnh hưởng</returns>
    [HttpPut("update")]
    public IActionResult Update(string id, [FromForm] CustomerCreateUpdateDto customerCreateUpdateDto)
    {
        if (customerCreateUpdateDto.Avatar != null)
        {
            string avatarUrl = _uploadFileService.UploadFile(customerCreateUpdateDto.Avatar);
            customerCreateUpdateDto.CustomerAvatar = avatarUrl;
        }

        var result = _customerService.Update(id, customerCreateUpdateDto);
        var response = new ApiResponse<int>(data: result);
        return Ok(response);
    }

    /// <summary>
    /// Xóa khách hàng theo mã KH, mặc định là xóa mềm
    /// </summary>
    /// <param name="id">Mã KH</param>
    /// <returns>Số bản ghi ảnh hưởng</returns>
    [HttpPost("delete")]
    public IActionResult Delete(string id)
    {
        var result = _customerService.Delete(id);
        var response = new ApiResponse<int>(data: result);
        return Ok(response);
    }

    /// <summary>
    /// Xóa nhiều khách hàng theo danh sách mã KH
    /// Mặc định là xóa mềm
    /// </summary>
    /// <param name="ids">Danh sách mã KH</param>
    /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
    [HttpPost("delete-multiple")]
    public IActionResult DeleteMulti(IEnumerable<string> ids)
    {
        var result = _customerService.Delete(ids);
        var response = new ApiResponse<int>(data: result);
        return Ok(response);
    }

    /// <summary>
    /// Nhập dữ liệu khách hàng bằng file csv
    /// </summary>
    /// <param name="file">file thông tin khách hàng</param>
    /// <returns>
    /// Kết quả import:
    /// Số lượng thành công
    /// Số lượng thất bại
    /// Thông tin các hàng thất bại
    /// </returns>
    [HttpPost("import")]
    public IActionResult Import(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is empty");

        var result = _customerService.Import(file);
        var response = new ApiResponse<ImportResult>(data: result);
        return Ok(response);
    }

    /// <summary>
    /// Kiểm tra email đã tồn tại chưa?
    /// </summary>
    /// <param name="email">Email cần kiểm tra</param>
    /// <returns>1 nếu tồn tại hoặc ngược lại</returns>
    [HttpGet("check-exist-email")]
    public IActionResult IsUniqueEmail(string email)
    {
        var result = _customerService.CheckEmailUnique(email);
        var response = new ApiResponse<int>
        (
            data: result
        );
        return Ok(response);
    }

    /// <summary>
    /// Kiểm tra số điện thoại đã tòn tại chưa?
    /// </summary>
    /// <param name="phone">Số điện thoại cần kiểm tra</param>
    /// <returns>1 nếu tồn tại hoặc ngược lại</returns>
    [HttpGet("check-exist-phone")]
    public IActionResult IsUniquePhone(string phone)
    {
        var result = _customerService.CheckPhoneUnique(phone);
        var response = new ApiResponse<int>
        (
            data: result
        );
        return Ok(response);
    }

    /// <summary>
    /// Thay đổi loại khách hàng
    /// </summary>
    /// <param name="changeCustomerTypeDto">Dánh sách khách hàng và mã loại KH</param>
    /// <returns>Số bản ghi thay đổi</returns>
    [HttpPut("change-customer-type")]
    public IActionResult ChangeCustomerType([FromBody] ChangeCustomerTypeDto changeCustomerTypeDto)
    {
        var result = _customerService.ChangeCustomerType(changeCustomerTypeDto.Ids, changeCustomerTypeDto.CustomerTypeId);
        var response = new ApiResponse<int>
        (
            data: result
        );
        return Ok(response);
    }
}
