using Misa.CRM.Business.Entities.Common;
using Misa.CRM.Business.Interfaces.Repositories;

namespace Misa.CRM.Data.Repositories;

public class CustomerTypeRepository : BaseRepository<CustomerType>, ICustomerTypeRepository
{
    public CustomerTypeRepository(MisaDbContext context) : base(context)
    {
    }
}
