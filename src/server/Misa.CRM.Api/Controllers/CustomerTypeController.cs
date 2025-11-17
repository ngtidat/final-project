using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _service.GetAll();
        return Ok(result);
    }
}
