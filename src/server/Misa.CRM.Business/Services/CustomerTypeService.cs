using AutoMapper;
using Misa.CRM.Business.Dtos.CustomerType;
using Misa.CRM.Business.Entities.Common;
using Misa.CRM.Business.Interfaces.Repositories;
using Misa.CRM.Business.Interfaces.Services;

namespace Misa.CRM.Business.Services;

public class CustomerTypeService : BaseService<CustomerType, CustomerTypeDto, CustomerTypeCreateUpdateDto>, ICustomerTypeService
{
    public CustomerTypeService(ICustomerTypeRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}

