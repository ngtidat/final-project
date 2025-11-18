using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public IActionResult GetAll()
    {
        var customers = _customerService.GetAll();
        return Ok(customers);
    }

    [HttpGet("with-type")]
    public IActionResult GetCustomersWithType()
    {
        var customers = _customerService.GetCustomersWithType();
        return Ok(customers);
    }

    [HttpGet("search")]
    public IActionResult Search([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 100, [FromQuery] string? strSearch = null, [FromQuery] string? sortColumn = "c.created_at", [FromQuery] int sortDirection = 1)
    {
        var result = _customerService.Paginate(strSearch, pageIndex, pageSize, sortColumn, sortDirection);
        return Ok(result);
    }
}
