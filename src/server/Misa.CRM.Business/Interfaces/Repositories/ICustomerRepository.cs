using Misa.CRM.Business.Common.Models;
using Misa.CRM.Business.Entities.Common;

namespace Misa.CRM.Business.Interfaces.Repositories;

public interface ICustomerRepository: IBaseRepository<Customer>
{
    public IEnumerable<Customer> GetCustomersWithTypeAsync();

    public PaginatedResult<Customer> SearchAndPaginate(string? search, int pageIndex, int pageSize, string? sortColumn, int sortDirection);
}
