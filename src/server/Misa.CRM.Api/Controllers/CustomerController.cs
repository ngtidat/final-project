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
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("with-type")]
    public IActionResult GetCustomersWithType()
    {
        var customers = _customerService.GetCustomersWithType();
        var response = new ApiResponse<IEnumerable<CustomerDto>>(
            data: customers
        );
        return Ok(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="strSearch"></param>
    /// <param name="sortColumn"></param>
    /// <param name="sortDirection"></param>
    /// <returns></returns>
    [HttpGet("search")]
    public IActionResult Search(int pageIndex = 1, int pageSize = 100, string? strSearch = null, string? sortColumn = "c.created_at", int sortDirection = 1)
    {
        var result = _customerService.Paginate(strSearch, pageIndex, pageSize, sortColumn, sortDirection);
        var response = new ApiResponse<PaginatedResult<CustomerDto>>(
            data: result
        );
        return Ok(response);
    }

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
    /// 
    /// </summary>
    /// <param name="customerDto"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="customerDto"></param>
    /// <returns></returns>
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

    [HttpPost("delete")]
    public IActionResult Delete(string id)
    {
        var result = _customerService.Delete(id);
        var response = new ApiResponse<int>(data: result);
        return Ok(response);
    }

    [HttpPost("delete-multiple")]
    public IActionResult DeleteMulti(IEnumerable<string> ids)
    {
        var result = _customerService.Delete(ids);
        var response = new ApiResponse<int>(data: result);
        return Ok(response);
    }

    [HttpPost("import")]
    public IActionResult Import(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is empty");

        var result = _customerService.Import(file);
        var response = new ApiResponse<ImportResult>(data: result);
        return Ok(response);
    }

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
}
