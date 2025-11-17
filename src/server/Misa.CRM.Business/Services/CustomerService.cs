using AutoMapper;
using Misa.CRM.Business.Dtos.Customer;
using Misa.CRM.Business.Entities.Common;
using Misa.CRM.Business.Interfaces.Repositories;
using Misa.CRM.Business.Interfaces.Services;

namespace Misa.CRM.Business.Services;

public class CustomerService : BaseService<Customer, CustomerDto>, ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    public CustomerService(ICustomerRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _customerRepository = repository;
    }

    public IEnumerable<CustomerDto> GetCustomersWithType ()
    {
        return _mapper.Map<IEnumerable<CustomerDto>>(_customerRepository.GetCustomersWithTypeAsync());
    }
}
