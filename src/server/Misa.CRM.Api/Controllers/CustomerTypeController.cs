using Microsoft.AspNetCore.Mvc;
using Misa.CRM.Api.Common.Responses;
using Misa.CRM.Business.Dtos.CustomerType;
using Misa.CRM.Business.Interfaces.Services;

namespace Misa.CRM.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerTypeController : ControllerBase
{
    private readonly ICustomerTypeService _service;

    public CustomerTypeController(ICustomerTypeService service)
    {
        _service = service;
    }

    /// <summary>
    /// Lấy ra danh sách của loại khách hàng
    /// </summary>
    /// <returns>Danh sách loại khách hàng</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _service.GetAll();
        var response = new ApiResponse<IEnumerable<CustomerTypeDto>>(
            data: result
        );
        return Ok(response);
    }
}
