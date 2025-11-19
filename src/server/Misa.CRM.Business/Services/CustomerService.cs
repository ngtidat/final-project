using AutoMapper;
using Misa.CRM.Business.Common.Models;
using Misa.CRM.Business.Dtos.Customer;
using Misa.CRM.Business.Entities.Common;
using Misa.CRM.Business.Interfaces.Repositories;
using Misa.CRM.Business.Interfaces.Services;

namespace Misa.CRM.Business.Services;

public class CustomerService : BaseService<Customer, CustomerDto, CustomerCreateUpdateDto>, ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    public CustomerService(ICustomerRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _customerRepository = repository;
    }

    public IEnumerable<CustomerDto> GetCustomersWithType()
    {
        return _mapper.Map<IEnumerable<CustomerDto>>(_customerRepository.GetCustomersWithTypeAsync());
    }

    public PaginatedResult<CustomerDto> Paginate(string? strSearch, int pageIndex, int pageSize, string? sortColumn, int sortDirection)
    {
        var result = _customerRepository.SearchAndPaginate(strSearch, pageIndex, pageSize, sortColumn, sortDirection);

        return new PaginatedResult<CustomerDto>(
            result.PageIndex,
            result.PageSize,
            result.TotalRecords,
            [.. _mapper.Map<IEnumerable<CustomerDto>>(result.Items)]
        );
    }
}
