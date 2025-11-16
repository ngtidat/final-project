using Misa.CRM.Business.Entities.Common;
using Misa.CRM.Business.Interfaces.Repositories;

namespace Misa.CRM.Data.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(MisaDbContext context) : base(context)
    {
    }
}
