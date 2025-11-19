using Microsoft.AspNetCore.Mvc;
using Misa.CRM.Api.Common.Responses;
using Misa.CRM.Business.Dtos.Customer;
using Misa.CRM.Business.Interfaces.Services;

namespace Misa.CRM.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
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
        return Ok(customer);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("with-type")]
    public IActionResult GetCustomersWithType()
    {
        var customers = _customerService.GetCustomersWithType();
        return Ok(customers);
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
        return Ok(result);
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
    public IActionResult Update(string id, [FromBody] CustomerCreateUpdateDto customerCreateUpdateDto)
    {
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

    // [HttpPost("delete-multiple")]
    // public IActionResult DeleteMulti(IEnumerable<CustomerDto> customerDto)
    // {
    //     var result = _customerService.Delete(customerDto);
    //     var response = new ApiResponse<int>(data: result);
    //     return Ok(response);
    // }
}
