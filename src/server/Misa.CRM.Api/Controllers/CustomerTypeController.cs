using Microsoft.AspNetCore.Mvc;
using Misa.CRM.Business.Interfaces.Repositories;

namespace Misa.CRM.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerTypeController : ControllerBase
{
    private readonly ICustomerTypeRepository _service;

    public CustomerTypeController(ICustomerTypeRepository service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _service.GetAll();
        return Ok(result);
    }

    [HttpGet("query")]
    public IActionResult GetQuery()
    {
        var result = _service.getQuery();
        return Ok(result);
    }
}
