using Misa.CRM.Business.Dtos.Customer;
using Misa.CRM.Business.Entities.Common;

namespace Misa.CRM.Business.Interfaces.Services;

public interface ICustomerService : IBaseService<Customer, CustomerDto>
{
    public IEnumerable<CustomerDto> GetCustomersWithType();
}
