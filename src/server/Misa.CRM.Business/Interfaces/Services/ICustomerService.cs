using Microsoft.AspNetCore.Http;
using Misa.CRM.Business.Common.Models;
using Misa.CRM.Business.Dtos.Customer;
using Misa.CRM.Business.Entities.Common;

namespace Misa.CRM.Business.Interfaces.Services;

public interface ICustomerService : IBaseService<Customer, CustomerDto, CustomerCreateUpdateDto>
{
    public IEnumerable<CustomerDto> GetCustomersWithType();

    public PaginatedResult<CustomerDto> Paginate(string? search, int pageIndex, int pageSize, string? sortColumn, int sortDirection);

    public ImportResult Import(IFormFile file);
}
